using MariaShop.Api;

ILogger? startupLogger = null;

try {
    var builder = WebApplication.CreateBuilder(args);

    // =======================
    // Bootstrap Logger
    // =======================
    startupLogger = LoggerFactory.Create(logging =>
    {
        logging.AddConsole();
    }).CreateLogger("Startup");

    builder.ConfigureServices();

    var app = builder.Build();

    app.ConfigurePipeline();

    app.Run();
}
catch (Exception ex) {
    startupLogger?.LogCritical(ex, "Falha crítica na inicialização da API");
    throw;
}

