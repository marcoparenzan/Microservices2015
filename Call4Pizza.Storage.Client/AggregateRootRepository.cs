using Call4Pizza.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Storage.Client
{
    public abstract class AggregateRootRepository<TAggregateRoot, TId> : IRepository<TAggregateRoot, TId>
        where TAggregateRoot: IAggregateRoot<TId>
    {
        private CloudStorageAccount _storageAccount = CloudStorageAccount.Parse(
            ConfigurationManager.ConnectionStrings["OrderRepository"].ConnectionString
        );

        protected CloudStorageAccount StorageAccount
        {
            get { return _storageAccount; }
        }

        private CloudBlobClient _blobClient;

        protected CloudBlobClient BlobClient
        {
            get
            {
                if (_blobClient == null) {
                    _blobClient = StorageAccount.CreateCloudBlobClient();
                }
                return _blobClient;
            }
        }

        protected byte[] Serialize(TAggregateRoot aggregateRoot)
        {
            var stream = new MemoryStream();
            IAggregateRootSerializable s = (IAggregateRootSerializable)(object)aggregateRoot;
            s.SerializeTo(stream);
            return stream.ToArray();
        }

        protected TAggregateRoot Deserialize(Stream stream)
        {
            var aggregateRoot = Activator.CreateInstance<TAggregateRoot>();
            IAggregateRootSerializable s = (IAggregateRootSerializable)(object)aggregateRoot;
            s.DeserializeFrom(stream);
            return aggregateRoot;
        }
        TAggregateRoot IRepository<TAggregateRoot, TId>.GetById(TId id)
        {
            var task = OnGetByIdAsync(id);
            task.Wait();
            return task.Result;
        }

        async Task<TAggregateRoot> IRepository<TAggregateRoot, TId>.GetByIdAsync(TId id)
        {
            return await OnGetByIdAsync(id);
        }

        protected abstract Task<TAggregateRoot> OnGetByIdAsync(TId id);

        void IRepository<TAggregateRoot, TId>.Set(TAggregateRoot aggregateRoot)
        {
            var buffer = Serialize(aggregateRoot);
            OnSetAsync(aggregateRoot).Wait();
        }
        
        async Task IRepository<TAggregateRoot, TId>.SetAsync(TAggregateRoot aggregateRoot)
        {
            var buffer = Serialize(aggregateRoot);
            await OnSetAsync(aggregateRoot);
        }

        protected abstract Task OnSetAsync(TAggregateRoot aggregateRoot);
    }
}
