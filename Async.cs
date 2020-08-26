using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace newHttp
{
    public class Async : IGetQuote
    {
        public async Task<RandomWord[]> getQuoteAsync(string[] urls)
        {
            Task<RandomWord> who = Task.Run(() => RandomWord.getWord(urls[0]));
            Task<RandomWord> how = Task.Run(() => RandomWord.getWord(urls[0]));
            Task<RandomWord> does = Task.Run(() => RandomWord.getWord(urls[0]));
            Task<RandomWord> what = Task.Run(() => RandomWord.getWord(urls[0]));

            RandomWord[] words = await Task.WhenAll(who, how, does, what);

            return words;
        }
        public RandomWord[] getQuote(string[] urls) => getQuoteAsync(urls).Result;
    }
}
