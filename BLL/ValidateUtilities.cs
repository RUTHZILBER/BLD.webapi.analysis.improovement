
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft;

namespace BLL
{
    public class ValidateUtilities
    {

        /// <summary>
        /// עברית אנגלית גרש
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool isValidateEnglishHebrewGeresh(string str)
        {
            string pattern = @"^[A-Za-z\u05D0-\u05EA']+$";
            Regex rg = new Regex(pattern);
            return rg.IsMatch(str);
        }
        /// <summary>
        /// בעברית
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool isValidateHebrew(string str)
        {
            string pattern = @"^[\u05D0-\u05EA']+$";
            Regex rg = new Regex(pattern);
            return rg.IsMatch(str);
        }
        /// <summary>
        /// כתובת מייל תקינה
        /// </summary>
        /// <param name="inputEmail"></param>
        /// <returns></returns>
        public static bool isValidateEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
          @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        /// <summary>
        /// סיסמא תקינה
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool isValidatePassword(string str)
        {
            //Regex rg1 = new Regex(@"[A - Za - z]");rg1.IsMatch(str) &&
            Regex rg2 = new Regex(@"^[@#a][A-Za-z0-9]{1,8}$");
            //Regex rg3 = new Regex("@[u]"); && !rg3.IsMatch(str)
            return  rg2.IsMatch(str);

        }
        public static bool isValidateIdentityNumber(string IDNum)
        {

            // Validate correct input
            if (!System.Text.RegularExpressions.Regex.IsMatch(IDNum, @"^\d{5,9}$"))
                return false;

            // The number is too short - add leading 0000
            if (IDNum.Length < 9)
            {
                while (IDNum.Length < 9)
                {
                    IDNum = '0' + IDNum;
                }
            }

            // CHECK THE ID NUMBER
            int mone = 0;
            int incNum;
            for (int i = 0; i < 9; i++)
            {
                incNum = Convert.ToInt32(IDNum[i].ToString());
                incNum *= (i % 2) + 1;
                if (incNum > 9)
                    incNum -= 9;
                mone += incNum;
            }
            if (mone % 10 == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// תקינות לקוח
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
       
    }
}
