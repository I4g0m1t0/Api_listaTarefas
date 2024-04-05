namespace API.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateOnly DataEntrada { get; set; } = DateOnly.FromDateTime(DateTime.Today); // Inicializa com a data atual sem a parte do tempo.



        // Novas propriedades
        public DateOnly? DataConclusao { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public bool? Concluida { get; set; } = false;
    }
}
