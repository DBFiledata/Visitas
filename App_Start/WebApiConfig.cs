using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace paginaOficial.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("API Default", "{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            var appXmlType = configuration.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            configuration.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}