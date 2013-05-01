using System.Web.Optimization;
using Hrm.Web.Bundling;

namespace Hrm.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Overrides web.config debug property
            BundleTable.EnableOptimizations = true;
            
            #region Scripts
            bundles.Add(new ScriptBundle("~/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/jquery-migrate").Include(
                       "~/Scripts/jquery-migrate-{version}.js"));

            bundles.Add(new ScriptBundle("~/jquery-validate").Include(
                       "~/Scripts/jquery.validate*",
                       "~/Scripts/jquery.validate.unobtrusive*"));

            bundles.Add(new ScriptBundle("~/bootstrap").Include(
                        "~/Scripts/bootstrap/bootstrap*"));
            #endregion

            #region CoffeeScript
            var coffeeTransform = new CoffeeBundleTransform();

            var bootstrapFormValidationBundle = new ScriptBundle("~/bootstrap-form-validation").Include(
                "~/Scripts/helpers/boostrap-form-validation.coffee");
            bootstrapFormValidationBundle.Transforms.Add(coffeeTransform);
            
            bundles.Add(bootstrapFormValidationBundle);
            #endregion

            #region Styles
          
            #endregion
        }
    }
}