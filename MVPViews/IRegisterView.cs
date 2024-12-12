using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPApp.Interfaces
{
    public interface IRegisterView
    {
        string Username { get; }
        string Password { get; }
        string Email { get; }
        void ShowMessage(string message);
        void CloseForm(); // New method
    }
}
