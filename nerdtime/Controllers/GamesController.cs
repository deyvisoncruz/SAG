using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using nerdtime.Models;
using System.IO;
using System.Drawing;

namespace nerdtime.Controllers
{
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.Categories);
            return View(games.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.CategorysId = new SelectList(db.GameCategories, "Id", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CategorysId,Cover,Img1,Img2,Img3,Video,Synopsis,Date_Launch,Language,Gamers")] Game game, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                game.CoverIByteData = ImgToByte(file);
                game.Img1 = ImgToByte(file1);
                
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategorysId = new SelectList(db.GameCategories, "Id", "Name", game.CategorysId);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategorysId = new SelectList(db.GameCategories, "Id", "Name", game.CategorysId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CategorysId,Cover,CoverIByteData,Img1,Img2,Img3,Video,Synopsis,Date_Launch,Language,Gamers")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategorysId = new SelectList(db.GameCategories, "Id", "Name", game.CategorysId);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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

    
       public byte[] ImgToByte(HttpPostedFileBase file)
        {
            byte[] result;
            if (file != null)
            {
                string extensao = Path.GetExtension(file.FileName);
                string[] extensoesValidas = new string[] { "jpg", "png" };

                if (!extensoesValidas.Contains(extensao))
                {
                    var img = Image.FromStream(file.InputStream);
                    Bitmap tempImage = new Bitmap(img, 100, 100);
                    ImageConverter _imageConverter = new ImageConverter();

                    byte[] xByte = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));

                    result = xByte;

                }
                else
                {
                    result = null;
                }
                
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
