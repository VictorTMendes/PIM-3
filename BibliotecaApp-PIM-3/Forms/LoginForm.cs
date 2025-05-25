using System;
using System.Windows.Forms;

namespace BibliotecaApp.Forms{
    public class LoginForm : Form{
        private TextBox usuarioInput;
        private TextBox senhaInput;
        private Button loginbtn;
        private Label statusbtn;

        public LoginForm(){
            Text = "Login";
            Width = 350;
            Height = 250;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = ColorTranslator.FromHtml("#F2F2F2");

            Label lblUsuario = new Label() { Text = "Usuário:", Left = 20, Top = 20, Width = 80 };
            ForeColor = Color.Black;
            usuarioInput = new TextBox() { Left = 110, Top = 20, Width = 180 };


            Label lblSenha = new Label() { Text = "Senha:", Left = 20, Top = 60, Width = 80 };
            ForeColor = Color.Black;

            senhaInput = new TextBox() { Left = 110, Top = 60, Width = 180, PasswordChar = '*' };

            loginbtn = new Button() { Text = "Entrar", Left = 110, Top = 100, Width = 180 };
            BackColor = ColorTranslator.FromHtml("#E0E0E0");
            loginbtn.Click += loginbtn_Click;

            statusbtn = new Label() { Text = "", Left = 20, Top = 140, Width = 300, ForeColor = System.Drawing.Color.Red };
            ForeColor = Color.Red;

            Controls.Add(lblUsuario);
            Controls.Add(usuarioInput);
            Controls.Add(lblSenha);
            Controls.Add(senhaInput);
            Controls.Add(loginbtn);
            Controls.Add(statusbtn);
        }

        private void loginbtn_Click(object? sender, EventArgs e){
            string usuario = usuarioInput.Text;
            string senha = senhaInput.Text;

            if (usuario == "admin" && senha == "1234"){
                Hide();
                var menu = new MainForm();
                menu.FormClosed += (s, args) => Close();
                menu.Show();
            } else{
                statusbtn.Text = "Usuário ou senha incorretos.";
                senhaInput.Clear();
                senhaInput.Focus();
            }
        }
    }
}
