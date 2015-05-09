using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.Commands
{
    public abstract class Call4PizzaCommand: ICommand
    {
        public Call4PizzaCommand()
        {
            CommandId = Guid.NewGuid();
        }

        [Required]
        public Guid CommandId { get; private set; }
        [Required]
        public CommandSource Source { get; set; }
        [Required]
        public string SourceId { get; set; }

        TCommand ICommand.As<TCommand>()
        {
            if (this is TCommand)
            {
                return (TCommand)(ICommand)this;
            }
            throw new InvalidCastException("This is not a " + typeof(TCommand).Name);
        }
    }
}
