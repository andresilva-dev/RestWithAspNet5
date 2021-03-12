using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlerTribunalJusticaBahia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txtNumeroDoProcesso_1.Text) || string.IsNullOrWhiteSpace(txtNumeroDoProcesso_1.Text))
            //{
            //    return;
            //}

            var dadosDoProcesso = ObtensorTribunalDeJusticaBahia.ObtenhaInformacoesDoProcessoPorCodigo(1);

            AtualizeTelaComInformacoesDoProcesso(dadosDoProcesso);
            if (dadosDoProcesso != null && dadosDoProcesso.Any())
            {
                
            }
        }

        private void AtualizeTelaComInformacoesDoProcesso(List<string> dadosDoProcesso)
        {
            lblRetorno_processo.Text = dadosDoProcesso[0];
            lblRetorno_classe.Text = dadosDoProcesso[1];
            lblRetorno_area.Text = dadosDoProcesso[2];
            lblRetorno_assunto.Text = dadosDoProcesso[3];
            lblRetorno_origem.Text = dadosDoProcesso[4];
            lblRetorno_distribuicao.Text = dadosDoProcesso[5];
            lblRetorno_relator.Text = dadosDoProcesso[6];
        }
    }
}
