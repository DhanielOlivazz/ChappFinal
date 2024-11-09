﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChappFinal.Models
{
    public class Post
    {
        public string user_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string url_img_post { get; set; }
        public DateTime publication_date { get; set; }
        public string[] categories { get; set; }
        public float min_budget { get; set; }
        public float? max_budget { get; set; }

    }
}
