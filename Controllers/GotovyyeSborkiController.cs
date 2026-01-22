using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PCConfigurator.Data;
using PCConfigurator.Models;
using System.Threading.Tasks;

namespace PCConfigurator.Controllers
{
    public class GotovyyeSborkiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GotovyyeSborkiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GotovyyeSborki
        public async Task<IActionResult> Index()
        {
            var gotovyyeSborki = await _context.GotovyyeSborki
                .Include(g => g.MaterinskayaPlata)
                .Include(g => g.Protsessor)
                .Include(g => g.OperativnayaPamyat)
                .Include(g => g.Videokarta)
                .Include(g => g.Nakopitel)
                .Include(g => g.BlokPitaniya)
                .Include(g => g.Korpus)
                .ToListAsync();
            return View(gotovyyeSborki);
        }

        // GET: GotovyyeSborki/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gotovayaSborka = await _context.GotovyyeSborki
                .Include(g => g.MaterinskayaPlata)
                .Include(g => g.Protsessor)
                .Include(g => g.OperativnayaPamyat)
                .Include(g => g.Videokarta)
                .Include(g => g.Nakopitel)
                .Include(g => g.BlokPitaniya)
                .Include(g => g.Korpus)
                .FirstOrDefaultAsync(m => m.IdSborki == id);
            
            if (gotovayaSborka == null)
            {
                return NotFound();
            }

