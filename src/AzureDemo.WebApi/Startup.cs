using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AzureDemo.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
			// This configures Google.Apis.Auth.AspNetCore3 for use in this app.
			services
				.AddAuthentication(o =>
				{
					// This forces challenge results to be handled by Google OpenID Handler, so there's no
					// need to add an AccountController that emits challenges for Login.
					o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
					// This forces forbid results to be handled by Google OpenID Handler, which checks if
					// extra scopes are required and does automatic incremental auth.
					o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
					// Default scheme that will handle everything else.
					// Once a user is authenticated, the OAuth2 token info is stored in cookies.
					o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				})
				.AddCookie()
				.AddGoogleOpenIdConnect(options =>
				{
					options.ClientId = Configuration["YouTube:ClientId"];
					options.ClientSecret = Configuration["YouTube:ClientSecret"];
					options.Scope.Add(YouTubeService.ScopeConstants.Youtube);
					//options.CallbackPath = "/google-auth-callback";
				});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
