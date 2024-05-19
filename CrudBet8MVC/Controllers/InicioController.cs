using CrudBet8MVC.Datos;
using CrudBet8MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CrudBet8MVC.Controllers
{
    public class InicioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InicioController(ApplicationDbContext context)
        {
           _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contacto.ToListAsync());
        }

        /// <summary>
        /// Metodo para crear un nuevo contacto
        /// Solo muestra el formulario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto contacto)
        {
            //Validacion de los datos
            if(ModelState.IsValid)
            {
                //Agregar la fecha de creacion
                contacto.FechaCreacion = DateTime.Now;
                
                _context.Contacto.Add(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            //Buscamos el contacto por el id
            var contacto =_context.Contacto.Find(id);

            if (contacto == null)
            {
                return NotFound();
            }


            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contacto contacto)
        {
            //Validacion de los datos
            if (ModelState.IsValid)
            {
                _context.Update(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detalle(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            //Buscamos el contacto por el id
            var contacto = _context.Contacto.Find(id);

            if (contacto == null)
            {
                return NotFound();
            }


            return View(contacto);
        }


        [HttpGet]
        public IActionResult Borrar(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            //Buscamos el contacto por el id
            var contacto = _context.Contacto.Find(id);

            if (contacto == null)
            {
                return NotFound();
            }


            return View(contacto);
        }

        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarContacto(int? id)
        {
           var contacto = await _context.Contacto.FindAsync(id);
            if(contacto == null)
            {
                //Lomanda a la misma vista de borrar
                return View();
            }
            _context.Contacto.Remove(contacto);
            //Ejecutamos el borrado de la base de datos
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
