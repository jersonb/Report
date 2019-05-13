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
    public class OcorrenciaTest
    {
        [TestInitialize]
        public void Inicializar()
        {
            ResponsavelTest.GerarArquioCom20ResponsaveisDeTeste();
            SeguimentoTest.GerarArquioCom20SeguimentoDeTeste();
            EmpresaTest.GerarArquivEmpresaCom20EmpresasdeDeTeste();
            ObjetivoTest.GerarArquioCom20ObjetivosDeTeste();
        }

        internal static void GerarArquioCom20OcorrenciaDeTeste()
        {
            for (int i = 1; i < 21; i++)
            {
                var r = new Random();

                Data.Save(new Ocorrencia
                {
                    Descricao = "Ocorrencia Teste" + i,
                    Date = DateTime.Parse("2018/12/" + i),
                    Tipo = Data.FindObjetivo(r.Next(0, 19)),
                    Empresa_ = Data.FindEmpresa(r.Next(0, 19)),
                    Usuario = Data.FindResponsavel(r.Next(0, 19))
                });
            }
        }

        [TestMethod]
        public void TestaSequenciaIdOcorrencia()
        {
            GerarArquioCom20OcorrenciaDeTeste();
            var ocorrencias = Data.Ocorrencias().OrderBy(o => o.IdOcorrencia).ToList();
            for (int i = 0; i < ocorrencias.Count - 1; i++)
            {
                Assert.IsTrue(ocorrencias[i].IdOcorrencia < ocorrencias[i + 1].IdOcorrencia);
            }
        }

        [TestMethod]
        public void TestaSalvarOcorrencia()
        {
            Data.Save(new Empresa
            {
                Nome = "Teste",
                Contatos = new List<Contato>()
                {
                    new Contato("linkedin","www.linkwdin.com.br"),
                    new Contato("Telefone","91321413")
                },
                Responsaveis = new List<Responsavel>()
                {
                    new Responsavel("Jerson"),
                    new Responsavel("Joao"),
                    new Responsavel("Fellipe")
                },
                Seguimentos = new List<Segmento>()
                {
                    new Segmento("Varejo"),
                    new Segmento("Atacado")
                }
            });

            Data.Save(new Responsavel("Teste Responsavel"));
            var ocorrencia = new Ocorrencia
            {
                Descricao = "Tese Ocorrencia",
                Date = DateTime.Now,
                Empresa_ = Data.FindEmpresa(1),
                Usuario = Data.FindResponsavel(1)

            };

            Data.Save(ocorrencia);
            var findOcorrencia = Data.FindOcorrencia(1);
            Assert.AreEqual(ocorrencia.ToString(), findOcorrencia.ToString());

        }

        [TestMethod]
        public void TestaEdicaoDeOcorrencia()
        {
            GerarArquioCom20OcorrenciaDeTeste();

            var ocorrencia = Data.FindOcorrencia(2);

            ocorrencia.Descricao = "Descricao alterado";

            Data.Edit(ocorrencia);
            Assert.IsTrue(Data.Ocorrencias().Count == 20);
        }

        [TestMethod]
        public void TestaBuscarOcorrencia()
        {
            GerarArquioCom20OcorrenciaDeTeste();
            var ocorrencia4 = Data.FindOcorrencia(4);

            Assert.AreEqual(4, ocorrencia4.IdOcorrencia);
            Assert.AreEqual("Ocorrencia Teste4", ocorrencia4.Descricao);
        }

        [TestMethod]
        public void TestaDeletarOcorrencia()
        {
            GerarArquioCom20OcorrenciaDeTeste();

            var ocorrencia4 = Data.FindOcorrencia(4);
            Data.Delete(ocorrencia4);

            Assert.IsTrue(Data.Ocorrencias().Count == 19);
        }

        [TestMethod]
        public void TestaExcecaoAoNaoLocalizarOcorrenciaProcurado()
        {
            GerarArquioCom20OcorrenciaDeTeste();

            var ocorrencia4 = Data.FindOcorrencia(4);
            Data.Delete(ocorrencia4);
            try
            {
                Data.FindOcorrencia(4);
            }
            catch (Exception)
            {
                Assert.Fail("Nenhuma ocorrencia encontrada");
            }

        }

        [TestMethod]
        public void TestaExcecaoAoNaoLocalizarOcorrencia()
        {
            try
            {
                Data.Ocorrencias();
            }
            catch (Exception)
            {

                Assert.Fail("Erro ao listar ocorrencias\nProvavelmente nenhuma ocorrencia foi criada");
            }

        }

        [TestCleanup]
        public void Finalizar()
        {
            File.Delete(Constantes.PathData(Tipo.OCORRENCIA));
            File.Delete(Constantes.PathData(Tipo.EMPRESA));
            File.Delete(Constantes.PathData(Tipo.RESPONSAVEL));
            File.Delete(Constantes.PathData(Tipo.SEGUIMENTO));
            File.Delete(Constantes.PathData(Tipo.OBJETIVO));
        }
    }
}
