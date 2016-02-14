using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiplomaDataModel.Startup))]
namespace DiplomaDataModel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
