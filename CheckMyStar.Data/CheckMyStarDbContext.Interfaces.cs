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
