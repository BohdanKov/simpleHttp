using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace newHttp
{
    public class Sync : IGetQuote
    {
        public string getQuote(string[] urls)
        {
            var watch = Stopwatch.StartNew();

            RandomWord who = Startup.getWord(Startup.randomUrl(urls) + "/who");
            RandomWord how = Startup.getWord(Startup.randomUrl(urls) + "/how");
            RandomWord does = Startup.getWord(Startup.randomUrl(urls) + "/does");   
            RandomWord what = Startup.getWord(Startup.randomUrl(urls) + "/what");

            RandomWord[] temp = { who, how, does, what };
            string outputData = Startup.writeDateIntoString(temp);

            watch.Stop();
            outputData += $"Execution Time: {watch.ElapsedMilliseconds} ms <br><br>";

            return outputData;
        }
    }
}
