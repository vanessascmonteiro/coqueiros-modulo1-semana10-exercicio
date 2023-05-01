using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace  projetoCarros.Models
{
    [Table("Carro")]
    public class CarroModel
    {
        [Key]
        [Column("Id")]
        public int Id {get; set;}

        [MaxLength(100)]
        public string Nome {get; set;}
        public DateTime DataLocacao{get; set;}

        
        [ForeignKey("Marca")]
        public int MarcaId { get; set; }

        public MarcaModel Marca { get; set; }

       

    }
}