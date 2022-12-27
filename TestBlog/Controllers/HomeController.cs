using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestBlog.Models;
using TestBlog.Models.EntityFramework;

namespace TestBlog.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        BlogEntities db = new BlogEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Posts()
        {
            return View(db.Posts.ToList().Reverse<Post>().ToList());
        }

        [Route("/Home/Post/{ID}")]
        public ActionResult Post(int ID)
        {
            return View(db.Posts.Find(ID));
        }

        [HttpPost]
        public ActionResult Update(int ID, string Title, string Body, string Thumbnail, string Image)
        {
            Post post = db.Posts.Find(ID);
            if (post == null)
            {
                return HttpNotFound();
            }
            post.Title = Title;
            post.Body = Body;
            post.Thumbnail = Thumbnail;
            post.Image = Image;
            db.SaveChanges();
            return Redirect("/Home/Posts");
        }

        [HttpGet]
        [Route("/Home/Update/{ID}")]
        public ActionResult Update(int ID)
        {
            Post post = db.Posts.Find(ID);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [Route("/Home/Delete/{ID}")]
        public ActionResult Delete(int ID)
        {
            Post post = db.Posts.Find(ID);
            if (post == null)
            {
                return HttpNotFound();
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            return Redirect("/Home/Posts");
        }

        [HttpPost]
        public ActionResult AddPost(string Title, string Body, string Thumbnail, string Image)
        {
            Post postToAdd = new Post
            {
                Title = Title,
                Body = Body,
                CreatedDate = DateTime.Now,
                Thumbnail = Thumbnail,
                Image = Image,
            };
            db.Posts.Add(postToAdd);
            db.SaveChanges();
            return Redirect("/Home/Posts");
        }

        [HttpGet]
        public ActionResult AddPost()
        {
            return View();
        }

        public ActionResult Projects()
        {
            return View(db.Projects.ToList());
        }
    }
}