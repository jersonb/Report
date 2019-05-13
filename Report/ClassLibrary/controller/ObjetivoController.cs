using ClassLibrary.interfaces;
using ClassLibrary.model;
using ClassLibrary.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClassLibrary.controller
{
    internal class ObjetivoController : IObjetivo
    {
        private string pathData = Constantes.PathData(Tipo.OBJETIVO);


        public bool Save(Objetivo objetivo)
        {
            var save = false;
            try
            {
                objetivo.IdObjetivo = AtualId();
                File.AppendAllText(pathData, objetivo.ToJson() + "\n");
                save = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Salvar esta objetivo\n" + ex.Message);
            }

            return save;
        }

        public bool Edit(Objetivo objetivo)
        {
            var save = false;

            try
            {
                var file = ReadText();

                file[file.FindIndex(o => o.Equals(FindObjetivo(objetivo.IdObjetivo).ToJson()))] = objetivo.ToJson();

                File.WriteAllLines(pathData, file);

                save = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar esta objetivo\n" + ex.Message);
            }

            return save;

        }

        public bool Delete(Objetivo objetivo)
        {
            var delete = false;
            try
            {
                var file = ReadText();

                delete = file.Remove(file[file.FindIndex(o => o.Equals(FindObjetivo(objetivo.IdObjetivo).ToJson()))]);

                File.WriteAllLines(pathData, file);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar este objetivo\n" + ex.Message);
            }

            return delete;
        }

        public List<Objetivo> Objetivos()
        {
            try
            {
                return ReadText().Select(o => JsonConvert.DeserializeObject<Objetivo>(o))
                                               .OrderBy(o => o.Descricao)
                                               .ToList();

            }
            catch (Exception)
            {

                throw new Exception("Erro ao listar objetivos\nProvavelmente nenhum objetivo foi criado");
            }
        }

        public Objetivo FindObjetivo(int id)
        {
            try
            {
                return Objetivos().Find(o => o.IdObjetivo.Equals(id));
            }
            catch (Exception)
            {

                throw new Exception("Nenhum objetivo encontrado");
            }

        }

        private List<string> ReadText()
        {
            return Reads.Read(Tipo.OBJETIVO);
        }

        private int AtualId()
        {
            var list = Objetivos().OrderBy(e => e.IdObjetivo).ToList();
            return list.Count() == 0 || list == null ? 1 : list.ElementAt(list.Count - 1).IdObjetivo + 1;
        }


    }
}
