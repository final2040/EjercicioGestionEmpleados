using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚüÜ ]+$", ErrorMessage = "El nombre contiene carácteres inválidos.")]
        public string Nombre { get; set; }
    }
}