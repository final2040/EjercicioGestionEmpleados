using System.Collections.Generic;
using System.Web.Mvc;
using Model;

namespace Empleados.Models.ViewModels
{
    public class EditViewModel
    {
        public Empleado Empleado { get; set; }
        public IEnumerable<SelectListItem> Puestos { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; }
    }
}