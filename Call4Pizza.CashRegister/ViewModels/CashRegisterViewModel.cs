using Call4Pizza.Models;
using Call4Pizza.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.CashRegister.ViewModels
{
    public partial class CashRegisterViewModel
    {
        public CashRegisterViewModel(ICashRegister cashRegister)
        {
            _cashRegister = cashRegister;
        }

        private ICashRegister _cashRegister;
        partial void OnAddIngredient(CashRegisterViewModel.AddIngredientArgs args)
        {
        }

        partial void OnAddPizza(CashRegisterViewModel.AddPizzaArgs args)
        {
        }

        partial void OnCreateOrder(CashRegisterViewModel.CreateOrderArgs args)
        {
            _cashRegister.Handle(new Models.Commands.CreateOrder
            {
                Source = CommandSource.CashRegister
                ,
                LastName = this.LastName
                ,
                FirstName = this.FirstName
                ,
                SourceId = "CashRegister1"
                ,
                Address = "Via XX Settembre 1"
                ,
                City = "Fiume Veneto"
                ,
                Province = "Pordenone"
                ,
                Country = "Italy"
                ,
                Phone = "348"
                ,
                EMail = this.EMail
                ,
                Date = DateTime.UtcNow
                ,
                PizzaCapricciosa = this.PizzaCapricciosa
                ,
                PizzaDiavola=this.PizzaDiavola
                ,
                Beer = this.Beer
            });
        }

        partial void OnException(Exception ex)
        {
            if (ex is UnauthorizedAccessException)
            {
 
            }
        }
    }
}
