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
    internal class ResponsavelController : IResponsavel
    {
        private string pathData = Constantes.PathData(Tipo.RESPONSAVEL);

        public bool Delete(Responsavel responsavel)
        {
            var delete = false;
            try
            {
                var file = ReadText();

                delete = file.Remove(file[file.FindIndex(o => o.Equals(FindResponsavel(responsavel.IdResponsavel).ToJson()))]);

                File.WriteAllLines(pathData, file);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar este responsavel\n" + ex.Message);
            }

            return delete;
        }

        public bool Edit(Responsavel responsavel)
        {
            var save = false;

            try
            {
                var file = ReadText();

                file[file.FindIndex(o => o.Equals(FindResponsavel(responsavel.IdResponsavel).ToJson()))] = responsavel.ToJson();

                File.WriteAllLines(pathData, file);

                save = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar este responsavel\n" + ex.Message);
            }

            return save;
        }

        public Responsavel FindResponsavel(int id)
        {
            try
            {
                return Responsaveis().Find(o => o.IdResponsavel.Equals(id));
            }
            catch (Exception)
            {

                throw new Exception("Nenhum responsavel encontrado");
            }

        }

        public List<Responsavel> Responsaveis()
        {
            try
            {
                return ReadText().Select(o => JsonConvert.DeserializeObject<Responsavel>(o))
                                               .OrderBy(o => o.Nome)
                                               .ToList();

            }
            catch (Exception)
            {

                throw new Exception("Erro ao listar Responsaveis\nProvavelmente nenhum responsavel foi criado");
            }
        }

        public bool Save(Responsavel responsavel)
        {
            var save = false;
            try
            {
                responsavel.IdResponsavel = AtualId();
                File.AppendAllText(pathData, responsavel.ToJson() + "\n");
                save = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Salvar este responsavel\n" + ex.Message);
            }

            return save;
        }


        private List<string> ReadText()
        {
            return Reads.Read(Tipo.RESPONSAVEL);
        }

        private int AtualId()
        {
            var list = Responsaveis().OrderBy(e => e.IdResponsavel).ToList();
            return list.Count() == 0 || list == null ? 1 : list.ElementAt(list.Count - 1).IdResponsavel + 1;
        }
    }
}
