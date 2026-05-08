using Azure.Storage.Blobs;

namespace EventEase.Services
{
    public class BlobService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobService(IConfiguration configuration)
        {
            string connectionString = configuration["AzureBlobStorage:ConnectionString"];

            string containerName = configuration["AzureBlobStorage:ContainerName"];

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            _containerClient.CreateIfNotExists();
        }
    }
}
