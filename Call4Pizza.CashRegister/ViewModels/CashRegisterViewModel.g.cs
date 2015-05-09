using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using Call4Pizza.CashRegister.ViewModels.Base;

namespace Call4Pizza.CashRegister.ViewModels
{
	#region Class CashRegisterViewModel

	public partial class CashRegisterViewModel: BaseViewModel
    {
			
			#region Local Command AddPizza
			
			public partial class AddPizzaArgs: BaseArgs
			{
			}
			
			public AddPizzaArgs AddPizzaCommandArgs
			{
				get
				{
					AddPizzaArgs args = new AddPizzaArgs {
					};
					return args;
				}
			}

			partial void OnAddPizza(AddPizzaArgs args);

			partial void OnAddPizzaEnable(Action<bool> enableHandler);

			private RelayCommand<AddPizzaArgs> _addPizza; // ICommand

			public RelayCommand<AddPizzaArgs> AddPizza // ICommand
			{
				get
				{
					if (_addPizza == null)
					{
						_addPizza = new RelayCommand<AddPizzaArgs>(new Action<AddPizzaArgs>(
							_ =>
							{
								try
								{
									OnAddPizza(_);
								}
								catch(Exception ex)
								{
									OnException(ex);
								}
							}
						),
						_ => {
							bool enabled = true;
							OnAddPizzaEnable(enable => {
								enabled = enable;
							});
							return enabled;
						});
					}
					return _addPizza;
				}
			}
			
			#endregion		
				
			#region Local Command AddIngredient
			
			public partial class AddIngredientArgs: BaseArgs
			{
			}
			
			public AddIngredientArgs AddIngredientCommandArgs
			{
				get
				{
					AddIngredientArgs args = new AddIngredientArgs {
					};
					return args;
				}
			}

			partial void OnAddIngredient(AddIngredientArgs args);

			partial void OnAddIngredientEnable(Action<bool> enableHandler);

			private RelayCommand<AddIngredientArgs> _addIngredient; // ICommand

			public RelayCommand<AddIngredientArgs> AddIngredient // ICommand
			{
				get
				{
					if (_addIngredient == null)
					{
						_addIngredient = new RelayCommand<AddIngredientArgs>(new Action<AddIngredientArgs>(
							_ =>
							{
								try
								{
									OnAddIngredient(_);
								}
								catch(Exception ex)
								{
									OnException(ex);
								}
							}
						),
						_ => {
							bool enabled = true;
							OnAddIngredientEnable(enable => {
								enabled = enable;
							});
							return enabled;
						});
					}
					return _addIngredient;
				}
			}
			
			#endregion		
				
			#region Local Command CreateOrder
			
			public partial class CreateOrderArgs: BaseArgs
			{
			}
			
			public CreateOrderArgs CreateOrderCommandArgs
			{
				get
				{
					CreateOrderArgs args = new CreateOrderArgs {
					};
					return args;
				}
			}

			partial void OnCreateOrder(CreateOrderArgs args);

			partial void OnCreateOrderEnable(Action<bool> enableHandler);

			private RelayCommand<CreateOrderArgs> _createOrder; // ICommand

			public RelayCommand<CreateOrderArgs> CreateOrder // ICommand
			{
				get
				{
					if (_createOrder == null)
					{
						_createOrder = new RelayCommand<CreateOrderArgs>(new Action<CreateOrderArgs>(
							_ =>
							{
								try
								{
									OnCreateOrder(_);
								}
								catch(Exception ex)
								{
									OnException(ex);
								}
							}
						),
						_ => {
							bool enabled = true;
							OnCreateOrderEnable(enable => {
								enabled = enable;
							});
							return enabled;
						});
					}
					return _createOrder;
				}
			}
			
			#endregion		
			
		partial void OnException(Exception ex);

					#region LastName Property
			
			private string _lastName;
			
			partial void OnSetLastName(string lastLastName, Action<string> handleSet);
			
			private void SetLastName(string alternateValue)
			{
				_lastName = alternateValue;
			}

