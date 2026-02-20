using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Data
{
    public partial class CheckMyStarDbContext : ICheckMyStarDbContext
    {
        IQueryable<Address> ICheckMyStarDbContext.Addresses => this.Addresses;
        IQueryable<Civility> ICheckMyStarDbContext.Civilities => this.Civilities;
        IQueryable<Country> ICheckMyStarDbContext.Countries => this.Countries;
        IQueryable<Role> ICheckMyStarDbContext.Roles => this.Roles;
        IQueryable<User> ICheckMyStarDbContext.Users => this.Users;
        IQueryable<Activity> ICheckMyStarDbContext.Activities => this.Activities;
        IQueryable<StarLevel> ICheckMyStarDbContext.StarLevels => this.StarLevels;
        IQueryable<StarLevelCriterion> ICheckMyStarDbContext.StarLevelCriterias => this.StarLevelCriteria;
        IQueryable<Criterion> ICheckMyStarDbContext.Criterias => this.Criteria;
        IQueryable<CriterionType> ICheckMyStarDbContext.CriterionTypes => this.CriterionTypes;
        IQueryable<Society> ICheckMyStarDbContext.Societies => this.Societies;
        IQueryable<Accommodation> ICheckMyStarDbContext.Accommodations => this.Accommodations;
        IQueryable<AccommodationType> ICheckMyStarDbContext.AccommodationTypes => this.AccommodationTypes;
        IQueryable<Folder> ICheckMyStarDbContext.Folders => this.Folders;
        IQueryable<FolderStatus> ICheckMyStarDbContext.FolderStatuses => this.FolderStatuses;
        IQueryable<Quote> ICheckMyStarDbContext.Quotes => this.Quotes;
        IQueryable<Invoice> ICheckMyStarDbContext.Invoices => this.Invoices;
        IQueryable<Appointment> ICheckMyStarDbContext.Appointments => this.Appointments;
        IQueryable<Assessment> ICheckMyStarDbContext.Assessments => this.Assessments;
        IQueryable<AssessmentCriterion> ICheckMyStarDbContext.AssessmentCriteria => this.AssessmentCriteria;

        public Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            return Task.FromResult(this.Update(entity));
        }

        public Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            this.UpdateRange(entities);

            return Task.CompletedTask;
        }

        Task ICheckMyStarDbContext.AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        {
            return this.AddAsync(entity, cancellationToken).AsTask();
        }

        Task ICheckMyStarDbContext.AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            return this.AddRangeAsync(entities, cancellationToken);
        }

        Task ICheckMyStarDbContext.RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            return Task.FromResult(this.Remove(entity));
        }

        Task ICheckMyStarDbContext.RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            this.RemoveRange(entities);

            return Task.CompletedTask;
        }
    }
}
