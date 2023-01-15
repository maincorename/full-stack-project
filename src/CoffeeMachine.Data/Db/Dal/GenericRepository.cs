namespace CoffeeMachine.Data.Db.Dal;

using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Обобщённый репозиторий контекста данных.
/// </summary>
/// <typeparam name="TEntity"> Тип репозитория. </typeparam>
public class GenericRepository<TEntity> where TEntity : class
{
    /// <inheritdoc cref="CoffeeMachineContext" />
    private readonly CoffeeMachineContext _context;

    /// <summary>
    /// Набор сущностей репозитория.
    /// </summary>
    private readonly DbSet<TEntity> _dbSet;

    /// <inheritdoc cref="GenericRepository{TEntity}" />
    /// <param name="context"> Контекст данных репозитория. </param>
    public GenericRepository(CoffeeMachineContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    /// <summary>
    /// Удалить объект из бд.
    /// </summary>
    /// <param name="entityToDelete"> Объект для удаления. </param>
    /// <returns> true - объект удалён, иначе false. </returns>
    public virtual bool Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
            _dbSet.Attach(entityToDelete);

        if (_dbSet.Contains(entityToDelete))
        {
            _dbSet.Remove(entityToDelete);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Получить все элементы.
    /// </summary>
    /// <returns> Перечисление элементов. </returns>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    /// <summary>
    /// Получить элемент по id.
    /// </summary>
    /// <param name="id"> Идентификатор объекта. </param>
    /// <returns> Найденный объект, либо null. </returns>
    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Добавить объект в бд.
    /// </summary>
    /// <param name="entityToInsert"> Объект для добавления. </param>
    /// <exception cref="ArgumentNullException"> </exception>
    public virtual async Task<TEntity> InsertAsync(TEntity entityToInsert)
    {
        if (entityToInsert is null)
            throw new ArgumentNullException("Объект не может быть null.");

        await _dbSet.AddAsync(entityToInsert);

        return entityToInsert;
    }

    /// <summary>
    /// Обновить объект в базе данных.
    /// </summary>
    /// <param name="entityToUpdate"> Объект для обновления. </param>
    /// <returns> Объект, помеченный для обновления. </returns>
    /// <exception cref="ArgumentNullException"> </exception>
    public virtual TEntity Update(TEntity entityToUpdate)
    {
        if (entityToUpdate is null)
            throw new ArgumentNullException("Объект не может быть null.");

        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;

        return entityToUpdate;
    }

    /// <summary>
    /// Получить элементы по фильтру.
    /// </summary>
    /// <param name="filter"> Фильтр-выражение. </param>
    /// <returns> Отфильтрованная коллекция. </returns>
    public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter)
    {
        return _dbSet.Where(filter);
    }
}