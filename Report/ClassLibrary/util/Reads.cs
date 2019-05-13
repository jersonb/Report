using System.Collections.Generic;
using System.IO;

namespace ClassLibrary.util
{
    public static class Reads
    {
        public static List<string> Read(Tipo t)
        {
            var texto = new List<string>();
            string path = Constantes.PathData(t);

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            foreach (var item in File.ReadAllLines(path))
            {
                texto.Add(item);
            }

            return texto;
        }

        internal static string SelectTipo(Tipo t)
        {
            string path;
            switch (t)
            {
                
                case Tipo.EMPRESA:
                    {
                        path = "empresa";
                        break;
                    }
                case Tipo.OBJETIVO:
                    {
                        path = "objetivo";
                        break;
                    }
                case Tipo.OCORRENCIA:
                    {
                        path = "ocorrencia";
                        break;
                    }
                case Tipo.RESPONSAVEL:
                    {
                        path = "responsavel";
                        break;
                    }
                case Tipo.SEGUIMENTO:
                    {
                        path = "seguimento";
                        break;
                    }
                default:
                    path = "";
                    break;
            }

            return path;

        }
    }
}
