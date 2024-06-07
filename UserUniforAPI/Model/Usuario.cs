namespace UserUniforAPI.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string userName { get; set; }
        public string senha { get; set; }
    }

    public class UsuarioCreate
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string userName { get; set; }
        public string senha { get; set; }
    }
}
