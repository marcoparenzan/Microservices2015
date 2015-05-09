using Call4Pizza.CashRegister.ViewModels;
using Call4Pizza.CashRegister.Views;
using Call4Pizza.Models.Contracts;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Call4Pizza.CashRegister
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var container = new IoCContainer();

            var viewModel = container.Resolve<CashRegisterViewModel>();

            var view = new CashRegisterWindow();
            view.DataContext = viewModel;
            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // view.WindowStyle = WindowStyle.None;
            //view.WindowState = WindowState.Maximized;

            var app = new Application();
            app.Run(view);
        }
    }
}
