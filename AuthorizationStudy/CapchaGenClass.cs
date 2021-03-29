using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationStudy
{
    public class CapchaGenClass
    {

        static public string CapchaGenerate()
        {
            Random random = new Random();
            string simbol;
            string CapchaString = "";

            for (int i = 0; i < 5; i++)
            {
                int rand = random.Next(0, 101);

                if (rand > 50)
                {
                    simbol = Convert.ToString((char)random.Next(65, 90));
                    CapchaString = CapchaString + simbol;
                }
                else
                {
                    simbol = Convert.ToString(random.Next(0, 9));
                    CapchaString = CapchaString + simbol;
                }
            }

            return CapchaString;
        }

    }
}
