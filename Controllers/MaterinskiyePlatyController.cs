using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator.Models;
using System.Threading.Tasks;

namespace PCConfigurator.Controllers
{
    /// <summary>
    /// Контроллер для управления материнскими платами
    /// </summary>
    public class MaterinskiyePlatyController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера материнских плат
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public MaterinskiyePlatyController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Отображает список всех материнских плат
        /// </summary>
        /// <returns>Представление со списком материнских плат</returns>
        public async Task<IActionResult> Index()
        {
            var materinskiyePlaty = await _context.MaterinskiyePlaty.ToListAsync();
            return View(materinskiyePlaty);
        }

        /// <summary>
        /// Отображает детальную информацию о материнской плате
        /// </summary>
        /// <param name="id">Модель материнской платы</param>
        /// <returns>Представление с детальной информацией</returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materinskayaPlata = await _context.MaterinskiyePlaty
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (materinskayaPlata == null)
            {
                return NotFound();
            }

            return View(materinskayaPlata);
        }

        /// <summary>
        /// Отображает форму для создания новой материнской платы
        /// </summary>
        /// <returns>Представление формы создания</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Обрабатывает данные из формы создания материнской платы
        /// </summary>
        /// <param name="materinskayaPlata">Данные материнской платы</param>
        /// <returns>Перенаправление на список материнских плат</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Model,FormFaktor,Soket,TipPodderzhivayemoyOzu,MaksimalnyyObyomOzuGb,TipPodderzhivayemixNakopiteli,Tsena")] MaterinskayaPlata materinskayaPlata)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materinskayaPlata);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materinskayaPlata);
        }

        /// <summary>
        /// Отображает форму для редактирования материнской платы
        /// </summary>
        /// <param name="id">Модель материнской платы</param>
        /// <returns>Представление формы редактирования</returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materinskayaPlata = await _context.MaterinskiyePlaty.FindAsync(id);
            if (materinskayaPlata == null)
            {
                return NotFound();
            }
            return View(materinskayaPlata);
        }

        /// <summary>
        /// Обрабатывает данные из формы редактирования материнской платы
        /// </summary>
        /// <param name="id">Модель материнской платы</param>
        /// <param name="materinskayaPlata">Обновленные данные материнской платы</param>
        /// <returns>Перенаправление на список материнских плат</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Model,FormFaktor,Soket,TipPodderzhivayemoyOzu,MaksimalnyyObyomOzuGb,TipPodderzhivayemixNakopiteli,Tsena")] MaterinskayaPlata materinskayaPlata)
        {
            if (id != materinskayaPlata.Model)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materinskayaPlata);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterinskayaPlataExists(materinskayaPlata.Model))
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
            return View(materinskayaPlata);
        }

        /// <summary>
        /// Отображает форму подтверждения удаления материнской платы
        /// </summary>
        /// <param name="id">Модель материнской платы</param>
        /// <returns>Представление формы удаления</returns>
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materinskayaPlata = await _context.MaterinskiyePlaty
                .FirstOrDefaultAsync(m => m.Model == id);
            
            if (materinskayaPlata == null)
            {
                return NotFound();
            }

            return View(materinskayaPlata);
        }

        /// <summary>
        /// Удаляет материнскую плату из базы данных
        /// </summary>
        /// <param name="id">Модель материнской платы</param>
        /// <returns>Перенаправление на список материнских плат</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var materinskayaPlata = await _context.MaterinskiyePlaty.FindAsync(id);
            if (materinskayaPlata != null)
            {
                _context.MaterinskiyePlaty.Remove(materinskayaPlata);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет существование материнской платы
        /// </summary>
        /// <param name="id">Модель материнской платы</param>
        /// <returns>True если материнская плата существует, иначе False</returns>
        private bool MaterinskayaPlataExists(string id)
        {
            return _context.MaterinskiyePlaty.Any(e => e.Model == id);
        }
    }
}