using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Empleados.Models;
using Model;
using Empleados.Models.ViewModels;

namespace Empleados.Controllers
{
    public class EmpleadoController : Controller
    {
        Repository _dbRepository = new Repository(new DataContext());

        // GET: Empleado
        public ActionResult Index()
        {
            try
            {
                return View(_dbRepository.GetAll<Empleado>("Departamento", "Puesto"));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }
        [HttpPost]
        public ActionResult Index(string criteria)
        {
            try
            {
                if (string.IsNullOrEmpty(criteria))
                    return Json(_dbRepository.GetAll<Empleado>("Departamento", "Puesto"));
                return Json(_dbRepository.Find<Empleado>(
                    e => 
                    e.Nombres.ToLower().Trim().Contains(criteria.ToLower().Trim()),
                    "Departamento", "Puesto"));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public ActionResult Create()
        {
            try
            {
                return View(GetEditViewModel(new Empleado()));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", ex);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_dbRepository.Delete<Empleado>(e => e.Id == id))
                    return Json(new ActionResponse(true, "Empleado eliminado con exito!."));

                return Json(new ActionResponse(false, "No se pudo eliminar al empleado actual."));

            }
            catch (Exception ex)
            {
                return Json(new ActionResponse(false, $"No se pudo eliminar al empleado:{ex.Message}"));
            }
        }

        [HttpPost]
        public ActionResult Create(Empleado empleado)
        {
            try
            {
                Empleado createdEmpleado = _dbRepository.Create<Empleado>(empleado);
                if (createdEmpleado != null)
                {
                    return Json(new ActionResponse(true, $"Empleado {createdEmpleado.Nombres} " +
                                                         $"creado satisfactoriamente."));
                }
                return Json(new ActionResponse(false, "No fue posible crear el nuevo empleado."));

            }
            catch (Exception ex)
            {
                return Json(new ActionResponse(false, "Ocurrio un error inesperado al crear el nuevo empleado."));
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                if (_dbRepository.Exists<Empleado>(e => e.Id == id))
                {
                    var viewModel = GetEditViewModel(_dbRepository.GetEntity<Empleado>(e => e.Id == id));

                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                RedirectToAction("Error", ex);
            }
            return HttpNotFound();
        }

        private EditViewModel GetEditViewModel(Empleado empleado)
        {
            EditViewModel viewModel = new EditViewModel();
            viewModel.Empleado = empleado;
            viewModel.Puestos = _dbRepository.GetAll<Puesto>()
            .Select(
                p => new SelectListItem()
                {
                    Text = p.Nombre,
                    Value = p.Id.ToString()
                });
            viewModel.Departamentos = _dbRepository.GetAll<Departamento>()
                .Select(d => new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.Id.ToString()
                });
            return viewModel;
        }

        [HttpPost]
        public ActionResult Edit(Empleado empleado)
        {
            try
            {
                _dbRepository.Update(empleado);
                return Json(new ActionResponse(true, "Empleado guardado correctamente"));
            }
            catch (Exception ex)
            {
                return
                    Json(new ActionResponse(false, $"Ocurrio un error " +
                                                   $"inesperado al guardar al empleado: {ex.Message}"));
            }
        }

        public ActionResult Error(Exception ex)
        {
            return View(ex);
        }

        #region Disposing
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbRepository != null)
                {
                    _dbRepository.Dispose();
                    _dbRepository = null;
                }
            }
            base.Dispose(disposing);
        }

        #endregion

    }
}