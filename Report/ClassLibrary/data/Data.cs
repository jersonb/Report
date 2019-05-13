using ClassLibrary.model;
using System.Collections.Generic;
using ClassLibrary.controller;
using System;
using ClassLibrary.util;

namespace ClassLibrary.data
{
    public static class Data 
    {
        #region Empresa
        public static bool Save(Empresa empresa)
        {
            try
            {
                return new EmpresaController().Save(empresa);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static List<Empresa> Empresas()
        {
            return new EmpresaController().Empresas();
        }

        public static Empresa FindEmpresa(int id)
        {
            return new EmpresaController().FindEmpresa(id);
        }

        public static bool Edit(Empresa empresa)
        {
            try
            {
                return new EmpresaController().Edit(empresa);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static bool Delete(Empresa empresa)
        {
            try
            {
                return new EmpresaController().Delete(empresa);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Objetivo
        public static bool Save(Objetivo objetivo)
        {
            try
            {
                return new ObjetivoController().Save(objetivo);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static List<Objetivo> Objetivos()
        {
            return new ObjetivoController().Objetivos();
        }

        public static Objetivo FindObjetivo(int id)
        {
            return new ObjetivoController().FindObjetivo(id);
        }

        public static bool Edit(Objetivo objetivo)
        {
            try
            {
                return new ObjetivoController().Edit(objetivo);
            }
            catch (Exception)
            {

                throw;
            }

        }
        
        public static bool Delete(Objetivo objetivo)
        {
            try
            {
                return new ObjetivoController().Delete(objetivo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Ocorrencia
        public static bool Save(Ocorrencia ocorrencia)
        {
            try
            {
                return new OcorrenciaController().Save(ocorrencia);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static List<Ocorrencia> Ocorrencias(int idEmpresa)
        {
            try
            {
                return new OcorrenciaController().Ocorrencias(idEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Ocorrencia> Ocorrencias()
        {
            return new OcorrenciaController().Ocorrencias();
        }

        public static Ocorrencia FindOcorrencia(int id)
        {
            return new OcorrenciaController().FindOcorrencia(id);
        }

        public static bool Edit(Ocorrencia ocorrencia)
        {
            try
            {
                return new OcorrenciaController().Edit(ocorrencia);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static bool Delete(Ocorrencia ocorrencia)
        {
            try
            {
                return new OcorrenciaController().Delete(ocorrencia);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Responsavel
        public static bool Save(Responsavel responsavel)
        {
            try
            {
                return new ResponsavelController().Save(responsavel);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static List<Responsavel> Responsaveis()
        {
            return new ResponsavelController().Responsaveis();
        }

        public static Responsavel FindResponsavel(int id)
        {
            return new ResponsavelController().FindResponsavel(id);
        }

        public static bool Edit(Responsavel responsavel)
        {
            try
            {
                return new ResponsavelController().Edit(responsavel);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static bool Delete(Responsavel responsavel)
        {
            try
            {
                return new ResponsavelController().Delete(responsavel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        
        #region Seguimento
        public static bool Save(Segmento seguimento)
        {
            try
            {
                return new SegmentoController().Save(seguimento);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static List<Segmento> Seguimentos()
        {
            return new SegmentoController().Segmentos();
        }

        public static Segmento FindSeguimento(int id)
        {
            return new SegmentoController().FindSegmento(id);
        }

        public static bool Edit(Segmento seguimento)
        {
            try
            {
                return new SegmentoController().Edit(seguimento);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static bool Delete(Segmento seguimento)
        {
            try
            {
                return new SegmentoController().Delete(seguimento);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Excel
        public static void GerarExcel(int idEmpresa,string nameFile)
        {
            new Excel().GerarArquivoEmpresa(idEmpresa,nameFile);
        }

        #endregion

    }
}

