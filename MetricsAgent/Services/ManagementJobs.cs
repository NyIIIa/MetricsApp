using Hangfire;
using Hangfire.Storage;
using MetricsAgent.Services.Abstractions;

namespace MetricsAgent.Services;

public static class ManagementJobs
{
    /// <summary>
    /// The method starts metrics' jobs.
    /// </summary>
    /// <param name="serviceProvider">current services' app</param>
    /// <param name="cronExpression">The string expresses regular action, that performs a describing regulation</param>
    public static void StartMetricsJobs(this IServiceProvider serviceProvider, string cronExpression)
    {
        //Delete recurring jobs, if they are exist
        using (var connection = serviceProvider.GetService<JobStorage>().GetConnection())
        {
            var recurringJobManager = serviceProvider.GetService<IRecurringJobManager>();
    
            foreach (var recurringJob in connection.GetRecurringJobs())
            {
                recurringJobManager.RemoveIfExists(recurringJob.Id);
            }
        }
        
        //Create new jobs
        using (var scope = serviceProvider.CreateScope()) 
        {
            var cpuPerformanceFactory = scope.ServiceProvider.GetRequiredService<ICpuPerformanceFactory>();
            var cpuPerformanceService = cpuPerformanceFactory.CreateService();
                        
            var gpuPerformanceFactory = scope.ServiceProvider.GetRequiredService<IGpuPerformanceFactory>();
            var gpuPerformanceService = gpuPerformanceFactory.CreateService();

            var ramPerformanceFactory = scope.ServiceProvider.GetRequiredService<IRamPerformanceFactory>();
            var ramPerformanceService = ramPerformanceFactory.CreateService();
           
            
            
            var recurringJobManager =  serviceProvider.GetRequiredService<IRecurringJobManager>();

            recurringJobManager.AddOrUpdate("cpuMetricsJob",() => cpuPerformanceService.StartParseAsync(), cronExpression); 
            recurringJobManager.AddOrUpdate("gpuMetricsJob", () => gpuPerformanceService.StartParseAsync(), cronExpression);
            recurringJobManager.AddOrUpdate("ramMetricsJob", () => ramPerformanceService.StartParseAsync(), cronExpression);
        }
    }
}