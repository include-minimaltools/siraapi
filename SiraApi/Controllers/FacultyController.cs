using SiraApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SiraApi.Controllers
{
    public class FacultyController : Controller
    {
        UniModel db = new UniModel();

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Get()
        {
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = Json(
                    from f in db.FACULTY.ToList()
                    select new
                    {
                        f.ID,
                        f.DESCRIPTION
                    }
                    )
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage InsertOrUpdate(FACULTY element)
        {
            try
            {
                FACULTY faculty = db.FACULTY.FirstOrDefault(x => x.ID == element.ID);
                if (faculty == null)
                {
                    db.FACULTY.Add(new FACULTY()
                    {
                        ID = element.ID,
                        DESCRIPTION = element.DESCRIPTION,
                        DATE_CREATE = element.DATE_CREATE = DateTime.Now,
                        USER_CREATE = "admin"
                    });
                }
                else
                {
                    faculty.DESCRIPTION = element.DESCRIPTION;
                    faculty.DATE_UPDATE = DateTime.Now;
                    faculty.USER_UPDATE = "admin";
                    db.Entry(faculty).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}