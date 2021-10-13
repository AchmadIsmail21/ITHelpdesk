using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> dbSet;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<Entity>();
        }
        public int Delete(Key key)
        {
            var entity = dbSet.Find(key);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            dbSet.Remove(entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<Entity> Get()
        {
            return dbSet.ToList();
        }

        public Entity Get(Key key)
        {
            return dbSet.Find(key);
        }

        public int Insert(Entity entity)
        {
            dbSet.Add(entity);
            var insert = myContext.SaveChanges();
            return insert;
        }

        public int Update(Entity entity)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }

    }
}
