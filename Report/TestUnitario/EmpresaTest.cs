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
    public class EmpresaTest
    {
        [TestInitialize]
        public void Inicializar()
        {
            ResponsavelTest.GerarArquioCom20ResponsaveisDeTeste();
            SeguimentoTest.GerarArquioCom20SeguimentoDeTeste();
        }

        internal static void GerarArquivEmpresaCom20EmpresasdeDeTeste()
        {
            var r = new Random();
            var responsaveis = new List<Responsavel>
            {
                Data.FindResponsavel(r.Next(0, 19)),
                Data.FindResponsavel(r.Next(0, 19))
            };
            var segmentos = new List<Segmento>
            {
                Data.FindSeguimento(r.Next(0, 19)),
                Data.FindSeguimento(r.Next(0, 19))
            };

            for (int i = 0; i < 20; i++)
            {
                Data.Save(new Empresa
                {
                    Nome = "Teste" + i,
                    Contatos = new List<Contato>()
                {
                    new Contato("linkedin","www.linkwdin.com.br"+i),
                    new Contato("Telefone","91321413-"+i)
                },
                    Responsaveis = responsaveis,
                    Seguimentos = segmentos
                });
            }
        }

        [TestMethod]
        public void TestaSequenciaIdEmpresa()
        {
            GerarArquivEmpresaCom20EmpresasdeDeTeste();
            var empresas = Data.Empresas().OrderBy(o=>o.IdEmpresa).ToList();
            for (int i = 0; i < empresas.Count - 1; i++)
            {
                Assert.IsTrue(empresas[i].IdEmpresa < empresas[i + 1].IdEmpresa);
            }
        }

        [TestMethod]
        public void TestaSalvarEmpresa()
        {
            var empresa = new Empresa
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
            };

            Data.Save(empresa);
            var findEmpresa = Data.FindEmpresa(1);
            Assert.AreEqual(empresa.ToString(), findEmpresa.ToString());

        }

        [TestMethod]
        public void TestaEdicaoDeEmpresa()
        {

            GerarArquivEmpresaCom20EmpresasdeDeTeste();

            var empresa = Data.FindEmpresa(2);

            empresa.Nome = "Nome alterado";

            Data.Edit(empresa);
            Assert.IsTrue(Data.Empresas().Count == 20);
        }

        [TestMethod]
        public void TestaBuscarEmpresa()
        {
            GerarArquivEmpresaCom20EmpresasdeDeTeste();
            var empresa4 = Data.FindEmpresa(4);

            Assert.AreEqual(4, empresa4.IdEmpresa);
            Assert.AreEqual("Teste3", empresa4.Nome);
        }

        [TestMethod]
        public void TestaDeletarEmpresa()
        {
            GerarArquivEmpresaCom20EmpresasdeDeTeste();

            var empresa4 = Data.FindEmpresa(4);
            Data.Delete(empresa4);

            Assert.IsTrue(Data.Empresas().Count == 19);
        }

        [TestMethod]
        public void TestaExcecaoAoNaoLocalizarEmpresaProcurada()
        {
            GerarArquivEmpresaCom20EmpresasdeDeTeste();

            var empresa4 = Data.FindEmpresa(4);
            Data.Delete(empresa4);
            try
            {
                Data.FindEmpresa(4);
            }
            catch (Exception)
            {

                Assert.Fail("Nenhuma empresa encontrada");
            }

        }

        [TestMethod]
        public void TestaExcecaoAoNaoLocalizarEmpresas()
        {
            try
            {
                Data.Empresas();
            }
            catch (Exception)
            {

                Assert.Fail("Erro ao listar empresas\nProvavelmente nenhuma empresa foi criada");
            }

        }

        [TestCleanup]
        public void Finalizar()
        {
            File.Delete(Constantes.PathData(Tipo.EMPRESA));
            File.Delete(Constantes.PathData(Tipo.RESPONSAVEL));
            File.Delete(Constantes.PathData(Tipo.SEGUIMENTO));
        }
    }
}

