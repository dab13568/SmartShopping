using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.HomeUserControlMVVM
{
    public class HomeUserControlM
    {
        public Tuple<string,string, string> getCurrentTime()
        {
            int HourNow = DateTime.Now.Hour;
            if (HourNow <= 12 && HourNow>=5)
            {
                return Tuple.Create("בוקר טוב!","את סטיסטיקות הקניות שלך כבר בדקת? אנו ממליצים לעקוב באופן יומיומי על מעקב הקניות שלך בשביל קניה נכונה יותר.", "https://www.metoda.co.il/metodapaperonlineshoppingsurvey");
             }
            else if (HourNow <= 16 && HourNow>=12)
            {
                return Tuple.Create( "צהריים טובים!","במערכת המלצת הקניות שלנו כבר השתמשת? במערכת זו אנו מספקים לך רשימת קניות, שבנויה מאלגורתמים חכמים מתחום הלמידת מכונה.", "https://he.wikipedia.org/wiki/%D7%9C%D7%9E%D7%99%D7%93%D7%AA_%D7%9E%D7%9B%D7%95%D7%A0%D7%94");
            }
            else if (HourNow <= 20 && HourNow>=16)
            {
                return Tuple.Create("ערב טוב!", "את סטיסטיקות הקניות שלך כבר בדקת? אנו ממליצים לעקוב באופן יומיומי על מעקב הקניות שלך בשביל קניה נכונה יותר.", "https://www.metoda.co.il/metodapaperonlineshoppingsurvey");
            }
            else
            {
                return Tuple.Create("לילה טוב!", "במערכת המלצת הקניות שלנו כבר השתמשת? במערכת זו אנו מספקים לך רשימת קניות, שבנויה מאלגורתמים חכמים מתחום הלמידת מכונה.", "https://he.wikipedia.org/wiki/%D7%9C%D7%9E%D7%99%D7%93%D7%AA_%D7%9E%D7%9B%D7%95%D7%A0%D7%94");
            }
        }
    }
}
