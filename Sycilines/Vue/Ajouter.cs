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

        private TextBox GetId()
        {
            return id;
        }

        private void AjoutBouton_Click(object sender, EventArgs e)
        {
            List<Port> listePort = PortDAO.chargementPort();
            string dp = listePort[listBox1.SelectedIndex].getNom();
            string arr = listePort[listBox2.SelectedIndex].getNom();
            int ind = Convert.ToInt32(id.Text);
            string dur = duree.Text;
            Liaison liaison = new Liaison(ind, dur, dp, arr, secteur.getId()); ;
            int count = LiaisonDAO.ajoutLiaison(liaison);

            if (count != 0) //Si le count est différent de 0 alors une ou plusieurs insertion a été effectué
            {
                confirmLiaison conf = new confirmLiaison("Votre enregistrement a été pris en compte", liaison);
                conf.ShowDialog();
                this.Close();
            }
            else //Dans le cas contraire aucun n'a été faites
            {
                confirmLiaison conf = new confirmLiaison("Votre enregitrement n'a pas abouti");
                conf.ShowDialog();
                this.Close();
            }
        }
    }
}
