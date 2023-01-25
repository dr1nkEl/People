using Microsoft.EntityFrameworkCore;
using People.Domain.Reviews.Entities;
using People.Domain.Users.Entities;

namespace People.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Application abstraction for unit of work.
/// </summary>
public interface IAppDbContext : IDbContextWithSets, IDisposable
{
    #region Users

    /// <summary>
    /// Roles.
    /// </summary>
    DbSet<AppIdentityRole> Roles { get; }

    /// <summary>
    /// Users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Attributes that can contain additional information for users.
    /// </summary>
    DbSet<UserAttribute> Attributes { get; }

    /// <summary>
    /// List of branches / offices.
    /// </summary>
    DbSet<Branch> Branches { get; }

    /// <summary>
    /// Existing positions in the company.
    /// </summary>
    DbSet<Position> Positions { get; }

    /// <summary>
    /// Selected values for attributes.
    /// </summary>
    DbSet<AttributeValue> AttributeValues { get; }

    /// <summary>
    /// Contains details of user hierarchy - who reports to who.
    /// </summary>
    DbSet<UserPositionHierarchy> UserPositionHierarchies { get; }

    /// <summary>
    /// Available currencies in the system.
    /// </summary>
    DbSet<Currency> Currencies { get; }

    /// <summary>
    /// Existing compensations.
    /// </summary>
    DbSet<Compensation> Compensations { get; }

    /// <summary>
    /// Created notifications for users.
    /// </summary>
    DbSet<Notification> Notifications { get; }

    #endregion

    #region Reviews

    /// <summary>
    /// All existing questions.
    /// </summary>
    DbSet<Question> Questions { get; }

    /// <summary>
    /// Reminders about a review.
    /// </summary>
    DbSet<ReviewReminder> ReviewReminders { get; }

    /// <summary>
    /// All existing types of reviews.
    /// </summary>
    DbSet<ReviewType> ReviewTypes { get; }

    /// <summary>
    /// Templates that can be used to create reviews.
    /// </summary>
    DbSet<ReviewTemplate> ReviewTemplates { get; }

    /// <summary>
    /// All existing performance reviews.
    /// </summary>
    DbSet<PerformanceReview> PerformanceReviews { get; }

    #endregion
}
