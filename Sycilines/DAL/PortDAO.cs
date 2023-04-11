using MySql.Data.MySqlClient;
using ScilyLinesMission2;
using Sycilines.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Sycilines.DAL
{
    public class PortDAO
    {
        // attributs de connexion statiques
        private static string provider = "localhost";

        private static string dataBase = "sycilines";

        private static string uid = "connexionBDD";

        private static string mdp = "f9(5HttDX0wXqA-R";



        private static ConnexionSql maConnexionSql;


        private static MySqlCommand commande;

        public static List<Port> chargementPort()
        {
            ConnexionSql connexion = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
            connexion.openConnection();
            List<Port> ports = new List<Port>();
            MySqlCommand portCommande = connexion.reqExec("select id,nom from Port"); //Requête affichant tous les secteurs
            MySqlDataReader port = portCommande.ExecuteReader(); //Requete renvoyant plusieurs résultat
            if (port.HasRows) //Condition vérifiant si la requête possèdes des résultats
            {
                while (port.Read()) //On va lire chaque ligne
                {
                    Port p = new Port((int)port.GetValue(0), (string)port.GetValue(1));
                    ports.Add(p);
                }
            }
            else
            {
                Console.WriteLine("No rows found."); //information de la console disant aucune ligne
            }
            port.Close();
            connexion.closeConnection();
            return ports;
        }


    }
}
