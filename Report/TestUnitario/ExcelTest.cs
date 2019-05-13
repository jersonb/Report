using ClassLibrary.data;
using ClassLibrary.model;
using ClassLibrary.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace TestUnitario
{
    [TestClass]
    public class ExcelTest
    {
        [TestInitialize]
        public void Inicarteste()
        {
            CriarObjetos();
        }

        private void CriarObjetos()
        {
            var r = new Random();

            for (int i = 1; i < 31; i++)
            {
                Data.Save(new Responsavel("Responsavel Teste" + i));
                Data.Save(new Objetivo("Objetivo Teste" + i));
                Data.Save(new Segmento("Seguimento Teste" + i));
                Data.Save(new Empresa
                {
                    Nome = "Teste" + i,
                    Contatos = new List<Contato>()
                {
                    new Contato("linkedin","www.linkwdin.com.br"+i),
                    new Contato("Telefone","91321413-"+i)
                },
                    Responsaveis = new List<Responsavel>()
                {
                    new Responsavel("Teste Responsavel A"+i),
                    new Responsavel("Teste Responsavel B"+i),
                    new Responsavel("Teste Responsavel C"+i)
                },
                    Seguimentos = new List<Segmento>()
                {
                    new Segmento("Seguimento A"+i),
                    new Segmento("Seguimento B"+i)
                }
                });

                Data.Save(new Ocorrencia
                {
                    Date = DateTime.Parse("2018/12/"+i),
                    Descricao = "Teste Ocorrencia" + i,
                    Empresa_ = Data.FindEmpresa(i),
                    Usuario = Data.FindResponsavel(i)

                });

            }
        }

        [TestMethod]
        public void TestaCricacaoExcel()
        {
            Data.GerarExcel(3, "Teste");
           
            Assert.IsTrue(File.Exists(Constantes.PATH_FILES+"Teste-Teste3.xlsx"));
        }

        [TestCleanup]
        public void FinalizaTeste()
        {
            File.Delete(Constantes.PathData(Tipo.EMPRESA));
            File.Delete(Constantes.PathData(Tipo.RESPONSAVEL));
            File.Delete(Constantes.PathData(Tipo.OBJETIVO));
            File.Delete(Constantes.PathData(Tipo.SEGUIMENTO));
            File.Delete(Constantes.PathData(Tipo.OCORRENCIA));
            File.Delete(Constantes.PATH_FILES + "Teste-Teste3.xlsx");
        }
    }
}
