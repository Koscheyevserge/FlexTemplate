﻿using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.WebServices.Home.Blog
{
    public class ViewModel
    {
        public int Id { get; set; }
        public HtmlString Text { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsAuthor { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsModerated { get; set; }
        public string IsModeratedText { get; set; }
        public string BannerPath { get; set; }
        public string AuthorDisplayName { get; set; }
        public string AuthorPhotoPath { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
        public string DeclineText { get; set; }
        public string AcceptText { get; set; }
    }
}
