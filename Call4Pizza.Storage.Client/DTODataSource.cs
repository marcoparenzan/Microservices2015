using Call4Pizza.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Storage.Client
{
    public abstract class DTODataSource<TDTO> : IDataSource<TDTO>
        where TDTO: IDTO
    {
        private CloudStorageAccount _storageAccount = CloudStorageAccount.Parse(
            ConfigurationManager.ConnectionStrings["TotemDataSource"].ConnectionString
        );

        protected CloudStorageAccount StorageAccount
        {
            get { return _storageAccount; }
        }

        private CloudTableClient _tableClient;

        protected CloudTableClient TableClient
        {
            get
            {
                if (_tableClient == null) {
                    _tableClient = StorageAccount.CreateCloudTableClient();
                }
                return _tableClient;
            }
        }

        public IEnumerable<TDTO> GetByCommandId(Guid commandId)
        {
            return OnGetByCommandId(commandId);
        }

        protected abstract IEnumerable<TDTO> OnGetByCommandId(Guid commandId);
    }
}
