using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinanceiro.Domain.Entities
{
    public class ValoresSaida
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(120)]
        public string Descricao { get; set; } = default!;
        [Required]
        public decimal Valor { get; set; } = default!;
        [Required]
        [StringLength(10)]  
        public string Tipo { get; set; } = default!;
        [Required]
        [StringLength(20)]
        public string Categoria { get; set; } = default!;
        
        [Required]
        [ForeignKey("User ")]
        public int IdUser  { get; set; } = default!;
        public User User { get; set; } = default!;

    }
}