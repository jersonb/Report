using ClassLibrary.data;
using ClassLibrary.model;
using ClassLibrary.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestUnitario
{
    [TestClass]
    public class SeguimentoTest
    {
        [TestInitialize]
        public void Inicializar() { }

        internal static void GerarArquioCom20SeguimentoDeTeste()
        {
            for (int i = 0; i < 20; i++)
            {
                Data.Save(new Segmento("Segmento Teste" + i));
            }
        }

        [TestMethod]
        public void TestaSequenciaIdSegmento()
        {
            GerarArquioCom20SeguimentoDeTeste();
            var seguimentos = Data.Seguimentos().OrderBy(o => o.IdSegmento).ToList();
            for (int i = 0; i < seguimentos.Count - 1; i++)
            {
                Assert.IsTrue(seguimentos[i].IdSegmento < seguimentos[i + 1].IdSegmento);
            }
        }

        [TestMethod]
        public void TestaSalvarSegmento()
        {
            var responsavel = new Responsavel("Teste Responsavel");

            Data.Save(responsavel);
            var findResponsavel = Data.FindResponsavel(1);
            Assert.AreEqual(responsavel.ToString(), findResponsavel.ToString());

        }

        [TestMethod]
        public void TestaEdicaoDeSeguimento()
        {
            GerarArquioCom20SeguimentoDeTeste();

            var segmento = Data.FindSeguimento(2);

            segmento.Descricao = "Descricao alterada";

            Data.Edit(segmento);
            Assert.IsTrue(Data.Seguimentos().Count == 20);
        }

        [TestMethod]
        public void TestaBuscarSegmento()
        {
            GerarArquioCom20SeguimentoDeTeste();
            var segmento4 = Data.FindSeguimento(4);

            Assert.AreEqual(4, segmento4.IdSegmento);
            Assert.AreEqual("Segmento Teste3", segmento4.Descricao);
        }

        [TestMethod]
        public void TestaDeletarSegmento()
        {
            GerarArquioCom20SeguimentoDeTeste();

            var seguimento4 = Data.FindSeguimento(4);
            Data.Delete(seguimento4);

            Assert.IsTrue(Data.Seguimentos().Count == 19);
        }

        [TestMethod]
        public void TestaExcecaoAoNaoLocalizarSegmentoProcurado()
        {
            GerarArquioCom20SeguimentoDeTeste();

            var seguimento4 = Data.FindSeguimento(4);
            Data.Delete(seguimento4);
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
        public void TestaExcecaoAoNaoLocalizarSegmento()
        {
            try
            {
                Data.Seguimentos();
            }
            catch (Exception)
            {

                Assert.Fail("Erro ao listar seguimentos\nProvavelmente nenhum segmento foi criado");
            }

        }

        [TestCleanup]
        public void Finalizar()
        {
            File.Delete(Constantes.PathData(Tipo.SEGUIMENTO));
        }

    }
}
