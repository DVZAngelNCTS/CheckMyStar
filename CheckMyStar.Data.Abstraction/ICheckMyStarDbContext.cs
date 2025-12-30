namespace CheckMyStar.Data.Abstractions
{
    public partial interface ICheckMyStarDbContext
    {
        IQueryable<Address> Addresses { get; }
        IQueryable<Civility> Civilities { get; }
        IQueryable<Country> Countries { get; }
        IQueryable<Role> Roles { get; }
        IQueryable<User> Users { get; }
    }
}
