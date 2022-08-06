using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sdakcc.EntityFrameworkCore;
using sdakcc.Localization;
using sdakcc.MultiTenancy;
using sdakcc.Web.Menus;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Identity;
using sdakcc.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Abp.Dependency;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Abp.Json;
using sdakcc.Application.AuthLogin;

namespace sdakcc.Web;

[DependsOn(
    typeof(sdakccHttpApiModule),
    typeof(sdakccApplicationModule),
    typeof(sdakccEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpSettingManagementWebModule),
    typeof(AbpAccountWebIdentityServerModule),
    typeof(AbpAspNetCoreMvcUiBasicThemeModule),
    typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
    
    )]
public class sdakccWebModule : AbpModule
{
    private const string _defaultCorsPolicyName = "localhost";
    
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(sdakccResource),
                typeof(sdakccDomainModule).Assembly,
                typeof(sdakccDomainSharedModule).Assembly,
                typeof(sdakccApplicationModule).Assembly,
                typeof(sdakccApplicationContractsModule).Assembly,
                typeof(sdakccWebModule).Assembly
            );
        });
        PreConfigure<IdentityBuilder>(identityBuilder =>
        {
            identityBuilder.AddSignInManager<LoginManager>();
        });

        
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        //var _defaultCorsPolicyName = "localhost";



    

        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAuthentication(context, configuration);
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureLocalizationServices();;
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureSwaggerServices(context.Services);
        ConfigureCors(context, configuration);
        //ConfigureIdentityServer(context, configuration);
        //ConfigureMvc(context.Services);
       


    }

    


    //private void ConfigureIdentityServer(ServiceConfigurationContext context, IConfiguration configuration)
    //{
    //    context.Services.AddIdentityServer()
    //    .AddDeveloperSigningCredential()
    //    .AddDefaultEndpoints();
    //}

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
       
        context.Services.AddCors(options =>
            {
                options.AddPolicy(
                    _defaultCorsPolicyName, 
                       builder =>
                         {
                         builder
                             .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>())
                             .AllowAnyHeader()
                             .AllowAnyMethod()
                             .AllowCredentials();
                        });
            });
    }
    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                BasicThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
               .AddJwtBearer(o =>
               {
                   o.TokenValidationParameters = new TokenValidationParameters
                   {

                       ValidIssuer = configuration["Jwt:Issuer"], // jwtSetting.Issuer,
                       ValidAudience = configuration["Jwt:Audience"], // jwtSetting.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                       //ValidateIssuerSigningKey = true,
                       //ValidateIssuer = jwtSetting.IssuerAvailable, //will tip：：Bearer error="invalid_token", error_description="The issuer is invalid"
                       //ValidateAudience = jwtSetting.AudienceAvailable, //will tip：Bearer error="invalid_token", error_description="The audience is invalid"error_description="The audience is invalid"
                   };

                   o.SecurityTokenValidators.Clear();
                   o.SecurityTokenValidators.Add(new sdakKccTokenValidator()); //add your Token Validator

               });

        //context.Services.AddAuthentication()
        //    .AddJwtBearer(options =>
        //    {
        //        options.Authority = configuration["AuthServer:Authority"];
        //        options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
        //        options.Audience = "sdakcc";
        //    });

        //context.Services.ForwardIdentityAuthenticationForBearer();
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<sdakccWebModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                    options.FileSets.ReplaceEmbeddedByPhysical<sdakccDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}sdakcc.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<sdakccDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}sdakcc.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<sdakccApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}sdakcc.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<sdakccApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}sdakcc.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<sdakccWebModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureLocalizationServices()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español"));
        });
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new sdakccMenuContributor());
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(sdakccApplicationModule).Assembly);
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "sdakcc API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();
    

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

       
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseIdentityServer();
        app.UseRouting();
        app.UseCors(_defaultCorsPolicyName);
        //app.UseAuthentication();
        app.UseJwtTokenMiddleware();
      
        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "sdakcc API");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints(endpoints=>
        {
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllerRoute("deafultWithArea", "{area}/{controller=Home}/{action=Index}");
        });
    }
}
