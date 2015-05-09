using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.Entities
{
    public abstract class Call4PizzaEntity<TKey> : IEntity<TKey>
    {
        private TKey _key;

        public TKey Key
        {
            get { return _key; }
            set { _key = value; }
        }
    }
}
