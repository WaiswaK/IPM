using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IPM.API.Models;

namespace IPM.API.Controllers
{
    public class ControlsController : Controller
    {
        private IPMEntities db = new IPMEntities();

        // GET: Controls
        public ActionResult Index()
        {
            return View(db.Controls.ToList());
        }

        // GET: Controls/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Control control = db.Controls.Find(id);
            if (control == null)
            {
                return HttpNotFound();
            }
            return View(control);
        }

        // GET: Controls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Controls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "C_ID,Description")] Control control)
        {
            var query = db.Controls.Count() + 1;
            string temp = "C-" + query;
            bool exist = false;
            try
            {
                var search = db.Controls.Where(c => c.C_ID == temp).Single();
                exist = true;
            }
            catch
            {
                exist = false;

            }
            if (exist)
            {
                var all = db.Controls.ToList();
                var cont = all.Last();
                control.C_ID = "C-" + DataModel.Constants.NextNumber(cont.C_ID);
            }
            else
            {
                control.C_ID = temp;
            }
            if (ModelState.IsValid)
            {
                db.Controls.Add(control);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(control);
        }

        // GET: Controls/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Control control = db.Controls.Find(id);
            if (control == null)
            {
                return HttpNotFound();
            }
            return View(control);
        }

        // POST: Controls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "C_ID,Description")] Control control)
        {
            if (ModelState.IsValid)
            {
                db.Entry(control).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(control);
        }

        // GET: Controls/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Control control = db.Controls.Find(id);
            if (control == null)
            {
                return HttpNotFound();
            }
            return View(control);
        }

        // POST: Controls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Control control = db.Controls.Find(id);
            db.Controls.Remove(control);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
