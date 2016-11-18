namespace Prod.DAL.UnitOfWork
{
    using Prod.DAL.GenericRepository;

    public interface IUnitOfWork
    {
        GenericRepository<Product> ProductRepository { get; }
        GenericRepository<User> UserRepository { get; }
        GenericRepository<Token> TokenRepository { get; } 
        
        void Save(); 
    }
}