            return View(gotovayaSborka);
        }

        // GET: GotovyyeSborki/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            
            var model = new GotovayaSborka
            {
                IdSborki = GenerateBuildId(),
                VychislitelnayaMoshchnost = "Стандартная",
                ObshchayaStoimost = 0,
                MoshnostVt = 0
            };
            
            return View(model);
        }

        // POST: GotovyyeSborki/Create
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(GotovayaSborka gotovayaSborka)
{
    if (ModelState.IsValid)
    {
        // Получаем выбранные значения из формы
        var materinskayaPlataModel = Request.Form["MaterinskayaPlataModel"];
        var protsessorModel = Request.Form["ProtsessorModel"];
        var operativnayaPamyatModel = Request.Form["OperativnayaPamyatModel"];
        var videokartaModel = Request.Form["VideokartaModel"];
        var nakopitelModel = Request.Form["NakopitelModel"];
        var blokPitaniyaModel = Request.Form["BlokPitaniyaModel"];
        var korpusModel = Request.Form["KorpusModel"];
        
        // Устанавливаем значения в модель
        gotovayaSborka.MaterinskayaPlataModel = materinskayaPlataModel;
        gotovayaSborka.ProtsessorModel = protsessorModel;
        gotovayaSborka.OperativnayaPamyatModel = operativnayaPamyatModel;
        gotovayaSborka.VideokartaModel = videokartaModel;
        gotovayaSborka.NakopitelModel = nakopitelModel;
        gotovayaSborka.BlokPitaniyaModel = blokPitaniyaModel;
        gotovayaSborka.KorpusModel = korpusModel;
        
        // Получаем значения из скрытых полей
        var obshchayaStoimostStr = Request.Form["ObshchayaStoimost"];
        var moshnostVtStr = Request.Form["MoshnostVt"];
        
        if (decimal.TryParse(obshchayaStoimostStr, out decimal obshchayaStoimost))
        {
            gotovayaSborka.ObshchayaStoimost = obshchayaStoimost;
        }
        
        if (int.TryParse(moshnostVtStr, out int moshnostVt))
        {
            gotovayaSborka.MoshnostVt = moshnostVt;
        }
        
        // Проверяем, что все обязательные поля заполнены
        if (string.IsNullOrEmpty(gotovayaSborka.MaterinskayaPlataModel) ||
            string.IsNullOrEmpty(gotovayaSborka.ProtsessorModel) ||
            string.IsNullOrEmpty(gotovayaSborka.OperativnayaPamyatModel) ||
            string.IsNullOrEmpty(gotovayaSborka.VideokartaModel) ||
            string.IsNullOrEmpty(gotovayaSborka.NakopitelModel) ||
            string.IsNullOrEmpty(gotovayaSborka.BlokPitaniyaModel) ||
            string.IsNullOrEmpty(gotovayaSborka.KorpusModel))
        {
            ModelState.AddModelError(string.Empty, "Все компоненты должны быть выбраны!");
            PopulateDropdowns();
            return View(gotovayaSborka);
        }
        
        // Проверяем, что сборка с таким ID еще не существует
        if (await _context.GotovyyeSborki.AnyAsync(g => g.IdSborki == gotovayaSborka.IdSborki))
        {
            ModelState.AddModelError("IdSborki", "Сборка с таким ID уже существует!");
            PopulateDropdowns();
            return View(gotovayaSborka);
        }
        
        try
        {
            _context.Add(gotovayaSborka);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Сборка успешно создана!";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError(string.Empty, $"Ошибка при сохранении: {ex.Message}");
            PopulateDropdowns();
            return View(gotovayaSborka);
        }
    }
    
    // Если модель не валидна, показываем ошибки
    PopulateDropdowns();
    return View(gotovayaSborka);
}

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gotovayaSborka = await _context.GotovyyeSborki.FindAsync(id);
            if (gotovayaSborka == null)
            {
                return NotFound();
            }
            
            PopulateDropdowns();
            return View(gotovayaSborka);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdSborki,MaterinskayaPlataModel,ProtsessorModel,OperativnayaPamyatModel,VideokartaModel,NakopitelModel,BlokPitaniyaModel,KorpusModel,VychislitelnayaMoshchnost,ObshchayaStoimost,MoshnostVt")] GotovayaSborka gotovayaSborka)
        {
            if (id != gotovayaSborka.IdSborki)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Загружаем компоненты для расчета
                    await LoadComponentsForBuild(gotovayaSborka);
                    
                    // Пересчитываем стоимость при редактировании
                    gotovayaSborka.ObshchayaStoimost = CalculateTotalPrice(gotovayaSborka);
                    gotovayaSborka.MoshnostVt = CalculateTotalPower(gotovayaSborka);
                    
                    _context.Update(gotovayaSborka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GotovayaSborkaExists(gotovayaSborka.IdSborki))
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
            
            PopulateDropdowns();
            return View(gotovayaSborka);
        }

        // GET: GotovyyeSborki/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gotovayaSborka = await _context.GotovyyeSborki
                .Include(g => g.MaterinskayaPlata)
                .Include(g => g.Protsessor)
                .Include(g => g.OperativnayaPamyat)
                .Include(g => g.Videokarta)
                .Include(g => g.Nakopitel)
                .Include(g => g.BlokPitaniya)
                .Include(g => g.Korpus)
                .FirstOrDefaultAsync(m => m.IdSborki == id);
            
            if (gotovayaSborka == null)
            {
                return NotFound();
            }

            return View(gotovayaSborka);
        }

        // POST: GotovyyeSborki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var gotovayaSborka = await _context.GotovyyeSborki.FindAsync(id);
            if (gotovayaSborka != null)
            {
                _context.GotovyyeSborki.Remove(gotovayaSborka);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // AJAX: Получить цену компонента
        [HttpGet]
        public async Task<IActionResult> GetComponentPrice(string componentType, string modelName)
        {
            if (string.IsNullOrEmpty(componentType) || string.IsNullOrEmpty(modelName))
            {
                return Json(new { price = 0m });
            }

            decimal price = 0m;
            int power = 0;

            switch (componentType.ToLower())
            {
                case "materinskayaplata":
                    var motherboard = await _context.MaterinskiyePlaty
                        .FirstOrDefaultAsync(m => m.Model == modelName);
                    price = motherboard?.Tsena ?? 0m;
                    break;
                    
                case "protsessor":
                    var processor = await _context.Protsessory
                        .FirstOrDefaultAsync(m => m.Model == modelName);
                    price = processor?.Tsena ?? 0m;
                    power = processor?.Moshchnost ?? 0;
                    break;
                    
                case "operativnayapamyat":
                    var memory = await _context.OperativnayaPamyat
                        .FirstOrDefaultAsync(m => m.Model == modelName);
                    price = memory?.Tsena ?? 0m;
                    break;
                    
                case "videokarta":
                    var gpu = await _context.Videokarty
                        .FirstOrDefaultAsync(m => m.Model == modelName);
                    price = gpu?.Tsena ?? 0m;
                    power = gpu?.Moshchnost ?? 0;
                    break;
                    
                case "nakopitel":
                    var storage = await _context.Nakopiteli
                        .FirstOrDefaultAsync(m => m.Model == modelName);
                    price = storage?.Tsena ?? 0m;
                    break;
                    
                case "blokpitaniya":
                    var psu = await _context.BlokiPitaniya
                        .FirstOrDefaultAsync(m => m.Model == modelName);
                    price = psu?.Tsena ?? 0m;
                    break;
                    
                case "korpus":
                    var @case = await _context.Korpusa
                        .FirstOrDefaultAsync(m => m.Model == modelName);
                    price = @case?.Tsena ?? 0m;
                    break;
            }

            return Json(new { price, power });
        }

        private bool GotovayaSborkaExists(string id)
        {
            return _context.GotovyyeSborki.Any(e => e.IdSborki == id);
        }

        private void PopulateDropdowns()
        {
            ViewData["MaterinskayaPlataModel"] = new SelectList(_context.MaterinskiyePlaty.OrderBy(m => m.Model), "Model", "Model");
            ViewData["ProtsessorModel"] = new SelectList(_context.Protsessory.OrderBy(p => p.Model), "Model", "Model");
            ViewData["OperativnayaPamyatModel"] = new SelectList(_context.OperativnayaPamyat.OrderBy(r => r.Model), "Model", "Model");
            ViewData["VideokartaModel"] = new SelectList(_context.Videokarty.OrderBy(v => v.Model), "Model", "Model");
            ViewData["NakopitelModel"] = new SelectList(_context.Nakopiteli.OrderBy(s => s.Model), "Model", "Model");
            ViewData["BlokPitaniyaModel"] = new SelectList(_context.BlokiPitaniya.OrderBy(p => p.Model), "Model", "Model");
            ViewData["KorpusModel"] = new SelectList(_context.Korpusa.OrderBy(c => c.Model), "Model", "Model");
        }

        private async Task LoadComponentsForBuild(GotovayaSborka build)
        {
            if (!string.IsNullOrEmpty(build.MaterinskayaPlataModel))
                build.MaterinskayaPlata = await _context.MaterinskiyePlaty
                    .FirstOrDefaultAsync(m => m.Model == build.MaterinskayaPlataModel);
                
            if (!string.IsNullOrEmpty(build.ProtsessorModel))
                build.Protsessor = await _context.Protsessory
                    .FirstOrDefaultAsync(m => m.Model == build.ProtsessorModel);
                
            if (!string.IsNullOrEmpty(build.OperativnayaPamyatModel))
                build.OperativnayaPamyat = await _context.OperativnayaPamyat
                    .FirstOrDefaultAsync(m => m.Model == build.OperativnayaPamyatModel);
                
            if (!string.IsNullOrEmpty(build.VideokartaModel))
                build.Videokarta = await _context.Videokarty
                    .FirstOrDefaultAsync(m => m.Model == build.VideokartaModel);
                
            if (!string.IsNullOrEmpty(build.NakopitelModel))
                build.Nakopitel = await _context.Nakopiteli
                    .FirstOrDefaultAsync(m => m.Model == build.NakopitelModel);
                
            if (!string.IsNullOrEmpty(build.BlokPitaniyaModel))
                build.BlokPitaniya = await _context.BlokiPitaniya
                    .FirstOrDefaultAsync(m => m.Model == build.BlokPitaniyaModel);
                
            if (!string.IsNullOrEmpty(build.KorpusModel))
                build.Korpus = await _context.Korpusa
                    .FirstOrDefaultAsync(m => m.Model == build.KorpusModel);
        }

        private decimal CalculateTotalPrice(GotovayaSborka build)
        {
            decimal total = 0;
            
            if (build.MaterinskayaPlata != null)
                total += build.MaterinskayaPlata.Tsena;
            if (build.Protsessor != null)
                total += build.Protsessor.Tsena;
            if (build.OperativnayaPamyat != null)
                total += build.OperativnayaPamyat.Tsena;
            if (build.Videokarta != null)
                total += build.Videokarta.Tsena;
            if (build.Nakopitel != null)
                total += build.Nakopitel.Tsena;
            if (build.BlokPitaniya != null)
                total += build.BlokPitaniya.Tsena;
            if (build.Korpus != null)
                total += build.Korpus.Tsena;
                
            return total;
        }

        private int CalculateTotalPower(GotovayaSborka build)
        {
            int totalPower = 0;
            
            if (build.Protsessor != null)
                totalPower += build.Protsessor.Moshchnost;
            if (build.Videokarta != null)
                totalPower += build.Videokarta.Moshchnost;
            
            // Добавляем 20% запаса на другие компоненты
            return (int)(totalPower * 1.2m);
        }

        private string GenerateBuildId()
        {
            var date = DateTime.Now.ToString("yyMMdd");
            var count = _context.GotovyyeSborki.Count(g => g.IdSborki.Contains(date)) + 1;
            return $"BUILD-{date}-{count:D3}";
        }
    }
}