using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IPM.API.Models;

namespace IPM.API.Controllers
{
    public class Disease_SolutionController : Controller
    {
        private IPMEntities db = new IPMEntities();

        // GET: Disease_Solution
        public ActionResult Index()
        {
            var disease_Solutions = db.Disease_Solutions.Include(d => d.Disease).Include(d => d.Solution);
            return View(disease_Solutions.ToList());
        }

        // GET: Disease_Solution/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease_Solution disease_Solution = db.Disease_Solutions.Find(id);
            if (disease_Solution == null)
            {
                return HttpNotFound();
            }
            return View(disease_Solution);
        }

        // GET: Disease_Solution/Create
        public ActionResult Create()
        {
            ViewBag.D_ID = new SelectList(db.Diseases, "D_ID", "Name");
            ViewBag.Sol_ID = new SelectList(db.Solutions, "Sol_ID", "Description");
            return View();
        }

        // POST: Disease_Solution/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DSol_ID,D_ID,Sol_ID")] Disease_Solution disease_Solution)
        {
            var query = db.Disease_Solutions.Count() + 1;
            string temp = "DSO-" + query;
            bool exist = false;
            try
            {
                var search = db.Disease_Solutions.Where(c => c.DSol_ID == temp).Single();
                exist = true;
            }
            catch
            {
                exist = false;

            }
            if (exist)
            {
                var all = db.Disease_Solutions.ToList();
                var dis = all.Last();
                disease_Solution.DSol_ID = "DSO-" + DataModel.Constants.NextNumber(dis.DSol_ID);
            }
            else
            {
                disease_Solution.DSol_ID = temp;
            }
            if (ModelState.IsValid)
            {
                db.Disease_Solutions.Add(disease_Solution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.D_ID = new SelectList(db.Diseases, "D_ID", "Name", disease_Solution.D_ID);
            ViewBag.Sol_ID = new SelectList(db.Solutions, "Sol_ID", "Description", disease_Solution.Sol_ID);
            return View(disease_Solution);
        }

        // GET: Disease_Solution/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease_Solution disease_Solution = db.Disease_Solutions.Find(id);
            if (disease_Solution == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_ID = new SelectList(db.Diseases, "D_ID", "Name", disease_Solution.D_ID);
            ViewBag.Sol_ID = new SelectList(db.Solutions, "Sol_ID", "Description", disease_Solution.Sol_ID);
            return View(disease_Solution);
        }

        // POST: Disease_Solution/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DSol_ID,D_ID,Sol_ID")] Disease_Solution disease_Solution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disease_Solution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.D_ID = new SelectList(db.Diseases, "D_ID", "Name", disease_Solution.D_ID);
            ViewBag.Sol_ID = new SelectList(db.Solutions, "Sol_ID", "Description", disease_Solution.Sol_ID);
            return View(disease_Solution);
        }

        // GET: Disease_Solution/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease_Solution disease_Solution = db.Disease_Solutions.Find(id);
            if (disease_Solution == null)
            {
                return HttpNotFound();
            }
            return View(disease_Solution);
        }

        // POST: Disease_Solution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Disease_Solution disease_Solution = db.Disease_Solutions.Find(id);
            db.Disease_Solutions.Remove(disease_Solution);
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
