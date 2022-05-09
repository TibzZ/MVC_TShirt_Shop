﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TShirt.Models.ViewModels
{
    public class ProductViewModel
    {
       public Product Product { get; set; }
       public IEnumerable<SelectListItem> CategoryList { get; set; }
       public IEnumerable<SelectListItem> DesignTypeList { get; set; }
    }
}
