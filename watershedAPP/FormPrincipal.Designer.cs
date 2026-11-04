namespace watershedAPP
{
    partial class FormPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            abrirToolStripMenuItem = new ToolStripMenuItem();
            fecharToolStripMenuItem = new ToolStripMenuItem();
            salvarToolStripMenuItem = new ToolStripMenuItem();
            preProcessamentoToolStripMenuItem = new ToolStripMenuItem();
            trocaFundoToolStripMenuItem = new ToolStripMenuItem();
            laplaceEShapToolStripMenuItem = new ToolStripMenuItem();
            grayScaleToolStripMenuItem = new ToolStripMenuItem();
            binarizaçãoToolStripMenuItem = new ToolStripMenuItem();
            dilatacaoToolStripMenuItem = new ToolStripMenuItem();
            erosaoToolStripMenuItem = new ToolStripMenuItem();
            aberturaToolStripMenuItem = new ToolStripMenuItem();
            fechamentoToolStripMenuItem = new ToolStripMenuItem();
            filtroMorfologicoToolStripMenuItem = new ToolStripMenuItem();
            thresholdToolStripMenuItem = new ToolStripMenuItem();
            watershedToolStripMenuItem = new ToolStripMenuItem();
            transformaDistanciaToolStripMenuItem = new ToolStripMenuItem();
            rodarWatershedToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            pictureBoxSrc = new PictureBox();
            pictureBoxDest = new PictureBox();
            openFileDialog = new OpenFileDialog();
            passoAPassoToolStripMenuItem = new ToolStripMenuItem();
            menuStrip.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSrc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDest).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem, preProcessamentoToolStripMenuItem, watershedToolStripMenuItem, passoAPassoToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // 
            // arquivoToolStripMenuItem
            // 
            arquivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { abrirToolStripMenuItem, fecharToolStripMenuItem, salvarToolStripMenuItem });
            arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            arquivoToolStripMenuItem.Size = new Size(61, 20);
            arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // abrirToolStripMenuItem
            // 
            abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            abrirToolStripMenuItem.Size = new Size(109, 22);
            abrirToolStripMenuItem.Text = "Abrir";
            abrirToolStripMenuItem.Click += abrirToolStripMenuItem_Click;
            // 
            // fecharToolStripMenuItem
            // 
            fecharToolStripMenuItem.Name = "fecharToolStripMenuItem";
            fecharToolStripMenuItem.Size = new Size(109, 22);
            fecharToolStripMenuItem.Text = "Fechar";
            fecharToolStripMenuItem.Click += fecharToolStripMenuItem_Click;
            // 
            // salvarToolStripMenuItem
            // 
            salvarToolStripMenuItem.Name = "salvarToolStripMenuItem";
            salvarToolStripMenuItem.Size = new Size(109, 22);
            salvarToolStripMenuItem.Text = "Salvar";
            salvarToolStripMenuItem.Click += salvarToolStripMenuItem_Click;
            // 
            // preProcessamentoToolStripMenuItem
            // 
            preProcessamentoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { trocaFundoToolStripMenuItem, laplaceEShapToolStripMenuItem, grayScaleToolStripMenuItem, binarizaçãoToolStripMenuItem, dilatacaoToolStripMenuItem, erosaoToolStripMenuItem, aberturaToolStripMenuItem, fechamentoToolStripMenuItem, filtroMorfologicoToolStripMenuItem, thresholdToolStripMenuItem });
            preProcessamentoToolStripMenuItem.Name = "preProcessamentoToolStripMenuItem";
            preProcessamentoToolStripMenuItem.Size = new Size(122, 20);
            preProcessamentoToolStripMenuItem.Text = "Pre-Processamento";
            // 
            // trocaFundoToolStripMenuItem
            // 
            trocaFundoToolStripMenuItem.Name = "trocaFundoToolStripMenuItem";
            trocaFundoToolStripMenuItem.Size = new Size(170, 22);
            trocaFundoToolStripMenuItem.Text = "Troca Fundo";
            trocaFundoToolStripMenuItem.Click += trocaFundoToolStripMenuItem_Click;
            // 
            // laplaceEShapToolStripMenuItem
            // 
            laplaceEShapToolStripMenuItem.Name = "laplaceEShapToolStripMenuItem";
            laplaceEShapToolStripMenuItem.Size = new Size(170, 22);
            laplaceEShapToolStripMenuItem.Text = "Laplace e Shap";
            laplaceEShapToolStripMenuItem.Click += laplaceEShapToolStripMenuItem_Click;
            // 
            // grayScaleToolStripMenuItem
            // 
            grayScaleToolStripMenuItem.Name = "grayScaleToolStripMenuItem";
            grayScaleToolStripMenuItem.Size = new Size(170, 22);
            grayScaleToolStripMenuItem.Text = "Gray-Scale";
            grayScaleToolStripMenuItem.Click += grayScaleToolStripMenuItem_Click;
            // 
            // binarizaçãoToolStripMenuItem
            // 
            binarizaçãoToolStripMenuItem.Name = "binarizaçãoToolStripMenuItem";
            binarizaçãoToolStripMenuItem.Size = new Size(170, 22);
            binarizaçãoToolStripMenuItem.Text = "Binarização";
            binarizaçãoToolStripMenuItem.Click += binarizaçãoToolStripMenuItem_Click;
            // 
            // dilatacaoToolStripMenuItem
            // 
            dilatacaoToolStripMenuItem.Name = "dilatacaoToolStripMenuItem";
            dilatacaoToolStripMenuItem.Size = new Size(170, 22);
            dilatacaoToolStripMenuItem.Text = "Dilatação";
            dilatacaoToolStripMenuItem.Click += dilatacaoToolStripMenuItem_Click;
            // 
            // erosaoToolStripMenuItem
            // 
            erosaoToolStripMenuItem.Name = "erosaoToolStripMenuItem";
            erosaoToolStripMenuItem.Size = new Size(170, 22);
            erosaoToolStripMenuItem.Text = "Erosão";
            erosaoToolStripMenuItem.Click += erosaoToolStripMenuItem_Click;
            // 
            // aberturaToolStripMenuItem
            // 
            aberturaToolStripMenuItem.Name = "aberturaToolStripMenuItem";
            aberturaToolStripMenuItem.Size = new Size(170, 22);
            aberturaToolStripMenuItem.Text = "Abertura";
            aberturaToolStripMenuItem.Click += aberturaToolStripMenuItem_Click;
            // 
            // fechamentoToolStripMenuItem
            // 
            fechamentoToolStripMenuItem.Name = "fechamentoToolStripMenuItem";
            fechamentoToolStripMenuItem.Size = new Size(170, 22);
            fechamentoToolStripMenuItem.Text = "Fechamento";
            fechamentoToolStripMenuItem.Click += fechamentoToolStripMenuItem_Click;
            // 
            // filtroMorfologicoToolStripMenuItem
            // 
            filtroMorfologicoToolStripMenuItem.Name = "filtroMorfologicoToolStripMenuItem";
            filtroMorfologicoToolStripMenuItem.Size = new Size(170, 22);
            filtroMorfologicoToolStripMenuItem.Text = "Filtro Morfológico";
            filtroMorfologicoToolStripMenuItem.Click += filtroMorfologicoToolStripMenuItem_Click;
            // 
            // thresholdToolStripMenuItem
            // 
            thresholdToolStripMenuItem.Name = "thresholdToolStripMenuItem";
            thresholdToolStripMenuItem.Size = new Size(170, 22);
            thresholdToolStripMenuItem.Text = "Threshold";
            thresholdToolStripMenuItem.Click += thresholdToolStripMenuItem_Click;
            // 
            // watershedToolStripMenuItem
            // 
            watershedToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { transformaDistanciaToolStripMenuItem, rodarWatershedToolStripMenuItem });
            watershedToolStripMenuItem.Name = "watershedToolStripMenuItem";
            watershedToolStripMenuItem.Size = new Size(75, 20);
            watershedToolStripMenuItem.Text = "Watershed";
            // 
            // transformaDistanciaToolStripMenuItem
            // 
            transformaDistanciaToolStripMenuItem.Name = "transformaDistanciaToolStripMenuItem";
            transformaDistanciaToolStripMenuItem.Size = new Size(184, 22);
            transformaDistanciaToolStripMenuItem.Text = "Transforma Distancia";
            transformaDistanciaToolStripMenuItem.Click += transformaDistanciaToolStripMenuItem_Click;
            // 
            // rodarWatershedToolStripMenuItem
            // 
            rodarWatershedToolStripMenuItem.Name = "rodarWatershedToolStripMenuItem";
            rodarWatershedToolStripMenuItem.Size = new Size(184, 22);
            rodarWatershedToolStripMenuItem.Text = "Rodar";
            rodarWatershedToolStripMenuItem.Click += rodarWatershedToolStripMenuItem_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(pictureBoxSrc, 0, 0);
            tableLayoutPanel1.Controls.Add(pictureBoxDest, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(800, 426);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // pictureBoxSrc
            // 
            pictureBoxSrc.Dock = DockStyle.Fill;
            pictureBoxSrc.Location = new Point(3, 3);
            pictureBoxSrc.Name = "pictureBoxSrc";
            pictureBoxSrc.Size = new Size(394, 420);
            pictureBoxSrc.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxSrc.TabIndex = 0;
            pictureBoxSrc.TabStop = false;
            // 
            // pictureBoxDest
            // 
            pictureBoxDest.Dock = DockStyle.Fill;
            pictureBoxDest.Location = new Point(403, 3);
            pictureBoxDest.Name = "pictureBoxDest";
            pictureBoxDest.Size = new Size(394, 420);
            pictureBoxDest.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxDest.TabIndex = 1;
            pictureBoxDest.TabStop = false;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog";
            // 
            // passoAPassoToolStripMenuItem
            // 
            passoAPassoToolStripMenuItem.Name = "passoAPassoToolStripMenuItem";
            passoAPassoToolStripMenuItem.Size = new Size(91, 20);
            passoAPassoToolStripMenuItem.Text = "Passo a Passo";
            passoAPassoToolStripMenuItem.Click += passoAPassoToolStripMenuItem_Click;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "FormPrincipal";
            Text = "Watershed";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxSrc).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDest).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem abrirToolStripMenuItem;
        private ToolStripMenuItem fecharToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBoxSrc;
        private PictureBox pictureBoxDest;
        private OpenFileDialog openFileDialog;
        private ToolStripMenuItem salvarToolStripMenuItem;
        private ToolStripMenuItem preProcessamentoToolStripMenuItem;
        private ToolStripMenuItem grayScaleToolStripMenuItem;
        private ToolStripMenuItem binarizaçãoToolStripMenuItem;
        private ToolStripMenuItem dilatacaoToolStripMenuItem;
        private ToolStripMenuItem erosaoToolStripMenuItem;
        private ToolStripMenuItem aberturaToolStripMenuItem;
        private ToolStripMenuItem fechamentoToolStripMenuItem;
        private ToolStripMenuItem filtroMorfologicoToolStripMenuItem;
        private ToolStripMenuItem trocaFundoToolStripMenuItem;
        private ToolStripMenuItem laplaceEShapToolStripMenuItem;
        private ToolStripMenuItem watershedToolStripMenuItem;
        private ToolStripMenuItem transformaDistanciaToolStripMenuItem;
        private ToolStripMenuItem thresholdToolStripMenuItem;
        private ToolStripMenuItem rodarWatershedToolStripMenuItem;
        private ToolStripMenuItem passoAPassoToolStripMenuItem;
    }
}
