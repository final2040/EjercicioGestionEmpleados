using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Model
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "El nombre no puede contener mas de {0} carácteres.")]
        [MinLength(2, ErrorMessage = "El nombre debe de contener mínimo {0} carácteres.")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚüÜ ]+$", ErrorMessage = "El nombre contiene carácteres inválidos.")]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "El apellido no puede contener masde {0} carácteres.")]
        [MinLength(2, ErrorMessage = "El apellido debe de contener mínimo {0} carácteres.")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚüÜ ]+$", ErrorMessage = "El apellido contiene carácteres inválidos.")]
        public string Apellidos { get; set; }

        [Required]
        [Range(18, 99, ErrorMessage = "La edad del empleado debe de ser entre {1} y {2} años")]
        public int Edad { get; set; }

        [Required]
        public int DepartamentoId { get; set; }

        [ForeignKey("DepartamentoId")]
        public Departamento Departamento { get; set; }

        [Required]
        public int PuestoId { get; set; }

        [ForeignKey("PuestoId")]
        public Puesto Puesto { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaContratacion { get; set; }
    }
}
