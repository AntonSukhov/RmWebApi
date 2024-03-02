using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RM.DAL.DbContexts;

/// <summary>
/// Базовый контекст работы с базой данных.
/// </summary>
/// <param name="options">Опции контекста работы с базой данных.</param>
public abstract class DbContextBase(DbContextOptions options) : DbContext(options)
{
    #region Методы

    /// <summary>
    /// Добавляет сущность в базу данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип данных добавляемой сущности.</typeparam>
    /// <param name="entity">Добавляемая сущность.</param>
    /// <returns/>
    public virtual async Task AddEntityAsync<TEntity>(TEntity entity) where TEntity: class
    {
        ArgumentNullException.ThrowIfNull(entity);

        await AddAsync(entity);
        
        try
        {
           await SaveChangesAsync();
        } 
        finally
        {
           Entry(entity).State = EntityState.Detached;
        }
    }

    /// <summary>
    /// Обновляет сущность в базе данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип данных обновляемой сущности.</typeparam>
    /// <param name="entity">Обновляемая сущность.</param>
    /// <returns/>
    public virtual async Task UpdateEntityAsync<TEntity>(TEntity entity) where TEntity: class
    {
        ArgumentNullException.ThrowIfNull(entity);

        Update(entity);

        try
        {
           await SaveChangesAsync();
        } 
        finally
        {
           Entry(entity).State = EntityState.Detached;
        } 
    }

    /// <summary>
    /// Удаляет сущность из базы данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип данных удаляемой сущности.</typeparam>
    /// <param name="entity">Удаляемая сущность.</param>
    /// <returns/>
    public virtual async Task RemoveEntityAsync<TEntity>(TEntity entity) where TEntity: class
    {
        ArgumentNullException.ThrowIfNull(entity);

        Remove(entity);

        try
        {
           await SaveChangesAsync();
        } 
        finally
        {
           Entry(entity).State = EntityState.Detached;
        } 
    }

    #endregion
}
