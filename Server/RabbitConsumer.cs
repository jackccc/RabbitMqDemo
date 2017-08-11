using System;
using System.Text;
using RabbitMqClient;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;

namespace Server
{
    /// <summary>
    /// Class to encapsulate recieving messages from RabbitMQ
    /// </summary>
    public class RabbitConsumer
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string QueueName = "Module2.Sample3.Queue1";
        private const bool IsDurable = true;
        //The two below settings are just to illustrate how they can be used but we are not using them in
        //this sample as we will use the defaults
        private const string VirtualHost = "";
        private int Port = 0;

        public delegate void OnReceiveMessage(string message);

        public bool Enabled { get; set; }
   
        /// <summary>
        /// Ctor with a key to lookup the configuration
        /// </summary>
        public RabbitConsumer()
        {
            var configModel = new ConnectionConfigModel
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password,
                QueueName = QueueName,
                IsDurable = IsDurable,
                VirtualHost = VirtualHost,
                Port = Port,
            };
            // 显示配置
            EstablishConnection.DisplaySettings(configModel);
            // 连接到指定的Queue
            EstablishConnection.CreateConnection(configModel);
        }

        /// <summary>
        /// 开始取消息
        /// </summary>
        public void Start()
        {
            var consumer = new ConsumeDelegate(EstablishConnection.Poll);
            consumer(Enabled);
        }
        private delegate void ConsumeDelegate(bool enable);
    }
}
