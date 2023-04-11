using MySql.Data.MySqlClient;
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
            modifier.Hide();
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
            string secteurNom = (string)secteurBox.SelectedItem;
            Secteur secteur = SecteurDAO.recupSecteur(secteurNom);
            List <Liaison> listeLiaison = LiaisonDAO.ChargementLiaisonSecteur(secteur);
            for (int i = 0;i < listeLiaison.Count; i++)
            {
                liaisonBox.Items.Add(listeLiaison[i].getDepart() + "-" + listeLiaison[i].getArrivee());
            }
        }

        private void ajouter_Click(object sender, EventArgs e)
        {

            string secteurNom = (string)secteurBox.SelectedItem;
            Secteur secteur = SecteurDAO.recupSecteur(secteurNom);
            List<Liaison> listeLiaison = LiaisonDAO.ChargementLiaisonSecteur(secteur);
            int indiceLiaison = liaisonBox.SelectedIndex;
            Ajouter ajout = new Ajouter(secteur);
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
            string secteurNom = (string)secteurBox.SelectedItem;
            Secteur secteur = SecteurDAO.recupSecteur(secteurNom);
            List<Liaison> listeLiaison = LiaisonDAO.ChargementLiaisonSecteur(secteur);
            int indice = liaisonBox.SelectedIndex;
            Supprimer suppr = new Supprimer(listeLiaison[indice]);
            suppr.ShowDialog();
        }

        private void modifier_Click(object sender, EventArgs e)
        {
            string secteurNom = (string)secteurBox.SelectedItem;
            Secteur secteur = SecteurDAO.recupSecteur(secteurNom);
            List<Liaison> listeLiaison = LiaisonDAO.ChargementLiaisonSecteur(secteur);
            int indice = liaisonBox.SelectedIndex;
            Modifier moddif = new Modifier(listeLiaison[indice]);
            moddif.ShowDialog();
            this.Close();
        }
    }
}
