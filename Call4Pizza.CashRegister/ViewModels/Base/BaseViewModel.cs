using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace Call4Pizza.CashRegister.ViewModels.Base
{
    [Serializable]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private DependencyObject _do;

        protected System.Windows.Threading.Dispatcher Dispatcher
        {
            get
            {
                if (_do == null)
                {
                    _do = new DependencyObject();
                }
                return _do.Dispatcher;
            }
        }

        public virtual void Notify(string propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private PropertyChangedEventHandler _propertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                _propertyChanged += value;
            }
            remove
            {
                _propertyChanged -= value;
            }
        }


        protected virtual void Discard()
        {
        }

        protected virtual void InvalidateData()
        {
            Discard();
            OnInvalidateData();
        }

        protected virtual void OnInvalidateData()
        {
        }

        protected virtual bool Validate()
        {
            List<string> messages = new List<string>();
            Action<string> onError = message =>
            {
                messages.Add(message);
            };
            if (!OnValidate(onError))
            {
                string allMessages = string.Join("\r\n", messages.ToArray());
                //Dialogs.Asterisk(allMessages);
                return false;
            }
            return true;
        }

        protected virtual bool OnValidate(Action<string> onError)
        {
            return true;
        }
    }
}
