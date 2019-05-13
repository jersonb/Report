using ClassLibrary.model;
using System.Collections.Generic;


namespace ClassLibrary.interfaces
{
    interface IObjetivo
    {
        bool Save(Objetivo objetivo);
        bool Edit(Objetivo objetivo);
        bool Delete(Objetivo objetivo);
        List<Objetivo> Objetivos();
        Objetivo FindObjetivo(int id);
    }
}