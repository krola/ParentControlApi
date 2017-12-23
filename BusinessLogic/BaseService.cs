using AutoMapper;

public abstract class BaseService<T, C> where T :class{
    protected readonly IUserProvider userProvider;
    protected readonly IRepository<T> entityRepository;

    public BaseService(IUserProvider userProvider, IRepository<T> entityRepository)
    {
        this.userProvider = userProvider;
        this.entityRepository = entityRepository;
    }

    public abstract System.Collections.Generic.IEnumerable<T> GetAll();
    public abstract T Get(string Id);

    public abstract void Create(T entity);

    public void Update(string oldId, C newEntity)
    {
        var oldEntity = Get(oldId);
        var updatedEntity = Mapper.Map<C, T>(newEntity, oldEntity);
    }

    public void Remove(string id)
    {
        var entity = Get(id);
        entityRepository.Delete(entity);
    }



}