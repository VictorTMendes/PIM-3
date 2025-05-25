using System;
using System.Windows.Forms;
using System.Drawing;

namespace BibliotecaApp.Forms;

public class MainForm : Form{
    public MainForm(){
        
        Text = "Sistema de Biblioteca";
        Width = 400;
        Height = 300;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = ColorTranslator.FromHtml("#F2F2F2");

        Panel header = new Panel();
        header.BackColor = Color.Orange;
        header.Dock = DockStyle.Top;
        header.Height = 20;
           
        Label title = new Label();
        title.Text = "MENU PRINCIPAL";
        title.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        title.ForeColor = Color.White;
        title.AutoSize = false;
        title.Dock = DockStyle.Fill;
        title.TextAlign = ContentAlignment.MiddleCenter;

        header.Controls.Add(title);
        Controls.Add(header);


        Button btnLivros = new Button();
        btnLivros.Text = "Gerenciar Livros";
        btnLivros.Width = 200;
        btnLivros.Top = 40;
        btnLivros.Left = 100;
        btnLivros.BackColor = ColorTranslator.FromHtml("#E0E0E0");
        btnLivros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnLivros.Click += (s, e) => new LivroForm().ShowDialog();  

        Button btnLeitores = new Button();
        btnLeitores.Text = "Gerenciar Leitores";
        btnLeitores.Width = 200;
        btnLeitores.Top = 90;
        btnLeitores.Left = 100;
        btnLeitores.BackColor = ColorTranslator.FromHtml("#E0E0E0");
        btnLeitores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnLeitores.Click += (s, e) => new LeitorForm().ShowDialog();

         Button btnEmprestimos = new Button();
        btnEmprestimos.Text = "Gerenciar Empréstimos";
        btnEmprestimos.Width = 200;
        btnEmprestimos.Top = 140;
        btnEmprestimos.Left = 100;
        btnEmprestimos.BackColor = ColorTranslator.FromHtml("#E0E0E0");
        btnEmprestimos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnEmprestimos.Click += (s, e) => new EmprestimoForm().ShowDialog();

        Controls.Add(btnLivros);
        Controls.Add(btnLeitores);
        Controls.Add(btnEmprestimos);
    }
}
