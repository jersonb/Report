using Newtonsoft.Json;

namespace ClassLibrary.model
{
    public class Responsavel : Model
    {
        public int IdResponsavel { get; set; }
        public string Nome { get; set; }

        public Responsavel(string nome)
        {
            Nome = nome;
        }

        public override string ToString()
        {
            return Nome;
        }

    }
}
