using Azure.Storage.Queues;
using System;
using System.Threading.Tasks;

namespace Cloud_Storage.Services
{
    public class QueueService
    {
        private readonly QueueClient _queueClient;

        public QueueService(string connectionString, string queueName)
        {
            _queueClient = new QueueClient(connectionString, queueName);
            _queueClient.CreateIfNotExists();
        }

        public async Task SendMessageAsync(string message)
        {
            if (!_queueClient.Exists())
            {
                throw new InvalidOperationException("The queue does not exist.");
            }

            // Encode the message before sending it to the queue
            string base64Message = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(message));
            await _queueClient.SendMessageAsync(base64Message);
        }

        public async Task<string?> ReceiveMessageAsync()
        {
            var response = await _queueClient.ReceiveMessageAsync();

            if (response.Value != null)
            {
                string decodedMessage = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(response.Value.MessageText));
                await _queueClient.DeleteMessageAsync(response.Value.MessageId, response.Value.PopReceipt);
                return decodedMessage;
            }

            return null;
        }
    }
}
