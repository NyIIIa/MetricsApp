using Hangfire;
using Hangfire.SqlServer;
using MetricsAgent;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto.Response;
using MetricsAgent.Services;
using MetricsAgent.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add Log4Net logger
builder.Logging.AddLog4Net("log4net.config");

// Add services to the container.
builder.Services.AddDbContext<MetricsDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("connStr")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add automapper
builder.Services.AddAutoMapper((configuration) =>
{
    configuration.CreateMap<CpuMetric, CpuMetricDto>();
    configuration.CreateMap<GpuMetric, GpuMetricDto>();
    configuration.CreateMap<RamMetric, RamMetricDto>();
},typeof(Program));

//Add database repositories.
builder.Services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
builder.Services.AddScoped<IGpuMetricsRepository, GpuMetricsRepository>();
builder.Services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();

//Add performance factories
builder.Services.AddScoped<ICpuPerformanceFactory, CpuPerformanceFactory>();
builder.Services.AddScoped<IGpuPerformanceFactory, GpuPerformanceFactory>();
builder.Services.AddScoped<IRamPerformanceFactory, RamPerformanceFactory>();

//Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("connStr"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(1),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(1),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.StartMetricsJobs("0/10 * * * * ?");

app.UseHttpsRedirection();
app.UseHangfireDashboard();
app.UseAuthorization();

app.MapControllers();

app.Run();