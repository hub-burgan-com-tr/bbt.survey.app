using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    [Keyless]
    public class User:IEntity
    {
        public string SicilNo { get; set; }
        public string FirstName { get; set; }
        public string? MeslekAd { get; set; }
        public string? DivisionName { get; set; }
        public string? UnitName { get; set; }
        public string? GorevAd { get; set; }
        public DateTime? RecTime { get; set; }
    }
}
