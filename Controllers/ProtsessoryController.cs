using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator.Models;
using System.Threading.Tasks;

namespace PCConfigurator.Controllers
{
    /// <summary>
    /// Контроллер для управления процессорами
    /// </summary>
    public class ProtsessoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера процессоров
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public ProtsessoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отображает список всех процессоров
        /// </summary>
        /// <returns>Представление со списком процессоров</returns>
        public async Task<IActionResult> Index()
        {
            var protsessory = await _context.Protsessory.ToListAsync();
            return View(protsessory);
        }

        /// <summary>
        /// Отображает детальную информацию о процессоре
        /// </summary>
        /// <param name="id">Модель процессора</param>
        /// <returns>Представление с детальной информацией</returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protsessor = await _context.Protsessory
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (protsessor == null)
            {
                return NotFound();
            }

            return View(protsessor);
        }

        /// <summary>
        /// Отображает форму для создания нового процессора
        /// </summary>
        /// <returns>Представление формы создания</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает данные из формы создания процессора
        /// </summary>
        /// <param name="protsessor">Данные процессора</param>
        /// <returns>Перенаправление на список процессоров</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Model,Soket,Moshchnost,TaktovayaChastotaGz,KolichestvoYader,Tsena")] Protsessor protsessor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(protsessor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(protsessor);
        }

        /// <summary>
        /// Отображает форму для редактирования процессора
        /// </summary>
        /// <param name="id">Модель процессора</param>
        /// <returns>Представление формы редактирования</returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protsessor = await _context.Protsessory.FindAsync(id);
            if (protsessor == null)
            {
                return NotFound();
            }
            return View(protsessor);
        }

        /// <summary>
        /// Обрабатывает данные из формы редактирования процессора
        /// </summary>
        /// <param name="id">Модель процессора</param>
        /// <param name="protsessor">Обновленные данные процессора</param>
        /// <returns>Перенаправление на список процессоров</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Model,Soket,Moshchnost,TaktovayaChastotaGz,KolichestvoYader,Tsena")] Protsessor protsessor)
        {
            if (id != protsessor.Model)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(protsessor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProtsessorExists(protsessor.Model))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(protsessor);
        }

        /// <summary>
        /// Отображает форму подтверждения удаления процессора
        /// </summary>
        /// <param name="id">Модель процессора</param>
        /// <returns>Представление формы удаления</returns>
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protsessor = await _context.Protsessory
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (protsessor == null)
            {
                return NotFound();
            }

            return View(protsessor);
        }

        /// <summary>
        /// Удаляет процессор из базы данных
        /// </summary>
        /// <param name="id">Модель процессора</param>
        /// <returns>Перенаправление на список процессоров</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var protsessor = await _context.Protsessory.FindAsync(id);
            if (protsessor != null)
            {
                _context.Protsessory.Remove(protsessor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет существование процессора
        /// </summary>
        /// <param name="id">Модель процессора</param>
        /// <returns>True если процессор существует, иначе False</returns>
        private bool ProtsessorExists(string id)
        {
            return _context.Protsessory.Any(e => e.Model == id);
        }
    }
}