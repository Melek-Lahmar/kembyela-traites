using iTextSharp.text;
using iTextSharp.text.pdf;
using Kembyela.Data;
using Kembyela.Helpers;
using Kembyela.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Text;

namespace Kembyela.Controllers
{
    public class TraiteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TraiteController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: /Traite/Create
        public IActionResult Create()
        {
            var traite = new Traite
            {
                DateEcheance = DateTime.Now.AddMonths(1),
                DateEdition = DateTime.Now,
                Monnaie = "DT",
                Protestable = true
            };
            return View(traite);
        }

        // POST: /Traite/Create - VERSION CORRIGÉE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Traite traite)
        {
            // DEBUG: Afficher les données reçues
            Console.WriteLine("=== DÉBUT CREATE POST ===");
            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");

            // Afficher toutes les erreurs de validation
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Erreurs de validation:");
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"{state.Key}: {error.ErrorMessage}");
                    }
                }

                // Afficher les valeurs
                Console.WriteLine($"Ville: {traite.Ville}");
                Console.WriteLine($"RIB: {traite.RIB}");
                Console.WriteLine($"Montant: {traite.Montant}");
                Console.WriteLine($"OrdreDe: {traite.OrdreDe}");
                Console.WriteLine($"Payer: {traite.Payer}");
                Console.WriteLine($"Banque: {traite.Banque}");
                Console.WriteLine($"Monnaie: {traite.Monnaie}");
                Console.WriteLine($"DateEdition: {traite.DateEdition}");
                Console.WriteLine($"DateEcheance: {traite.DateEcheance}");
                Console.WriteLine($"Aval: {traite.Aval}");
                Console.WriteLine($"Protestable: {traite.Protestable}");
            }

            // SI ModelState n'est pas valide, on peut quand même tenter d'insérer
            try
            {
                // S'assurer que Monnaie a une valeur
                if (string.IsNullOrEmpty(traite.Monnaie))
                {
                    traite.Monnaie = "DT";
                }

                // Convertir le montant en lettres
                traite.MontantEnLettres = NumberToWordsFrench.ConvertToWords(traite.Montant);

                // S'assurer que CreatedAt a une valeur
                traite.CreatedAt = DateTime.Now;

                // MODIFICATION ICI : Traiter Aval si vide (optionnel)
                // Si l'utilisateur laisse le champ vide, mettre à null
                if (string.IsNullOrWhiteSpace(traite.Aval))
                {
                    traite.Aval = null;
                }

                // DEBUG: Afficher avant insertion
                Console.WriteLine("Avant insertion:");
                Console.WriteLine($"ID: {traite.Id}");
                Console.WriteLine($"MontantEnLettres: {traite.MontantEnLettres}");
                Console.WriteLine($"CreatedAt: {traite.CreatedAt}");
                Console.WriteLine($"Aval: {traite.Aval}"); // Vérifier la valeur

                // Sauvegarder dans la base de données
                _context.Traites.Add(traite);
                int rowsAffected = await _context.SaveChangesAsync();

                Console.WriteLine($"Rows affected: {rowsAffected}");
                Console.WriteLine($"Nouvel ID: {traite.Id}");
                Console.WriteLine("=== FIN CREATE POST ===");

                TempData["SuccessMessage"] = $"Lettre de change #{traite.Id} créée avec succès!";
                return RedirectToAction("Print", new { id = traite.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION: {ex.Message}");
                Console.WriteLine($"INNER: {ex.InnerException?.Message}");
                Console.WriteLine($"STACK: {ex.StackTrace}");

                TempData["ErrorMessage"] = $"Erreur: {ex.Message}";
                if (ex.InnerException != null)
                {
                    TempData["ErrorMessage"] += $" - Détail: {ex.InnerException.Message}";
                }

                // Réinitialiser les valeurs par défaut
                traite.DateEcheance = DateTime.Now.AddMonths(1);
                traite.DateEdition = DateTime.Now;
                traite.Monnaie = "DT";
                traite.Protestable = true;

                return View(traite);
            }
        }
        // AJOUTEZ ces actions dans votre TraiteController.cs

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarquerCommePayee(int id)
        {
            try
            {
                var traite = await _context.Traites.FindAsync(id);
                if (traite == null)
                {
                    TempData["ErrorMessage"] = "Lettre de change non trouvée.";
                    return RedirectToAction("Index");
                }

                traite.EstPayee = true;
                _context.Update(traite);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Lettre de change marquée comme payée avec succès.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erreur: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Stats()
        {
            var traites = _context.Traites
                .AsNoTracking()
                .OrderByDescending(t => t.CreatedAt)
                .ToList();

            return View(traites);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarquerCommeNonPayee(int id)
        {
            try
            {
                var traite = await _context.Traites.FindAsync(id);
                if (traite == null)
                {
                    TempData["ErrorMessage"] = "Lettre de change non trouvée.";
                    return RedirectToAction("Index");
                }

                traite.EstPayee = false;
                _context.Update(traite);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Lettre de change marquée comme non payée avec succès.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erreur: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        // GET: /Traite/Index
        // GET: /Traite/Index
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParam"] = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["EcheanceSortParam"] = sortOrder == "echeance_asc" ? "echeance_desc" : "echeance_asc";
            ViewData["MontantSortParam"] = sortOrder == "montant_asc" ? "montant_desc" : "montant_asc";
            ViewData["CurrentFilter"] = searchString;

            var traites = _context.Traites.AsQueryable();

            // Appliquer la recherche si un terme est fourni
            if (!string.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower();
                traites = traites.Where(t =>
                    (!string.IsNullOrEmpty(t.OrdreDe) && t.OrdreDe.ToLower().Contains(search)) ||
                    (!string.IsNullOrEmpty(t.Payer) && t.Payer.ToLower().Contains(search)) ||
                    (!string.IsNullOrEmpty(t.Banque) && t.Banque.ToLower().Contains(search))
                );
            }

            // Appliquer le tri
            switch (sortOrder)
            {
                case "date_desc":
                    traites = traites.OrderByDescending(t => t.CreatedAt);
                    break;
                case "echeance_asc":
                    traites = traites.OrderBy(t => t.DateEcheance);
                    break;
                case "echeance_desc":
                    traites = traites.OrderByDescending(t => t.DateEcheance);
                    break;
                case "montant_asc":
                    traites = traites.OrderBy(t => t.Montant);
                    break;
                case "montant_desc":
                    traites = traites.OrderByDescending(t => t.Montant);
                    break;
                default: // Tri par défaut : création décroissante
                    traites = traites.OrderByDescending(t => t.CreatedAt);
                    break;
            }

            var traitesList = await traites.ToListAsync();

            // Calculer le nombre de lettres en retard et préparer les données pour la vue
            int lettresEnRetard = 0;
            var traitesAvecStatut = new List<object>();

            foreach (var traite in traitesList)
            {
                bool estEnRetard = traite.DateEcheance.Date < DateTime.Now.Date;

                if (estEnRetard)
                {
                    lettresEnRetard++;
                }

                // Créer un objet anonyme avec la traite et son statut
                traitesAvecStatut.Add(new
                {
                    Traite = traite,
                    EstEnRetard = estEnRetard,
                    JoursRetard = estEnRetard ? (DateTime.Now.Date - traite.DateEcheance.Date).Days : 0
                });
            }

            // Passer les informations de recherche à la vue
            ViewData["SearchCount"] = traitesList.Count;
            ViewData["TotalCount"] = await _context.Traites.CountAsync();
            ViewData["LettresEnRetard"] = lettresEnRetard;
            ViewData["TraitesAvecStatut"] = traitesAvecStatut;

            return View(traitesList);
        }

        // GET: /Traite/Delete/id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var traite = await _context.Traites.FindAsync(id);

            if (traite == null)
            {
                TempData["ErrorMessage"] = "Lettre de change non trouvée!";
                return RedirectToAction("Index");
            }

            return View(traite);
        }

        // POST: /Traite/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var traite = await _context.Traites.FindAsync(id);

            if (traite != null)
            {
                _context.Traites.Remove(traite);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Lettre de change supprimée avec succès!";
            }

            return RedirectToAction("Index");
        }

        // POST: /Traite/Duplicate/id
        [HttpPost]
        public async Task<IActionResult> Duplicate(int id)
        {
            try
            {
                var originalTraite = await _context.Traites.FindAsync(id);

                if (originalTraite == null)
                {
                    return Json(new { success = false, message = "Lettre de change non trouvée" });
                }

                // Créer une copie
                var duplicateTraite = new Traite
                {
                    CreatedAt = DateTime.Now,
                    DateEcheance = originalTraite.DateEcheance.AddMonths(1),
                    Ville = originalTraite.Ville,
                    DateEdition = DateTime.Now,
                    RIB = originalTraite.RIB,
                    Montant = originalTraite.Montant,
                    Monnaie = originalTraite.Monnaie,
                    OrdreDe = originalTraite.OrdreDe,
                    Payer = originalTraite.Payer,
                    Aval = originalTraite.Aval,
                    Banque = originalTraite.Banque,
                    Protestable = originalTraite.Protestable,
                    MontantEnLettres = originalTraite.MontantEnLettres
                };

                _context.Traites.Add(duplicateTraite);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Lettre de change dupliquée avec succès!", id = duplicateTraite.Id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erreur: {ex.Message}" });
            }
        }
        // GET: /Traite/Print/id (vue HTML)
        public async Task<IActionResult> Print(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Create");
            }

            var traite = await _context.Traites.FindAsync(id);

            if (traite == null)
            {
                TempData["ErrorMessage"] = "Lettre de change non trouvée!";
                return RedirectToAction("Create");
            }

            // Formater le RIB ici
            traite.RIB = FormaterRIB(traite.RIB);

            return View(traite);
        }

        private string FormaterRIB(string rib)
        {
            if (string.IsNullOrWhiteSpace(rib))
                return rib;

            // Extraire uniquement les chiffres
            var chiffres = new StringBuilder();
            foreach (char c in rib)
            {
                if (char.IsDigit(c))
                {
                    chiffres.Append(c);
                }
            }

            string ribNettoye = chiffres.ToString();

            // Vérifier la longueur
            if (ribNettoye.Length != 20)
            {
                if (ribNettoye.Length > 20)
                {
                    ribNettoye = ribNettoye.Substring(0, 20);
                }
                else
                {
                    ribNettoye = ribNettoye.PadLeft(20, '0');
                }
            }

            // Construction du RIB formaté avec &nbsp; (espaces non-breaking)
            var resultat = new StringBuilder();
            resultat.Append(ribNettoye.Substring(0, 2));        // 2 premiers
            resultat.Append("&nbsp;"); // 6 espaces HTML
            resultat.Append(ribNettoye.Substring(2, 3));        // 3 suivants
            resultat.Append("&nbsp;&nbsp;&nbsp"); // 6 espaces HTML
            resultat.Append(ribNettoye.Substring(5, 13));       // 13 suivants
            resultat.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp"); // 6 espaces HTML
            resultat.Append(ribNettoye.Substring(18, 2));       // 2 derniers

            return resultat.ToString();
        }
        // ===== MÉTHODES UTILITAIRES =====

        private void AddTableRow(PdfPTable table, string label, string value, bool boldLabel = false)
        {
            var labelFont = boldLabel ?
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11) :
                FontFactory.GetFont(FontFactory.HELVETICA, 11);

            var labelCell = new PdfPCell(new Phrase(label, labelFont));
            labelCell.Border = Rectangle.NO_BORDER;
            labelCell.Padding = 5;
            table.AddCell(labelCell);

            var valueCell = new PdfPCell(new Phrase(value,
                FontFactory.GetFont(FontFactory.HELVETICA, 11)));
            valueCell.Border = Rectangle.NO_BORDER;
            valueCell.Padding = 5;
            table.AddCell(valueCell);
        }
    }
}