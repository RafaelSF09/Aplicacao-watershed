using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using watershedAPP.classes;

namespace watershedAPP
{
    public partial class Passos : Form
    {
        private Bitmap[]? passos;

        private Image? image;

        private int pos;

        public Passos()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Arquivos de Imagem(*.jpg;*.png)|*.jpg;*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog1.FileName);
            }

            calcula();
        }

        private void calcula()
        {
            if (image != null)
            {
                pos = 0;
                passos = Watershed.runPassoAPasso(new Bitmap(image), checkBox1.Checked, (double)numericUpDown1.Value);
                pictureBox1.Image = passos[0];
            }
        }

        private void limparToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            image = null;
            passos = null;
        }

        private void proximo_Click(object sender, EventArgs e)
        {
            if (passos != null && pos < passos.Length - 1)
            {
                pos++;
                pictureBox1.Image = passos[pos];
            }
        }

        private void anterior_Click(object sender, EventArgs e)
        {
            if (passos != null && pos > 0)
            {
                pos--;
                pictureBox1.Image = passos[pos];
            }
        }

        private void buttonRecalcula_Click(object sender, EventArgs e)
        {
            calcula();
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
