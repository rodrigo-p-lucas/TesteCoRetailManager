using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDesktopUI.Library.Models
{
    public class SaleModel
    {
        public IList<SaleDetailModel> SaleDetails { get; set; } = new List<SaleDetailModel>();
    }
}
