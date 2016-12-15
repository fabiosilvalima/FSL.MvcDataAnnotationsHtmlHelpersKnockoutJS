using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS.Startup))]
namespace FSL.MvcDataAnnotationsHtmlHelpersKnockoutJS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
