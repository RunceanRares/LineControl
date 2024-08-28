using AutoMapper;
using LineControllerCore.Interface;
using LineControllerInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LineControllerCore.Service
{
  public abstract class BaseService<TEntity>
     where TEntity : class
  {
    protected BaseService(LineContextDb context, IMapper mapper, ILogger logger) 
    {
      Context = context;
      Mapper = mapper;
      Logger = logger;
    }

    protected BaseService(IMapper mapper)
    {
      Mapper = mapper;
    }

    protected internal LineContextDb Context { get; }

    protected DbSet<TEntity> Entities { get; }

    protected IMapper Mapper { get; }

    protected IIdentityService IdentityService { get; }

    protected ILogger Logger { get; }
  }
}
