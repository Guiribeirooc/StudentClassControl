using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentClass.Controllers
{
    public class RelateClassController : Controller
    {
        // GET: RelateClassController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RelateClassController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RelateClassController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RelateClassController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RelateClassController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RelateClassController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RelateClassController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RelateClassController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
