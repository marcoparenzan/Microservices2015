using Call4Pizza.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.SQL.Client
{
    public abstract class DTODataSource<TDTO> : DbContext, IDataSource<TDTO>
        where TDTO: IDTO
    {
        public IEnumerable<TDTO> GetByCommandId(Guid commandId)
        {
            return OnGetByCommandId(commandId);
        }

        protected abstract IEnumerable<TDTO> OnGetByCommandId(Guid commandId);
    }
}
