using ClassLibrary.model;
using System.Collections.Generic;


namespace ClassLibrary.interfaces
{
    interface ISeguimento
    {
        bool Save(Segmento segmento);
        bool Edit(Segmento segmento);
        bool Delete(Segmento segmento);
        List<Segmento> Segmentos();
        Segmento FindSegmento(int id);
    }
}