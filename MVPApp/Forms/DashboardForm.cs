using MVPApp.Models;

namespace MVPApp
{
    public partial class DashboardForm : Form
    {
        private readonly User _user;

        public DashboardForm(User user)
        {
            InitializeComponent();
            lblUsername.Text = user.Username;
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            lblUsername.Text = _user.Username;
        }
    }
}