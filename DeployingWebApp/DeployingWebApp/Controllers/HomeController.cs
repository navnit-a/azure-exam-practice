﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace DeployingWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var env = ConfigurationManager.AppSettings.Get("Env");

            Trace.WriteLine("Request Received: HomeController");
            Trace.WriteLine("This is the app running in :: " + env);
            //return /*Content(env);*/
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}