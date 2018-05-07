
using System.ComponentModel.DataAnnotations;

namespace SV.Entity
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}