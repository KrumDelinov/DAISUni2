namespace EfRepsitory
{
    public interface IEntityRepository
    {
        void Add<T>(T entity) where T : class;
        Task SaveChangesAsync();
        void Update<T>(T entity) where T : class;
    }
}