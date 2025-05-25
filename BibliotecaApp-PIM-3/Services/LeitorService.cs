using System.Text.Json;
using BibliotecaApp.Models;

namespace BibliotecaApp.Services;

public class LeitorService {
    private List<Leitor> leitores = new();
    private int contadorId = 1;
    private readonly string filePath = "leitores.json";

    public LeitorService() => Carregar();

    public List<Leitor> Listar() => leitores;

    public void Adicionar(Leitor leitor){
        leitor.Id = contadorId++;
        leitores.Add(leitor);
        Salvar();
    }

    public void Remover(int id){
        var leitor = leitores.FirstOrDefault(l => l.Id == id);
        if (leitor != null){
            leitores.Remove(leitor);
            Salvar();
        }
    }

    public Leitor? BuscarPorId(int id) => leitores.FirstOrDefault(l => l.Id == id);

    public void Salvar(){
        var json = JsonSerializer.Serialize(leitores);
        File.WriteAllText(filePath, json);
    }

    public void Carregar(){
        if (File.Exists(filePath)){
            var json = File.ReadAllText(filePath);
            leitores = JsonSerializer.Deserialize<List<Leitor>>(json) ?? new List<Leitor>();
            contadorId = leitores.Any() ? leitores.Max(l => l.Id) + 1 : 1;
        }
    }
}
