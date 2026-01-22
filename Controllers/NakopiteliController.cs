using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator.Models;
using System.Threading.Tasks;

namespace PCConfigurator.Controllers
{
    /// <summary>
    /// Контроллер для управления накопителями
    /// </summary>
    public class NakopiteliController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера накопителей
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public NakopiteliController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отображает список всех накопителей
        /// </summary>
        /// <returns>Представление со списком накопителей</returns>
        public async Task<IActionResult> Index()
        {
            var nakopiteli = await _context.Nakopiteli.ToListAsync();
            return View(nakopiteli);
        }

        /// <summary>
        /// Отображает детальную информацию о накопителе
        /// </summary>
        /// <param name="id">Модель накопителя</param>
        /// <returns>Представление с детальной информацией</returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nakopitel = await _context.Nakopiteli
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (nakopitel == null)
            {
                return NotFound();
            }

            return View(nakopitel);
        }

        /// <summary>
        /// Отображает форму для создания нового накопителя
        /// </summary>
        /// <returns>Представление формы создания</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает данные из формы создания накопителя
        /// </summary>
        /// <param name="nakopitel">Данные накопителя</param>
        /// <returns>Перенаправление на список накопителей</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Model,Tip,ObyomGb,SkorostChteniyaMbS,SkorostZapisiMbS,Tsena")] Nakopitel nakopitel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nakopitel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nakopitel);
        }

        /// <summary>
        /// Отображает форму для редактирования накопителя
        /// </summary>
        /// <param name="id">Модель накопителя</param>
        /// <returns>Представление формы редактирования</returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nakopitel = await _context.Nakopiteli.FindAsync(id);
            if (nakopitel == null)
            {
                return NotFound();
            }
            return View(nakopitel);
        }

        /// <summary>
        /// Обрабатывает данные из формы редактирования накопителя
        /// </summary>
        /// <param name="id">Модель накопителя</param>
        /// <param name="nakopitel">Обновленные данные накопителя</param>
        /// <returns>Перенаправление на список накопителей</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Model,Tip,ObyomGb,SkorostChteniyaMbS,SkorostZapisiMbS,Tsena")] Nakopitel nakopitel)
        {
            if (id != nakopitel.Model)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nakopitel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NakopitelExists(nakopitel.Model))
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
            return View(nakopitel);
        }

        /// <summary>
        /// Отображает форму подтверждения удаления накопителя
        /// </summary>
        /// <param name="id">Модель накопителя</param>
        /// <returns>Представление формы удаления</returns>
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nakopitel = await _context.Nakopiteli
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (nakopitel == null)
            {
                return NotFound();
            }

            return View(nakopitel);
        }

        /// <summary>
        /// Удаляет накопитель из базы данных
        /// </summary>
        /// <param name="id">Модель накопителя</param>
        /// <returns>Перенаправление на список накопителей</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nakopitel = await _context.Nakopiteli.FindAsync(id);
            if (nakopitel != null)
            {
                _context.Nakopiteli.Remove(nakopitel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет существование накопителя
        /// </summary>
        /// <param name="id">Модель накопителя</param>
        /// <returns>True если накопитель существует, иначе False</returns>
        private bool NakopitelExists(string id)
        {
            return _context.Nakopiteli.Any(e => e.Model == id);
        }
    }
}