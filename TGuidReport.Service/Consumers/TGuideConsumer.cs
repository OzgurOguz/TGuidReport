using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using TGuidReportModels;

namespace TGuidReport.Service.Consumers
{
    public class TGuideConsumer
    {
        public TGuideConsumerModel Consumer()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://nohtjnhs:ov2OEjCyIJuw09Oum-bmsed6Fx_DPGFm@jaguar.rmq.cloudamqp.com/nohtjnhs");
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("TGuide-Info", true, false, false, null);

                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume("TGuide-Info", true, consumer);


                    TGuideConsumerModel tTGuideConsumerModel = new TGuideConsumerModel();
                    consumer.Received += (model, ea) =>
                    {
                        tTGuideConsumerModel = (TGuideConsumerModel)JsonConvert.DeserializeObject<TGuideConsumerModel>(Encoding.UTF8.GetString(ea.Body.ToArray()));
                    };

                    return tTGuideConsumerModel;
                }
            }
        }
    }
}
