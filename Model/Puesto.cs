using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Puesto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚüÜ ]+$", ErrorMessage = "El nombre contiene carácteres inválidos.")]
        public string Nombre { get; set; }
    }
}