
using System.ComponentModel.DataAnnotations;

namespace SV.Entity.Query
{
    public class BaseEntityQ
    {
        [Key]
        public long Id { get; set; }
    }
}