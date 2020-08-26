using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace newHttp
{
    public class Sync : IGetQuote
    {
        public RandomWord[] getQuote(string[] urls)
        {
            RandomWord who = RandomWord.getWord(urls[0]);
            RandomWord how = RandomWord.getWord(urls[1]);
            RandomWord does = RandomWord.getWord(urls[2]);
            RandomWord what = RandomWord.getWord(urls[3]);

            RandomWord[] temp = { who, how, does, what };

            return temp;
        }
    }
}
