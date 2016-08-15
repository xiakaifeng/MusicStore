﻿using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace MusicStore.Controllers
{

    [HandleError]
    public class StoreController : Controller
    {
        MusicStoreEntity db = new MusicStoreEntity();
        
        // GET: Store
        public ActionResult Index(string search, int page=1)
        {
            var pageSize = 16;
            IPagedList<Albums> list = null;
            if (string.IsNullOrEmpty(search))
            {
                list = db.Albums.OrderByDescending(a => a.AlbumId)
                    .ToPagedList(page, pageSize);
            }
            else
            {
                ViewBag.search = search;
                list = db.Albums.Where(a=>a.Title.Contains(search)).OrderByDescending(a => a.AlbumId)
                    .ToPagedList(page, pageSize);
            }

            return View(list);
        }

        public ActionResult Details(int id)
        {
            Albums a = db.Albums.Find(id);
            return View(a);
        }
    }
}