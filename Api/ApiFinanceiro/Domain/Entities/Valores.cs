using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ApiFinanceiro.Domain.Enuns;

namespace ApiFinanceiro.Domain.Entities
{
    public class Valores
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

    }
}