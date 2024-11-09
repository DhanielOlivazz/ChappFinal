using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChappFinal.Models.DTOs
{
    public class DTO_Post
    {
        public string title { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public byte[] post_img_file { get; set; }
        public DateTime publication_date { get; set; }
        public string[] categories { get; set; }
        public float min_budget { get; set; }
        public float? max_budget { get; set; }
    }
}
