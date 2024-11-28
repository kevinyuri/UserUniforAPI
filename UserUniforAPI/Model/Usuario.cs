namespace UserUniforAPI.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string userName { get; set; }
        public string senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
    }

    public enum TipoUsuario
    {
        Vendedor,
        Cliente
    }

    public class UsuarioCreate
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string userName { get; set; }
        public string senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }

    public class LoginRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
