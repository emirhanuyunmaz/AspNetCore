using System.Threading.Tasks;
using Efcore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Efcore.Controllers
{
    public class KursController:Controller
    {
        private readonly DataContext _context;
        public KursController(DataContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(){
            var kurslar = await _context.Kurslar.ToListAsync(); 
            return View(kurslar);
        }

        [HttpGet]
        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kurs kurs){
            _context.Kurslar.Add(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id){
            
            if(id==null){
                return NotFound();
            }
            // sadece id bilgisi ile aram yapma işlemi
            // var ogr = await _context.Ogrenciler.FindAsync(id);

            // Herhangi bir parametre ile arama işlemi.
            var kurs = await _context.Kurslar.FirstOrDefaultAsync(k => k.KursId == id);
            if (kurs== null){
                return NotFound();
            }
            return View(kurs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Token kotrolü yapılmasına olanak sağlar.
        public async Task<IActionResult> Edit(int id,Kurs model){
            
            if(id != model.KursId){
                return NotFound();
            }

            if(ModelState.IsValid){
                try{
                    _context.Update(model);
                    await _context.SaveChangesAsync();

                }catch(Exception err){
                    if(!_context.Kurslar.Any(k => k.KursId == model.KursId)){
                        return NotFound();
                    }
                    else{
                        Console.WriteLine(err);
                        throw ;
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id ){

            if(id== null){
                return NotFound();
            }
            var kurs = await _context.Kurslar.FindAsync(id);
            
            if(kurs == null){
                return NotFound();
            }
            return View(kurs);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id){
            var kurs = await _context.Kurslar.FindAsync(id);
            if(kurs == null){
                return NotFound();
            }
            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}