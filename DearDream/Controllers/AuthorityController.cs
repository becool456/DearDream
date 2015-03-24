using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Web.Mvc;

using DearDreamBLL;
using DearDreamModels;
using ImplOfRepository;

namespace DearDream.Controllers
{
    public class AuthorityController : Controller
    {
        private SqlDbContext db = new SqlDbContext();
        [Ninject.Inject]
        private ISqlTreament _sqlTreatment { get; set; }
        [Ninject.Inject]
        public IAuthorityRepository repository { get; set; }
        // GET: Authority
        public ActionResult Index()
        {
            //_sqlTreatment.DealNewsNull();
            //_sqlTreatment.DealNewsDescriptionNull();

            return View(repository.Entities.ToList());
        }

        // GET: Authority/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authority authority = repository.GetByKey(id);
            if (authority == null)
            {
                return HttpNotFound();
            }
            return View(authority);
        }

        // GET: Authority/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authority/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ControllerName,ActionName,EventName")] Authority authority)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(authority);
                return RedirectToAction("Index");
            }

            return View(authority);
        }

        // GET: Authority/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authority authority = repository.GetByKey(id);
            if (authority == null)
            {
                return HttpNotFound();
            }
            return View(authority);
        }

        // POST: Authority/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ControllerName,ActionName,EventName")] Authority authority)
        {
            if (ModelState.IsValid)
            {
                repository.Update(authority);
                return RedirectToAction("Index");
            }
            return View(authority);
        }

        // GET: Authority/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authority authority = repository.GetByKey(id);
            if (authority == null)
            {
                return HttpNotFound();
            }
            return View(authority);
        }

        // POST: Authority/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Authority authority = repository.GetByKey(id);
            repository.Delete(authority);
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
