using Microsoft.Extensions.DependencyInjection;
using MVPApp.Interfaces;
using MVPApp.Models;

namespace MVPApp
{
    public partial class LoginForm : Form, ILoginView
    {
        private readonly LoginPresenter _presenter;

        public IServiceProvider ServiceProvider { get; set; } // Add this property
        public string Username => txtUsername.Text;
        public string Password => txtPassword.Text;

        public LoginForm(LoginPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            //_presenter.SetView(this); // Pass the view (this) to the presenter
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _presenter.Login();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (ServiceProvider != null)
            {
                using var registerForm = ServiceProvider.GetRequiredService<RegisterForm>();
                registerForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("ServiceProvider is not set.");
            }
        }

        public void NavigateToDashboard(User user)
        {
            // Hide the current login form
            this.Hide();

            // Resolve the DashboardForm from the DI container
            //var dashboardForm = ServiceProvider.GetRequiredService<DashboardForm()>();
            //dashboardForm.FormClosed += (s, e) => Application.Exit(); // Ensure app exits when DashboardForm closes
            //dashboardForm.Show();

            using var dashboardForm = new DashboardForm(user);
            this.Hide();
            dashboardForm.ShowDialog();
            dashboardForm.FormClosed += (s, e) => Application.Exit(); // Ensure app exits when DashboardForm closes
            this.Show();
        }
    }
}