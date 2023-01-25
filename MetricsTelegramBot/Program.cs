using System.Net.Http.Headers;
using MetricsTelegramBot;
using MetricsTelegramBot.Controllers;
using MetricsTelegramBot.Models;
using MetricsTelegramBot.Services;
using MetricsTelegramBot.Services.Abstractions;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Setup Bot configuration
var botConfigurationSection = builder.Configuration.GetSection(BotConfiguration.Configuration);
builder.Services.Configure<BotConfiguration>(botConfigurationSection);

var botConfiguration = botConfigurationSection.Get<BotConfiguration>();

// Register named HttpClient to get benefits of IHttpClientFactory
// and consume it with ITelegramBotClient typed client.
// More read:
//  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#typed-clients
//  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
builder.Services.AddHttpClient("telegram_bot_client")
    .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
    {
        BotConfiguration? botConfig = sp.GetConfiguration<BotConfiguration>();
        TelegramBotClientOptions options = new(botConfig.BotToken);
        return new TelegramBotClient(options, httpClient);
    });

//Add named HttpClient to interact with remote microservice
builder.Services.AddHttpClient("Metrics", httpClient =>
{
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}).AddPolicyHandler(PollyRegistryExtension.GetRetryPolicy());

//Business-logic services
builder.Services.AddScoped<MetricsBotHandler>();
builder.Services.AddScoped<ITelegramBotCommand, MetricsBotApi>();
builder.Services.AddScoped<ValidatorService>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<ResponseWriter>();
builder.Services.AddScoped<RequestBuilder>();
builder.Services.AddScoped<ICommonMetricsFactory, CommonMetricsFactory>();
builder.Services.AddScoped<IDateTimeMetricsFactory, DateTimeMetricsFactory>();

// There are several strategies for completing asynchronous tasks during startup.
// Some of them could be found in this article https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-part-1/
// We are going to use IHostedService to add and later remove Webhook
builder.Services.AddHostedService<ConfigureWebhook>();

// The Telegram.Bot library heavily depends on Newtonsoft.Json library to deserialize
// incoming webhook updates and send serialized responses back.
// Read more about adding Newtonsoft.Json to ASP.NET Core pipeline:
//   https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-6.0#add-newtonsoftjson-based-json-format-support
builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();

await app.Services.SetMyCommandsToMetricsBotAsync();
app.MapBotWebhookRoute<BotController>(route: botConfiguration.Route);
app.MapControllers();
app.Run();