using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinanceiro.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;  

        [Required]
        [StringLength(120)]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(20)]
        public string Password {get; set; } = default!;
    }
}