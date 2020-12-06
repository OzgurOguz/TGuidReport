using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using TGuidReportModels;
using TGuidReportModels.IRepositories;

namespace TGuidReport.Service.Services
{
    public class TGuideConsumerModelRepository : ITGuideConsumerModelRepository
    {
        public List<TGuideConsumerModel> Consumer()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://nohtjnhs:ov2OEjCyIJuw09Oum-bmsed6Fx_DPGFm@jaguar.rmq.cloudamqp.com/nohtjnhs");
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    List<TGuideConsumerModel> tTGuideConsumerModel = new List<TGuideConsumerModel>();

                    channel.QueueDeclare("TGuide-Info", true, false, false, null);

                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume("TGuide-Info", true, consumer);


                    consumer.Received += (model, ea) =>
                        {
                            tTGuideConsumerModel.RemoveAll(x=>true);
                            tTGuideConsumerModel.AddRange(JsonConvert.DeserializeObject<JArray>(Encoding.UTF8.GetString(ea.Body.ToArray())).ToObject<List<TGuideConsumerModel>>());
                           // throw new Exception("");
                        };

                    return tTGuideConsumerModel;
                }
            }

        }
    }
}
