using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PrOps.Classes
{
    public static class TDHtmlHelper
    {
        public static MvcHtmlString TDHelper(int? iMaxLevel, int? iNumberOfPercent)
        {
            string strHelper = "";
            if (iNumberOfPercent>=0 && iNumberOfPercent<25 && iMaxLevel!=0)
            {
                strHelper += "class='td-danger'";
            }
            if (iNumberOfPercent >= 25 && iNumberOfPercent < 50 && iMaxLevel != 0)               
            {
              strHelper += "class='td-warning'";
            }
            if (iNumberOfPercent >= 50 && iNumberOfPercent < 75 && iMaxLevel != 0)
            {
                strHelper += "class='td-info'";
            }
            if (iNumberOfPercent >= 75 && iNumberOfPercent <= 100 && iMaxLevel != 0)
            {
                strHelper += "class='td-success'";
             
            }

            return MvcHtmlString.Create(strHelper);

            
        }

        public static MvcHtmlString TDTonerLevel( int? iNumberOfPercent)
        {
            string strHelper = "";
            if (iNumberOfPercent >= 0 && iNumberOfPercent < 25 )
            {
                strHelper += "class='td-danger'";
            }
            if (iNumberOfPercent >= 25 && iNumberOfPercent < 50 )
            {
                strHelper += "class='td-warning'";
            }
            if (iNumberOfPercent >= 50 && iNumberOfPercent < 75 )
            {
                strHelper += "class='td-info'";
            }
            if (iNumberOfPercent >= 75 && iNumberOfPercent <= 100 )
            {
                strHelper += "class='td-success'";

            }

            return MvcHtmlString.Create(strHelper);


        }

        private static string UptimeParser(string strDate)
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.Clear();
            if (strDate != null)
            {
                Debug.WriteLine("strDate=" + strDate);
                Regex rgx = new Regex(@"\((\d*?)\.*?(\d+?)\:(\d+?)\:(\d+?)");
                Match matches = rgx.Match(strDate);
                Debug.WriteLine("matches.Groups.Count=" + matches.Groups.Count.ToString());
                
                if (matches.Groups.Count == 5)
                {
                    Debug.WriteLine("matches=" + matches.Groups[0]);
                    if (matches.Groups[1].Length == 0)
                    {
                        Debug.WriteLine("matches[0].Groups[0]=" + matches.Groups[0]);
                        sbResult.Append(matches.Groups[2]);
                        sbResult.Append(" часов ");
                        sbResult.Append(matches.Groups[3]);
                        sbResult.Append(" минут ");
                        sbResult.Append(matches.Groups[4]);
                        sbResult.Append(" секунд ");
                    }

                    else
                    {
                        sbResult.Append(matches.Groups[1]);
                        sbResult.Append(" дней ");
                        sbResult.Append(matches.Groups[2]);
                        sbResult.Append(" часов ");
                        sbResult.Append(matches.Groups[3]);
                        sbResult.Append(" минут ");
                        sbResult.Append(matches.Groups[4]);
                        sbResult.Append(" секунд ");
                    }

                }
                else
                {
                    sbResult.Append("N/A");
                }
            }
            return sbResult.ToString();
        }


        public static MvcHtmlString UptimeHelper(string strUptime)
        {
            return MvcHtmlString.Create(UptimeParser(strUptime));
        }
    }
}