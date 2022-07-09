using DataAccsess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EfRepsitory
{
    public class EntityRepository : IEntityRepository
    {
        private DAISUni2Context dbContext;

        public EntityRepository(DAISUni2Context dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
        }

        public void Add<T>(T entity) where T : class
        {
            dbContext.Add(entity);
        }


        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Update<T>(T entity) where T : class
        {

            var set = dbContext.Set<T>();

            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                set.Attach(entity);
            }

            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.Update(entity);
        }
    }
}