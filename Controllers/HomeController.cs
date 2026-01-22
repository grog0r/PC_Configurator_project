using Microsoft.AspNetCore.Mvc;
using PCConfigurator.Data;
using PCConfigurator.Models;
using System.Diagnostics;

namespace PCConfigurator.Controllers
{
    /// <summary>
    /// Контроллер главной страницы приложения
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера главной страницы
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="context">Контекст базы данных</param>
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Отображает главную страницу приложения
        /// </summary>
        /// <returns>Представление главной страницы</returns>
        public IActionResult Index()
        {
            // Получаем статистику по количеству компонентов
            var stats = new
            {
                MotherboardsCount = _context.MaterinskiyePlaty.Count(),
                ProcessorsCount = _context.Protsessory.Count(),
                MemoryCount = _context.OperativnayaPamyat.Count(),
                GraphicsCardsCount = _context.Videokarty.Count(),
                StorageCount = _context.Nakopiteli.Count(),
                PowerSuppliesCount = _context.BlokiPitaniya.Count(),
                CasesCount = _context.Korpusa.Count(),
                BuildsCount = _context.GotovyyeSborki.Count()
            };

            ViewBag.Stats = stats;
            return View();
        }

        /// <summary>
        /// Обрабатывает ошибки приложения
        /// </summary>
        /// <returns>Представление ошибки</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}