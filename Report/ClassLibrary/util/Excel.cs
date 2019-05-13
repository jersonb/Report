using ClassLibrary.data;
using ClosedXML.Excel;
using System.Diagnostics;
using System.Threading;

namespace ClassLibrary.util
{
    public class Excel
    {
        public void GerarArquivoEmpresa(int idEmpresa, string nameFile)
        {
            var empresa = Data.FindEmpresa(idEmpresa);
            var list = Data.Ocorrencias(idEmpresa);
           
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(empresa.Nome);
            int linha = 1;

            ws.Cell(linha, 1).Value = "Empresa: ";
            ws.Cell(linha, 2).Value = empresa.Nome;
            ws.Range(linha, 2, linha, 8).Merge();
            ws.Range(linha, 1, linha, 8).Style.Font.SetBold();

            linha++;
            linha++;
            ws.Cell(linha, 1).Value = "Seguimento";
            ws.Cell(linha, 1).Style.Font.SetBold();
            linha++;
            foreach (var item in empresa.Seguimentos)
            {
                ws.Cell(linha, 1).Value = item.Descricao;
                linha++;
            }
            linha++;
            //ws.Cell(linha, 1).Value = "Objetivos";
            //ws.Cell(linha, 1).Style.Font.SetBold();
            //linha++;
            //foreach (var item in list)
            //{
            //    ws.Cell(linha, 1).Value = item.Tipo;
            //    linha++;
            //}

            //linha++;

            ws.Cell(linha, 1).Value = "Responsaveis";
            ws.Cell(linha, 1).Style.Font.SetBold();

            linha++;
            foreach (var item in empresa.Responsaveis)
            {
                ws.Cell(linha, 1).Value = item.Nome;
                linha++;
            }

            linha++;

            var rContatos = ws.Range(linha, 1, linha, 2).Merge();
            rContatos.Value = "Contatos";
            rContatos.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rContatos.Style.Font.SetBold();

            linha++;
            foreach (var item in Data.FindEmpresa(empresa.IdEmpresa).Contatos)
            {
                ws.Cell(linha, 1).Value = item.Descricao;
                ws.Cell(linha, 2).Value = item.Informacao;
                linha++;
            }

            linha = 2;
            var tOcorrencia = ws.Range(linha, 4, linha, 8).Merge();
            tOcorrencia.Value = "Ocorrencias";
            tOcorrencia.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            tOcorrencia.Style.Font.SetBold();

            linha++;
            ws.Cell(linha, 4).Value = "Data";
            ws.Cell(linha, 5).Value = "Tipo";
            ws.Cell(linha, 6).Value = "Descricao";
            ws.Cell(linha, 7).Value = "Notas";
            ws.Cell(linha, 8).Value = "Monitoramento";
            ws.Range(linha, 4, linha, 8).Style.Font.SetBold();

            linha++;
            foreach (var item in list)
            {
                ws.Cell(linha, 4).Value = item.Date;
                ws.Cell(linha, 5).Value = item.Tipo.Descricao;
                ws.Cell(linha, 6).Value = item.Descricao;
                ws.Cell(linha, 7).Value = item.Notas;
                ws.Cell(linha, 8).Value = item.Monitoramento == true ? "Sim" : "Nao"; 
                linha++;
            }

            ws.Column(7).Style.Alignment.WrapText = true;
            
            ws.Columns(1, 8).AdjustToContents();
            ws.Columns(1, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            string path = string.Format("{0}{1}-{2}.xlsx", Constantes.PATH_FILES, nameFile, empresa.Nome);
            wb.SaveAs(path);
            Thread.Sleep(1000);
            Process.Start(path);
        }

        //public static void GerarArquivoLista(string idEmpresa)
        //{
        //    var nomeEmpresa = FindEmpresa(idEmpresa).Nome;
        //    var list = Ocorrencias(idEmpresa);
        //    var wb = new XLWorkbook();
        //    var ws = wb.Worksheets.Add(nomeEmpresa);


        //    string[] cabecalho = { "Empresa", "Seguimento", "Objetivos", "Contatos", "Responsaveis", "Ocorrencia" };
        //    for (int i = 0; i < cabecalho.Length; i++)
        //    {
        //        ws.Cell(1, i + 1).Value = cabecalho[i];
        //    }

        //    int linha = 2;
        //    foreach (var item in list)
        //    {
        //        int coluna = 1;
        //        ws.Cell(linha, coluna++).Value = item.Empresa_.Nome;
        //        ws.Cell(linha, coluna++).Value = item.Seguimento_.Descricao;
        //        ws.Cell(linha, coluna++).Value = item.Objetivo_.Descricao;
        //        ws.Cell(linha, coluna++).Value = item.Contato_.Descricao + ": " + item.Contato_.Informacao;
        //        ws.Cell(linha, coluna++).Value = item.Responsavel_.Nome;
        //        ws.Cell(linha, coluna++).Value = item.Descricao;
        //        linha++;
        //    }

        //    ws.Columns(1, 6).AdjustToContents();

        //    string path = string.Format(@"\\Mac\Home\Documents\DataBase\Monitoramento - {0}.xlsx", nomeEmpresa);
        //    wb.SaveAs(path);

        //}
    }
}
