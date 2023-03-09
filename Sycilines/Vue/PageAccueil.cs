﻿using MySql.Data.MySqlClient;
using Sycilines;
using Sycilines.DAL;
using Sycilines.Modele;
using Sycilines.Vue;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScilyLinesMission2
{
    public partial class PageAccueil : Form
    {
        public PageAccueil()
        {
            InitializeComponent(); //Initialiser la page pour que les formulaires s'affichent
        }

        private void PageAccueil_Load(object sender, EventArgs e)
        {
            ajouter.Hide();
            supprimer.Hide();
            modifier.Hide(); //Pour le moments on les chache parce que y a aucune liaison a modifier ou supprimer ou ajouter
            /*ConnexionSql connexion = ConnexionSql.getInstance("localhost", "sycilines", "connexionBDD", "f9(5HttDX0wXqA-R");
            connexion.openConnection();
            MySqlCommand secteurCommande = connexion.reqExec("select id,nom from secteur"); //Requête affichant tous les secteurs
            MySqlDataReader secteur = secteurCommande.ExecuteReader(); //Requete renvoyant plusieurs résultat
            if (secteur.HasRows) //Condition vérifiant si la requête possèdes des résultats
            {
                while (secteur.Read()) //On va lire chaque ligne
                {
                    secteurBox.Items.Add(secteur.GetString(0) + " - " + secteur.GetString(1)); //On ajoute l'id et le nom des secteur à chaque item de la listbox
                }
            }
            else
            {
                Console.WriteLine("No rows found."); //information de la console disant aucune ligne
            }
            secteur.Close();
            connexion.closeConnection();*/
            List < Secteur > listeSecteur = SecteurDAO.chargementSecteur();

            for (int i = 0; i < listeSecteur.Count; i++)
            {
                secteurBox.Items.Add(listeSecteur[i].getNom());
            }
        }

        private void secteurBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            supprimer.Hide();
            modifier.Hide();
            ajouter.Show();
            liaisonBox.Items.Clear();
            /*//après seclectionner le secteur le button Ajouter s'affiche
            int indiceSecteur = secteurBox.SelectedIndex+1; //pour affiche le secteur identique à son indice
            ConnexionSql connexion = ConnexionSql.getInstance("localhost", "sycilines", "connexionBDD", "f9(5HttDX0wXqA-R");
            connexion.openConnection();
            // Requête qui donne les ports d'arrivées des liaisons du secteur séléctionné
            MySqlCommand liaisonCommande = connexion.reqExec("select P.nom from port P join liaison L On P.id = L.portDepart JOIN secteur S on S.id = L.idSecteur where S.id = " + indiceSecteur);
            MySqlDataReader resultLiaison = liaisonCommande.ExecuteReader();
            int i = 0;
            List<string> listeDepart = new List<string>();
            if (resultLiaison.HasRows) // Condition qui permet de lire chaque element de ligne
            {
                while (resultLiaison.Read())
                {
                    listeDepart.Add(resultLiaison.GetString(0));
                    i += 1;
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            Console.WriteLine(i);
            resultLiaison.Close();
            MySqlCommand liaisonCommande2 = connexion.reqExec("select P.nom from port P join liaison L On P.id = L.portArrivee JOIN secteur S on S.id = L.idSecteur where S.id = " + indiceSecteur);
            MySqlDataReader resultLiaison2 = liaisonCommande2.ExecuteReader();
            if (resultLiaison2.HasRows) // Condition qui permet de lire chaque element de ligne
            {
                int j = 0;
                while (resultLiaison2.Read() && j < i)
                {
                    liaisonBox.Items.Add(listeDepart[j]+"-"+resultLiaison2.GetString(0));
                    j += 1;
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            resultLiaison2.Close();
            //fermeture de la connextion
            connexion.closeConnection();*/

            List<Liaison> listeLiaison = new List<Liaison>;
            listeLiaison = LiaisonDAO.chargementLiaisonSecteur(secteurBox.SelectedItem);
        }

        private void ajouter_Click(object sender, EventArgs e)
        {
            int indiceSecteur = secteurBox.SelectedIndex + 1;
            ConnexionSql connexion = ConnexionSql.getInstance("localhost", "sycilines", "connexionBDD", "f9(5HttDX0wXqA-R");
            connexion.openConnection();
            MySqlCommand secteurCommande = connexion.reqExec("select nom from secteur where id=" + indiceSecteur); // Requête qui donne le nom de secteur selectionné
            Ajouter ajout = new Ajouter((string)secteurCommande.ExecuteScalar(), indiceSecteur);
            connexion.closeConnection();
            ajout.ShowDialog();
            this.Close();
        }

        private void liaisonBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //après selectionner le secteur et la liaison les buttons supprimer et modifier s'affichent
            supprimer.Show();
            modifier.Show();
            
        }

        private void supprimer_Click(object sender, EventArgs e)
        {
            ConnexionSql connexion = ConnexionSql.getInstance("localhost", "sycilines", "connexionBDD", "f9(5HttDX0wXqA-R");
            connexion.openConnection();
            MySqlCommand supprId = connexion.reqExec("select id from liaison where portArrivee=(select id from port where nom='"+ liaisonBox.SelectedItem+"')");//sélectionner la liaison avec port d'arrivée
            int idLiaison = (int)supprId.ExecuteScalar();
            connexion.closeConnection();
            Supprimer suppr = new Supprimer(idLiaison);
            suppr.ShowDialog();
        }

        private void modifier_Click(object sender, EventArgs e)
        {
            ConnexionSql connexion = ConnexionSql.getInstance("localhost", "sycilines", "connexionBDD", "f9(5HttDX0wXqA-R");
            connexion.openConnection();
            MySqlCommand idLiaison = connexion.reqExec("select id from liaison where portArrivee = (select id from port where nom='" + liaisonBox.SelectedItem + "')");//sélectionner la liaison avec port d'arrivée
            int resultLiaison = (int) idLiaison.ExecuteScalar();
            connexion.closeConnection();
            Modifier moddif = new Modifier(resultLiaison);
            moddif.ShowDialog();
            this.Close();
        }
    }
}
