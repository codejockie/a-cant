using System.ComponentModel.DataAnnotations.Schema;

namespace Hahn.ApplicatonProcess.May2020.Data.Models.Base
{
    public class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
    }
}
