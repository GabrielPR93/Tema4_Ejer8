using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema4_Ejer8
{
    public partial class Form2 : Form
    {
        Form1 f1;


        public Form2(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;
        }


        private void cerrar(object sender, EventArgs e)
        {
            f1.button1.Enabled = true;
            f1.arrayList.Clear();
            this.Close();
        }

        private void siguienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f1.cont++;
            try
            {
                f1.labelError.Text = "";

                if (f1.cont < f1.arrayList.Count)
                {
                    pictureBox1.Image = Image.FromFile(f1.arrayList[f1.cont].ToString());
                }
                if (f1.cont >= f1.arrayList.Count - 1)
                {
                    f1.cont = f1.arrayList.Count - 1;
                }
            }
            catch (Exception)
            {
                f1.labelError.Text = "Imagen corrupta";

            }
        }

        private void anteriorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f1.cont--;
            try
            {

                f1.labelError.Text = "";

                if (f1.cont >= 0)
                {
                    pictureBox1.Image = Image.FromFile(f1.arrayList[f1.cont].ToString());
                }
                if (f1.cont < 0)
                {
                    f1.cont = 0;
                }
            }
            catch (Exception)
            {

                f1.labelError.Text = "Imagen corrupta";

            }
        }
    }
}
