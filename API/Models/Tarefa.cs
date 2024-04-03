namespace API.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataEntrada { get; set; } = DateTime.Now;
    }
}
