using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TodoApi.Models;

namespace TodoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<ITodoService, TodoService>();
            services.AddDirectoryBrowser();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            // Fix No 'Access-Control-Allow-Origin' header is present on the requested resource.
            app.UseCors(x => x.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());

            //添加一个默认页面
            // var options = new DefaultFilesOptions();
            // options.DefaultFileNames.Clear();
            // options.DefaultFileNames.Add("mydefault.html");
            app.UseDefaultFiles(); // marks the files in web root as servable. 
            app.UseStaticFiles(); // 让 wwwroot下的文件可访问

            // 配置路由中间件，添加MVC作为默认的handler
            app.UseMvc();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // 设置 http response headers
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age=600");
                    // if (ctx.Context.User.Identity.IsAuthenticated)
                    // {
                    //     return;
                    // }

                    // throw new UnauthorizedAccessException();
                }
            });

            // 让图片可以在浏览器 访问
            // services.AddDirectoryBrowser();
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            // 可访问图片文件
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(imagePath),
                RequestPath = "/MyImages"
            });
            // 可访问文件夹
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(imagePath),
                RequestPath = "/MyImages"
            });


            // UseFileServer 是 UseStaticFiles, UseDefaultFiles 和 UseDirectoryBrowser 功能的组合
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles");
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(filePath),
                RequestPath = "/StaticFiles",
                EnableDirectoryBrowsing = true // DirectoryBrowser 可用
            });
        }
    }
}
