namespace ClassLibrary.model
{
    public class Contato
    {
        public string Descricao { get; set; }
        public string Informacao { get; set; }

        public Contato(string descricao, string informacao)
        {
            Descricao = descricao;
            Informacao = informacao;
        }
    }
}
