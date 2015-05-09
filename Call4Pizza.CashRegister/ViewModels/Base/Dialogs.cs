using System.Windows;
using System;
using System.Windows.Input;

namespace Call4Pizza.CashRegister.ViewModels.Base
{
    public static class Dialogs
    {
        internal partial class MessageViewModel: BaseViewModel
        {
					#region Text Property
			
			private string _text;
			
			partial void OnSetText(string lastText, Action<string> handleSet);
			
			private void SetText(string alternateValue)
			{
				_text = alternateValue;
			}

			public string Text
			{
				get
				{
					return _text;
				}
				
				set
				{
					var lastText = _text;
					_text = value;
					OnSetText(
						lastText
						, new Action<string>(SetText));
					Notify("Text");
				}
			}
			
			#endregion
						#region Result Property
			
			private MessageBoxResult _result;
			
			partial void OnSetResult(MessageBoxResult lastResult, Action<MessageBoxResult> handleSet);
			
			private void SetResult(MessageBoxResult alternateValue)
			{
				_result = alternateValue;
			}

			public MessageBoxResult Result
			{
				get
				{
					return _result;
				}
				
				set
				{
					var lastResult = _result;
					_result = value;
					OnSetResult(
						lastResult
						, new Action<MessageBoxResult>(SetResult));
					Notify("Result");
				}
			}
			
			#endregion

            public event EventHandler Exiting;
        }

        //public static MessageBoxResult AskYesNo(string text)
        //{
        //    var view = ViewBinder.Default.FindWindow("AskYesNoWindow");
        //    view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //    var viewModel = new MessageViewModel
        //    {
        //        Text = text
        //    };
        //    viewModel.Exiting += (s, e) =>
        //    {
        //        view.Close();
        //    };
        //    view.DataContext = viewModel;
        //    view.ShowDialog();
        //    return viewModel.Result;
        //    //return MessageBox.Show(text, "Calybra", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //}

        //public static void Asterisk(string text)
        //{
        //    var view = ViewBinder.Default.FindWindow("AsteriskWindow");
        //    view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //    var viewModel = new MessageViewModel
        //    {
        //        Text = text
        //    };
        //    viewModel.Exiting += (s, e) =>
        //    {
        //        view.Close();
        //    };
        //    view.DataContext = viewModel;
        //    view.ShowDialog();
        //}

        //public static void ContinueAsterisk(string text)
        //{
        //    var view = ViewBinder.Default.FindWindow("ContinueAsteriskWindow");
        //    view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //    var viewModel = new MessageViewModel
        //    {
        //        Text = text
        //    };
        //    viewModel.Exiting += (s, e) =>
        //    {
        //        view.Close();
        //    };
        //    view.DataContext = viewModel;
        //    view.ShowDialog();
        //}

    }
}