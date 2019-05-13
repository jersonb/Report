using ClassLibrary.data;
using ClassLibrary.model;
using ClassLibrary.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace TestUnitario
{
    [TestClass]
    public class ObjetivoTest
    {
        [TestInitialize]
        public void Inicializar() { }

        internal static void GerarArquioCom20ObjetivosDeTeste()
        {
            for (int i = 0; i < 20; i++)
            {
                Data.Save(new Objetivo("Objetivo Teste"+i));
            }
        }

        [TestMethod]
        public void TestaSequenciaIdObjetivo()
        {
            GerarArquioCom20ObjetivosDeTeste();
            var objetivos = Data.Objetivos().OrderBy(o => o.IdObjetivo).ToList();
            for (int i = 0; i < objetivos.Count - 1; i++)
            {
                Assert.IsTrue(objetivos[i].IdObjetivo < objetivos[i + 1].IdObjetivo);
            }
        }

        [TestMethod]
        public void TestaSalvarObjetivo()
        {
            var objetivo = new Objetivo("Teste Objetivo");

            Data.Save(objetivo);
            var findObjetivo = Data.FindObjetivo(1);
            Assert.AreEqual(objetivo.ToString(), findObjetivo.ToString());

        }

        [TestMethod]
        public void TestaEdicaoDeObjetivo()
        {
            GerarArquioCom20ObjetivosDeTeste();

            var objetivo = Data.FindObjetivo(2);

            objetivo.Descricao = "Descricao alterado";

            Data.Edit(objetivo);
            Assert.IsTrue(Data.Objetivos().Count == 20);
        }

        [TestMethod]
        public void TestaBuscarObjetivos()
        {
            GerarArquioCom20ObjetivosDeTeste();
            var objetivo4 = Data.FindObjetivo(4);

            Assert.AreEqual(4, objetivo4.IdObjetivo);
            Assert.AreEqual("Objetivo Teste3", objetivo4.Descricao);
        }

        [TestMethod]
        public void TestaDeletarObjetivo()
        {
            GerarArquioCom20ObjetivosDeTeste();

            var objetivo4 = Data.FindObjetivo(4);
            Data.Delete(objetivo4);

            Assert.IsTrue(Data.Objetivos().Count == 19);
        }

        [TestMethod]
        public void TestaExcecaoAoNaoLocalizarObjetivoProcurado()
        {
            GerarArquioCom20ObjetivosDeTeste();

            var objetivo4 = Data.FindObjetivo(4);
            Data.Delete(objetivo4);
            try
            {
                Data.FindObjetivo(4);
            }
            catch (Exception)
            {
                Assert.Fail("Nenhum objetivo encontrado");
            }

        }

        [TestMethod]
        public void TestaExcecaoAoNaoLocalizarObjetivos()
        {
            try
            {
                Data.Objetivos();
            }
            catch (Exception)
            {

                Assert.Fail("Erro ao listar objetivos\nProvavelmente nenhum objetivo foi criado");
            }

        }

        [TestCleanup]
        public void Finalizar()
        {
            File.Delete(Constantes.PathData(Tipo.OBJETIVO));
        }
    }
}

