using ClassLibrary.model;
using System.Collections.Generic;


namespace ClassLibrary.interfaces
{
    interface IOcorrencia
    {
        bool Save(Ocorrencia ocorrencia);
        bool Edit(Ocorrencia ocorrencia);
        bool Delete(Ocorrencia ocorrencia);
        List<Ocorrencia> Ocorrencias();
        Ocorrencia FindOcorrencia(int id);
    }
}