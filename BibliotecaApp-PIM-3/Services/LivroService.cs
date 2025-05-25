using System.Text.Json;
using BibliotecaApp.Models;

namespace BibliotecaApp.Services;

public class LivroService{
    private List<Livro> livros = new();
    private int contadorId = 1;
    private readonly string filePath = "livros.json";

    public LivroService() => Carregar();

    public List<Livro> Listar() => livros;

    public void Adicionar(Livro livro){
        livro.Id = contadorId++;
        livros.Add(livro);
        Salvar();
    }

    public void Remover(int id){
        var livro = livros.FirstOrDefault(l => l.Id == id);
        if (livro != null){
            livros.Remove(livro);
            Salvar();
        }
    }

    public Livro? BuscarPorId(int id) => livros.FirstOrDefault(l => l.Id == id);

    public void Salvar(){
        var json = JsonSerializer.Serialize(livros);
        File.WriteAllText(filePath, json);
    }

    public void Carregar(){
        if (File.Exists(filePath)){
            var json = File.ReadAllText(filePath);
            livros = JsonSerializer.Deserialize<List<Livro>>(json) ?? new List<Livro>();
            contadorId = livros.Any() ? livros.Max(l => l.Id) + 1 : 1;
        }
    }
}
