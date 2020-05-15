using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hongda.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace hongda
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
       
        /// <summary>
        /// 启动项
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            //启动配置实体类
            new AppsettingsUtility().Initial(configuration);
            Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.MySQL);

        }


        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";//名字随便起

        /// <summary>
        /// 中间件
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //上传大小
            services.Configure<FormOptions>(x => {
                x.MultipartBodyLengthLimit = 300_000_000;//不到300M
            });
            //l拦截
            services.AddMvc(options => {
                options.Filters.Add<ApiResultFilterAttribute>();
                options.Filters.Add<ValidateModelAttribute>();
            });
            //设置所有可以访问
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,

                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            });

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //调用跨域
            app.UseCors(MyAllowSpecificOrigins);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
