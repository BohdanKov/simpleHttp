using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace newHttp
{
    public class Async : IGetQuote
    {
        public async Task<string> getQuoteAsync(string[] urls)
        {
            var watch = Stopwatch.StartNew();

            Task<RandomWord> who = Task.Run(() => Startup.getWord(Startup.randomUrl(urls) + "/who"));
            Task<RandomWord> how = Task.Run(() => Startup.getWord(Startup.randomUrl(urls) + "/how"));
            Task<RandomWord> does = Task.Run(() => Startup.getWord(Startup.randomUrl(urls) + "/does"));
            Task<RandomWord> what = Task.Run(() => Startup.getWord(Startup.randomUrl(urls) + "/what"));

            var words = await Task.WhenAll(who, how, does, what);
            string outputData = Startup.writeDateIntoString(words);

            watch.Stop();
            outputData += $"Execution Time: {watch.ElapsedMilliseconds} ms ";
            return outputData;
        }
        public string getQuote(string[] urls) => getQuoteAsync(urls).Result;
    }
}
