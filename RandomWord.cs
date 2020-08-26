using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Net.Mime;
using System.IO.Pipelines;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;

namespace newHttp
{
    public class RandomWord
    {
        public string word { get; set; }
        public string nameFromHeader { get; set; }

        public RandomWord(string word, string nameFromHeader)
        {
            this.word = word;
            this.nameFromHeader = nameFromHeader;
        }
        public static RandomWord getWord(string url)
        {
            string word;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                word = stream.ReadToEnd();
            }
            string header = response.Headers["InCamp-Student"];
            RandomWord temp = new RandomWord(word, header);

            response.Close();
            return temp;
        }

        public static string writeDateIntoString(RandomWord[] words)
        {
            string quote = words[0].word + " " + words[1].word + " " + words[2].word + " " + words[3].word + "<br>";
            quote += words[0].word + "\t'received from'  -> " + words[0].nameFromHeader + "<br>";
            quote += words[1].word + "\t'received from'  -> " + words[1].nameFromHeader + "<br>";
            quote += words[2].word + "\t'received from'  -> " + words[2].nameFromHeader + "<br>";
            quote += words[3].word + "\t'received from'  -> " + words[3].nameFromHeader + "<br>";
            return quote;
        }
    }
}
