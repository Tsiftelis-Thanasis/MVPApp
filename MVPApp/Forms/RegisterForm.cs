using MVPApp.Interfaces;
using MVPApp.Models;

namespace MVPApp
{
    public partial class RegisterForm : Form, IRegisterView
    {
        private readonly RegisterPresenter _presenter;

        public RegisterForm(RegisterPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;

            // Set the view in the presenter after initialization
            _presenter.SetView(this);
        }

        public string Username => txtUsername.Text;
        public string Password => txtPassword.Text;
        public string Email => txtEmail.Text;

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            _presenter.RegisterUser();
        }

        public void CloseForm()
        {
            this.Close();
        }
    }
}