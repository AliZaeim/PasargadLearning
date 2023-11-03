using System;
using System.Globalization;


namespace PLCore.Convertors
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" +
                   pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }
        public static string ToShamsiWithTime(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" +
                   pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00") + " | " + pc.GetHour(value).ToString("0#") + ":" + pc.GetMinute(value).ToString("0#");
        }
        public static string ToShamsiN(this DateTime? value)
        {
            if (!value.HasValue)
            {
                return string.Empty;
            }
            else
            {
                DateTime dtb = (DateTime)value.Value;
                PersianCalendar pc = new PersianCalendar();
                string dt = pc.GetYear(dtb.Date) + "/" +
                       pc.GetMonth(dtb.Date).ToString("00") + "/" +
                       pc.GetDayOfMonth(dtb.Date).ToString("00");
                return dt;


            }
        }
        public static string ToShamsiN_WithTime(this DateTime? value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {

                PersianCalendar pc = new PersianCalendar();
                return pc.GetYear((DateTime)value) + "/" +
                       pc.GetMonth((DateTime)value).ToString("00") + "/" +
                       pc.GetDayOfMonth((DateTime)value).ToString("00")
                + " | " + pc.GetHour((DateTime)value).ToString("0#") + ":" + pc.GetMinute((DateTime)value).ToString("0#") + ":" + pc.GetSecond((DateTime)value).ToString("0#");
            }
        }
        public static DateTime ToMiladi(this string Shamsi)
        {
            string[] strDateTime = Shamsi.Split("|");
            string strDate = ""; string strTime = "";
            int y = 0; int m = 0; int d = 0;
            if (strDateTime.Length >= 1)
            {
                strDate = strDateTime[0];
            }
            if (strDateTime.Length >= 2)
            {
                strTime = strDateTime[1];
            }
            if (!string.IsNullOrEmpty(strDate))
            {
                string[] str = strDate.Split("/");
                y = int.Parse(str[0]);
                m = int.Parse(str[1]);
                d = int.Parse(str[2]);
            }
            int h = 0;
            int min = 0;
            int s = 0;
            if (strTime.Contains(":"))
            {
                string[] tm = strTime.Split(":");
                if (tm.Length >= 1)
                {
                    h = int.Parse(tm[0]);
                    if (h == 24)
                    {
                        h = 0;
                    }
                }
                if (tm.Length >= 2)
                {
                    min = int.Parse(tm[1]);
                }
                if (tm.Length >= 3)
                {
                    s = int.Parse(tm[2]);
                }


            }
            DateTime date1 = new DateTime(y, m, d, h, min, s, new PersianCalendar());
            return date1;
        }
        public static DateTime ChangeToMiladiWithoutTime(this string shamsiDate)
        {
            string[] formats = { "yyyy/MM/dd", "yyyy/M/d", "yyyy/MM/d", "yyyy/M/dd" };
            DateTime d1 = DateTime.ParseExact(shamsiDate, formats,
                                              CultureInfo.CurrentCulture, DateTimeStyles.None);
            PersianCalendar persian_date = new PersianCalendar();
            DateTime dt = persian_date.ToDateTime(d1.Year, d1.Month, d1.Day, 0, 0, 0, 0, 0);
            return dt;
        }
        public static DateTime ChangeToMiladiWithTime(this string shamsiDate)
        {
            string[] formats = { "yyyy/MM/dd", "yyyy/M/d", "yyyy/MM/d", "yyyy/M/dd" };
            DateTime d1 = DateTime.ParseExact(shamsiDate, formats,
                                              CultureInfo.CurrentCulture, DateTimeStyles.None);
            PersianCalendar persian_date = new PersianCalendar();
            DateTime dt = persian_date.ToDateTime(d1.Year, d1.Month, d1.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, 0);
            return dt;
        }
        public static DateTime ChangeToMiladi(this string shamsiDate, string time)
        {
            if (string.IsNullOrEmpty(shamsiDate))
            {
                return DateTime.Now;
            }
            //shamsi with format "yyyy/mm/dd" and time with format HH:MM
            PersianCalendar pc = new PersianCalendar();
            string[] DParts = shamsiDate.Split("/");
            int y = int.Parse(DParts[0].ToString());
            int m = int.Parse(DParts[1].ToString());
            int d = int.Parse(DParts[2].ToString());
            int h = 0; int min = 0;
            if (!string.IsNullOrEmpty(time))
            {
                string[] TParts = time.Split(":");
                h = int.Parse(TParts[0].ToString());
                min = int.Parse(TParts[1].ToString());
            }
            DateTime dateT = pc.ToDateTime(y, m, d, h, min, 0, 0);
            return dateT;
        }
    }
}
