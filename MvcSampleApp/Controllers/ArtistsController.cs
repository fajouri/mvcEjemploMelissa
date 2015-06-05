using MvcSampleApp.Core.Models;
using MvcSampleApp.Data;
using MvcSampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSampleApp.Extensions;

namespace MvcSampleApp.Controllers
{
    public class ArtistsController : Controller
    {
        // GET: Artists
        public ActionResult Index()
        {
            using (var context = new MusicStoreContext())
            {
                var artists = context.Artists.OrderBy(m => m.Name).ToList();                
                return View(artists);
            }            
        }

        public ActionResult New()
        {
            ArtistViewModel artist = new ArtistViewModel();
            return View(artist);
        }

        [HttpPost]
        public ActionResult New(ArtistViewModel viewModel)
        {
            var artist = Helpers.ModelHelpers.ToModel<Artist, ArtistViewModel>(viewModel);
            using (var context = new MusicStoreContext())
            {
                var artists = context.Artists.Add(artist);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Update(int artistId)
        {
            using (var context = new MusicStoreContext())
            {
                var artist = context.Artists.FirstOrDefault(a => a.ArtistId.Equals(artistId));
                var viewModel = Helpers.ModelHelpers.ToModel<ArtistViewModel, Artist>(artist);
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Update(ArtistViewModel viewModel)
        {
            //var artist = Helpers.ModelHelpers.ToModel<Artist, ArtistViewModel>(viewModel);
            using (var context = new MusicStoreContext())
            {
                var currentArtist = context.Artists.FirstOrDefault(a => a.ArtistId.Equals(viewModel.ArtistId));
                currentArtist.Name = viewModel.Name;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int artistId)
        {
            using (var context = new MusicStoreContext())
            {
                var artist = context.Artists.FirstOrDefault(a => a.ArtistId.Equals(artistId));
                var viewModel = Helpers.ModelHelpers.ToModel<ArtistViewModel, Artist>(artist);
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteArtist(int artistId)
        {
            using (var context = new MusicStoreContext())
            {
                var artist = context.Artists.FirstOrDefault(a => a.ArtistId.Equals(artistId));
                context.Artists.Remove(artist);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}