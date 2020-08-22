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

namespace newHttp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        string[] urls = { "http://localhost:5000" };
        /* string[] urls = { "http://feb2ec000271.ngrok.io",
                             "http://df1f5b98672e.ngrok.io",
                             "http://5b341e7ae688.ngrok.io",
                             "http://74334191d2b3.ngrok.io",
                             "http://14add9edba41.ngrok.io",
                             "http://9220ec0c3226.ngrok.io",
                             "http://8ef7bd7680d4.ngrok.io",
                             "http://700b7e95e1da.ngrok.io",
                             "http://771e37434dfe.ngrok.io" 
                             "http://ef845d6343d7.ngrok.io", 
                             "http://5e9e572e07b3.ngrok.io", 
                             "http://67e5aa89deb6.ngrok.io"};*/
        string[] who = { "Люк Скайуокер", "Тайлер Дерден", "Джек Воробей", "Нео", "Т-800", "Чубака" };
        string[] how = { "красиво", "хитро", "виртуозно", "умно", "глупо", "лучше всех", "просто" };
        string[] does = { "пишет", "танцует", "ест", "бросает" };
        string[] what = { "маслину", "код", "брейк-данс", "письмо", "ламбаду" };
        
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public string chooseStrategy(string[] urls)
        {
            IGetQuote strategy;
            if (Program.arguments.Length == 0)
            {
                strategy = new Sync();
                
            } else
            {
                strategy = new Async();
                
            }
            return strategy.getQuote(urls);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var rand = new Random();
            app.UseRouting(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/who", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Bogdan Kovalchuk");
                    await context.Response.WriteAsync(who[rand.Next(who.Length)]);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/how", async context =>
                {   
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Bogdan Kovalchuk");
                    await context.Response.WriteAsync(how[rand.Next(how.Length)]);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/does", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Bogdan Kovalchuk");
                    await context.Response.WriteAsync(does[rand.Next(does.Length)]);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/what", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Bogdan Kovalchuk");
                    await context.Response.WriteAsync(what[rand.Next(what.Length)]);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/quote", async context =>
                {
                    context.Response.ContentType = "text/html; charset=UTF-8";
                    
                    context.Response.Headers.Add("InCamp-Student", "Bogdan Kovalchuk");
                    await context.Response.WriteAsync(who[rand.Next(who.Length)] + " " + how[rand.Next(how.Length)] + " " + does[rand.Next(does.Length)] + " " + what[rand.Next(what.Length)]);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/incamp18-quote", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Bogdan Kovalchuk");
                    await context.Response.WriteAsync(chooseStrategy(urls));
                });
            });
            
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
        public static string randomUrl(string[] urls)
        {
            var rand = new Random();
            return urls[rand.Next(urls.Length)];
        }

        public static string writeDateIntoString (RandomWord[] words)
        {
            string quote = words[0].word + " " + words[1].word + " " + words[2].word + " " + words[3].word + "<br>";
            quote += words[0].nameFromHeader + "\t'received from' -> " + words[0].nameFromHeader + "<br>";
            quote += words[1].nameFromHeader + "\t'received from'-> " + words[1].nameFromHeader + "<br>";
            quote += words[2].nameFromHeader + "\t'received from' -> " + words[2].nameFromHeader + "<br>";
            quote += words[3].nameFromHeader + "\t'received from' -> " + words[3].nameFromHeader + "<br>";
            return quote;
        }

        



        


    }
}
