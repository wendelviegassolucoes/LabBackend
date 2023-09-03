//namespace WebApiConsole.AppStart
//{
//    public class RouteConfig
//    {
//        public static IRouteBuilder Include(IRouteBuilder routes)
//        {
//            routes.MapRoute("DefaultApiWithAction",
//                            "{controller}/{action}");

//            routes.MapRoute("DefaultApiGet",
//                            "{controller}/{action}",
//                            new { action = "GET" });

//            routes.MapRoute("DefaultApiPost",
//                            "{controller}/{action}",
//                            new { action = "POST" });

//            routes.MapRoute("DefaultApiPut",
//                            "{controller}/{action}",
//                            new { action = "PUT" });

//            routes.MapRoute("DefaultApiDelete",
//                            "{controller}/{action}",
//                            new { action = "DELETE" });

//            routes.MapRoute("Ingredient",
//                            "/Ingredient/{controller}/{action}",
//                             new { area = "Ingredient" });

//            routes.MapRoute("Logon",
//                            "/Logon/{controller}/{action}",
//                             new { area = "Logon" });

//            routes.MapRoute("WeatherForecast",
//                           "/WeatherForecast/{controller}/{action}",
//                            new { area = "WeatherForecast" });

//            return routes;
//        }
//    }
//}
