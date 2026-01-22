using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator.Models;
using System.Threading.Tasks;

namespace PCConfigurator.Controllers
{
    /// <summary>
    /// Контроллер для управления оперативной памятью
    /// </summary>
    public class OperativnayaPamyatController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера оперативной памяти
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public OperativnayaPamyatController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отображает список всей оперативной памяти
        /// </summary>
        /// <returns>Представление со списком оперативной памяти</returns>
        public async Task<IActionResult> Index()
        {
            var operativnayaPamyat = await _context.OperativnayaPamyat.ToListAsync();
            return View(operativnayaPamyat);
        }

        /// <summary>
        /// Отображает детальную информацию об оперативной памяти
        /// </summary>
        /// <param name="id">Модель оперативной памяти</param>
        /// <returns>Представление с детальной информацией</returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operativnayaPamyat = await _context.OperativnayaPamyat
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (operativnayaPamyat == null)
            {
                return NotFound();
            }

            return View(operativnayaPamyat);
        }

        /// <summary>
        /// Отображает форму для создания новой оперативной памяти
        /// </summary>
        /// <returns>Представление формы создания</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает данные из формы создания оперативной памяти
        /// </summary>
        /// <param name="operativnayaPamyat">Данные оперативной памяти</param>
        /// <returns>Перенаправление на список оперативной памяти</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Model,Tip,ObyomModulyaGb,ChastotaMgts,Tsena")] OperativnayaPamyat operativnayaPamyat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operativnayaPamyat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(operativnayaPamyat);
        }

        /// <summary>
        /// Отображает форму для редактирования оперативной памяти
        /// </summary>
        /// <param name="id">Модель оперативной памяти</param>
        /// <returns>Представление формы редактирования</returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operativnayaPamyat = await _context.OperativnayaPamyat.FindAsync(id);
            if (operativnayaPamyat == null)
            {
                return NotFound();
            }
            return View(operativnayaPamyat);
        }

        /// <summary>
        /// Обрабатывает данные из формы редактирования оперативной памяти
        /// </summary>
        /// <param name="id">Модель оперативной памяти</param>
        /// <param name="operativnayaPamyat">Обновленные данные оперативной памяти</param>
        /// <returns>Перенаправление на список оперативной памяти</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Model,Tip,ObyomModulyaGb,ChastotaMgts,Tsena")] OperativnayaPamyat operativnayaPamyat)
        {
            if (id != operativnayaPamyat.Model)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operativnayaPamyat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperativnayaPamyatExists(operativnayaPamyat.Model))
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
            return View(operativnayaPamyat);
        }

        /// <summary>
        /// Отображает форму подтверждения удаления оперативной памяти
        /// </summary>
        /// <param name="id">Модель оперативной памяти</param>
        /// <returns>Представление формы удаления</returns>
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operativnayaPamyat = await _context.OperativnayaPamyat
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (operativnayaPamyat == null)
            {
                return NotFound();
            }

            return View(operativnayaPamyat);
        }

        /// <summary>
        /// Удаляет оперативную память из базы данных
        /// </summary>
        /// <param name="id">Модель оперативной памяти</param>
        /// <returns>Перенаправление на список оперативной памяти</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var operativnayaPamyat = await _context.OperativnayaPamyat.FindAsync(id);
            if (operativnayaPamyat != null)
            {
                _context.OperativnayaPamyat.Remove(operativnayaPamyat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет существование оперативной памяти
        /// </summary>
        /// <param name="id">Модель оперативной памяти</param>
        /// <returns>True если оперативная память существует, иначе False</returns>
        private bool OperativnayaPamyatExists(string id)
        {
            return _context.OperativnayaPamyat.Any(e => e.Model == id);
        }
    }
}