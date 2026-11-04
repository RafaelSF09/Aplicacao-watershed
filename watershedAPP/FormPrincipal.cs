

using watershedAPP.classes;

namespace watershedAPP
{
    public partial class FormPrincipal : Form
    {
        private Image image;

        private Bitmap imageBitmap;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Arquivos de Imagem(*.jpg;*.png)|*.jpg;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                pictureBoxSrc.Image = image;
            }
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "Png Image|*.png";
                saveFileDialog1.Title = "Salvar Imagem";
                saveFileDialog1.DefaultExt = "png"; // Garante que a extensão padrão seja .png

                // Verifica se o usuário realmente clicou no botão "Salvar"
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // O bloco 'using' garante que o FileStream seja fechado e liberado da memória
                        using (System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile())
                        {
                            if (pictureBoxDest.Image != null)
                                pictureBoxDest.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao salvar a imagem: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxSrc.Image = null;
            pictureBoxDest.Image = null;
        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            PreProcessamento.grayScale(imgDest);
            pictureBoxDest.Image = imgDest;
        }

        private void binarizaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            PreProcessamento.binarizacao(imgDest);
            pictureBoxDest.Image = imgDest;
        }

        private void dilatacaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = new Bitmap(image);
            FiltroMorfologico.dilatacao(imageBitmap, imgDest);
            pictureBoxDest.Image = imgDest;
        }

        private void erosaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(imgDest))
            {
                g.Clear(Pixel.corFundo);
            }
            imageBitmap = new Bitmap(image);
            FiltroMorfologico.erosao(imageBitmap, imgDest);
            pictureBoxDest.Image = imgDest;
        }

        private void aberturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(imgDest))
            {
                g.Clear(Pixel.corFundo);
            }
            imageBitmap = new Bitmap(image);
            FiltroMorfologico.abertura(imageBitmap, imgDest);
            pictureBoxDest.Image = imgDest;
        }

        private void fechamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(imgDest))
            {
                g.Clear(Pixel.corFundo);
            }
            imageBitmap = new Bitmap(image);
            FiltroMorfologico.fechamento(imageBitmap, imgDest);
            pictureBoxDest.Image = imgDest;
        }

        private void filtroMorfologicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(imgDest))
            {
                g.Clear(Pixel.corFundo);
            }
            imageBitmap = new Bitmap(image);
            FiltroMorfologico.run(imageBitmap, imgDest);
            pictureBoxDest.Image = imgDest;
        }

        private void trocaFundoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            PreProcessamento.trocaFundo(imgDest);
            pictureBoxDest.Image = imgDest;
        }

        private void laplaceEShapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = new Bitmap(image);
            PreProcessamento.laplaceESharp(imageBitmap, imgDest);
            pictureBoxDest.Image = imgDest;
        }

        private void transformaDistanciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            TransformadaDistancia.run(imgDest);
            pictureBoxDest.Image = imgDest;
        }

        private void thresholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            PreProcessamento.threshold(imgDest, 0.4);
            pictureBoxDest.Image = imgDest;
        }

        private void rodarWatershedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageBitmap = new Bitmap(image);
            Bitmap imgDest = Watershed.run(imageBitmap);
            pictureBoxDest.Image = imgDest;
        }

        private void passoAPassoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Passos formPassos = new Passos();
            formPassos.ShowDialog();
        }
    }
}
