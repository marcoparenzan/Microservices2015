using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.AggregateRoots
{
    public abstract class Call4PizzaAggregateRoot<TAggregateRoot, TId> : IAggregateRoot<TId>
        where TAggregateRoot: IAggregateRoot<TId>
    {
        public void Apply<TCommand>(TCommand command)
            where TCommand: ICommand
        {
            var handled = false;
            OnApply(command, ref handled);
        }

        protected virtual void OnApply<TCommand>(TCommand command, ref bool handled)
        {
        }

        private EventHandler<IEvent> _event;

        public event EventHandler<IEvent> Event
        {
            add { _event += value; }
            remove { _event -= value; }
        }

        protected void Notify<TEvent>(TEvent e)
            where TEvent: IEvent
        {
            if (_event == null) return;
            _event(this, e);
        }

        public TId Id
        {
            get { return OnGetId(); }
        }

        protected abstract TId OnGetId();
    }
}
