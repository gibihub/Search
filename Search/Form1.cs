using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Search
{
    public partial class Form1 : Form
    {
    public List<string> streets = new List<string>();
        public List<string> info = new List<string>();
        public Form1()
        {// CITIRE DATE DIN FISIERUL CSV
            InitializeComponent();
            using (var reader = new StreamReader("Geo.Coord.TM.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    streets.Add(values[3]);
                    info.Add(line);
                }
            }
            label1.Visible = false;
            listBox1.Visible = false;
            listBox2.Visible = false;
        }//................................FUNCTIA SEARCH CARE CAUTA PRIN FISIERUL CSV SE APELEAZA PRIN CLICK PE BUTTON-UL "SEARCH"..............................
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Nu ati introdus nimic in caseta de cautare");
            }
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            label1.Visible = false;
            listBox1.Visible = false;
            listBox2.Visible = false;
            
            for (int i=0;i<streets.Count;i++)
            {
                string xd = streets[i];
                xd=xd.Remove(0, 1);
                xd = xd.Remove(xd.Length - 1, 1);
                //................DACA E BUN CUVANTUL SE AFISEAZA LISTA DE REZULTATE........................
                if(textBox1.Text.ToLower() == xd.ToLower())
                {
                    listBox1.Visible = true;
                    label1.Visible = true;
                    listBox1.Items.Add(info[i]);
                }
              
            }
        }
        //........................IN TIMP CE TEXTUL ESTE INTRODUS IN CASETA DE CAUTARE APARE O LISTA DE SUGESTII PENTRU AUTO COMPLETARE................................
        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox2.SelectedItem.ToString();
            button1.PerformClick();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox2.Visible = false;
            foreach (string street in streets)
            {
                string xd = street;
                xd = xd.Remove(0, 1);
                xd = xd.Remove(xd.Length - 1, 1);
                if (xd.ToLower().Contains(textBox1.Text.ToLower()))
                {
                    bool exists = false;
                    foreach (var item in listBox2.Items)
                    {
                        if (xd == item.ToString())
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        listBox2.Visible = true;
                        listBox2.Items.Add(xd);
                    }
                }
            }
        }
    }
}
