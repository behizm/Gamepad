using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class Post : BaseEntity
    {
        [Required, StringLength(200, MinimumLength = 4)]
        public string Title { get; set; }

        [Required, StringLength(200, MinimumLength = 4)]
        public string Name { get; set; }

        public PostType PostType { get; set; }

        [Required, StringLength(300, MinimumLength = 4)]
        public string Description { get; set; }

        public DateTime? EditDate { get; set; }

        public DateTime? PublishDate { get; set; }

        public long View { get; set; }



        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }
        public virtual User Author { get; set; }

        [ForeignKey("MainImage")]
        public Guid? MainImageId { get; set; }
        public virtual File MainImage { get; set; }

        public virtual PostContent PostContent { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<PostReview> ReviewScores { get; set; }

        public virtual ICollection<File> Images { get; set; }

        public virtual File Video { get; set; }

        public virtual ICollection<File> Attachments { get; set; }

        public virtual ICollection<PostComment> PostComments { get; set; }
    }

    public enum PostType
    {
        News = 0,
        Report = 1,
        ImageGallery = 2,
        Video = 3,
        Interview = 4,
        Preview = 5,
        Review = 6,
    }
}
