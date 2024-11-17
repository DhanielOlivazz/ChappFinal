using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        public string publication_date { get; set; }
        //[JsonConverter(typeof(CategoriesConverter))]
        public List<string> categories { get; set; }
        public string min_budget { get; set; }
        public string max_budget { get; set; }
        public Users user { get; set; }

        public string CategoriesText => categories != null ? string.Join(", ", categories) : string.Empty;
    }

}
