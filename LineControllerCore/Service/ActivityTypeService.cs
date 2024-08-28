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
    public ActivityTypeService(LineContextDb context, IMapper mapper, ILogger<ActivityTypeService> logger)
         : base(context, mapper, logger)
    {
    }

    public IQueryable<ActivityTypeViewModel> GetSelectViewModels()
    {
      return Context.ActivityTypes.Where(s => s.Id != null).ProjectTo<ActivityTypeViewModel>(Mapper.ConfigurationProvider);
    }

    public ActivityTypeViewModel GetActivityById(int id)
    {
      var query = Context.ActivityTypes.FirstOrDefault(s => s.Id == id);
      if (query is null)
      {
        return null;
      }

      var activityViewModel = Mapper.Map<ActivityTypeViewModel>(query);

      return activityViewModel;
    } 
  }
}
