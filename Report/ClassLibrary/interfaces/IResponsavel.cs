using ClassLibrary.model;
using System.Collections.Generic;

namespace ClassLibrary.interfaces
{
    interface IResponsavel
    {
        bool Save(Responsavel responsavel);
        bool Edit(Responsavel responsavel);
        bool Delete(Responsavel responsavel);
        List<Responsavel> Responsaveis();
        Responsavel FindResponsavel(int id);
    }
}
