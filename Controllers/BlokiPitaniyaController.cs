using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator.Models;
using System.Threading.Tasks;

namespace PCConfigurator.Controllers
{
    /// <summary>
    /// Контроллер для управления блоками питания
    /// </summary>
    public class BlokiPitaniyaController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера блоков питания
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public BlokiPitaniyaController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отображает список всех блоков питания
        /// </summary>
        /// <returns>Представление со списком блоков питания</returns>
        public async Task<IActionResult> Index()
        {
            var blokiPitaniya = await _context.BlokiPitaniya.ToListAsync();
            return View(blokiPitaniya);
        }

        /// <summary>
        /// Отображает детальную информацию о блоке питания
        /// </summary>
        /// <param name="id">Модель блока питания</param>
        /// <returns>Представление с детальной информацией</returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokPitaniya = await _context.BlokiPitaniya
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (blokPitaniya == null)
            {
                return NotFound();
            }

            return View(blokPitaniya);
        }

        /// <summary>
        /// Отображает форму для создания нового блока питания
        /// </summary>
        /// <returns>Представление формы создания</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает данные из формы создания блока питания
        /// </summary>
        /// <param name="blokPitaniya">Данные блока питания</param>
        /// <returns>Перенаправление на список блоков питания</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Model,MoshchnostVt,Sertifikatsiya,Tsena")] BlokPitaniya blokPitaniya)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blokPitaniya);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blokPitaniya);
        }

        /// <summary>
        /// Отображает форму для редактирования блока питания
        /// </summary>
        /// <param name="id">Модель блока питания</param>
        /// <returns>Представление формы редактирования</returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokPitaniya = await _context.BlokiPitaniya.FindAsync(id);
            if (blokPitaniya == null)
            {
                return NotFound();
            }
            return View(blokPitaniya);
        }

        /// <summary>
        /// Обрабатывает данные из формы редактирования блока питания
        /// </summary>
        /// <param name="id">Модель блока питания</param>
        /// <param name="blokPitaniya">Обновленные данные блока питания</param>
        /// <returns>Перенаправление на список блоков питания</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Model,MoshchnostVt,Sertifikatsiya,Tsena")] BlokPitaniya blokPitaniya)
        {
            if (id != blokPitaniya.Model)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blokPitaniya);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlokPitaniyaExists(blokPitaniya.Model))
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
            return View(blokPitaniya);
        }

        /// <summary>
        /// Отображает форму подтверждения удаления блока питания
        /// </summary>
        /// <param name="id">Модель блока питания</param>
        /// <returns>Представление формы удаления</returns>
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blokPitaniya = await _context.BlokiPitaniya
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (blokPitaniya == null)
            {
                return NotFound();
            }

            return View(blokPitaniya);
        }

        /// <summary>
        /// Удаляет блок питания из базы данных
        /// </summary>
        /// <param name="id">Модель блока питания</param>
        /// <returns>Перенаправление на список блоков питания</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var blokPitaniya = await _context.BlokiPitaniya.FindAsync(id);
            if (blokPitaniya != null)
            {
                _context.BlokiPitaniya.Remove(blokPitaniya);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет существование блока питания
        /// </summary>
        /// <param name="id">Модель блока питания</param>
        /// <returns>True если блок питания существует, иначе False</returns>
        private bool BlokPitaniyaExists(string id)
        {
            return _context.BlokiPitaniya.Any(e => e.Model == id);
        }
    }
}