using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class Article : BaseEntity
    {
        [Required, StringLength(100, MinimumLength = 2)]
        [Index("IX_Title")]
        public string Title { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        [Index("IX_Name")]
        public string Name { get; set; }
         
        public ArticleType Type { get; set; }

        public DateTime? EditDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public GamePlatform? Platform { get; set; }

        public short? SiteScore { get; set; }

        public short? UserScoresAverage { get; set; }

        

        [ForeignKey("Poster")]
        public Guid? PosterId { get; set; }
        public virtual File Poster { get; set; }

        public virtual ArticleInfo MoreInfo { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<Cast> Crews { get; set; }

        public virtual ICollection<File> ImageGallery { get; set; }

        public virtual ICollection<UserReview> UserReviews { get; set; }

        public virtual ICollection<ExternalRank> ExternalRates { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }

        public virtual ICollection<SystemRequirment> SystemRequirments { get; set; }

        public virtual ICollection<Community> Communities { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }

    public enum ArticleType
    {
        Game = 0,
        Movie = 1,
        Serial = 2,
        Hardware = 3
    }

    public enum GamePlatform
    {
        Windows = 0,
        XboxOn = 1,
        Ps4 = 2
    }
}
