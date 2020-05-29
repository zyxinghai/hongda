using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using hongda.Middlewares;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace hongda
{
    /// <summary>
    /// 启动
    /// </summary>
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


        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";//名字随便起(调用跨域)

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
            //拦截
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

            //ids4身份验证
            var builder = services.AddIdentityServer()
                .AddInMemoryApiResources(IdentityServerConfig.Apis)
                .AddInMemoryClients(IdentityServerConfig.Clients)
                .AddResourceOwnerValidator<ResourceOwnerValidator>();
                //.AddTestUsers(IdentityServerConfig.Users);
            builder.AddDeveloperSigningCredential();

            //session
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession();

            //生成文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
                var mvcXmlFile = $"{ Assembly.GetEntryAssembly().GetName().Name }.xml";
                var entityXmlFile = $"hongda.Entity.xml";
                var mvcXmlPath = Path.Combine(AppContext.BaseDirectory, mvcXmlFile);
                var entityXmlPath = Path.Combine(AppContext.BaseDirectory, entityXmlFile);
                c.IncludeXmlComments(mvcXmlPath);
                c.IncludeXmlComments(entityXmlPath);
                #region
                // 接入identityserver4
                /* c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                 {
                     Type = SecuritySchemeType.OAuth2,
                     Flows = new OpenApiOAuthFlows
                     {
                         // 因为是 api 项目，那肯定是前后端分离的，所以用的是Implicit模式
                         Implicit = new OpenApiOAuthFlow
                         {
                             // 这里配置 identityServer 项目的域名
                             //AuthorizationUrl = new Uri($"https:ids.neters.club/connect/authorize"),
                             AuthorizationUrl = new Uri($"http://localhost:10112/connect/authorize"),
                             // 这里配置是 scope 作用域，
                             // 只需要填写 api资源 的id即可，
                             // 不需要把 身份资源 的内容写上，比如openid 
                             Scopes = new Dictionary<string, string> {
                             {
                             "blog.core.api","ApiResource id"
                              }
                             }
                         }
                     }
                 });*/
                #endregion
            });

            #region
            // blog.admin 前端vue项目
            /* new Client
             {
                 ClientId = "blogadminjs",
                 ClientName = "Blog.Admin JavaScript Client",
                 AllowedGrantTypes = GrantTypes.Implicit,
                 AllowAccessTokensViaBrowser = true,

                 // 回调地址uri集合，可以写多个
                 RedirectUris =
     {
         "http://vueadmin.neters.club/callback",
         "http://apk.neters.club/oauth2-redirect.html",
         "http://localhost:8081/oauth2-redirect.html",
     },
                 PostLogoutRedirectUris = { "http://vueadmin.neters.club" },
                 AllowedCorsOrigins = { "http://vueadmin.neters.club" },

                 AllowedScopes = {
         IdentityServerConstants.StandardScopes.OpenId,
         IdentityServerConstants.StandardScopes.Profile,
         "roles",
         // 上边的这三个 scope ，可以不用配置到swagger中
         "blog.core.api"// 这个资源api的name，要一致
     }
             };*/
            #endregion

            services.AddControllers();
            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:10112";
                options.RequireHttpsMetadata = false;
                options.Audience = "api1";
            });
        }

        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            app.UseSession();//Session存储
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpSession.Configure(httpContextAccessor);

            
            app.UseCors(MyAllowSpecificOrigins);//调用跨域

            app.UseIdentityServer();//ids4

            app.UseAuthentication();//认证	
            app.UseAuthorization();//授权	


            //生成文档
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.InjectJavascript("");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
