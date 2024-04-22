using AutoMapper;
using AutoMapper.QueryableExtensions;
using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;
using Microsoft.Extensions.Logging;

namespace LineControllerCore.Service
{
  public class ActivityTypeService : BaseService<ActivityType>, IActivityTypeService
  {
    private readonly LineContextDb context;

    public ActivityTypeService(LineContextDb context, IMapper mapper, ILogger<ActivityTypeService> logger)
         : base(context, mapper, logger)
    {
      this.context = context;
    }

    public IQueryable<ActivityTypeViewModel> GetSelectViewModels()
    {
      return context.ActivityTypes.Where(s => s.Id != null).ProjectTo<ActivityTypeViewModel>(Mapper.ConfigurationProvider);
    }

    public ActivityTypeViewModel GetActivityById(int id)
    {
      var query = context.ActivityTypes.FirstOrDefault(s => s.Id == id);
      if (query == null)
      {
        return null;
      }

      var activityViewModel = Mapper.Map<ActivityTypeViewModel>(query);

      return activityViewModel;
    } 
  }
}
