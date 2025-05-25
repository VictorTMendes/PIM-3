using System;
using System.Drawing;
using System.Windows.Forms;
using BibliotecaApp.Models;
using BibliotecaApp.Services;
using System.Collections.Generic;

namespace BibliotecaApp.Forms{
    public class LeitorForm : Form{
        private readonly LeitorService service = new();
        private ListBox listBox;

        public LeitorForm(){
            Text = "Gerenciar Leitores";
            Width = 500;
            Height = 400;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = ColorTranslator.FromHtml("#F2F2F2");

        Panel header = new Panel{
            BackColor = Color.Orange,
            Dock = DockStyle.Top,
            Height = 20
        };

        Label titulo = new Label{
            Text = "LEITORES",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        header.Controls.Add(titulo);
        Controls.Add(header);

        listBox = new ListBox(){
            Top = 30,
            Left = 50,
            Width = 400,
            Height = 200
        };
        Controls.Add(listBox);

        int top = 270;
        int leftInicial = 50;
        int espacamento = 120;
            
        Button btnAdd = CriarBotao("Adicionar", top, leftInicial);
        btnAdd.Click += BtnAdd_Click;
        Controls.Add(btnAdd);

        Button btnRemove = CriarBotao("Remover", top, leftInicial + espacamento);
        btnRemove.Click += BtnRemover_Click;
        Controls.Add(btnRemove);

        Button btnVoltar = CriarBotao("Voltar", top, leftInicial + 2 * espacamento);
        btnVoltar.Click += (s, e) => this.Close();
        Controls.Add(btnVoltar);

        AtualizarLista();
    }

    private Button CriarBotao(string texto, int top, int left){
        return new Button
        {
            Text = texto,
            Top = top,
            Left = left ,
            Width = 100,
            Height = 40,
            BackColor = ColorTranslator.FromHtml("#E0E0E0"),
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.Black
            
        };
    }

    private void BtnAdd_Click(object? sender, EventArgs e){
        string nome = Prompt("Informe o nome do leitor:");
        if (string.IsNullOrWhiteSpace(nome)) return;

        string email = Prompt("Informe o email do leitor:");
        if (string.IsNullOrWhiteSpace(email)) return;

        string telefone = Prompt("Informe o telefone do leitor:");
        if (string.IsNullOrWhiteSpace(telefone)) return;

        var leitor = new Leitor { Nome = nome, Email = email, Telefone = telefone };
        service.Adicionar(leitor);
        AtualizarLista();
    }

    private void BtnRemover_Click(object? sender, EventArgs e){
        if (listBox.SelectedItem is Leitor leitor)
        {
            var confirm = MessageBox.Show($"Deseja remover {leitor.Nome}?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes){
                service.Remover(leitor.Id);
                AtualizarLista();
            }
        }
    }

    private void AtualizarLista(){
        List<Leitor> leitores = service.Listar();

        listBox.DataSource = null;
        listBox.DataSource = leitores;
        listBox.DisplayMember = "Nome";
    }

    private string Prompt(string mensagem){
        Form prompt = new Form()
        {
            Width = 400,
            Height = 150,
            Text = mensagem,
            StartPosition = FormStartPosition.CenterScreen
        };

        Label textLabel = new Label() { Left = 20, Top = 20, Text = mensagem, Width = 340 };
        TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 340 };
        Button confirmation = new Button(){
            Text = "OK",
            Left = 270,
            Width = 90,
            Top = 80,
            DialogResult = DialogResult.OK,
            BackColor = ColorTranslator.FromHtml("#E0E0E0"),
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9)
        };

        confirmation.Click += (sender, e) => { prompt.Close(); };

        prompt.Controls.Add(confirmation);
        prompt.Controls.Add(textLabel);
        prompt.Controls.Add(inputBox);
        prompt.AcceptButton = confirmation;

        return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
        }
    }
}
