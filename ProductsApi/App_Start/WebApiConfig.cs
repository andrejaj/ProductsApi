using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace ProductsApi
{
    public static class WebApiConfig
    {
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				 name: "DefaultApiAction",
				 routeTemplate: "api/{controller}/{action}"
			);

			//config.Routes.MapHttpRoute(
			//	 name: "DefaultApi",
			//	 routeTemplate: "api/{controller}/{name}",
			//	 defaults: new { name = RouteParameter.Optional }
			//);
		}
	}
}
