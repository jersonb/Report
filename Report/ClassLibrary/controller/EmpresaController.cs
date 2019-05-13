using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClassLibrary.interfaces;
using ClassLibrary.model;
using ClassLibrary.util;
using Newtonsoft.Json;

namespace ClassLibrary.controller
{
    internal class EmpresaController : IEmpresa
    {
        private string pathData = Constantes.PathData(Tipo.EMPRESA);





        private bool VerificaArquivos()
        {

            if (!File.Exists(Constantes.PathData(Tipo.RESPONSAVEL))
                || !File.Exists(Constantes.PathData(Tipo.SEGUIMENTO)))
            {
                throw new Exception("E necessario cadastrar Seguimento e Responsavel \nantes de cadastrar uma empresa");
            }
            return true;
        }










        public bool Save(Empresa empresa)
        {
            var save = false;
            try
            {
                VerificaArquivos();
                empresa.IdEmpresa = AtualId();
                File.AppendAllText(pathData, empresa.ToJson() + "\n");
                save = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Salvar esta empresa\n" + ex.Message);
            }

            return save;
        }

        public bool Edit(Empresa empresa)
        {
            var save = false;

            try
            {
                var file = ReadText();

                file[file.FindIndex(o => o.Equals(FindEmpresa(empresa.IdEmpresa).ToJson()))] = empresa.ToJson();

                File.WriteAllLines(pathData, file);

                save = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar esta empresa\n" + ex.Message);
            }

            return save;

        }

        public bool Delete(Empresa empresa)
        {
            var delete = false;
            try
            {
                var file = ReadText();

                delete = file.Remove(file[file.FindIndex(o => o.Equals(FindEmpresa(empresa.IdEmpresa).ToJson()))]);

                File.WriteAllLines(pathData, file);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar esta empresa\n" + ex.Message);
            }

            return delete;
        }

        public List<Empresa> Empresas()
        {
            try
            {
                return ReadText().Select(o => JsonConvert.DeserializeObject<Empresa>(o))
                                               .OrderBy(o=>o.Nome)
                                               .ToList();
               
            }
            catch (Exception)
            {

                throw new Exception("Erro ao listar empresas\nProvavelmente nenhuma empresa foi criada");
            }
        }

        public Empresa FindEmpresa(int id)
        {
            try
            {
                return Empresas().Find(o => o.IdEmpresa.Equals(id));
            }
            catch (Exception)
            {

                throw new Exception("Nenhuma empresa encontrada");
            }
            
        }

        private List<string> ReadText()
        {
            return Reads.Read(Tipo.EMPRESA);
        }

        private int AtualId()
        {
            var list = Empresas().OrderBy(e => e.IdEmpresa).ToList();
            return list.Count() == 0 || list == null ? 1 : list.ElementAt(list.Count - 1).IdEmpresa + 1;
        }
        
    }
}
