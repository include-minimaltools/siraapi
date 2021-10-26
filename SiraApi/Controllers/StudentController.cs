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
    public class StudentController : Controller
    {
        UniModel db = new UniModel();

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Get()
        {
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = Json(from s in db.STUDENT.ToList() select new { s.CARNET, s.NAME, s.LASTNAME, s.ADDRESS, s.PHONE, s.CAREER})
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage InsertOrUpdate(STUDENT element)
        {
            try
            {
                STUDENT student = db.STUDENT.FirstOrDefault(x => x.CARNET == element.CARNET);
                if (student == null)
                {
                    db.STUDENT.Add(new STUDENT()
                    {
                        CARNET = element.CARNET,
                        NAME = element.NAME,
                        LASTNAME = element.LASTNAME,
                        ADDRESS = element.ADDRESS,
                        PHONE = element.PHONE,
                        CAREER = element.CAREER,
                        DATE_CREATE = element.DATE_CREATE = DateTime.Now,
                        USER_CREATE = "admin"
                    });
                }
                else
                {
                    student.CARNET = element.CARNET;
                    student.NAME = element.NAME;
                    student.LASTNAME = element.LASTNAME;
                    student.ADDRESS = element.ADDRESS;
                    student.PHONE = element.PHONE;
                    student.CAREER = element.CAREER;
                    student.DATE_UPDATE = DateTime.Now;
                    student.USER_UPDATE = "admin";
                    db.Entry(student).State = System.Data.Entity.EntityState.Modified;
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