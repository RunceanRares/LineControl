using AutoMapper;
using AutoMapper.QueryableExtensions;

using LineControllerCore.Interface;
using LineControllerCore.Model;

using LineControllerInfrastructure;
using LineControllerInfrastructure.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Service
{
  public class InventoryLocationService : BaseService<InventoryLocation>, IInventoryLocationService
  {
    public InventoryLocationService(LineContextDb context, IMapper mapper, ILogger<DeviceService> logger)
           : base(context, mapper, logger)
    {
    }

    public async Task<IEnumerable<InventoryLocationViewModel>> GetInventoryLocationAsync()
    {
      var result = await Context.InventoryLocations.OrderBy(s => s.Id)
                                .ProjectTo<InventoryLocationViewModel>(Mapper.ConfigurationProvider)
                                .ToListAsync().ConfigureAwait(false);
      return result;
    }

    public Task<IEnumerable<InventoryLocationViewModel>> GetAuthorizedInventoryLocationsAsync(int userId, string text, int? includeId, bool forResponsible, string culture)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<StoragePlaceSelectViewModel>> GetAuthorizedStoragePlacesAsync(int userId, int inventoryLocationId, string text, int? includeLocationId, int? includeStorageId, bool forResponsible)
    {
      throw new NotImplementedException();
    }
  }
}
