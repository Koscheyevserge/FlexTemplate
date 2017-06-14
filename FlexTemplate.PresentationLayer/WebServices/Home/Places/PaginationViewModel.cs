using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Home.Places
{
    public class PaginationViewModel
    {
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int TotalPagesCount { get; set; }
    }
}
