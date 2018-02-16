using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IPM.API.Models;

namespace IPM.API.Controllers
{
    public class PestsController : Controller
    {
        private IPMEntities db = new IPMEntities();

        // GET: Pests
        public ActionResult Index()
        {
            return View(db.Pests.ToList());
        }

        // GET: Pests/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pest pest = db.Pests.Find(id);
            if (pest == null)
            {
                return HttpNotFound();
            }
            return View(pest);
        }

        // GET: Pests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "P_ID,Name,About,Spread")] Pest pest)
        {
            var query = db.Pests.Count() + 1;
            string temp = "P-" + query;
            bool exist = false;
            try
            {
                var search = db.Pests.Where(c => c.P_ID == temp).Single();
                exist = true;
            }
            catch
            {
                exist = false;

            }
            if (exist)
            {
                var all = db.Pests.ToList();
                var dis = all.Last();
                pest.P_ID = "P-" + DataModel.Constants.NextNumber(dis.P_ID);
            }
            else
            {
                pest.P_ID = temp;
            }

            if (ModelState.IsValid)
            {
                db.Pests.Add(pest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pest);
        }

        // GET: Pests/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pest pest = db.Pests.Find(id);
            if (pest == null)
            {
                return HttpNotFound();
            }
            return View(pest);
        }

        // POST: Pests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "P_ID,Name,About,Spread")] Pest pest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pest);
        }

        // GET: Pests/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pest pest = db.Pests.Find(id);
            if (pest == null)
            {
                return HttpNotFound();
            }
            return View(pest);
        }

        // POST: Pests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Pest pest = db.Pests.Find(id);
            db.Pests.Remove(pest);
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
