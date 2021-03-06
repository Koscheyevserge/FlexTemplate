﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlexTemplate.DataAccessLayer.Entities;

namespace FlexTemplate.PresentationLayer.WebServices.Upload
{
    public class UploadController : FlexController
    {
        private FilesProvider FilesProvider { get; set; }
        private FlexTemplateContext Context { get; set; }

        public UploadController(ControllerServices services, FilesProvider filesProvider, FlexTemplateContext context) : base(services)
        {
            FilesProvider = filesProvider;
            Context = context;
        }

        #region Page
        /*[HttpPost]
        [Route("/api/upload/pagephoto/{pageName}")]
        public async Task UploadPagePhotoAsync(string pageName)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            var filename = Guid.NewGuid().ToString();
            await FilesProvider.SaveFileAsync(HttpContext.Request.Form.Files[0], $@"pages\{pageName}\heads", filename);
        }*/
        #endregion

        #region Place

        #region PlaceHead
        [HttpPost]
        [Route("/api/upload/placehead/{placeId}")]
        public async Task<string> UploadPlaceHeadPhotoAsync(string placeId)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return null;
            }
            var filename = Guid.NewGuid();
            var uri = await FilesProvider
                .SaveFileAsync(HttpContext.Request.Form.Files[0], $@"places\{placeId}\heads", filename.ToString());
            var head = new PlaceHeaderPhoto
            {
                BlobKey = Guid.Parse(placeId),
                IsActive = true,
                Uri = uri.ToString()
            };
            var place = await Context.Places.Include(p => p.Headers)
                .SingleOrDefaultAsync(p => p.BlobKey.ToString().ToUpperInvariant() == placeId.ToUpperInvariant());
            if (place != null)
            {
                place.Headers.ForEach(h => h.IsActive = false);
                place.Headers.Add(head);
            }
            else
            {
                Context.PlaceHeaderPhotos.Add(head);
            }
            await Context.SaveChangesAsync();
            return filename.ToString();
        }
        [HttpDelete]
        [Route("/api/upload/placehead/{placeId}")]
        public async Task DeletePlaceHeadPhotoAsync(string placeId)
        {
            var placeGallery = await Context.PlaceHeaderPhotos
                .Where(p => p.BlobKey.ToString().ToUpperInvariant() == placeId.ToUpperInvariant()).ToListAsync();
            placeGallery.ForEach(h => h.IsActive = false);
            await Context.SaveChangesAsync();
        }
        #endregion

        #region PlaceBanner
        [HttpPost]
        [Route("/api/upload/placebanner/{placeId}")]
        public async Task UploadBannerPhotoAsync(string placeId)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            var filename = Guid.NewGuid();
            var uri = await FilesProvider
                .SaveFileAsync(HttpContext.Request.Form.Files[0], $@"places\{placeId}\banners", filename.ToString());
            var banner = new PlaceBannerPhoto
            {
                BlobKey = Guid.Parse(placeId),
                IsActive = true,
                Uri = uri.ToString()
            };
            var place = await Context.Places.Include(p => p.Banners)
                .SingleOrDefaultAsync(p => p.BlobKey.ToString().ToUpperInvariant() == placeId.ToUpperInvariant());
            if (place != null)
            {
                place.Banners.ForEach(h => h.IsActive = false);
                place.Banners.Add(banner);
            }
            else
            {
                Context.PlaceBannerPhotos.Add(banner);
            }
            await Context.SaveChangesAsync();
        }
        [HttpDelete]
        [Route("/api/upload/placebanner/{placeId}")]
        public async Task DeleteBannerPhotoAsync(string placeId)
        {
            var placeBanners = await Context.PlaceBannerPhotos
                .Where(p => p.BlobKey.ToString().ToUpperInvariant() == placeId.ToUpperInvariant()).ToListAsync();
            placeBanners.ForEach(h => h.IsActive = false);
            await Context.SaveChangesAsync();
        }
        #endregion

        #region PlaceGallery
        [HttpPost]
        [Route("/api/upload/newplace/{placeId}")]
        public async Task<string> UploadPlaceGalleryPhotoAsync(string placeId)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return null;
            }
            var filename = Guid.NewGuid();
            var uri = await FilesProvider
                .SaveFileAsync(HttpContext.Request.Form.Files[0], $@"places\{placeId}\gallery", filename.ToString());
            var photo = new PlaceGalleryPhoto
            {
                BlobKey = Guid.Parse(placeId),
                IsActive = true,
                Uri = uri.ToString()
            };
            var place = await Context.Places.Include(p => p.Gallery)
                .SingleOrDefaultAsync(p => p.BlobKey.ToString().ToUpperInvariant() == placeId.ToUpperInvariant());            
            if (place != null)
            {
                place.Gallery.ForEach(h => h.IsActive = false);
                place.Gallery.Add(photo);
            }
            else
            {
                Context.PlaceGalleryPhotos.Add(photo);
            }
            await Context.SaveChangesAsync();
            return filename.ToString();
        }
        [HttpDelete]
        [Route("/api/upload/newplace/{fileDescriptor}")]
        public async Task DeletePlacePhotoAsync(string fileDescriptor)
        {
            var placeGallery = await Context.PlaceGalleryPhotos
                .Where(p => p.Uri.ToString().ToUpperInvariant().Contains(fileDescriptor.ToUpperInvariant())).ToListAsync();
            placeGallery.ForEach(h => h.IsActive = false);
            await Context.SaveChangesAsync();
        }
        #endregion

        #endregion

        #region Blog

        #region BlogHead
        [HttpPost]
        [Route("/api/upload/bloghead/{blogId}")]
        public async Task<string> UploadBlogHeadPhotoAsync(string blogId)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return null;
            }
            var filename = Guid.NewGuid();
            var uri = await FilesProvider
                .SaveFileAsync(HttpContext.Request.Form.Files[0], $@"blogs\{blogId}\heads", filename.ToString());
            var photo = new BlogPhoto
            {
                BlobKey = Guid.Parse(blogId),
                IsActive = true,
                Uri = uri.ToString()
            };
            var blog = await Context.Blogs.Include(p => p.Photos)
                .SingleOrDefaultAsync(p => p.BlobKey.ToString().ToUpperInvariant() == blogId.ToUpperInvariant());
            if (blog != null)
            {
                blog.Photos.ForEach(h => h.IsActive = false);
                blog.Photos.Add(photo);
            }
            else
            {
                Context.BlogPhotos.Add(photo);
            }
            await Context.SaveChangesAsync();
            return filename.ToString();          
        }
        [HttpDelete]
        [Route("/api/upload/bloghead/{blogId}")]
        public async Task DeleteBlogHeadPhotoAsync(string blogId)
        {
            var blog = await Context.Blogs.Include(p => p.Photos)
                .SingleOrDefaultAsync(p => p.BlobKey.ToString().ToUpperInvariant() == blogId.ToUpperInvariant());
            if (blog != null)
            {
                blog.Photos.ForEach(h => h.IsActive = false);
            }
            else
            {
                Context.BlogPhotos
                    .Where(bp => bp.BlobKey == Guid.Parse(blogId)).ToList()
                    .ForEach(h => h.IsActive = false);
            }
            await Context.SaveChangesAsync();
        }
        #endregion

        #endregion

        #region Product

        #region ProductHead
        [HttpPost]
        [Route("/api/upload/producthead/{fileDescriptor}")]
        public async Task<string> UploadProductHeadPhoto(string fileDescriptor)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return null;
            }
            var filename = Guid.NewGuid();
            var uri = await FilesProvider.SaveFileAsync(HttpContext.Request.Form.Files[0], $@"products\{fileDescriptor}\heads", filename.ToString());
            var photo = new ProductPhoto
            {
                BlobKey = filename,
                IsActive = true,
                Uri = uri.ToString()
            };            
            var product = await Context.Products.Include(p => p.Photos)
                .SingleOrDefaultAsync(p => p.BlobKey.ToString().ToUpperInvariant() == fileDescriptor.ToUpperInvariant());
            if (product != null)
            {
                product.Photos.ForEach(h => h.IsActive = false);
                product.Photos.Add(photo);
            }
            else
            {
                Context.ProductPhotos.Add(photo);
            }
            await Context.SaveChangesAsync();
            return filename.ToString();
        }
        [HttpDelete]
        [Route("/api/upload/producthead/{fileDescriptor}")]
        public async Task DeleteProductHeadAsync(string fileDescriptor)
        {
            var products = await Context.ProductPhotos
                .Where(p => p.BlobKey.ToString().ToUpperInvariant() == fileDescriptor.ToUpperInvariant()).ToListAsync();
            products.ForEach(h => h.IsActive = false);
            await Context.SaveChangesAsync();
        }
        #endregion

        #endregion
    }
}
