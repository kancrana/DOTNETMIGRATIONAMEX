using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace eShop.Web
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

        public string ApiBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiBaseUrl"];
            }
        }
    }
}