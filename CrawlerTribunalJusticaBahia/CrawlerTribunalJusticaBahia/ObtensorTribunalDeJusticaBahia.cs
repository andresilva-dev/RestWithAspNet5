using CrawlerTribunalJusticaBahia.Model;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace CrawlerTribunalJusticaBahia
{
    public static class ObtensorTribunalDeJusticaBahia
    {
        private static string ObtenhaInformacaoDoElemento(HtmlNode elemento)
        {
            var elementoSpan = elemento.SelectSingleNode(".//span");

            if (elementoSpan == null)
                return string.Empty;

            string valor;
            var property = elementoSpan.GetAttributeValue("class", "");

            if (property == "labelClass")
            {
                valor = elemento.InnerText.Trim().Replace("Área: ", "");
                return valor;
            }

            var elementoSpanInteno = elementoSpan.SelectSingleNode(".//span");
            
            if (elementoSpanInteno != null)
            {
                valor = elementoSpanInteno.ChildNodes.First().InnerText.Trim();
            }
            else
            {
                valor = elementoSpan.ChildNodes.First().InnerText.Trim();
            }

            return valor;
        }

        public static List<string> ObtenhaInformacoesDoProcessoPorCodigo(long codigo)
        {
            var url = @"http://esaj.tjba.jus.br/cpo/sg/search.do;jsessionid=51F6CF2B414333AB2B6AF450D7980B05.cposg4?paginaConsulta=1&cbPesquisa=NUMPROC&tipoNuProcesso=UNIFICADO&numeroDigitoAnoUnificado=0809979-67.2015&foroNumeroUnificado=0080&dePesquisaNuUnificado=0809979-67.2015.8.05.0080&dePesquisa=";

            string markup;
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF7;
                markup = wc.DownloadString(url);
            }

            var html = new HtmlAgilityPack.HtmlDocument();

            html.LoadHtml(markup);
            var processo = ObtenhaProcesso(html);

            return new List<string>();
        }

        private static Processo ObtenhaProcesso(HtmlAgilityPack.HtmlDocument html)
        {
            var secao = html.DocumentNode.SelectNodes("//table[@class='secaoFormBody']").Last();

            var listaDeElementos = secao.SelectNodes(".//tr");
            var processo = new Processo();
            foreach (var elemento in listaDeElementos)
            {
                var div = elemento.SelectSingleNode(".//div[@class='labelClass']");

                if (div == null)
                    continue;

                if (div.InnerText.StartsWith("Processo:"))
                {
                    processo.NumeroDoProcesso = ObtenhaValorDoElemento(elemento);
                }
                if (div.InnerText.StartsWith("Classe:"))
                {
                    processo.Classe = ObtenhaValorDoElemento(elemento);
                    var elemtentoArea = elemento.SelectSingleNode("//td[contains(span, 'Área:')]");

                    processo.Area = elemtentoArea.InnerText.Replace("Área:", "").Trim();
                }
                if (div.InnerText.StartsWith("Assunto:"))
                {
                    processo.Assunto = ObtenhaValorDoElemento(elemento);
                }
                if (div.InnerText.StartsWith("Origem:"))
                {
                    processo.Origem = ObtenhaValorDoElemento(elemento);
                }
                if (div.InnerText.StartsWith("Números de origem:"))
                {
                    processo.NumeroDeOrigem = ObtenhaValorDoElemento(elemento);
                }
                if (div.InnerText.StartsWith("Distribuição:"))
                {
                    processo.Distribuicao = ObtenhaValorDoElemento(elemento);
                }
                if (div.InnerText.StartsWith("Relator:"))
                {
                    processo.Relator = ObtenhaValorDoElemento(elemento);
                }
            }

            var a = ObtenhaMovimentacoesDoProcesso(html);
            return processo;
        }

        private static string ObtenhaMovimentacoesDoProcesso(HtmlAgilityPack.HtmlDocument html)
        {

            var secao = html.DocumentNode.SelectSingleNode("//table[@id='tabelaUltimasMovimentacoes']");

            var listaDeElementos = secao.SelectNodes(".//tr");

            var teste = new Dictionary<string, string>();
            foreach (var elemento in listaDeElementos)
            {
                var itens = elemento.SelectNodes(".//td");

                teste.Add($"{teste.Count + 1}: {itens.First().InnerText.Trim()}", itens.Last().InnerText.Trim());
            }
            

            return string.Empty;
        }

        private static string ObtenhaValorDoElemento(HtmlNode elemento)
        {
            return elemento.SelectNodes(".//span").First().InnerText.Trim();
        }
    }
}
