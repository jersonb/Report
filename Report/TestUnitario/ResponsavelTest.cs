using ClassLibrary.data;
using ClassLibrary.model;
using ClassLibrary.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace TestUnitario
{   [TestClass]
    public class ResponsavelTest
    {
        [TestInitialize]
        public void Inicializar() { }

        internal static void GerarArquioCom20ResponsaveisDeTeste()
        {
            for (int i = 0; i < 20; i++)
            {
                Data.Save(new Responsavel("Responsavel Teste" + i));
            }
        }

        [TestMethod]
        public void TestaSequenciaIdResponsavel()
        {
            GerarArquioCom20ResponsaveisDeTeste();
            var responsaveis = Data.Responsaveis().OrderBy(o => o.IdResponsavel).ToList();
            for (int i = 0; i < responsaveis.Count - 1; i++)
            {
                Assert.IsTrue(responsaveis[i].IdResponsavel < responsaveis[i + 1].IdResponsavel);
            }
        }

        [TestMethod]
        public void TestaSalvarResponsavel()
        {
            var responsavel = new Responsavel("Teste Responsavel");

            Data.Save(responsavel);
            var findResponsavel = Data.FindResponsavel(1);
            Assert.AreEqual(responsavel.ToString(), findResponsavel.ToString());

        }

        [TestMethod]
        public void TestaEdicaoDeResponsavel()
        {
            GerarArquioCom20ResponsaveisDeTeste();

            var responsavel = Data.FindResponsavel(2);

            responsavel.Nome = "Nome alterado";

            Data.Edit(responsavel);
            Assert.IsTrue(Data.Responsaveis().Count == 20);
        }

        [TestMethod]
        public void TestaBuscarResponsavel()
        {
            GerarArquioCom20ResponsaveisDeTeste();
            var responsavel4 = Data.FindResponsavel(4);

            Assert.AreEqual(4, responsavel4.IdResponsavel);
            Assert.AreEqual("Responsavel Teste3", responsavel4.Nome);
        }

        [TestMethod]
        public void TestaDeletarResponsvel()
        {
            GerarArquioCom20ResponsaveisDeTeste();

            var responsavel4 = Data.FindResponsavel(4);
            Data.Delete(responsavel4);

            Assert.IsTrue(Data.Responsaveis().Count == 19);
        }

        [TestMethod]
        public void TestaExcecaoAoNaoLocalizarResponsavelProcurado()
        {
            GerarArquioCom20ResponsaveisDeTeste();

            var responsavel4 = Data.FindResponsavel(4);
            Data.Delete(responsavel4);
            try
            {
                Data.FindResponsavel(4);
            }
            catch (Exception)
            {
                Assert.Fail("Nenhum responsavel encontrado");
            }

        }

        [TestMethod]
        public void TestaExcecaoAoNaoLocalizarResponsavel()
        {
            try
            {
                Data.Responsaveis();
            }
            catch (Exception)
            {

                Assert.Fail("Erro ao listar responsaveis\nProvavelmente nenhum responsavel foi criado");
            }

        }

        [TestCleanup]
        public void Finalizar()
        {
            File.Delete(Constantes.PathData(Tipo.RESPONSAVEL));
        }
    }
}
