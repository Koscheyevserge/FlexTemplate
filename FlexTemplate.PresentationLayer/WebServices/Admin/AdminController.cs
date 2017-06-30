using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Admin
{
    public class AdminController : FlexController
    {
        public AdminController(ControllerServices services) : base(services)
        {

        }
    }
}