using Exercice02.Models;
using Exercice02.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercice02.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Register()
        {
            // On récupère les niveaux d'études
            ViewBag.Levels = Inscription.getLevels();
            // Si on a des messages à afficher
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            // On retourne la vue
            return View();
        }

        [HttpPost]
        public ActionResult Register(Inscription student)
        {
            // On check si les champs sont remplis
            if (student.Name == null || student.Email == null)
            {
                TempData["Error"] = student.Name == null ? "Name is required" : student.Email == null ? "Email is required" : "Level is required";
                RedirectToAction("Register");
            }
            // On Cree le nouvel étudiant et on lui envoie un email de confirmation
            ErrorManager err = Inscription.AddStudent(student);
            // Si l'étudiant est bien enregistré on envoie un message de succès sinon on envoie un message d'erreur
            if (err.success)
            {
                TempData["Success"] = err.message;
            }
            else
            {
                TempData["Error"] = err.message;
            }
            return RedirectToAction("Register");
        }
    }
}