using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class Post : BaseEntity
    {
        private string _title;
        [Required, StringLength(200, MinimumLength = 4)]
        [Index("IX_Title")]
        public string Title
        {
            get { return _title; }
            set
            {
                var title = value.Trim();
                while (title.Contains("  "))
                {
                    title = title.Replace("  ", " ");
                }
                Name = title.ToLower().Replace(" ", "_");
                _title = title;

            }
        }

        [Required, StringLength(200, MinimumLength = 4)]
        [Index("IX_Name")]
        public string Name { get; private set; }

        public PostType PostType { get; set; }

        [Required, StringLength(300, MinimumLength = 4)]
        public string Description { get; set; }

        public DateTime? EditDate { get; set; }

        public DateTime? PublishDate { get; set; }

        public long View { get; set; }

        public bool IsHidden { get; set; }

        public bool IsForbiddenComment { get; set; }



        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }
        public virtual User Author { get; set; }

        [ForeignKey("MainImage")]
        public Guid? MainImageId { get; set; }
        public virtual File MainImage { get; set; }

        [ForeignKey("Video")]
        public Guid? VideoId { get; set; }
        public virtual File Video { get; set; }

        public virtual PostContent PostContent { get; set; }

        public virtual ICollection<PostReview> ReviewScores { get; set; }

        public virtual ICollection<PostComment> PostComments { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<File> ImageGallery { get; set; }

        public virtual ICollection<File> Attachments { get; set; }
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
