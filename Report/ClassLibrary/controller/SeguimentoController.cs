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
    internal class SegmentoController : ISeguimento
    {
        private string pathData = Constantes.PathData(Tipo.SEGUIMENTO);

        public bool Save(Segmento seguimento)
        {
            var save = false;
            try
            {
                seguimento.IdSegmento = AtualId();
                File.AppendAllText(pathData, seguimento.ToJson() + "\n");
                save = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Salvar esta seguimento\n" + ex.Message);
            }

            return save;
        }

        public bool Edit(Segmento seguimento)
        {
            var save = false;

            try
            {
                var file = ReadText();

                file[file.FindIndex(o => o.Equals(FindSegmento(seguimento.IdSegmento).ToJson()))] = seguimento.ToJson();

                File.WriteAllLines(pathData, file);

                save = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar esta seguimento\n" + ex.Message);
            }

            return save;

        }

        public bool Delete(Segmento segmento)
        {
            var delete = false;
            try
            {
                var file = ReadText();

                delete = file.Remove(file[file.FindIndex(o => o.Equals(FindSegmento(segmento.IdSegmento).ToJson()))]);

                File.WriteAllLines(pathData, file);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar esta seguimento\n" + ex.Message);
            }

            return delete;
        }

        public List<Segmento> Segmentos()
        {
            try
            {
                return ReadText().Select(o => JsonConvert.DeserializeObject<Segmento>(o))
                                               .OrderBy(o => o.Descricao)
                                               .ToList();

            }
            catch (Exception)
            {

                throw new Exception("Erro ao listar empresas\nProvavelmente nenhuma seguimento foi criada");
            }
        }

        public Segmento FindSegmento(int id)
        {
            try
            {
                return Segmentos().Find(o => o.IdSegmento.Equals(id));
            }
            catch (Exception)
            {

                throw new Exception("Nenhuma seguimento encontrada");
            }

        }

        private List<string> ReadText()
        {
            return Reads.Read(Tipo.SEGUIMENTO);
        }

        private int AtualId()
        {
            var list = Segmentos().OrderBy(e => e.IdSegmento).ToList();
            return list.Count() == 0 || list == null ? 1 : list.ElementAt(list.Count - 1).IdSegmento + 1;
        }
    }
}
