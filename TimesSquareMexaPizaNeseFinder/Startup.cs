using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimesSquareMexaPizaNeseFinder.Startup))]
namespace TimesSquareMexaPizaNeseFinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
