using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    [DataContract]
    public class Vote:IEntity
    {
        [Key]
        [JsonIgnore]
        [Column(Order = 1)]
        public int Id { get; set; }
        [IgnoreDataMember]
        [NotMapped]
        public string UserId { get; set; }
        [DataMember]
        public string? Department { get; set; }
        [DataMember]
        public string? Section { get; set; }
        [DataMember]
        public string? Unit { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime VoteDate { get; set; }
        [DataMember]
        public int UserVote { get; set; }
    }
}
