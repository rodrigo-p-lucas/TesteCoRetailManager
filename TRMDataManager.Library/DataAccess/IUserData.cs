using System.Collections.Generic;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public interface IUserData
    {
        IList<UserModel> GetUsersById(string id);
    }
}