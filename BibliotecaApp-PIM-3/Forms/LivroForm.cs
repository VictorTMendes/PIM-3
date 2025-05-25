using System;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaApp.Models;
using BibliotecaApp.Services;

namespace BibliotecaApp.Forms;

public class LivroForm : Form {
    private readonly LivroService service = new();
    private ListBox listBox = new();

    public LivroForm(){
        Text = "Gerenciar Livros";
        Width = 600;
        Height = 400;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = ColorTranslator.FromHtml("#F2F2F2");

        Panel header = new Panel{
            BackColor = Color.Orange,
            Dock = DockStyle.Top,
            Height = 20
        };

        Label titulo = new Label{
            Text = "LIVROS",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        header.Controls.Add(titulo);
        Controls.Add(header);

        listBox.Top = 30;
        listBox.Left = 100;
        listBox.Width = 400;
        listBox.Height = 200;
        Controls.Add(listBox);

        int top = 270;
        int leftInicial = 100;
        int espacamento = 120;

        Button btnAdd = CriarBotao("Adicionar", top, leftInicial);
        btnAdd.Click += (s, e) =>{
            string titulo = Prompt("Título:");
            string autor = Prompt("Autor:");
            if (titulo != "" && autor != ""){
                service.Adicionar(new Livro { Titulo = titulo, Autor = autor });
                AtualizarLista();
            }
        };
        Controls.Add(btnAdd);

        Button btnRemove = CriarBotao("Remover", top, leftInicial + espacamento);
        btnRemove.Click += (s, e) =>{
            if (listBox.SelectedItem is Livro livro){
                service.Remover(livro.Id);
                AtualizarLista();
            }
        };
        Controls.Add(btnRemove);

        AtualizarLista();

        Button btnVoltar = CriarBotao("Voltar", top, leftInicial + 2* espacamento  );
        btnVoltar.Click += (s, e) => this.Close();
        Controls.Add(btnVoltar);

    }


    private Button CriarBotao(string texto, int top, int left){
        return new Button{
            Text = texto,
            Width = 100,
            Height = 40,
            Top = top,
            Left = left,
            BackColor = ColorTranslator.FromHtml("#E0E0E0"),
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10)
        };
    }

    private void AtualizarLista(){
        listBox.DataSource = null;
        listBox.DataSource = service.Listar();
        listBox.DisplayMember = "Titulo";
    }

    private string Prompt(string text){
        return Microsoft.VisualBasic.Interaction.InputBox(text, "Entrada de Dados", "");
    }
}
