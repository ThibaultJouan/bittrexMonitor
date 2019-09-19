using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bittrex
{
    class LectureWeb
    {
        public static String LireContenuWeb(WebClient client, string adresse)
        {
            if (String.IsNullOrEmpty(adresse) == true)
            {
                return String.Empty;
            }
            string result = String.Empty;

            client.Headers.Add("Accept", "application/json");
            Stream data = client.OpenRead(adresse);
            StreamReader reader = null;

            reader = new StreamReader(data);
            result = reader.ReadToEnd();

            return result;
        }

        /// <summary>
        /// Permet de configurer l'objet WebClient que l'on utilise, dans le cas ou une authentification proxy est nécessaire
        /// </summary>
        /// <param name="adresseProxy">L'adresse du proxy a fournir, sous forme dce string</param>
        /// <param name="logOn">Le login a fournir au proxy, sous forme de String</param>
        /// <param name="password">Le mot de passe a fournir au proxy, sous forme de string</param>
        /// <param name="client">un objet WebClient à configurer pour fonctionner avec le proxy que l'on souhaite utiliser</param>
        /// <returns>un objet WebClient configuré pour fonctionner avec le serveur proxy que l'on utilise</returns>
        public static WebClient DefinirProxy(string adresseProxy, string logOn, string password, WebClient client)
        {
            if (client == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(adresseProxy) == false)
            {
                WebProxy p = new WebProxy(adresseProxy, true);
                p.Credentials = new NetworkCredential(logOn, password);
                WebRequest.DefaultWebProxy = p;
                client.Proxy = p;
            }
            return client;
        }
    }
}
