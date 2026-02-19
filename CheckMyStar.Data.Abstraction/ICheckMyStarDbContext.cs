namespace CheckMyStar.Data.Abstractions
{
    public partial interface ICheckMyStarDbContext
    {
        IQueryable<Address> Addresses { get; }
        IQueryable<Civility> Civilities { get; }
        IQueryable<Country> Countries { get; }
        IQueryable<Role> Roles { get; }
        IQueryable<User> Users { get; }
        IQueryable<Activity> Activities { get; }
        IQueryable<StarLevel> StarLevels { get; }
        IQueryable<StarLevelCriterion> StarLevelCriterias { get; }
        IQueryable<Criterion> Criterias { get; }
        IQueryable<CriterionType> CriterionTypes { get; }
        IQueryable<Society> Societies { get; }
        IQueryable<Accommodation> Accommodations { get; }
        IQueryable<AccommodationType> AccommodationTypes { get; }
        IQueryable<Folder> Folders { get; }
        IQueryable<FolderStatus> FolderStatuses { get; }
        IQueryable<Quote> Quotes { get; }
        IQueryable<Invoice> Invoices { get; }
        IQueryable<Appointment> Appointments { get; }
        IQueryable<Assessment> Assessments { get; }
        IQueryable<AssessmentCriterion> AssessmentCriteria { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;

        Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;

        Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;
    }
}
