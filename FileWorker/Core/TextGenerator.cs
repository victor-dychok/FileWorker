using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWorker.Core
{
    public class TextGenerator
    {
        private Random random;
        private string latinChars = "";
        private string kirilicChars = "";
        public TextGenerator()
        {
            //creating char arrays to add random chars to result string
            for (int i = 'A'; i <= 'Z'; i++)
            {
                latinChars += Convert.ToChar(i);
                latinChars += Convert.ToChar(i + 32);
            }

            for (int i = 'А'; i <= 'Я'; i++)
            {
                kirilicChars += Convert.ToChar(i);
                kirilicChars += Convert.ToChar(i + 32);
            }

            random = new Random();
        }
        private string generateString() //returns 1 generated string
        {
            string result = "";
            result += randomDay().ToString("dd.MM.yyyy") + "||";

            for (int i = 0; i < 10; i++)
            {
                result += latinChars[random.Next(0, latinChars.Length)];
            }
            result += "||";


            for (int i = 0; i < 10; i++)
            {
                result += kirilicChars[random.Next(0, kirilicChars.Length)];
            }
            result += "||";

            result += random.Next(0, 100000000);

            result += "||";

            result += random.Next(100000000, 2000000000) / 100000000.0;
            result += "\n";

            return result;

        }

        public List<string> GetStringsRange(int numberOfStrings) //for getting all strings
        {
            List<string> resultStringList = new List<string>();

            for (int i = 0; i < numberOfStrings; i++)
            {
                resultStringList.Add(generateString());
            }

            return resultStringList;
        }

        private DateTime randomDay()
        {
            DateTime start = DateTime.Today.AddYears(-5);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}
