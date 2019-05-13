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
    internal class OcorrenciaController : IOcorrencia
    {
        private string pathData = Constantes.PathData(Tipo.OCORRENCIA);

        private bool VerificaArquivos()
        {

            if (!File.Exists(Constantes.PathData(Tipo.EMPRESA))
                || !File.Exists(Constantes.PathData(Tipo.RESPONSAVEL))
                || !File.Exists(Constantes.PathData(Tipo.OBJETIVO))
                || !File.Exists(Constantes.PathData(Tipo.SEGUIMENTO)))
            {
                throw new Exception("E necessario cadastrar Empresa, Seguimento, Objetivo e Responsavel \nantes de cadastrar uma ocorrencia");
            }
            return true;
        }

        public bool Save(Ocorrencia ocorrencia)
        {
            VerificaArquivos();
            var save = false;
            try
            {
                ocorrencia.IdOcorrencia = AtualId();
                File.AppendAllText(pathData, ocorrencia.ToJson() + "\n");
                save = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Salvar esta ocorrencia\n" + ex.Message);
            }

            return save;
        }

        public bool Edit(Ocorrencia ocorrencia)
        {
            var save = false;

            try
            {
                var file = ReadText();

                file[file.FindIndex(o => o.Equals(FindOcorrencia(ocorrencia.IdOcorrencia).ToJson()))] = ocorrencia.ToJson();

                File.WriteAllLines(pathData, file);

                save = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar esta ocorrencia\n" + ex.Message);
            }

            return save;

        }

        public bool Delete(Ocorrencia ocorrencia)
        {
            var delete = false;
            try
            {
                var file = ReadText();

                delete = file.Remove(file[file.FindIndex(o => o.Equals(FindOcorrencia(ocorrencia.IdOcorrencia).ToJson()))]);

                File.WriteAllLines(pathData, file);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar esta ocorrencia\n" + ex.Message);
            }

            return delete;
        }

        public List<Ocorrencia> Ocorrencias()
        {
            try
            {
                return ReadText().Select(o => JsonConvert.DeserializeObject<Ocorrencia>(o))
                                               .OrderBy(o => o.Descricao)
                                               .ToList();

            }
            catch (Exception)
            {

                throw new Exception("Erro ao listar ocorrencias\nProvavelmente nenhuma ocorrencia foi criada");
            }
        }

        public List<Ocorrencia> Ocorrencias(int idEmpresa)
        {
            try
            {
                return Ocorrencias().Where(o=>o.Empresa_.IdEmpresa.Equals(idEmpresa)).ToList();
            }
            catch (Exception)
            {

                throw new Exception("Erro ao listar ocorrencias\nProvavelmente nenhuma ocorrencia foi criada");
            }
        }

        public Ocorrencia FindOcorrencia(int id)
        {
            try
            {
                return Ocorrencias().Find(o => o.IdOcorrencia.Equals(id));
            }
            catch (Exception)
            {

                throw new Exception("Nenhuma ocorrencia encontrada");
            }

        }

        private List<string> ReadText()
        {
            return Reads.Read(Tipo.OCORRENCIA);
        }

        private int AtualId()
        {
            var list = Ocorrencias().OrderBy(e => e.IdOcorrencia).ToList();
            return list.Count() == 0 || list == null ? 1 : list.ElementAt(list.Count - 1).IdOcorrencia + 1;
        }
    }
}
