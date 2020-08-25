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
        //string[] urls = { "http://localhost:5000" };
        string[] urls = { "http://server1", "http://server2", "http://server3" };

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
        string hostname = Dns.GetHostName();

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
                    context.Response.Headers.Add("InCamp-Student", hostname);
                    await context.Response.WriteAsync(who[rand.Next(who.Length)]);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/how", async context =>
                {   
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", hostname);
                    await context.Response.WriteAsync(how[rand.Next(how.Length)]);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/does", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", hostname);
                    await context.Response.WriteAsync(does[rand.Next(does.Length)]);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/what", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", hostname);
                    await context.Response.WriteAsync(what[rand.Next(what.Length)]);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/quote", async context =>
                {
                    context.Response.ContentType = "text/html; charset=UTF-8";
                    context.Response.Headers.Add("InCamp-Student", hostname);
                    await context.Response.WriteAsync(who[rand.Next(who.Length)] + " " + how[rand.Next(how.Length)] + " " + does[rand.Next(does.Length)] + " " + what[rand.Next(what.Length)]);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/incamp18-quote", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", hostname);
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
            quote += words[0].word + "\t'received from' -> " + words[0].nameFromHeader + "<br>";
            quote += words[1].word + "\t'received from' -> " + words[1].nameFromHeader + "<br>";
            quote += words[2].word + "\t'received from' -> " + words[2].nameFromHeader + "<br>";
            quote += words[3].word + "\t'received from' -> " + words[3].nameFromHeader + "<br>";
            return quote;
        }
    }
}
