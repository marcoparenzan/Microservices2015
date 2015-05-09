using Call4Pizza.Models;
using Call4Pizza.Models.AggregateRoots;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Call4Pizza.Storage.Client
{
    public class CommadingOrderRepository : AggregateRootRepository<CommandingOrder, Guid>
    {
        protected override async Task OnSetAsync(CommandingOrder aggregateRoot)
        {
            var blobContainer = BlobClient.GetContainerReference("orders");
            var blob = blobContainer.GetBlockBlobReference("order/" + aggregateRoot.Id.ToString());

            var buffer = Serialize(aggregateRoot);
            await blob.UploadFromByteArrayAsync(buffer, 0, buffer.Length);
        }

        protected override async Task<CommandingOrder> OnGetByIdAsync(Guid id)
        {
            var blobContainer = BlobClient.GetContainerReference("orders");
            var blobList = blobContainer.ListBlobs("order/" + id.ToString());
            if (!blobList.Any())
            {
                return new CommandingOrder { };
            }
            var blob = blobContainer.GetBlockBlobReference("order/" + id.ToString());
            var ms = new MemoryStream();
            await blob.DownloadToStreamAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return Deserialize(ms);
        }
    }
}