			public string LastName
			{
				get
				{
					return _lastName;
				}
				
				set
				{
					if (value == _lastName) return;
					var lastLastName = _lastName;
					_lastName = value;
					OnSetLastName(
						lastLastName
						, new Action<string>(SetLastName));
					Notify("LastName");
				}
			}
			
			#endregion
						#region FirstName Property
			
			private string _firstName;
			
			partial void OnSetFirstName(string lastFirstName, Action<string> handleSet);
			
			private void SetFirstName(string alternateValue)
			{
				_firstName = alternateValue;
			}

			public string FirstName
			{
				get
				{
					return _firstName;
				}
				
				set
				{
					if (value == _firstName) return;
					var lastFirstName = _firstName;
					_firstName = value;
					OnSetFirstName(
						lastFirstName
						, new Action<string>(SetFirstName));
					Notify("FirstName");
				}
			}
			
			#endregion
						#region EMail Property
			
			private string _eMail;
			
			partial void OnSetEMail(string lastEMail, Action<string> handleSet);
			
			private void SetEMail(string alternateValue)
			{
				_eMail = alternateValue;
			}

			public string EMail
			{
				get
				{
					return _eMail;
				}
				
				set
				{
					if (value == _eMail) return;
					var lastEMail = _eMail;
					_eMail = value;
					OnSetEMail(
						lastEMail
						, new Action<string>(SetEMail));
					Notify("EMail");
				}
			}
			
			#endregion
						#region Total Property
			
			private decimal _total;
			
			partial void OnSetTotal(decimal lastTotal, Action<decimal> handleSet);
			
			private void SetTotal(decimal alternateValue)
			{
				_total = alternateValue;
			}

			public decimal Total
			{
				get
				{
					return _total;
				}
				
				set
				{
					if (value == _total) return;
					var lastTotal = _total;
					_total = value;
					OnSetTotal(
						lastTotal
						, new Action<decimal>(SetTotal));
					Notify("Total");
				}
			}
			
			#endregion
						#region PizzaCapricciosa Property
			
			private int _pizzaCapricciosa;
			
			partial void OnSetPizzaCapricciosa(int lastPizzaCapricciosa, Action<int> handleSet);
			
			private void SetPizzaCapricciosa(int alternateValue)
			{
				_pizzaCapricciosa = alternateValue;
			}

			public int PizzaCapricciosa
			{
				get
				{
					return _pizzaCapricciosa;
				}
				
				set
				{
					if (value == _pizzaCapricciosa) return;
					var lastPizzaCapricciosa = _pizzaCapricciosa;
					_pizzaCapricciosa = value;
					OnSetPizzaCapricciosa(
						lastPizzaCapricciosa
						, new Action<int>(SetPizzaCapricciosa));
					Notify("PizzaCapricciosa");
				}
			}
			
			#endregion
						#region PizzaDiavola Property
			
			private int _pizzaDiavola;
			
			partial void OnSetPizzaDiavola(int lastPizzaDiavola, Action<int> handleSet);
			
			private void SetPizzaDiavola(int alternateValue)
			{
				_pizzaDiavola = alternateValue;
			}

			public int PizzaDiavola
			{
				get
				{
					return _pizzaDiavola;
				}
				
				set
				{
					if (value == _pizzaDiavola) return;
					var lastPizzaDiavola = _pizzaDiavola;
					_pizzaDiavola = value;
					OnSetPizzaDiavola(
						lastPizzaDiavola
						, new Action<int>(SetPizzaDiavola));
					Notify("PizzaDiavola");
				}
			}
			
			#endregion
						#region Beer Property
			
			private int _beer;
			
			partial void OnSetBeer(int lastBeer, Action<int> handleSet);
			
			private void SetBeer(int alternateValue)
			{
				_beer = alternateValue;
			}

			public int Beer
			{
				get
				{
					return _beer;
				}
				
				set
				{
					if (value == _beer) return;
					var lastBeer = _beer;
					_beer = value;
					OnSetBeer(
						lastBeer
						, new Action<int>(SetBeer));
					Notify("Beer");
				}
			}
			
			#endregion
				}
	
	#endregion
}
