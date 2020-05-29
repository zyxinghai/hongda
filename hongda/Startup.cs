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
    /// ����
    /// </summary>
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
       
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            //��������ʵ����
            new AppsettingsUtility().Initial(configuration);
            Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.MySQL);

        }


        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";//���������(���ÿ���)

        /// <summary>
        /// �м��
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //�ϴ���С
            services.Configure<FormOptions>(x => {
                x.MultipartBodyLengthLimit = 300_000_000;//����300M
            });
            //����
            services.AddMvc(options => {
                options.Filters.Add<ApiResultFilterAttribute>();
                options.Filters.Add<ValidateModelAttribute>();
            });
            //�������п��Է���
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,

                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            });

            //ids4�����֤
            var builder = services.AddIdentityServer()
                .AddInMemoryApiResources(IdentityServerConfig.Apis)
                .AddInMemoryClients(IdentityServerConfig.Clients)
                .AddResourceOwnerValidator<ResourceOwnerValidator>();
                //.AddTestUsers(IdentityServerConfig.Users);
            builder.AddDeveloperSigningCredential();

            //session
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession();

            //�����ĵ�
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
                // ����identityserver4
                /* c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                 {
                     Type = SecuritySchemeType.OAuth2,
                     Flows = new OpenApiOAuthFlows
                     {
                         // ��Ϊ�� api ��Ŀ���ǿ϶���ǰ��˷���ģ������õ���Implicitģʽ
                         Implicit = new OpenApiOAuthFlow
                         {
                             // �������� identityServer ��Ŀ������
                             //AuthorizationUrl = new Uri($"https:ids.neters.club/connect/authorize"),
                             AuthorizationUrl = new Uri($"http://localhost:10112/connect/authorize"),
                             // ���������� scope ������
                             // ֻ��Ҫ��д api��Դ ��id���ɣ�
                             // ����Ҫ�� �����Դ ������д�ϣ�����openid 
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
            // blog.admin ǰ��vue��Ŀ
            /* new Client
             {
                 ClientId = "blogadminjs",
                 ClientName = "Blog.Admin JavaScript Client",
                 AllowedGrantTypes = GrantTypes.Implicit,
                 AllowAccessTokensViaBrowser = true,

                 // �ص���ַuri���ϣ�����д���
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
         // �ϱߵ������� scope �����Բ������õ�swagger��
         "blog.core.api"// �����Դapi��name��Ҫһ��
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
        /// ����
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


            app.UseSession();//Session�洢
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpSession.Configure(httpContextAccessor);

            
            app.UseCors(MyAllowSpecificOrigins);//���ÿ���

            app.UseIdentityServer();//ids4

            app.UseAuthentication();//��֤	
            app.UseAuthorization();//��Ȩ	


            //�����ĵ�
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
