using ClassLibrary.model;
using System.Collections.Generic;


namespace ClassLibrary.interfaces
{
    interface IEmpresa
    {
         bool Save(Empresa empresa);
         bool Edit(Empresa empresa);
         bool Delete(Empresa empresa);
         List<Empresa> Empresas();
         Empresa FindEmpresa(int id);
    }
}