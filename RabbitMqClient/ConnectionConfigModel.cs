using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqClient
{
    public class ConnectionConfigModel
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
        public bool IsDurable { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
        public bool IsConsumer { get; set; }
    }
}
