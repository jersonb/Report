using System.Collections.Generic;

namespace ClassLibrary.model
{
    public class Empresa : Model
    {
        public int IdEmpresa { get; set; }
        public string Nome { get; set; }
        public List<Responsavel> Responsaveis { get; set; }
        public List<Contato> Contatos { get; set; }
        public List<Segmento> Seguimentos { get; set; }
       
        public override string ToString()
        {
            return Nome;
        }
    }
}
