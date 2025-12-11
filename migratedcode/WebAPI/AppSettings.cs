using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace eShop.WebApi
{
    public class AppSettings
    {
        public string UnityContainer
        {
            get
            {
                return ConfigurationManager.AppSettings["UnityContainer"];
            }
        }

        public string ImageBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ImageBaseUrl"];
            }
        }
    }
}