using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRISSystemBackend.Entities
{
    [Table("position")]
    public class Position
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }

        
    }
}
