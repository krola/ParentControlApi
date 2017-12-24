using AutoMapper;

public abstract class BaseService<T, C> where T :class{
    protected readonly IUserProvider userProvider;
    protected readonly IRepository<T> entityRepository;
    protected readonly IMapper mapper;

    public BaseService(IUserProvider userProvider, IRepository<T> entityRepository, IMapper mapper)
    {
        this.userProvider = userProvider;
        this.entityRepository = entityRepository;
        this.mapper = mapper;
    }

    public abstract System.Collections.Generic.IEnumerable<C> GetAll();
    public abstract C Get(string Id);

    public abstract void Create(C entity);

    protected abstract T GetEntity(string Id);

    public void Update(string oldId, C newEntity)
    {
        var oldEntity = GetEntity(oldId);
        var updatedEntity = Mapper.Map<C, T>(newEntity, oldEntity);
        entityRepository.Edit(updatedEntity);
    }

    public void Remove(string id)
    {
        var entity = GetEntity(id);
        entityRepository.Delete(entity);
    }
}