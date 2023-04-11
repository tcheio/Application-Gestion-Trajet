using MySql.Data.MySqlClient;
using ScilyLinesMission2;
using Sycilines.DAL;
using Sycilines.Modele;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sycilines.Vue
{
    public partial class Supprimer : Form
    {
        private Liaison l;

        public Supprimer(Liaison liaison)
        {
            InitializeComponent();
            this.l = liaison;
        }

        

        private void suppr_Load(object sender, EventArgs e)
        {
            
        }

        private void annuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void suppr_Click(object sender, EventArgs e)
        {
            int count = LiaisonDAO.suppressionLiaison(l); //Appel à la méthode de suppression
            if (count != 0)
            {
                confirmLiaison conf = new confirmLiaison("La liaison a été supprimé.");
                conf.ShowDialog();
                this.Close();
            }
            else
            {
                confirmLiaison conf = new confirmLiaison("Aucune suppréssion n'a été réalisé");
                conf.ShowDialog();
                this.Close();
            }
            
        }
    }
}
