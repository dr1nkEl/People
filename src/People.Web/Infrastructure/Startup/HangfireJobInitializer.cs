using Extensions.Hosting.AsyncInitialization;
using Hangfire;

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
    }
}
