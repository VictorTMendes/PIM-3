using System.Text.Json;
using BibliotecaApp.Models;

namespace BibliotecaApp.Services;

public class EmprestimoService{
    private List<Emprestimo> emprestimos = new();
    private int contadorId = 1;
    private readonly string filePath = "emprestimos.json";
    private readonly LivroService livroService;

    public EmprestimoService(LivroService livroService){
        this.livroService = livroService;
        Carregar();
    }

    public List<Emprestimo> Listar() => emprestimos;

    public void RealizarEmprestimo(Leitor leitor, Livro livro){
        if (!livro.Disponivel) return;

        var emprestimo = new Emprestimo{
            Id = contadorId++,
            Leitor = leitor,
            Livro = livro,
            DataEmprestimo = DateTime.Now
        };

        livro.Disponivel = false;

        var livroOriginal = livroService.BuscarPorId(livro.Id);
        if (livroOriginal is not null){
            livroOriginal.Disponivel = false;
            livroService.Salvar();
        }

        emprestimos.Add(emprestimo);
        Salvar();
    }

    public void DevolverLivro(int idEmprestimo){
        var emprestimo = emprestimos.FirstOrDefault(e => e.Id == idEmprestimo);
        if (emprestimo is not null && emprestimo.DataDevolucao is null){
            emprestimo.DataDevolucao = DateTime.Now;

            var livroOriginal = livroService.BuscarPorId(emprestimo.Livro.Id);
            if (livroOriginal is not null){
                livroOriginal.Disponivel = true;
                livroService.Salvar();
            }

            Salvar();
        }
    }

    public void Salvar(){
        var json = JsonSerializer.Serialize(emprestimos);
        File.WriteAllText(filePath, json);
    }

    public void Carregar(){
        if (File.Exists(filePath)){
            var json = File.ReadAllText(filePath);
            emprestimos = JsonSerializer.Deserialize<List<Emprestimo>>(json) ?? new List<Emprestimo>();
            contadorId = emprestimos.Any() ? emprestimos.Max(e => e.Id) + 1 : 1;
        }
    }
}

    

