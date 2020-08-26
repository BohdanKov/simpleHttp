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
    public class Urls
    {
        public static string[] makeUrls(string[] urlsList)
        {
            string[] temp = { randomUrl(urlsList) + "/who",
                              randomUrl(urlsList) + "/how",
                              randomUrl(urlsList) + "/does",
                              randomUrl(urlsList) + "/what",  };
            return temp;
        }

        public static string randomUrl(string[] urlsList)
        {
            var rand = new Random();
            return urlsList[rand.Next(urlsList.Length)];
        }
    }
}
