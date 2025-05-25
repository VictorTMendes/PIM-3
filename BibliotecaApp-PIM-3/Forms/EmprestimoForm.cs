using System;
using System.Linq;
using System.Windows.Forms;
using BibliotecaApp.Models;
using BibliotecaApp.Services;

namespace BibliotecaApp.Forms;

public class EmprestimoForm : Form {
    private readonly LivroService livroService = new();
    private readonly LeitorService leitorService = new();
    private readonly EmprestimoService emprestimoService;

    private ListBox listBoxEmprestimos = new();

    public EmprestimoForm() {
        emprestimoService = new EmprestimoService(livroService);

        Text = "Gerenciar Empréstimos";
        Width = 600;
        Height = 600;
        StartPosition = FormStartPosition.CenterScreen;

        listBoxEmprestimos.Top = 45;
        listBoxEmprestimos.Left = 10;
        listBoxEmprestimos.Width = 550;
        listBoxEmprestimos.Height = 300;
        Controls.Add(listBoxEmprestimos);

        Panel header = new Panel {
            BackColor = Color.Orange,
            Dock = DockStyle.Top,
            Height = 40 
        };
        Label titulo = new Label{
            Text = "EMPRÉSTIMOS",
            Font = new Font("Segoe UI", 14, FontStyle.Bold), 
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        header.Controls.Add(titulo);
        Controls.Add(header);

        int buttonWidth = 150;
        int topPosition = 380;
        int spacing = 10;

        int totalWidth = (buttonWidth * 3) + (spacing * 2);  
        int initialLeft = (this.Width - totalWidth) / 2;  

        Button btnEmprestar = CriarBotao("Realizar Empréstimo", topPosition, initialLeft);
        btnEmprestar.Click += (s, e) => RealizarEmprestimo();
        Controls.Add(btnEmprestar);

        Button btnDevolver = CriarBotao("Devolver Livro", topPosition, initialLeft + buttonWidth + spacing);
        btnDevolver.Click += (s, e) => DevolverLivro();
        Controls.Add(btnDevolver);

        Button btnVoltar = CriarBotao("Voltar", topPosition, initialLeft + 2 * (buttonWidth + spacing));
        btnVoltar.Click += (s, e) => this.Close();
        Controls.Add(btnVoltar);

        AtualizarLista();
    }

    private Button CriarBotao(string texto, int top, int left){
        return new Button{
            Text = texto,
            Top = top,
            Left = left,
            Width = 150,
            Height = 40,
            BackColor = ColorTranslator.FromHtml("#E0E0E0"),
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.Black
        };
    }

    private void AtualizarLista(){
        listBoxEmprestimos.DataSource = null;
        listBoxEmprestimos.DataSource = emprestimoService.Listar();
        listBoxEmprestimos.DisplayMember = "Descricao";
    }

    private void RealizarEmprestimo(){
        var leitores = leitorService.Listar();
        var livrosDisponiveis = livroService.Listar().Where(l => l.Disponivel).ToList();

        if (!leitores.Any()) {
            MessageBox.Show("Nenhum leitor cadastrado.");
            return;
        }

        if (!livrosDisponiveis.Any()){
            MessageBox.Show("Nenhum livro disponível.");
            return;
        }

        var leitor = SelecionarLeitor(leitores);
        if (leitor == null) return;

        var livro = SelecionarLivro(livrosDisponiveis);
        if (livro == null) return;

        emprestimoService.RealizarEmprestimo(leitor, livro);
        livroService.Salvar();
        AtualizarLista();
    }

    private void DevolverLivro(){
        if (listBoxEmprestimos.SelectedItem is Emprestimo emprestimo){
            if (emprestimo.DataDevolucao != null){
                MessageBox.Show("Este livro já foi devolvido.");
                return;
            }

            emprestimoService.DevolverLivro(emprestimo.Id);
            livroService.Salvar();
            AtualizarLista();
        }
    }

    private Leitor? SelecionarLeitor(System.Collections.Generic.List<Leitor> leitores){
        var nomes = leitores.Select(l => l.Nome).ToArray();
        var escolha = MostrarSelecao("Selecione um leitor:", nomes);
        return escolha != null ? leitores.FirstOrDefault(l => l.Nome == escolha) : null;
    }

    private Livro? SelecionarLivro(System.Collections.Generic.List<Livro> livros){
        var titulos = livros.Select(l => l.Titulo).ToArray();
        var escolha = MostrarSelecao("Selecione um livro:", titulos);
        return escolha != null ? livros.FirstOrDefault(l => l.Titulo == escolha) : null;
    }

    private string? MostrarSelecao(string titulo, string[] opcoes){
        var form = new Form { Width = 400, Height = 200, Text = titulo };
        var listBox = new ListBox { Left = 10, Top = 10, Width = 360, Height = 100 };
        listBox.Items.AddRange(opcoes);

        var btnOk = new Button { Text = "OK", Left = 100, Width = 80, Top = 120, DialogResult = DialogResult.OK };
        var btnCancel = new Button { Text = "Cancelar", Left = 200, Width = 80, Top = 120, DialogResult = DialogResult.Cancel };

        form.Controls.Add(listBox);
        form.Controls.Add(btnOk);
        form.Controls.Add(btnCancel);
        form.AcceptButton = btnOk;
        form.CancelButton = btnCancel;

        return form.ShowDialog() == DialogResult.OK ? listBox.SelectedItem?.ToString() : null;
    }
}
