using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projetoCarros.Models
{
    [Table("Marca")]
    public class MarcaModel
    {
        [Key]
        [Column("Id")]
        public int Id {get; set;}

        [MaxLength(100)]
        public string Nome {get; set;}
        
    }
}