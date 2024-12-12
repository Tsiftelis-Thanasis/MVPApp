using MVPApp.Context;
using MVPApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPApp.Models
{
    public class RegisterPresenter
    {
        private IRegisterView _view;
        private readonly AppDbContext _dbContext;

        public RegisterPresenter(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SetView(IRegisterView view)
        {
            _view = view;
        }

        public void RegisterUser()
        {
            if (string.IsNullOrWhiteSpace(_view.Username) || string.IsNullOrWhiteSpace(_view.Password) || string.IsNullOrWhiteSpace(_view.Email))
            {
                _view.ShowMessage("Username, email and password are required.");
                return;
            }

            if (_dbContext.Users.Any(u => u.Username == _view.Username))
            {
                _view.ShowMessage("Username already exists.");
                return;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(_view.Password);
            var user = new User { Username = _view.Username, Email = _view.Email, PasswordHash = hashedPassword };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            _view.ShowMessage("User registered successfully!");
            _view.CloseForm(); // Call the new method to close the form
        }
    }
}