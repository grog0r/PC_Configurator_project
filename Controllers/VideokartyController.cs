using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator.Models;
using System.Threading.Tasks;

namespace PCConfigurator.Controllers
{
    /// <summary>
    /// Контроллер для управления видеокартами
    /// </summary>
    public class VideokartyController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера видеокарт
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public VideokartyController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отображает список всех видеокарт
        /// </summary>
        /// <returns>Представление со списком видеокарт</returns>
        public async Task<IActionResult> Index()
        {
            var videokarty = await _context.Videokarty.ToListAsync();
            return View(videokarty);
        }

        /// <summary>
        /// Отображает детальную информацию о видеокарте
        /// </summary>
        /// <param name="id">Модель видеокарты</param>
        /// <returns>Представление с детальной информацией</returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videokarta = await _context.Videokarty
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (videokarta == null)
            {
                return NotFound();
            }

            return View(videokarta);
        }

        /// <summary>
        /// Отображает форму для создания новой видеокарты
        /// </summary>
        /// <returns>Представление формы создания</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает данные из формы создания видеокарты
        /// </summary>
        /// <param name="videokarta">Данные видеокарты</param>
        /// <returns>Перенаправление на список видеокарт</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Model,VychislitelnayaMoshchnost,ObyemPamyatiGb,Moshchnost,Tsena")] Videokarta videokarta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videokarta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videokarta);
        }

        /// <summary>
        /// Отображает форму для редактирования видеокарты
        /// </summary>
        /// <param name="id">Модель видеокарты</param>
        /// <returns>Представление формы редактирования</returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videokarta = await _context.Videokarty.FindAsync(id);
            if (videokarta == null)
            {
                return NotFound();
            }
            return View(videokarta);
        }

        /// <summary>
        /// Обрабатывает данные из формы редактирования видеокарты
        /// </summary>
        /// <param name="id">Модель видеокарты</param>
        /// <param name="videokarta">Обновленные данные видеокарты</param>
        /// <returns>Перенаправление на список видеокарт</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Model,VychislitelnayaMoshchnost,ObyemPamyatiGb,Moshchnost,Tsena")] Videokarta videokarta)
        {
            if (id != videokarta.Model)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videokarta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideokartaExists(videokarta.Model))
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
            return View(videokarta);
        }

        /// <summary>
        /// Отображает форму подтверждения удаления видеокарты
        /// </summary>
        /// <param name="id">Модель видеокарты</param>
        /// <returns>Представление формы удаления</returns>
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videokarta = await _context.Videokarty
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (videokarta == null)
            {
                return NotFound();
            }

            return View(videokarta);
        }

        /// <summary>
        /// Удаляет видеокарту из базы данных
        /// </summary>
        /// <param name="id">Модель видеокарты</param>
        /// <returns>Перенаправление на список видеокарт</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var videokarta = await _context.Videokarty.FindAsync(id);
            if (videokarta != null)
            {
                _context.Videokarty.Remove(videokarta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет существование видеокарты
        /// </summary>
        /// <param name="id">Модель видеокарты</param>
        /// <returns>True если видеокарта существует, иначе False</returns>
        private bool VideokartaExists(string id)
        {
            return _context.Videokarty.Any(e => e.Model == id);
        }
    }
}