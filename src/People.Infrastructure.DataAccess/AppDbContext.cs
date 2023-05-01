using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using People.Domain.Reviews.Entities;
using People.Domain.Users.Entities;
using People.Infrastructure.Abstractions.Interfaces;
using People.Infrastructure.DataAccess.Extensions;

namespace People.Infrastructure.DataAccess;

/// <summary>
/// Application unit of work.
/// </summary>
public class AppDbContext : IdentityDbContext<User, AppIdentityRole, int>, IAppDbContext
{
    #region Users

    /// <inheritdoc />
    public DbSet<UserAttribute> Attributes { get; protected set; }

    /// <inheritdoc />
    public DbSet<Branch> Branches { get; protected set; }

    /// <inheritdoc />
    public DbSet<Position> Positions { get; protected set; }

    /// <inheritdoc />
    public DbSet<AttributeValue> AttributeValues { get; protected set; }

    /// <inheritdoc />
    public DbSet<UserPositionHierarchy> UserPositionHierarchies { get; protected set; }

    /// <inheritdoc />
    public DbSet<Currency> Currencies { get; protected set; }

    /// <inheritdoc />
    public DbSet<Compensation> Compensations { get; protected set; }

    /// <inheritdoc />
    public DbSet<Notification> Notifications { get; protected set; }

    #endregion

    #region Reviews

    /// <inheritdoc />
    public DbSet<Question> Questions { get; protected set; }

    /// <inheritdoc />
    public DbSet<ReviewReminder> ReviewReminders { get; protected set; }

    /// <inheritdoc />
    public DbSet<ReviewType> ReviewTypes { get; protected set; }

    /// <inheritdoc />
    public DbSet<ReviewTemplate> ReviewTemplates { get; protected set; }

    /// <inheritdoc />
    public DbSet<PerformanceReview> PerformanceReviews { get; protected set; }

    #endregion

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="Microsoft.EntityFrameworkCore.DbContext" />.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        RestrictCascadeDelete(modelBuilder);
        ForceHavingAllStringsAsVarchars(modelBuilder);

        modelBuilder.Entity<Branch>()
            .HasOne(b => b.Director)
            .WithMany()
            .HasForeignKey(b => b.DirectorId);

        modelBuilder.Entity<UserAttribute>()
            .HasMany(a => a.AllowViewRoles);

        modelBuilder.Entity<UserAttribute>()
            .HasMany(a => a.AllowEditRoles)
            .WithMany(r => r.EditableAttributes)
            .UsingEntity(j => j.ToTable("EditableRoleAttributes"));

        modelBuilder.Entity<UserAttribute>()
            .HasMany(a => a.AllowViewRoles)
            .WithMany(r => r.ViewableAttributes)
            .UsingEntity(j => j.ToTable("ViewableRoleAttributes"));

        modelBuilder.Entity<UserAttribute>()
            .HasMany(opt => opt.AttributeOptions)
            .WithOne(attr => attr.Attribute)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserAttribute>()
            .HasQueryFilter(attr => attr.DeletedAt == null);

        modelBuilder.Entity<AttributeOption>()
            .HasQueryFilter(opt => opt.Attribute.DeletedAt == null);

        modelBuilder.Entity<AttributeValue>()
            .HasQueryFilter(value => value.Attribute.DeletedAt == null);

        modelBuilder.Entity<Position>()
            .HasOne(p => p.ParentPosition)
            .WithMany(p => p.ChildPositions)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Compensation>()
            .HasOne(p => p.CreatedBy)
            .WithMany()
            .HasForeignKey(p => p.CreatedById);

        modelBuilder.Entity<Compensation>()
            .HasOne(p => p.User)
            .WithMany(u => u.Compensations)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Compensation>()
            .HasOne(p => p.Currency)
            .WithMany()
            .HasForeignKey(p => p.CurrencyId);

        modelBuilder.Entity<Question>()
            .Property(q => q.Options)
            .HasJsonConversion();

        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Feedback)
            .WithMany(r => r.Answers)
            .HasForeignKey(a => a.FeedbackId);

        modelBuilder.Entity<ReviewTemplate>()
            .HasMany(t => t.ReviewedUserQuestions)
            .WithOne(q => q.UserReviewTemplate);

        modelBuilder.Entity<ReviewTemplate>()
            .HasMany(t => t.FeedbackQuestions)
            .WithOne(q => q.FeedbackTemplate);

        modelBuilder.Entity<ReviewTemplate>()
            .HasOne(t => t.RelatedPosition)
            .WithMany()
            .HasForeignKey(t => t.RelatedPositionId);

        modelBuilder.Entity<ReviewTemplate>()
            .HasOne(t => t.ReviewType)
            .WithMany()
            .HasForeignKey(t => t.ReviewTypeId);

        modelBuilder.Entity<ReviewTemplate>()
            .HasQueryFilter(template => template.DeletedAt == null);

        modelBuilder.Entity<PerformanceReview>()
            .HasOne(r => r.ReviewedUser)
            .WithMany()
            .HasForeignKey(r => r.ReviewedUserId);

        modelBuilder.Entity<PerformanceReview>()
            .HasMany(r => r.ReviewedUserQuestions)
            .WithOne(q => q.UserReview);

        modelBuilder.Entity<PerformanceReview>()
            .HasMany(r => r.FeedbackQuestions)
            .WithOne(q => q.FeedbackReview);

        modelBuilder.Entity<PerformanceReview>()
            .HasMany(r => r.FeedbackUsers);

        modelBuilder.Entity<PerformanceReview>()
            .HasOne(r => r.CreatedBy)
            .WithMany()
            .HasForeignKey(r => r.CreatedById);

        modelBuilder.Entity<PerformanceReview>()
            .HasMany(r => r.Feedback)
            .WithOne();

        modelBuilder.Entity<PerformanceReview>()
            .HasOne(r => r.ReviewedUserReply)
            .WithOne()
            .HasForeignKey<PerformanceReview>(r => r.ReviewedUserReplyId);

        modelBuilder.Entity<PerformanceReview>()
            .HasOne(r => r.FinalReply)
            .WithOne()
            .HasForeignKey<PerformanceReview>(r => r.FinalReplyId);
    }

    private static void RestrictCascadeDelete(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    private static void ForceHavingAllStringsAsVarchars(ModelBuilder modelBuilder)
    {
        var stringColumns = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .Where(p => p.ClrType == typeof(string));
        foreach (IMutableProperty mutableProperty in stringColumns)
        {
            mutableProperty.SetIsUnicode(false);
        }
    }
}
