namespace PostsService.Features.BackgroundServices;

//TODO: Migrate to Azure Functions
public class UpdateFrequentPosts : BackgroundService
{
    private readonly ILogger<UpdateFrequentPosts> _logger;
    private readonly IServiceProvider _serviceProvider;


    private const int MILLISECONDS_UNTIL_APP_IS_READY = 1000;

    public UpdateFrequentPosts(ILogger<UpdateFrequentPosts> logger,
                               IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //Wait for App to Startup
        await Task.Delay(MILLISECONDS_UNTIL_APP_IS_READY);

        // calculate seconds till midnight for first run
        DateTime tomorrow = DateTime.Today.AddDays(1);
        DateTime today = DateTime.Now;
        var secondsTillMidnight = (tomorrow - today).TotalSeconds;

        using PeriodicTimer timer = new(TimeSpan.FromSeconds(secondsTillMidnight));

        try
        {
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                timer.Period = TimeSpan.FromDays(1);
                _logger.LogInformation($"==> Timer is running at {DateTime.Now}");
                _logger.LogInformation($"==> Next run will after {timer.Period.Days} Days, {timer.Period.Hours}:{timer.Period.Minutes}:{timer.Period.Seconds}");
                await RunJob(stoppingToken);
            }
        }
        catch (Exception e)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
            _logger.LogError(e.ToString());
        }

    }

    private async Task RunJob(CancellationToken cancellationToken)
    {
        using var serviceScope = _serviceProvider.CreateScope();
    }



}
