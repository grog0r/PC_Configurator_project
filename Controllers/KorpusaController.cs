using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator.Models;
using System.Threading.Tasks;

namespace PCConfigurator.Controllers
{
    /// <summary>
    /// Контроллер для управления корпусами
    /// </summary>
    public class KorpusaController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера корпусов
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public KorpusaController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отображает список всех корпусов
        /// </summary>
        /// <returns>Представление со списком корпусов</returns>
        public async Task<IActionResult> Index()
        {
            var korpusa = await _context.Korpusa.ToListAsync();
            return View(korpusa);
        }

        /// <summary>
        /// Отображает детальную информацию о корпусе
        /// </summary>
        /// <param name="id">Модель корпуса</param>
        /// <returns>Представление с детальной информацией</returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korpus = await _context.Korpusa
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (korpus == null)
            {
                return NotFound();
            }

            return View(korpus);
        }

        /// <summary>
        /// Отображает форму для создания нового корпуса
        /// </summary>
        /// <returns>Представление формы создания</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает данные из формы создания корпуса
        /// </summary>
        /// <param name="korpus">Данные корпуса</param>
        /// <returns>Перенаправление на список корпусов</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Model,PodderzhivayemyyeFormFaktory,Razmery,Tsena")] Korpus korpus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korpus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(korpus);
        }

        /// <summary>
        /// Отображает форму для редактирования корпуса
        /// </summary>
        /// <param name="id">Модель корпуса</param>
        /// <returns>Представление формы редактирования</returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korpus = await _context.Korpusa.FindAsync(id);
            if (korpus == null)
            {
                return NotFound();
            }
            return View(korpus);
        }

        /// <summary>
        /// Обрабатывает данные из формы редактирования корпуса
        /// </summary>
        /// <param name="id">Модель корпуса</param>
        /// <param name="korpus">Обновленные данные корпуса</param>
        /// <returns>Перенаправление на список корпусов</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Model,PodderzhivayemyyeFormFaktory,Razmery,Tsena")] Korpus korpus)
        {
            if (id != korpus.Model)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korpus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorpusExists(korpus.Model))
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
            return View(korpus);
        }

        /// <summary>
        /// Отображает форму подтверждения удаления корпуса
        /// </summary>
        /// <param name="id">Модель корпуса</param>
        /// <returns>Представление формы удаления</returns>
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korpus = await _context.Korpusa
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (korpus == null)
            {
                return NotFound();
            }

            return View(korpus);
        }

        /// <summary>
        /// Удаляет корпус из базы данных
        /// </summary>
        /// <param name="id">Модель корпуса</param>
        /// <returns>Перенаправление на список корпусов</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var korpus = await _context.Korpusa.FindAsync(id);
            if (korpus != null)
            {
                _context.Korpusa.Remove(korpus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет существование корпуса
        /// </summary>
        /// <param name="id">Модель корпуса</param>
        /// <returns>True если корпус существует, иначе False</returns>
        private bool KorpusExists(string id)
        {
            return _context.Korpusa.Any(e => e.Model == id);
        }
    }
}