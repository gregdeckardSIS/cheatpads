﻿using System;
using System.Linq;
using Microsoft.Data.Entity;

namespace CheatPads.Framework.Entity
{
    using CheatPads.Framework.Extensions;

    public class GenericStore<TEntity> : IEntityStore<TEntity> where TEntity : class
    {
        public DbContext DbContext;
        public DbSet<TEntity> DbSet;
       
        /// <summary>
        /// Base constructor for initializing the DataStore class
        /// </summary>
        /// <param name="context">The entity's parent db context</param>
        /// <param name="dbSetName">The entity's dataset name within it's db context</param>
        public GenericStore(DbContext context)
        {
            this.DbContext = context;
            this.DbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Checks for an entity's existance based on it's primary key or keys
        /// </summary>
        /// <param name="keys">Primary key arguments in column order</param>
        /// <returns>True or False</returns>
        public virtual bool Exists(params object[] keys)
        {
            return DbSet.Find(keys) != null;
        }

        /// <summary>
        /// Retrieves an instance of a single entity by it's primary key or keys
        /// </summary>
        /// <param name="keys">Primary key arguments in column order</param>
        /// <returns>TEntity</returns>
        public virtual TEntity Get(params object[] keys)
        {
            return DbSet.Find<TEntity>(keys);
        }
    
        /// <summary>
        /// Reads all entities without filtering
        /// </summary>
        /// <returns>An IQueryable collection of TEntity</returns>
        public virtual IQueryable<TEntity> List()
        {
            return DbSet.AsNoTracking();
        }

        /// <summary>
        /// Commits an entity's changes to the database for new or updated entities
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The updated entity</returns>
        public virtual bool Update(TEntity entity, params object[] key)
        {
            var dbEntity = DbSet.Find(key);
            if (dbEntity != null)
            {
                dbEntity.SetValues(entity);
            }
            else {
                Create(entity);
            }

            return true;
        }

        public virtual TEntity Create(TEntity entity)
        {        
            if (typeof(TEntity).Implements<IAuditRecord>())
            {
                (entity as IAuditRecord).Created = DateTime.Now.ToUniversalTime();
                (entity as IAuditRecord).Updated = DateTime.Now.ToUniversalTime();
            }
            DbSet.Add(entity);
            
            return entity;
        }

       
        /// <summary>
        /// Deletes an entity instance based on the entity's keys
        /// </summary>
        /// <param name="keys">The entity's primary key components in column order</param>
        /// <returns>The deleted entity</returns>
        public virtual bool Delete(params object[] key)
        {
            var entity = DbSet.Find(key);

            if (entity != null)
            {
                DbSet.Remove(entity);
                return true;
            }

            return false;
        }

    }
}