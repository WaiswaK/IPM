using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IPM.API.Models;

namespace IPM.API.Controllers
{
    public class Pest_SolutionController : Controller
    {
        private IPMEntities db = new IPMEntities();

        // GET: Pest_Solution
        public ActionResult Index()
        {
            var pest_Solutions = db.Pest_Solutions.Include(p => p.Pest).Include(p => p.Solution);
            return View(pest_Solutions.ToList());
        }

        // GET: Pest_Solution/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pest_Solution pest_Solution = db.Pest_Solutions.Find(id);
            if (pest_Solution == null)
            {
                return HttpNotFound();
            }
            return View(pest_Solution);
        }

        // GET: Pest_Solution/Create
        public ActionResult Create()
        {
            ViewBag.P_ID = new SelectList(db.Pests, "P_ID", "Name");
            ViewBag.Sol_ID = new SelectList(db.Solutions, "Sol_ID", "Description");
            return View();
        }

        // POST: Pest_Solution/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PSol_ID,P_ID,Sol_ID")] Pest_Solution pest_Solution)
        {
            var query = db.Pest_Solutions.Count() + 1;
            string temp = "PSO-" + query;
            bool exist = false;
            try
            {
                var search = db.Pest_Solutions.Where(c => c.PSol_ID == temp).Single();
                exist = true;
            }
            catch
            {
                exist = false;

            }
            if (exist)
            {
                var all = db.Pest_Solutions.ToList();
                var dis = all.Last();
                pest_Solution.PSol_ID = "PSO-" + DataModel.Constants.NextNumber(dis.PSol_ID);
            }
            else
            {
                pest_Solution.PSol_ID = temp;
            }


            if (ModelState.IsValid)
            {
                db.Pest_Solutions.Add(pest_Solution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.P_ID = new SelectList(db.Pests, "P_ID", "Name", pest_Solution.P_ID);
            ViewBag.Sol_ID = new SelectList(db.Solutions, "Sol_ID", "Description", pest_Solution.Sol_ID);
            return View(pest_Solution);
        }

        // GET: Pest_Solution/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pest_Solution pest_Solution = db.Pest_Solutions.Find(id);
            if (pest_Solution == null)
            {
                return HttpNotFound();
            }
            ViewBag.P_ID = new SelectList(db.Pests, "P_ID", "Name", pest_Solution.P_ID);
            ViewBag.Sol_ID = new SelectList(db.Solutions, "Sol_ID", "Description", pest_Solution.Sol_ID);
            return View(pest_Solution);
        }

        // POST: Pest_Solution/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PSol_ID,P_ID,Sol_ID")] Pest_Solution pest_Solution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pest_Solution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.P_ID = new SelectList(db.Pests, "P_ID", "Name", pest_Solution.P_ID);
            ViewBag.Sol_ID = new SelectList(db.Solutions, "Sol_ID", "Description", pest_Solution.Sol_ID);
            return View(pest_Solution);
        }

        // GET: Pest_Solution/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pest_Solution pest_Solution = db.Pest_Solutions.Find(id);
            if (pest_Solution == null)
            {
                return HttpNotFound();
            }
            return View(pest_Solution);
        }

        // POST: Pest_Solution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Pest_Solution pest_Solution = db.Pest_Solutions.Find(id);
            db.Pest_Solutions.Remove(pest_Solution);
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
