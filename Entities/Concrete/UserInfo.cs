using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserInfo:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? Department { get; set; }
        public string? Section { get; set; }
        public string? Unit { get; set; }

        [ForeignKey("UserTest")]
        public int UserTestRefId { get; set; }
        public UserTest UserTest { get; set; }
    }
}
