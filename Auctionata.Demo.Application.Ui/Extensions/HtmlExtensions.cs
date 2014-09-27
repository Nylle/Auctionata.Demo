using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Auctionata.Demo.Application.Ui.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString SemanticUiMenuItem(this HtmlHelper htmlHelper, String linkText, String actionName, String controllerName)
        {
            var cssClass = "item";

            if (htmlHelper.ViewContext.RequestContext.IsCurrentRoute(null, controllerName, actionName))
            {
                cssClass = "active item";
            }

            var link = htmlHelper.ActionLink(linkText, actionName, controllerName, null, new { @class = cssClass }).ToString();

            return MvcHtmlString.Create(link);
        }

        public static MvcHtmlString SemanticUiMenuItem(this HtmlHelper htmlHelper, String linkText, String actionName, String controllerName, string cssClass)
        {
            if (string.IsNullOrWhiteSpace(cssClass))
            {
                return SemanticUiMenuItem(htmlHelper, linkText, actionName, controllerName);
            }

            var newCssClass = "item";

            if (htmlHelper.ViewContext.RequestContext.IsCurrentRoute(null, controllerName, actionName))
            {
                newCssClass = "active item";
            }

            var link = htmlHelper.ActionLink(linkText, actionName, controllerName, null, new { @class = newCssClass + " " + cssClass }).ToString();

            return MvcHtmlString.Create(link);
        }
    }
}
