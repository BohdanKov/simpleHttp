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
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        string[] who = { "Люк Скайуокер", "Тайлер Дерден", "Джек Воробей", "Нео", "Т-800", "Чубака" };
        string[] how = { "красиво", "хитро", "виртуозно", "умно", "глупо", "лучше всех", "просто" };
        string[] does = { "пишет", "танцует", "ест", "бросает" };
        string[] what = { "маслину", "код", "брейк-данс", "письмо", "ламбаду" };

        static string[] urlsList = Environment.GetEnvironmentVariable("urls_list").Split(",");

        string[] urls = Urls.makeUrls(urlsList);
        public void ConfigureServices(IServiceCollection services)
        {
        }
        public string chooseStrategy(string[] urls)
        {
            IGetQuote strategy;
            if (Program.arguments.Length == 0)
            { strategy = new Sync(); }
            else { strategy = new Async(); }

            var watch = Stopwatch.StartNew();
            RandomWord[] quote = strategy.getQuote(urls);
            watch.Stop();

            string outputData = RandomWord.writeDateIntoString(quote);
            outputData += $"Execution Time: {watch.ElapsedMilliseconds} ms <br><br>";

            return outputData;
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
    }
}
