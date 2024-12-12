using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVPApp.Context;
using MVPApp.Interfaces;

namespace MVPApp.Models
{
    public class LoginPresenter
    {
        private ILoginView _view;
        private readonly AppDbContext _dbContext;

        public LoginPresenter(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SetView(ILoginView view)
        {
            _view = view;
        }

        public void Login()
        {
            if (_view == null)
            {
                throw new InvalidOperationException("The view is not set.");
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Username == _view.Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(_view.Password, user.PasswordHash))
            {
                _view.NavigateToDashboard(user); // Navigate to the dashboard
            }
            else
            {
                _view.ShowMessage("Invalid credentials");
            }
        }
    }
}