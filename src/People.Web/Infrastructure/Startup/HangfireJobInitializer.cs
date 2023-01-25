using Extensions.Hosting.AsyncInitialization;
using Hangfire;
using People.Web.BackgroundJobs;

namespace People.Web.Infrastructure.Startup;

/// <summary>
/// Hangfire job initializer.
/// </summary>
internal class HangfireJobnitializer : IAsyncInitializer
{
    /// <inheritdoc/>.
    public Task InitializeAsync()
    {
        AddDefaultRecurringJobs();

        return Task.CompletedTask;
    }

    private static void AddDefaultRecurringJobs()
    {
        RecurringJob.AddOrUpdate<SyncCrmDataJob>("SyncCrmDataJob", x => x.SyncData(default), Cron.Daily);
        RecurringJob.Trigger("SyncCrmDataJob");
    }
}
