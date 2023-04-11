using MySql.Data.MySqlClient;
using Sycilines;
using Sycilines.DAL;
using Sycilines.Modele;
using Sycilines.Vue;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScilyLinesMission2
{
    public partial class Ajouter : Form
    {
        private Secteur secteur;
        private int ind;

        public Ajouter()
        {
            InitializeComponent();
        }


        public Ajouter(Secteur secteur)
        {
            InitializeComponent();
            this.secteur = secteur;

        }



        private void Ajouter_Load(object sender, EventArgs e)
        {
            sousTitre.Text += secteur.getNom();
            List<Port> listePort = PortDAO.chargementPort();
            for (int i = 0; i < listePort.Count; i++)
            {
                listBox1.Items.Add(listePort[i].getNom());
                listBox2.Items.Add(listePort[i].getNom());
            }

        }

        private void ajout_Click(object sender, EventArgs e)
        {


            //Listebox méthode
            ConnexionSql connexion = ConnexionSql.getInstance("localhost", "sycilines", "connexionBDD", "f9(5HttDX0wXqA-R");
            connexion.openConnection();
            //Requêtes permettant de récupérer les ID des ports d'arrivée et de départ
            MySqlCommand chercheDep = connexion.reqExec("Select id from port where nom='" + listBox1.SelectedItem + "'");
            MySqlCommand chercheArr = connexion.reqExec("Select id from port where nom='" + listBox2.SelectedItem + "'");
            string depart = (string)listBox1.SelectedItem;
            string arrivee = (string)listBox2.SelectedItem;
            int pDep = (int)chercheDep.ExecuteScalar();
            int pArr = (int)chercheArr.ExecuteScalar();
            //Requête effectuant l'insertion
            MySqlCommand insertion = connexion.reqExec("insert into liaison(id,duree,portDepart,portArrivee,idSecteur) values (" + Convert.ToInt32(indice.Text) + ",'" + duree.Text + "'," + pDep + "," + pArr + "," + this.indiceSecteur + ")");
            int count = insertion.ExecuteNonQuery(); //On garde en mémoire le nombre de ligne crée
            connexion.closeConnection();
            //ind = indice.Text;
            if (count != 0) //Si le count est différent de 0 alors une ou plusieurs insertion a été effectué
            {
                Liaison nouvelleLiaison = new Liaison(ind, duree.Text,depart,arrivee, this.indiceSecteur);
                confirmLiaison conf = new confirmLiaison("Votre enregistrement a été pris en compte", nouvelleLiaison);
                this.Close();
                conf.ShowDialog();
            }
            else //Dans le cas contraire aucun n'a été faites
            {
                confirmLiaison conf = new confirmLiaison("Votre enregitrement n'a pas abouti");
                this.Close();
                conf.ShowDialog();
            }
        }

    }
}
