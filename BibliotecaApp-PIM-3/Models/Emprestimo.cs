namespace BibliotecaApp.Models;

public class Emprestimo
{
    public int Id { get; set; }
    public Leitor Leitor { get; set; } = null!;
    public Livro Livro { get; set; } = null!;
    public DateTime DataEmprestimo { get; set; }
    public DateTime? DataDevolucao { get; set; }

    public string Descricao =>
    $"{Livro.Titulo} - {Leitor.Nome} | " +
    $"Emprestado em {DataEmprestimo:dd/MM/yyyy}" +
    $"{(DataDevolucao is null ? " (Em aberto)" : $" | Devolvido em {DataDevolucao:dd/MM/yyyy}")}";

}
