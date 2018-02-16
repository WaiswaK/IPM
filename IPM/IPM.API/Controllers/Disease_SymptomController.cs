using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IPM.API.Models;

namespace IPM.API.Controllers
{
    public class Disease_SymptomController : Controller
    {
        private IPMEntities db = new IPMEntities();

        // GET: Disease_Symptom
        public ActionResult Index()
        {
            var disease_Symptoms = db.Disease_Symptoms.Include(d => d.Disease).Include(d => d.Part).Include(d => d.Symptom);
            return View(disease_Symptoms.ToList());
        }

        // GET: Disease_Symptom/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease_Symptom disease_Symptom = db.Disease_Symptoms.Find(id);
            if (disease_Symptom == null)
            {
                return HttpNotFound();
            }
            return View(disease_Symptom);
        }

        // GET: Disease_Symptom/Create
        public ActionResult Create()
        {
            ViewBag.D_ID = new SelectList(db.Diseases, "D_ID", "Name");
            ViewBag.Part_ID = new SelectList(db.Parts, "Part_ID", "Name");
            ViewBag.S_ID = new SelectList(db.Symptoms, "S_ID", "Description");
            return View();
        }

        // POST: Disease_Symptom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DS_ID,D_ID,S_ID,Part_ID")] Disease_Symptom disease_Symptom)
        {
            var query = db.Disease_Symptoms.Count() + 1;
            string temp = "DSY-" + query;
            bool exist = false;
            try
            {
                var search = db.Disease_Symptoms.Where(c => c.DS_ID == temp).Single();
                exist = true;
            }
            catch
            {
                exist = false;

            }
            if (exist)
            {
                var all = db.Disease_Symptoms.ToList();
                var dis = all.Last();
                disease_Symptom.DS_ID = "DSY-" + DataModel.Constants.NextNumber(dis.DS_ID);
            }
            else
            {
                disease_Symptom.DS_ID = temp;
            }
            if (ModelState.IsValid)
            {
                db.Disease_Symptoms.Add(disease_Symptom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.D_ID = new SelectList(db.Diseases, "D_ID", "Name", disease_Symptom.D_ID);
            ViewBag.Part_ID = new SelectList(db.Parts, "Part_ID", "Name", disease_Symptom.Part_ID);
            ViewBag.S_ID = new SelectList(db.Symptoms, "S_ID", "Description", disease_Symptom.S_ID);
            return View(disease_Symptom);
        }

        // GET: Disease_Symptom/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease_Symptom disease_Symptom = db.Disease_Symptoms.Find(id);
            if (disease_Symptom == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_ID = new SelectList(db.Diseases, "D_ID", "Name", disease_Symptom.D_ID);
            ViewBag.Part_ID = new SelectList(db.Parts, "Part_ID", "Name", disease_Symptom.Part_ID);
            ViewBag.S_ID = new SelectList(db.Symptoms, "S_ID", "Description", disease_Symptom.S_ID);
            return View(disease_Symptom);
        }

        // POST: Disease_Symptom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DS_ID,D_ID,S_ID,Part_ID")] Disease_Symptom disease_Symptom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disease_Symptom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.D_ID = new SelectList(db.Diseases, "D_ID", "Name", disease_Symptom.D_ID);
            ViewBag.Part_ID = new SelectList(db.Parts, "Part_ID", "Name", disease_Symptom.Part_ID);
            ViewBag.S_ID = new SelectList(db.Symptoms, "S_ID", "Description", disease_Symptom.S_ID);
            return View(disease_Symptom);
        }

        // GET: Disease_Symptom/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease_Symptom disease_Symptom = db.Disease_Symptoms.Find(id);
            if (disease_Symptom == null)
            {
                return HttpNotFound();
            }
            return View(disease_Symptom);
        }

        // POST: Disease_Symptom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Disease_Symptom disease_Symptom = db.Disease_Symptoms.Find(id);
            db.Disease_Symptoms.Remove(disease_Symptom);
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
