using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;

namespace s3listing
{
    class Program
    {
        static IConfigurationRoot _configRoot { get; set; }
        static AWSOptions _awsOptions { get; set; }
        public IAmazonS3 _s3Client { get; set; }
        
        static async Task Main(string[] args)
        {
            _configRoot = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            _awsOptions = _configRoot.GetAWSOptions();

            var p = new Program();
            await p.ListBuckets();
        }

        public Program()
        {
            _s3Client = _awsOptions.CreateServiceClient<IAmazonS3>();
        }

        async Task ListBuckets()
        {
            Console.WriteLine("Listing Buckets...");
            var response = await _s3Client.ListBucketsAsync();
            foreach (var b in response.Buckets)
            {
                System.Console.WriteLine($"{b.BucketName}");
            }
        }
    }
}
