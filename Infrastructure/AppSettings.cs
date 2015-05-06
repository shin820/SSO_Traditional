﻿using System.Configuration;

namespace Infrastructure
{
    public static class AppSettings
    {
        public static string AuthorizeUrl
        {
            get
            {
                string url = ConfigurationManager.AppSettings["AuthorizeUrl"];
                if (string.IsNullOrEmpty(url))
                {
                    throw new ConfigurationErrorsException("'AuthorizeUrl' have not been found in web.config.");
                }

                return url;
            }
        }

        public static string TokenUrl
        {
            get
            {
                string url = ConfigurationManager.AppSettings["TokenUrl"];
                if (string.IsNullOrEmpty(url))
                {
                    throw new ConfigurationErrorsException("'TokenUrl' have not been found in web.config.");
                }

                return url;
            }
        }

        public static string ClientId
        {
            get
            {
                string clientId = ConfigurationManager.AppSettings["ClientId"];
                if (string.IsNullOrEmpty(clientId))
                {
                    throw new ConfigurationErrorsException("'ClientId' have not been found in web.config.");
                }

                return clientId;
            }
        }

        public static string ClientSecret
        {
            get
            {
                string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
                if (string.IsNullOrEmpty(clientSecret))
                {
                    throw new ConfigurationErrorsException("'ClientSecret' have not been found in web.config.");
                }

                return clientSecret;
            }
        }
    }
}
