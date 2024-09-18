using AutoMapper;
using AutoMapper.QueryableExtensions;
using LineControllerCore.Interface;
using LineControllerCore.Model;
using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;

using Microsoft.EntityFrameworkCore;
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

    public ActivityTypeViewModel Update(ActivityTypeViewModel model)
    {
      ActivityType entity = MapUpdateViewModel(model);
      try
      {
        var dateTime = DateTime.Now;
        var userId = IdentityService.UserId;
        entity.LastChangedDate = dateTime;
        entity.LastChangedUserId = userId;

        Entities.Update(entity);
        Context.SaveChanges();

        return Mapper.Map<ActivityTypeViewModel>(entity);
      }
      catch (Exception ex)
      {
        Logger.LogError(ex, "Eroare la actualizarea modelului {ModelId}", model.Id);
        return null;
      }
    }

    public ActivityTypeViewModel AddActivityType(ActivityTypeViewModel model)
    {
      var result = CheckValidActivity(model);
      if (result == null) 
      {
        var activityModel = new ActivityType
        {
          Id = model.Id,
          Name = model.Name,
          Code = model.Code,
          CostCenter = model.CostCenter,
          Rate = (decimal)model.Rate,
          PassiveCostFactor = model.PassiveCostFactor,
        };

        Context.ActivityTypes.Add(activityModel);
        Context.SaveChanges();

        return model;
      }
      else
      {
        Logger.LogWarning("Unable to add the activity type. Id already exists.");
        return result.Result;
      }
    }

    protected ActivityType MapUpdateViewModel(ActivityTypeViewModel model)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      ActivityType entity = Context.ActivityTypes.FirstOrDefault(s => s.Id == model.Id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
      if (entity is null)
      {
        return null;
      }

      if (entity.Rate != model.Rate)
      {
        Logger.LogWarning("For the activity type {Id} {Code} {CostCenter} the rate is changing from {OldRate} to {NewRate}", model.Id, model.Code, model.CostCenter, entity.Rate, model.Rate);
      }

      Entities.Attach(entity);
      Mapper.Map(model, entity);
      return entity;
    }

    private async Task<ActivityTypeViewModel> CheckValidActivity(ActivityTypeViewModel model)
    {
      var result = Context.ActivityTypes.FirstOrDefault(s => s.Id == model.Id);
      if (model.Id != 0)
      {
        Logger.LogInformation("Unable to add the activity type. Id already exists.");
        return Mapper.Map<ActivityTypeViewModel>(result);
      }
      else
      {
        return await CheckName(model).ConfigureAwait(false);
      } 
    }

    private async Task<ActivityTypeViewModel> CheckName(ActivityTypeViewModel model)
    {
      if (await Context.ActivityTypes.AnyAsync(s => s.Name == model.Name && s.Id != model.Id))
      {
        Logger.LogInformation("The activity type '{0}' already exists.", model.Name);
      }

      return model;
    }
  }
}
