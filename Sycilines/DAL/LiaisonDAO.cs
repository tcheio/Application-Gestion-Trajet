﻿using MySql.Data.MySqlClient;
using ScilyLinesMission2;
using Sycilines.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sycilines.DAL
{
    public class LiaisonDAO
    {
        // attributs de connexion statiques
        private static string provider = "localhost";

        private static string dataBase = "sycilines";

        private static string uid = "connexionBDD";

        private static string mdp = "f9(5HttDX0wXqA-R";



        private static ConnexionSql maConnexionSql;


        private static MySqlCommand commande;


        public static List<Liaison> ChargementLiaisonSecteur(Secteur secteur)
        {
            List<Liaison> listeLiaison = new List<Liaison>();
            ConnexionSql connexion = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
            connexion.openConnection();
            // Requête qui donne les ports d'arrivées des liaisons du secteur séléctionné
            MySqlCommand liaisonCommande = connexion.reqExec("select P.nom from port P join liaison L On P.id = L.portDepart JOIN secteur S on S.id = L.idSecteur where S.id ='"+ secteur.getId()+"';");
            MySqlDataReader resultLiaison = liaisonCommande.ExecuteReader();
            List<string> listeDepart = new List<string>();
            if (resultLiaison.HasRows) // Condition qui permet de lire chaque element de ligne
            {
                while (resultLiaison.Read())
                {
                    listeDepart.Add(resultLiaison.GetString(0));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            resultLiaison.Close();

            MySqlCommand liaisonCommande2 = connexion.reqExec("select P.nom from port P join liaison L On P.id = L.portArrivee JOIN secteur S on S.id = L.idSecteur where S.id ='"+ secteur.getId()+"';");
            MySqlDataReader resultLiaison2 = liaisonCommande2.ExecuteReader();
            List<string> listeArrivee = new List<string>();
            if (resultLiaison2.HasRows) // Condition qui permet de lire chaque element de ligne
            {
                while (resultLiaison2.Read())
                {
                    listeArrivee.Add(resultLiaison2.GetString(0));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            resultLiaison2.Close();

            MySqlCommand liaisonCommande3 = connexion.reqExec("select id, duree from liaison where idSecteur = '"+ secteur.getId()+"';") ;
            MySqlDataReader resultLiaison3 = liaisonCommande3.ExecuteReader();
            int i = 0;
            if (resultLiaison3.HasRows) // Condition qui permet de lire chaque element de ligne
            {
                while (resultLiaison3.Read())
                {
                    int id = (int)resultLiaison3.GetValue(0);
                    string duree = (string)resultLiaison3.GetValue(1);
                    
                    Liaison liaison = new Liaison(id, duree, listeDepart[i], listeArrivee[i], secteur.getId());
                    
                    listeLiaison.Add(liaison);
                    i++;

                }
            }
            //fermeture de la connextion
            connexion.closeConnection();

            return listeLiaison;
        }


        public static int ajoutLiaison(Liaison l)
        {
            //Listebox méthode
            ConnexionSql connexion = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
            connexion.openConnection();
            //Requêtes permettant de récupérer les ID des ports d'arrivée et de départ
            MySqlCommand chercheDep = connexion.reqExec("Select id from port where nom='" + l.getDepart() + "'");
            MySqlCommand chercheArr = connexion.reqExec("Select id from port where nom='" + l.getArrivee() + "'");
            int pDep = (int)chercheDep.ExecuteScalar();
            int pArr = (int)chercheArr.ExecuteScalar();
            //Requête effectuant l'insertion
            MySqlCommand insertion = connexion.reqExec("insert into liaison values" +
                "("+l.getId()+",'"+ l.getDuree() + "'," + pDep + "," + pArr + "," + l.getIdSecteur() + ");");
            int count = insertion.ExecuteNonQuery(); //On garde en mémoire le nombre de ligne crée
            connexion.closeConnection();
            return count;
        }
        public static int suppressionLiaison(Liaison l)
        {
            ConnexionSql connexion = ConnexionSql.getInstance("localhost", "sycilines", "connexionBDD", "f9(5HttDX0wXqA-R");
            //Requête effectuant la suppresion
            MySqlCommand suppression = connexion.reqExec("Delete from liaison where id=" + l.getId() + ";");
            connexion.openConnection();
            int count = suppression.ExecuteNonQuery();
            connexion.closeConnection();
            return count;
        }

        public static int modifierLiaison(Liaison l, string duree)
        {
            ConnexionSql connexion = ConnexionSql.getInstance("localhost", "sycilines", "connexionBDD", "f9(5HttDX0wXqA-R");
            connexion.openConnection();
            MySqlCommand updateLiaison = connexion.reqExec("UPDATE liaison SET duree='" + duree + "' WHERE id =" + l.getId()+";");
            int count = updateLiaison.ExecuteNonQuery();
            connexion.closeConnection();
            return count;
        }
        
    }


}
