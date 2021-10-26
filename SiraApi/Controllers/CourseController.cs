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
    public class CourseController : Controller
    {
        UniModel db = new UniModel();

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Get()
        {
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = Json(from c in db.COURSE.ToList() select new { c.ID, c.DESCRIPTION, c.CREDITS, c.FRECUENCY, c.HOURS, c.ID_CAREER })
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult InsertOrUpdate(COURSE element)
        {
            try
            {
                COURSE course = db.COURSE.FirstOrDefault(x => x.ID == element.ID);
                if (course == null)
                {
                    db.COURSE.Add(new COURSE()
                    {
                        ID = element.ID,
                        DESCRIPTION = element.DESCRIPTION,
                        CREDITS = element.CREDITS,
                        FRECUENCY = element.FRECUENCY,
                        ID_CAREER = element.ID_CAREER,
                        HOURS = element.HOURS,
                        DATE_CREATE = DateTime.Now,
                        USER_CREATE = "admin"
                    });
                }
                else
                {
                    course.DESCRIPTION = element.DESCRIPTION;
                    course.CREDITS = element.CREDITS;
                    course.FRECUENCY = element.FRECUENCY;
                    course.ID_CAREER = element.ID_CAREER;
                    course.HOURS = element.HOURS;
                    course.DATE_UPDATE = DateTime.Now;
                    course.USER_UPDATE = "admin";
                    db.Entry(course).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();

                return new JsonResult()
                {
                    Data = HttpStatusCode.OK,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return new JsonResult()
                {
                    Data = HttpStatusCode.InternalServerError,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}