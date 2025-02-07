using System;
using System.Collections.Generic;
using System.Text;
using com.nlf.calendar.util;
using com.nlf.calendar.eightchar;

namespace com.nlf.calendar
{
    /// <summary>
    /// 八字
    /// </summary>
    public class EightChar
    {
        /// <summary>
        /// 月支，按正月起寅排列
        /// </summary>
        public static readonly string[] MONTH_ZHI = { "", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥", "子", "丑" };
        /// <summary>
        /// 长生十二神
        /// </summary>
        public static readonly string[] CHANG_SHENG = { "长生", "沐浴", "冠带", "临官", "帝旺", "衰", "病", "死", "墓", "绝", "胎", "养" };
        private static readonly Dictionary<string, int> CHANG_SHENG_OFFSET = new Dictionary<string, int>();

        static EightChar()
        {
            //阳
            CHANG_SHENG_OFFSET.Add("甲", 1);
            CHANG_SHENG_OFFSET.Add("丙", 10);
            CHANG_SHENG_OFFSET.Add("戊", 10);
            CHANG_SHENG_OFFSET.Add("庚", 7);
            CHANG_SHENG_OFFSET.Add("壬", 4);
            //阴
            CHANG_SHENG_OFFSET.Add("乙", 6);
            CHANG_SHENG_OFFSET.Add("丁", 9);
            CHANG_SHENG_OFFSET.Add("己", 9);
            CHANG_SHENG_OFFSET.Add("辛", 0);
            CHANG_SHENG_OFFSET.Add("癸", 3);
        }

        /// <summary>
        /// 流派，2晚子时日柱按当天，1晚子时日柱按明天
        /// </summary>
        private int sect = 2;

        /// <summary>
        /// 阴历
        /// </summary>
        private Lunar lunar;

        public EightChar(Lunar lunar)
        {
            this.lunar = lunar;
        }

        public static EightChar fromLunar(Lunar lunar)
        {
            return new EightChar(lunar);
        }

        public string toString()
        {
            return getYear() + " " + getMonth() + " " + getDay() + " " + getTime();
        }

        public override string ToString()
        {
            return toString();
        }

        public int getSect()
        {
            return sect;
        }

        public void setSect(int sect)
        {
            this.sect = (1 == sect) ? 1 : 2;
        }

        public string getYear()
        {
            return lunar.getYearInGanZhiExact();
        }

        public string getYearGan()
        {
            return lunar.getYearGanExact();
        }

        public string getYearZhi()
        {
            return lunar.getYearZhiExact();
        }

        public List<string> getYearHideGan()
        {
            return LunarUtil.ZHI_HIDE_GAN[getYearZhi()];
        }

        public string getYearWuXing()
        {
            return LunarUtil.WU_XING_GAN[getYearGan()] + LunarUtil.WU_XING_ZHI[getYearZhi()];
        }

        public string getYearNaYin()
        {
            return LunarUtil.NAYIN[getYear()];
        }

        public string getYearShiShenGan()
        {
            return LunarUtil.SHI_SHEN_GAN[getDayGan() + getYearGan()];
        }

        private List<string> getShiShenZhi(string zhi)
        {
            List<string> hideGan = LunarUtil.ZHI_HIDE_GAN[zhi];
            List<string> l = new List<string>(hideGan.Count);
            foreach (string gan in hideGan)
            {
                l.Add(LunarUtil.SHI_SHEN_ZHI[getDayGan() + zhi + gan]);
            }
            return l;
        }

        public List<string> getYearShiShenZhi()
        {
            return getShiShenZhi(getYearZhi());
        }

        public int getDayGanIndex()
        {
            return (2 == sect) ? lunar.getDayGanIndexExact2() : lunar.getDayGanIndexExact();
        }

        public int getDayZhiIndex()
        {
            return (2 == sect) ? lunar.getDayZhiIndexExact2() : lunar.getDayZhiIndexExact();
        }

        private string getDiShi(int zhiIndex)
        {
            int offset = CHANG_SHENG_OFFSET[getDayGan()];
            int index = offset + (getDayGanIndex() % 2 == 0 ? zhiIndex : -zhiIndex);
            if (index >= 12)
            {
                index -= 12;
            }
            if (index < 0)
            {
                index += 12;
            }
            return CHANG_SHENG[index];
        }

        public string getYearDiShi()
        {
            return getDiShi(lunar.getYearZhiIndexExact());
        }

        public string getMonth()
        {
            return lunar.getMonthInGanZhiExact();
        }

        public string getMonthGan()
        {
            return lunar.getMonthGanExact();
        }

        public string getMonthZhi()
        {
            return lunar.getMonthZhiExact();
        }

        public List<string> getMonthHideGan()
        {
            return LunarUtil.ZHI_HIDE_GAN[getMonthZhi()];
        }

        public string getMonthWuXing()
        {
            return LunarUtil.WU_XING_GAN[getMonthGan()] + LunarUtil.WU_XING_ZHI[getMonthZhi()];
        }

        public string getMonthNaYin()
        {
            return LunarUtil.NAYIN[getMonth()];
        }

        public string getMonthShiShenGan()
        {
            return LunarUtil.SHI_SHEN_GAN[getDayGan() + getMonthGan()];
        }

        public List<string> getMonthShiShenZhi()
        {
            return getShiShenZhi(getMonthZhi());
        }

        public string getMonthDiShi()
        {
            return getDiShi(lunar.getMonthZhiIndexExact());
        }

        public string getDay()
        {
            return (2 == sect) ? lunar.getDayInGanZhiExact2() : lunar.getDayInGanZhiExact();
        }

        public string getDayGan()
        {
            return (2 == sect) ? lunar.getDayGanExact2() : lunar.getDayGanExact();
        }

        public string getDayZhi()
        {
            return (2 == sect) ? lunar.getDayZhiExact2() : lunar.getDayZhiExact();
        }

        public List<string> getDayHideGan()
        {
            return LunarUtil.ZHI_HIDE_GAN[getDayZhi()];
        }

        public string getDayWuXing()
        {
            return LunarUtil.WU_XING_GAN[getDayGan()] + LunarUtil.WU_XING_ZHI[getDayZhi()];
        }

        public string getDayNaYin()
        {
            return LunarUtil.NAYIN[getDay()];
        }

        public string getDayShiShenGan()
        {
            return "日主";
        }

        public List<string> getDayShiShenZhi()
        {
            return getShiShenZhi(getDayZhi());
        }

        public string getDayDiShi()
        {
            return getDiShi(getDayZhiIndex());
        }

        public string getTime()
        {
            return lunar.getTimeInGanZhi();
        }

        public string getTimeGan()
        {
            return lunar.getTimeGan();
        }

        public string getTimeZhi()
        {
            return lunar.getTimeZhi();
        }

        public List<string> getTimeHideGan()
        {
            return LunarUtil.ZHI_HIDE_GAN[getTimeZhi()];
        }

        public string getTimeWuXing()
        {
            return LunarUtil.WU_XING_GAN[lunar.getTimeGan()] + LunarUtil.WU_XING_ZHI[lunar.getTimeZhi()];
        }

        public string getTimeNaYin()
        {
            return LunarUtil.NAYIN[getTime()];
        }

        public string getTimeShiShenGan()
        {
            return LunarUtil.SHI_SHEN_GAN[getDayGan() + getTimeGan()];
        }

        public List<string> getTimeShiShenZhi()
        {
            return getShiShenZhi(getTimeZhi());
        }

        public string getTimeDiShi()
        {
            return getDiShi(lunar.getTimeZhiIndex());
        }

        public string getTaiYuan()
        {
            int ganIndex = lunar.getMonthGanIndexExact() + 1;
            if (ganIndex >= 10)
            {
                ganIndex -= 10;
            }
            int zhiIndex = lunar.getMonthZhiIndexExact() + 3;
            if (zhiIndex >= 12)
            {
                zhiIndex -= 12;
            }
            return LunarUtil.GAN[ganIndex + 1] + LunarUtil.ZHI[zhiIndex + 1];
        }

        public string getTaiYuanNaYin()
        {
            return LunarUtil.NAYIN[getTaiYuan()];
        }

        public string getTaiXi()
        {
            int ganIndex = 2 == sect ? lunar.getDayGanIndexExact2() : lunar.getDayGanIndexExact();
            int zhiIndex = 2 == sect ? lunar.getDayZhiIndexExact2() : lunar.getDayZhiIndexExact();
            return LunarUtil.HE_GAN_5[ganIndex] + LunarUtil.HE_ZHI_6[zhiIndex];
        }

        public string getTaiXiNaYin()
        {
            return LunarUtil.NAYIN[getTaiXi()];
        }

        public string getMingGong()
        {
            int monthZhiIndex = 0;
            int timeZhiIndex = 0;
            for (int i = 0, j = MONTH_ZHI.Length; i < j; i++)
            {
                string zhi = MONTH_ZHI[i];
                if (lunar.getMonthZhiExact().Equals(zhi))
                {
                    monthZhiIndex = i;
                }
                if (lunar.getTimeZhi().Equals(zhi))
                {
                    timeZhiIndex = i;
                }
            }
            int zhiIndex = 26 - (monthZhiIndex + timeZhiIndex);
            if (zhiIndex > 12)
            {
                zhiIndex -= 12;
            }
            int jiaZiIndex = LunarUtil.getJiaZiIndex(lunar.getMonthInGanZhiExact()) - (monthZhiIndex - zhiIndex);
            if (jiaZiIndex >= 60)
            {
                jiaZiIndex -= 60;
            }
            if (jiaZiIndex < 0)
            {
                jiaZiIndex += 60;
            }
            return LunarUtil.JIA_ZI[jiaZiIndex];
        }

        public string getMingGongNaYin()
        {
            return LunarUtil.NAYIN[getMingGong()];
        }

        public string getShenGong()
        {
            int monthZhiIndex = 0;
            int timeZhiIndex = 0;
            for (int i = 0, j = MONTH_ZHI.Length; i < j; i++)
            {
                string zhi = MONTH_ZHI[i];
                if (lunar.getMonthZhiExact().Equals(zhi))
                {
                    monthZhiIndex = i;
                }
                if (lunar.getTimeZhi().Equals(zhi))
                {
                    timeZhiIndex = i;
                }
            }
            int zhiIndex = 2 + monthZhiIndex + timeZhiIndex;
            if (zhiIndex > 12)
            {
                zhiIndex -= 12;
            }
            int jiaZiIndex = LunarUtil.getJiaZiIndex(lunar.getMonthInGanZhiExact()) - (monthZhiIndex - zhiIndex);
            if (jiaZiIndex >= 60)
            {
                jiaZiIndex -= 60;
            }
            if (jiaZiIndex < 0)
            {
                jiaZiIndex += 60;
            }
            return LunarUtil.JIA_ZI[jiaZiIndex];
        }

        public string getShenGongNaYin()
        {
            return LunarUtil.NAYIN[getShenGong()];
        }

        public Lunar getLunar()
        {
            return lunar;
        }

        /// <summary>
        /// 使用默认流派1获取运
        /// </summary>
        /// <param name="gender">性别：1男，0女</param>
        /// <returns>运</returns>
        public Yun getYun(int gender)
        {
            return getYun(gender, 1);
        }

        /// <summary>
        /// 获取运
        /// </summary>
        /// <param name="gender">性别：1男，0女</param>
        /// <param name="sect">流派，1按天数和时辰数计算，3天1年，1天4个月，1时辰10天；2按分钟数计算</param>
        /// <returns>运</returns>
        public Yun getYun(int gender, int sect)
        {
            return new Yun(this, gender, sect);
        }

        /// <summary>
        /// 获取年柱所在旬
        /// </summary>
        /// <returns>旬</returns>
        public string getYearXun()
        {
            return lunar.getYearXunExact();
        }

        /// <summary>
        /// 获取年柱旬空(空亡)
        /// </summary>
        /// <returns>旬空(空亡)</returns>
        public string getYearXunKong()
        {
            return lunar.getYearXunKongExact();
        }

        /// <summary>
        /// 获取月柱所在旬
        /// </summary>
        /// <returns>旬</returns>
        public string getMonthXun()
        {
            return lunar.getMonthXunExact();
        }

        /// <summary>
        /// 获取月柱旬空(空亡)
        /// </summary>
        /// <returns>旬空(空亡)</returns>
        public string getMonthXunKong()
        {
            return lunar.getMonthXunKongExact();
        }

        /// <summary>
        /// 获取日柱所在旬
        /// </summary>
        /// <returns>旬</returns>
        public string getDayXun()
        {
            return (2 == sect) ? lunar.getDayXunExact2() : lunar.getDayXunExact();
        }

        /// <summary>
        /// 获取日柱旬空(空亡)
        /// </summary>
        /// <returns>旬空(空亡)</returns>
        public string getDayXunKong()
        {
            return (2 == sect) ? lunar.getDayXunKongExact2() : lunar.getDayXunKongExact();
        }

        /// <summary>
        /// 获取时柱所在旬
        /// </summary>
        /// <returns>旬</returns>
        public string getTimeXun()
        {
            return lunar.getTimeXun();
        }

        /// <summary>
        /// 获取时柱旬空(空亡)
        /// </summary>
        /// <returns>旬空(空亡)</returns>
        public string getTimeXunKong()
        {
            return lunar.getTimeXunKong();
        }

    }
}