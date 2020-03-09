using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Peticiones
{
    public partial class Form1 : Form
    {
        private List <string> datos; 
        public Form1()
        {
            InitializeComponent();
        }

        private void bBuscar_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            MyWebRequest wr = new MyWebRequest(textBox1.Text);
            string respuesta = wr.GetResponse();
            buscaDatos(respuesta);
        }

        private void buscaDatos(string respuesta)
        {
            if (!respuesta.Contains("ZERO_RESULTS"))
            {
                listBox1.Items.Add("Resultados de la busqueda: ");
                int ix = 0, j, k;
                ix = respuesta.IndexOf("formatted_address", ix);
                j = respuesta.IndexOf(": ", ix + 5);
                k = respuesta.IndexOf("\",", j + 1);
                listBox1.Items.Add("Direccion formal:");
                listBox1.Items.Add("    " + respuesta.Substring(j + 2, (k - j - 1)) + "\n");

                ix = respuesta.IndexOf("lat", ix);
                j = respuesta.IndexOf(": ", ix + 5);
                k = respuesta.IndexOf(",", j + 1);
                listBox1.Items.Add("Latitud:");
                listBox1.Items.Add("    " + respuesta.Substring(j + 2, (k - j - 1)) + "\n");

                ix = respuesta.IndexOf("lng", ix);
                j = respuesta.IndexOf(": ", ix + 5);
                k = respuesta.IndexOf("}", j + 1);
                listBox1.Items.Add("Longitud:");
                listBox1.Items.Add("    " + respuesta.Substring(j + 1, (k - j - 1)) + "\n");
            }
            else
            {
                listBox1.Items.Add("No existe el lugar");
            }


        }
    }
}
