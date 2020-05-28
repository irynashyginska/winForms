using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace wForms_Members
{
    public class Member
    {
        public int id { get; set; }
        private string surname;
        public string Surname { get { return this.surname; } set { this.surname = words(value); } }
        private int number;
        public int Number { get { return this.number; } set { this.number = numbsint(value.ToString()); } }
        private string report;
        public string Report { get { return this.report; } set { this.report = value; } }
        private string time;
        public string Time { get { return this.time; } set { this.time = check_time(value); } }
        public Member() { }
        public Member(string n,string num,string r,string t)
        {
            this.surname = words(n);
            this.number = numbsint(num);
            this.report = r;
            this.time = check_time(t);
        }
        public static Boolean isAlpha(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z\s,]*$");
            return rg.IsMatch(strToCheck);
        }
        public string words(string word)
        {
            while (word.All(Char.IsLetter) == false)
            {
                
                word = Console.ReadLine();
            }
            return word;
        }
        public int numbsint(string num)
        {
            while (num.All(Char.IsDigit) == false)
            {
                
                num = Console.ReadLine();

            }
            return int.Parse(num);
        }
        public string check_time(string num)
        {
            Regex pattern = new Regex(@"(^[0-1][0-9]:[0-5][0-9]$)|(^[2][0-3]:[0-5][0-9]$)");
            while(!pattern.IsMatch(num))
            {
                num = Console.ReadLine();
                
            }
            return num;
        }
        public override string ToString()
        {
            return surname + "," + number.ToString() + "," + report + "," + time;
        }
    }
}
