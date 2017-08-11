using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace RabbitMqClient
{

    public  class EstablishConnection : IDisposable
    {
        public static ConnectionFactory _connectionFactory;
        public static IConnection _connection;
        public static IModel _model;
        public static Subscription _subscription;

        public static void CreateConnection(ConnectionConfigModel coneConfigModel)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = coneConfigModel.HostName,
                UserName = coneConfigModel.UserName,
                Password = coneConfigModel.Password,
            };

            if (string.IsNullOrEmpty(coneConfigModel.VirtualHost) == false)
                _connectionFactory.VirtualHost = coneConfigModel.VirtualHost;
            if (coneConfigModel.Port > 0)
                _connectionFactory.Port = coneConfigModel.Port;
            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model.BasicQos(0, 1, false);
            _subscription = new Subscription(_model, coneConfigModel.QueueName, false);
            
        }

        public static void DisplaySettings(ConnectionConfigModel coneConfigModel)
        {
            Console.WriteLine("Host: {0}", coneConfigModel.HostName);
            Console.WriteLine("Username: {0}", coneConfigModel.UserName);
            Console.WriteLine("Password: {0}", coneConfigModel.Password);
            Console.WriteLine("QueueName: {0}", coneConfigModel.QueueName);
            Console.WriteLine("VirtualHost: {0}", coneConfigModel.VirtualHost);
            Console.WriteLine("Port: {0}", coneConfigModel.Port);
            Console.WriteLine("Is Durable: {0}", coneConfigModel.IsDurable);
        }

        public static void Poll(bool enabled)
        {
            while (enabled)
            {
                //Get next message
                var deliveryArgs = _subscription.Next();
                //Deserialize message
                var message = Encoding.Default.GetString(deliveryArgs.Body);

                //Handle Message
                Console.WriteLine("Message Recieved - {0}", message);

                //Acknowledge message is processed
                _subscription.Ack(deliveryArgs);
            }
        }

        public void Dispose()
        {
            if (_model != null)
                _model.Dispose();
            if (_connection != null)
                _connection.Dispose();

            _connectionFactory = null;

            GC.SuppressFinalize(this);
        }
    }
}
