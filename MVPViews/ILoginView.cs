using MVPApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPApp.Interfaces
{
    public interface ILoginView
    {
        string Username { get; }
        string Password { get; }
        void ShowMessage(string message);
        void NavigateToDashboard(User user); // New method
    }
}
