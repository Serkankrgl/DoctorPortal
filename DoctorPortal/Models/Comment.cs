using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorPortal.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string CommentContent { get; set; }
        public Post Post { get; set; }
    }
}
