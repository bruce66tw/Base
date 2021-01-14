﻿using Base.Enums;
using Base.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Base.Services
{
    public static class _Date
    {        

        #region get difference
        /// <summary>
        /// 2 date difference
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>days difference, return 0 if same</returns>
        public static int DayDiff(DateTime start, DateTime end)
        {
            return (start == end)
                ? 0
                : (end - start).Days;
        }

        /// <summary>
        /// 2 string date difference
        /// </summary>
        /// <param name="start">date string</param>
        /// <param name="end">date string</param>
        /// <returns></returns>
        public static int StrDayDiff(string start, string end)
        {
            if (start == end)
                return 0;

            try
            {
                var start2 = DateTime.Parse(start);
                var end2 = DateTime.Parse(end);
                return DayDiff(start2, end2);
            }
            catch(Exception ex)
            {
                _Log.Error("_Date.StrDiffDay() failed: " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 2 min second difference
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double MiniSecDiff(DateTime start, DateTime end)
        {
            return (end - start).TotalMilliseconds;
        }

        //2 min second string difference
        public static double StrMiniSecDiff(string start, string end)
        {
            if (start == end)
                return 0;

            try
            {
                var start2 = DateTime.Parse(start);
                var end2 = DateTime.Parse(end);
                return MiniSecDiff(start2, end2);
            }
            catch (Exception ex)
            {
                _Log.Error("_Date.StrMiniSecDiff() failed: " + ex.Message);
                return 0;
            }
        }
        #endregion
        
        #region check
        //is dates same
        public static bool IsSameDate(DateTime start, DateTime end)
        {
            return (start.Date == end.Date);
        }

        //is string a date or not
        public static bool IsDate(string date)
        {
            //DateTime date2;
            return DateTime.TryParse(date, out var date2);
        }

        /// <summary>
        /// check first 2 dates are inside last 2 dates or not
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="start2"></param>
        /// <param name="end2"></param>
        /// <returns></returns>
        public static bool IsInRange4(DateTime? start, DateTime? end, DateTime start2, DateTime end2)
        {
            return 
                (start == null && end == null) ? true :
                (start != null && end != null) ? (start <= end2 && end >= start2) :
                (start != null && start <= end2) ? true : 
                (end != null && end >= start2);
        }
        #endregion
        
        #region now/today
        /*
        public static long NowTn()
        {
            return Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        public static string NowHms6()
        {
            //return Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
            return DateTime.Now.ToString("HHmmss");
        }
        */

        /// <summary>
        /// count second from 1970/1/1 (unix time)
        /// </summary>
        /// <returns></returns>
        public static int NowSec()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return (int)ts.TotalSeconds;
        }

        /// <summary>
        /// get now string(yyyyMMdd_HHmmss) for file name
        /// </summary>
        /// <returns></returns>
        public static string NowSecStr()
        {
            return DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }

        /*
        //today string
        public static string TodayStr()
        {
            //"yyyy/MM/dd"
            return DateTime.Now.ToString(BackDateFormat);
        }
 
        public static int TodayDn()
        {
            return Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
        }
        */
        #endregion
        
        #region date convert
        /// <summary>
        /// get dt string for write db
        /// TODO: check other database engine
        /// </summary>
        /// <returns>datetime string</returns>
        public static string ToDbStr(DateTime dt)
        {
            return (_Fun.GetDbType() == DbTypeEnum.MSSql)
                ? dt.ToString("yyyy-MM-dd HH:mm:ss")
                : "";
        }

        public static string NowDbStr()
        {
            return ToDbStr(DateTime.Now);
        }

        /// <summary>
        /// time string(hh:mm) add minute
        /// </summary>
        /// <param name="hm5">time string(hh:mm)</param>
        /// <param name="addMin">minute</param>
        /// <returns>new string (hh:mm)</returns>
        public static string Hm5AddMin(string hm5, int addMin)
        {
            var pos = hm5.IndexOf(":");
            if (pos <= 0)
                return hm5;

            var hour = Convert.ToInt32(hm5.Substring(0, pos));
            var min = Convert.ToInt32(hm5.Substring(pos + 1)) + addMin;
            if (min < 0)
            {
                hour--;
                min += 60;
            }
            else if (min >= 60)
            {
                hour++;
                min -= 60;
            }
            //return tn_hour.ToString("00") + ":" + tn_min.ToString("00");
            return HmToHm5(hour, min);
        }

        //to HH:mm string
        public static string ToHm5(DateTime dt)
        {
            return dt.ToString("HH:mm");
        }

        //to tick
        public static long ToTick(DateTime dt)
        {
            var date0 = new DateTime(1970, 1, 1);
            return (dt.Ticks - date0.Ticks) / 10000000 - 8 * 60 * 60;
        }

        //to age
        public static int ToAge(DateTime? birth)
        {
            if (birth == null)
                return 0;

            var today = DateTime.Today;
            var age = today.Year - birth.Value.Year;
            if (birth > today.AddYears(-age))
                age--;
            return age;
        }

        //convert datetime string to time(unix time)
        public static long StrToTick(string dt)
        {
            return ToTick(Convert.ToDateTime(dt));
        }

        //string to date
        public static DateTime StrToDate(string dt)
        {
            return DateTime.Parse(dt).Date;
        }

        //string to datetime
        public static DateTime? StrToDt(string dt)
        {
            return string.IsNullOrEmpty(dt) ? (DateTime?)null : DateTime.Parse(dt);
        }

        //tick to datetime
        public static DateTime TickToDt(long tick)
        {
            // Unix timestamp is seconds past epoch
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return dt.AddSeconds(tick).ToLocalTime();
        }

        //hour min to hm string (hh:mm)
        public static string HmToHm5(int hour, int min)
        {
            return hour.ToString("00") + ":" + min.ToString("00");
        }

        /// <summary>
        /// frontEnd datetime string to backEnd datetime, consider locale type
        /// </summary>
        /// <param name="dateStr">input date string</param>
        /// <returns></returns>
        public static DateTime FrontToBack(string dateStr)
        {
            //var dt = Convert.ToDateTime(dateStr).ToString(XpService.DateFormat);
            //var dt = DateTime.ParseExact(dateStr, XpService.DateFormat, CultureInfo.InvariantCulture);
            //DateTime dt;
            DateTime.TryParseExact(dateStr, _Fun.BackDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt);
            return dt;
        }

        /*
        //??
        public static string Ymd8ToDateStr(string ymd)
        {
            return (ymd.Length == 8)
                ? ymd.Substring(0, 4) + "/" + ymd.Substring(4, 2) + "/" + ymd.Substring(6, 2)
                : "";
        }
        */
        #endregion

        #region db related
        //to date+hm string
        public static string ToDateHmStr(DateTime? dt, DbFormatDto dbFormat)
        {
            return (dt == null)
                ? ""
                : dt.Value.ToString(dbFormat.FrontDtNoSec);
        }

        //datetime string to date string
        public static string StrToDateStr(string dt, string format)
        {
            return _Str.IsEmpty(dt)
                ? ""
                : ToDateStr(DateTime.Parse(dt), format);
        }

        //to date string
        public static string ToDateStr(DateTime? dt, string format)
        {
            return (dt == null)
                ? ""
                : dt.Value.ToString(format);
        }

        //to front datetime string
        public static string ToFrontStr(DateTime? dt, DbFormatDto dbFormat)
        {
            return (dt == null)
                ? ""
                : dt.Value.ToString(dbFormat.FrontDt);
            //: Convert.ToString(dt, new CultureInfo("ja-JP")).Trim();
        }
        #endregion

        #region chinese date
        /// <summary>
        /// to chinese date string
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="type">1(年月日), 2(/), 3(無分隔符號)</param>
        /// <returns></returns>
        public static string ToTwDateStr(DateTime? dt, int type)
        {
            if (dt == null)
                return "";

            //DateTime dt = DateTime.Parse(date);
            var dt2 = dt.Value;
            var year = (dt2.Year - 1911).ToString();
            if (type == 1)
                return year + "年" + dt2.Month + "月" + dt2.Day + "日";
            if (type == 2)
                return year + "/" + dt2.Month.ToString("00") + "/" + dt2.Day.ToString("00");
            if (type == 3)
                return year + dt2.Month.ToString("00") + dt2.Day.ToString("00");
            else
                return "??";
        }

        /// <summary>
        /// ym 轉換為民國年月
        /// </summary>
        /// <param name="ym">yyyy/mm</param>
        /// <param name="type">1(年月日), 2(/), 3(無分隔符號)</param>
        /// <returns></returns>
        public static string Ym7ToTwYm(string ym, int type)
        {
            if (_Str.IsEmpty(ym))
                return "";

            var sep = ym.IndexOf("/");
            if (sep <= 0)
                return "";

            var year = Convert.ToInt32(ym.Substring(0, sep)) - 1911;
            var month = Convert.ToInt32(ym.Substring(sep + 1));
            if (type == 1)
                return year + "年" + month + "月";
            if (type == 2)
                return year + "/" + month.ToString("00");
            if (type == 3)
                return year + month.ToString("00");
            else
                return "??";
        }

        //傳回民國年月日
        public static string StrToTwDateStr(string dt, int type)
        {
            return string.IsNullOrEmpty(dt)
                ? ""
                : ToTwDateStr(DateTime.Parse(dt), type);
        }

        /// <summary>
        /// 民國日期(yyymmdd) to 日期字串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string TwDate7ToDateStr(string date)
        {
            var len = date.Length;
            if (len == 6)
                return (Convert.ToInt32(date.Substring(0, 2)) + 1911) + "/" + date.Substring(2, 2) + "/" + date.Substring(4, 2);
            if (len == 7)
                return (Convert.ToInt32(date.Substring(0, 3)) + 1911) + "/" + date.Substring(3, 2) + "/" + date.Substring(5, 2);
            else
                return "";
        }
        #endregion

        #region hour/minute source list
        public static List<IdStrDto> GetHourList()
        {
            var list = new List<IdStrDto>();
            for(var i=0; i<23; i++)
            {
                list.Add(new IdStrDto() {
                    Id = i.ToString(),
                    Str = i.ToString(),
                });
            }
            return list;
        }
        public static List<IdStrDto> GetMinuteList(int step)
        {
            var list = new List<IdStrDto>();
            for (var i = 0; i < 60; i=i+step)
            {
                list.Add(new IdStrDto()
                {
                    Id = i.ToString(),
                    Str = i.ToString(),
                });
            }
            return list;
        }
        #endregion

    }//class
}