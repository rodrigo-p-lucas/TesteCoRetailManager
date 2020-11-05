using System.Collections.Generic;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public interface IInventoryData
    {
        IList<InventoryModel> GetInventory();
        void SaveInventoryRecord(InventoryModel item);
    }
}