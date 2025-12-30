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
    }
}
