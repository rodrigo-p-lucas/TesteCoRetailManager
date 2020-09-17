using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRMDataManager.Library.Models
{
    public class SaleModel
    {
        public IList<SaleDetailModel> SaleDetails { get; set; }
    }
}