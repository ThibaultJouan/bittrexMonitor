using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace bittrex
{
    class Program
    {
        static void Main(string[] args)
        {
            string requete = String.Empty;

            string monnaie = "ERC";

            requete = "https://bittrex.com/api/v1.1/public/getmarkethistory?market=BTC-" + monnaie;

            WebClient client = new WebClient();

            Market.Result marcheActuel = GetMarket(monnaie, client);

            GetVolumeEchange(monnaie, client);
            GetOrders(monnaie, client, marcheActuel);

            int i = 0;
            i++;
            i++;
        }

        public static void GetVolumeEchange(string monnaie, WebClient client)
        {
            string requete = "https://bittrex.com/api/v1.1/public/getmarkethistory?market=BTC-" + monnaie; 
            string resultat = LectureWeb.LireContenuWeb(client, requete);

            Echange buffer = JsonConvert.DeserializeObject<Echange>(resultat);

            DateTime plusAncien = DateTime.Now;
            double achat            = 0;
            double quantiteAchat    = 0;
            double moyenneAchat     = 0;
            double vente            = 0;
            double quantiteVente    = 0;
            double moyenneVente     = 0;

            foreach (Echange.Result unique in buffer.result)
            {
                if (unique.TimeStamp < plusAncien)
                {
                    plusAncien = unique.TimeStamp;
                }
                if (unique.OrderType == "BUY")
                {
                    achat += unique.Total;
                    quantiteAchat += unique.Total;
                    if (moyenneAchat == 0)
                    {
                        moyenneAchat = unique.Price;
                    }
                    else
                    {
                        moyenneAchat += unique.Price;
                        moyenneAchat /= 2;
                    }
                }
                else
                {
                    vente += unique.Total;
                    quantiteVente += unique.Total;
                    if (moyenneVente == 0)
                    {
                        moyenneVente = unique.Price;
                    }
                    else
                    {
                        moyenneVente += unique.Price;
                        moyenneVente /= 2;
                    }
                }
            }
            Console.WriteLine("Market : BTC-{0}", monnaie);
            Console.WriteLine("Temps ecoule : {0} minutes", (DateTime.Now - plusAncien).Minutes);
            Console.WriteLine("Volume achete : {0}", quantiteAchat);
            Console.WriteLine("Volume vendu  : {0}", quantiteVente);
            Console.WriteLine("Prix d'achat moyen  : {0}", moyenneAchat);
            Console.WriteLine("Prix de vente moyen : {0}", moyenneVente);
        }

        public static void GetOrders(string monnaie, WebClient client, Market.Result marche)
        {
            string requete = "https://bittrex.com/api/v1.1/public/getorderbook?market=BTC-" + monnaie + "&type=both"; 
            string resultat = LectureWeb.LireContenuWeb(client, requete);

            Ordre ordres = JsonConvert.DeserializeObject<Ordre>(resultat);

            double masseAchat = 0;
            double masseVente = 0;

            foreach (Ordre.Buy achat in ordres.result.buy)
            {
                masseAchat += achat.Quantity;
            }
            foreach (Ordre.Sell vente in ordres.result.sell)
            {
                masseVente += vente.Quantity;
            }

            Console.WriteLine("Market : BTC-{0}", monnaie);
            Console.WriteLine("Volume a l'achat  : {0}", masseAchat);
            Console.WriteLine("Volume a la vente : {0}", masseVente);
        }

        public static Market.Result GetMarket(string monnaie, WebClient client)
        {
            string requete = "https://bittrex.com/api/v1.1/public/getmarketsummary?market=btc-" + monnaie;
            string resultat = LectureWeb.LireContenuWeb(client, requete);

            Market marche = JsonConvert.DeserializeObject<Market>(resultat);

            return marche.result[0];
        }

    }
}
