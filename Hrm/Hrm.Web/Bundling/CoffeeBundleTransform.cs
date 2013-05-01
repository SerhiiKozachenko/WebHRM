using System.IO;
using System.Web;
using System.Web.Optimization;
using CoffeeSharp;

namespace Hrm.Web.Bundling
{
    public class CoffeeBundleTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            var coffeeEngine = new CoffeeScriptEngine();
            var compiledCoffeeScript = string.Empty;
            foreach (var file in response.Files)
            {
                using (var sr = new StreamReader(file.FullName))
                {
                    compiledCoffeeScript += coffeeEngine.Compile(sr.ReadToEnd());
                    sr.Close();
                }
            }

            response.Content = compiledCoffeeScript;
            response.ContentType = "text/javascript";
            response.Cacheability = HttpCacheability.Public;
        }
    }
}