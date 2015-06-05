﻿using System;
using System.Web.Mvc;

namespace MvcSampleApp.Controllers
{
    using Core.Models;
    using Data;
    using MvcSampleApp.Models;
    using Services;

    public class GenresController : Controller
    {

        private readonly GenreService genreService;

        public GenresController()
        {
            genreService = new GenreService();
        }

        public ActionResult Index()
        {
            var genres = genreService.GetAll();
            return View(genres);
        }

        public ActionResult New()
        {
            var genre = new GenreViewModel();

            return View(genre);
        }

        [HttpPost]
        public ActionResult New(GenreViewModel viewModel)
        {
            try
            {
                var genre = Helpers.ModelHelpers.ToModel<Genre, GenreViewModel>(viewModel);
                genreService.Save(genre);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(viewModel);
            }
        }

        public ActionResult Update(int genreId)
        {
            var genre = genreService.GetById(genreId);
            var viewModel = Helpers.ModelHelpers.ToModel<GenreViewModel, Genre>(genre);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(GenreViewModel viewModel)
        {
            try
            {
                var genre = Helpers.ModelHelpers.ToModel<Genre, GenreViewModel>(viewModel);
                genreService.Save(genre);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(viewModel);
            }
        }

        public ActionResult Delete(int genreId)
        {
            var genre = genreService.GetById(genreId);

            var viewModel = Helpers.ModelHelpers.ToModel<GenreViewModel, Genre>(genre);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteGenre(GenreViewModel model)
        {
            try
            {
                genreService.Delete(model.GenreId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View("Delete", model);
            }
        }
	}
}