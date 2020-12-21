using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema4_Ejer8
{
    public partial class Form1 : Form
    {
        OpenFileDialog openFileDialog;
        Form2 f = new Form2();
        FileInfo[] imagenes;
        ArrayList arrayList = new ArrayList();
        int cont = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] medida = { "B", "KB", "MB" };

            int cont = 0;

            using (openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetEnvironmentVariable("home");
                openFileDialog.Filter = "imagenes(*.jpg)|*.jpg|imagenes(*.png)|*.png|Todos los archivos|*.*";
                openFileDialog.FilterIndex = 3;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    arrayList.Add(openFileDialog.FileName);
                    string ruta = Path.GetDirectoryName(openFileDialog.FileName);//obtenemos el nombre del directorio de la imagen
                    DirectoryInfo directorio = new DirectoryInfo(ruta);
                    imagenes = directorio.GetFiles(); //obtenemos todos los archivos

                    foreach (FileInfo item in imagenes) //Los recorremos y guardamos las imagenes
                    {
                        if (item.Extension.Equals(".jpg") || item.Extension.Equals(".png"))
                        {
                            if (!arrayList.Contains(item.FullName))
                            {
                                arrayList.Add(item.FullName);
                            }
                        }
                    }
                    try
                    {

                        f.pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                        Text = "Visor de Imagenes" + "-<" + openFileDialog.SafeFileName + ">";

                        //Información del archivo seleccionado
                        FileInfo info = new FileInfo(openFileDialog.FileName);
                        labelDirectorio.Text = info.DirectoryName;
                        long tamaño = info.Length;

                        while (tamaño >= 1024)
                        {
                            cont++;
                            tamaño /= 1024;
                        }
                        string tamañoFinal = String.Format("{0} {1}", tamaño, medida[cont]);
                        Image img = Image.FromFile(openFileDialog.FileName);

                        label1.Text = info.Name + "\n" + tamañoFinal + "\n" + img.Width + " x " + img.Height;


                        f.Show();
                        button1.Enabled = false;
                        labelError.Text = "";
                    }
                    catch (Exception)
                    {

                        labelError.Text = "Error al seleccionar imagen";
                        arrayList.Clear();

                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Está seguro de que quiere salir ? ", "Salir", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            cont++;

            if (cont < arrayList.Count)
            {
                f.pictureBox1.Image = Image.FromFile(arrayList[cont].ToString());
            }
            if (cont >= arrayList.Count - 1)
            {
                cont = arrayList.Count - 1;
            }

        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            cont--;

            if (cont >= 0)
            {
                f.pictureBox1.Image = Image.FromFile(arrayList[cont].ToString());
            }
            if (cont < 0)
            {
                cont = 0;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.D)
            {
                cont++;

                if (cont < arrayList.Count)
                {
                    f.pictureBox1.Image = Image.FromFile(arrayList[cont].ToString());
                }
                if (cont >= arrayList.Count - 1)
                {
                    cont = arrayList.Count - 1;
                }
            }
            else if (e.KeyCode == Keys.A)
            {
                cont--;

                if (cont >= 0)
                {
                    f.pictureBox1.Image = Image.FromFile(arrayList[cont].ToString());
                }
                if (cont < 0)
                {
                    cont = 0;
                }
            }
        }

    }
}

