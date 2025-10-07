using AutoMapper;
using AutoMapper.QueryableExtensions;
using LineControl.Models;
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
      var dateTime = DateTime.Now;
      //ActivityType entity = MapUpdateViewModel(model);
      try
      {
        var activity = new ActivityType
        {
          Id = model.Id,
          Code = model.Code,
          CostCenter = model.CostCenter,
          LastChangedDate = dateTime,
          Name = model.Name,
          Rate = (decimal)model.Rate,
          PassiveCostFactor = model.PassiveCostFactor,
        };

        Context.ActivityTypes.Update(activity);
        Context.SaveChanges();

        return model;
      }
      catch (Exception ex)
      {
        Logger.LogError(ex, "Eroare la actualizarea modelului {ModelId}", model.Id);
        return null;
      }
    }

    public async Task<ActivityTypeViewModel> AddActivityType(ActivityTypeViewModel model)
    {
      var existingActivity = await CheckValidActivity(model).ConfigureAwait(false);
      if (existingActivity.Id != 0)
      {
        Logger.LogWarning("Unable to add the activity type. Id already exists.");
        return null; 
      }
      else
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
    }

    protected ActivityType MapUpdateViewModel(ActivityTypeViewModel model)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      ActivityType entity = Context.ActivityTypes.FirstOrDefault(s => s.Id == model.Id);
      var date = DateTime.Now;
      entity.LastChangedDate = date;
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
      if (model.Id == 0)
      {
        return model;
      }

      var result = Context.ActivityTypes.FirstOrDefault(s => s.Id == model.Id);
      if (result != null)
      {
        Logger.LogInformation("Unable to add the activity type. Id already exists.");
        return Mapper.Map<ActivityTypeViewModel>(result);
      }
      else
      {
        return model;
      } 
    }
  }
}
