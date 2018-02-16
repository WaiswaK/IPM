using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IPM.API.Models;

namespace IPM.API.Controllers
{
    public class ChemicalsController : Controller
    {
        private IPMEntities db = new IPMEntities();

        // GET: Chemicals
        public ActionResult Index()
        {
            return View(db.Chemicals.ToList());
        }

        // GET: Chemicals/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chemical chemical = db.Chemicals.Find(id);
            if (chemical == null)
            {
                return HttpNotFound();
            }
            return View(chemical);
        }

        // GET: Chemicals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chemicals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Chem_ID,Name")] Chemical chemical)
        {
            var query = db.Chemicals.Count() + 1;
            string temp = "Chem-" + query;
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
                chemical.Chem_ID = "Chem-" + DataModel.Constants.NextNumber(cont.C_ID);
            }
            else
            {
                chemical.Chem_ID = temp;
            }
            if (ModelState.IsValid)
            {
                db.Chemicals.Add(chemical);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chemical);
        }

        // GET: Chemicals/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chemical chemical = db.Chemicals.Find(id);
            if (chemical == null)
            {
                return HttpNotFound();
            }
            return View(chemical);
        }

        // POST: Chemicals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Chem_ID,Name")] Chemical chemical)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chemical).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chemical);
        }

        // GET: Chemicals/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chemical chemical = db.Chemicals.Find(id);
            if (chemical == null)
            {
                return HttpNotFound();
            }
            return View(chemical);
        }

        // POST: Chemicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Chemical chemical = db.Chemicals.Find(id);
            db.Chemicals.Remove(chemical);
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
