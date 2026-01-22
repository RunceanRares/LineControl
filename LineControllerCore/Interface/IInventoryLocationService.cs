using LineControllerCore.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineControllerCore.Interface
{
  public interface IInventoryLocationService
  {
    Task<IEnumerable<StoragePlaceSelectViewModel>> GetInventoryLocationAsync();

    Task<IEnumerable<InventoryLocationViewModel>> GetAuthorizedInventoryLocationsAsync(int userId, string text, int? includeId, bool forResponsible, string culture);

    Task<IEnumerable<StoragePlaceSelectViewModel>> GetAuthorizedStoragePlacesAsync(int userId, int inventoryLocationId, string text, int? includeLocationId, int? includeStorageId, bool forResponsible);
  }
}
