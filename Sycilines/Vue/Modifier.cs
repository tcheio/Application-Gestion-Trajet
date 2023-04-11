using MySql.Data.MySqlClient;
using ScilyLinesMission2;
using Sycilines.DAL;
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

namespace Sycilines
{
    public partial class Modifier : Form
    {
        private Liaison liaison;
        public Modifier(Liaison liaison)
        {
            InitializeComponent();
            this.liaison = liaison;
        }

        private void moddif_Click_1(object sender, EventArgs e)
        {

            int count = LiaisonDAO.modifierLiaison(liaison, duree.Text);
            if (count != 0) //Si le count est différent de 0 alors une ou plusieurs update a été effectué
            {
                confirmLiaison conf = new confirmLiaison("La modification a été prise en compte");
                conf.ShowDialog();
            }
            else //Dans le cas contraire aucun n'a été faites
            {
                confirmLiaison conf = new confirmLiaison("La modification n'a pas été prise en compte"); 
                conf.ShowDialog();
            }
        }
    }
}
