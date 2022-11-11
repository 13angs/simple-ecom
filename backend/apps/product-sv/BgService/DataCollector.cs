using product_sv.Interfaces;
using SeBackend.Common.Interfaces;

namespace product_sv.BgService
{
  public class DataCollector : IHostedService
  {
    private readonly IServiceProvider serviceProvider;
    private readonly IConfiguration configuration;

    public DataCollector(IServiceProvider serviceProvider, IConfiguration configuration)
    {
      this.serviceProvider = serviceProvider;
      this.configuration = configuration;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
      using(var scope = serviceProvider.CreateScope())
      {
        IMessageSubscriber subscriber = scope.ServiceProvider.GetRequiredService<IMessageSubscriber>();
        subscriber.Connect(
                        configuration["RabbitMQ:HostName"],
                        "report_se_exchange",
                        "product_se_queue",
                        "report.se",
                        null,
                        "fanout"
                    );
        subscriber.Subscribe(ProcessMessage);
        return Task.CompletedTask;
      }
    }

    public bool ProcessMessage(string message, IDictionary<string, object>? headers)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            if(message.Contains("comment"))
            {
                // get the comment scope
                IProductCommentDataCollector commentDataCollector = 
                    scope.ServiceProvider.GetRequiredService<IProductCommentDataCollector>();
                if(message.Contains("create"))
                {
                    return commentDataCollector.Create(message);
                }
            }
        }
        return false;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}