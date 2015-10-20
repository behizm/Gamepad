using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class File : BaseEntity
    {
        [Required, StringLength(50, MinimumLength = 2)]
        public string Title { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string Filename { get; set; }

        public int Size { get; set; }

        public FileType FileType { get; set; }

        public FileCategory Category { get; set; }

        [Required]
        public string Address { get; set; }

        public bool IsPublic { get; set; }

        public DateTime? EditDate { get; set; }



        [ForeignKey("Creator")]
        public Guid CreatorId { get; set; }
        public virtual User Creator { get; set; }
    }

    public enum FileType
    {
        Image = 0,
        Video = 1,
        Archive=2,
        WinApp = 3,
        AndroidApp = 4,
        IosApp = 5,
        Document = 6,
        Unknown = 99
    }

    public enum FileCategory
    {
        ArticlePoster = 0,
        ArticleImage = 1,
        ArticleVideo = 2,
        UserAvatar = 3,
    }
}
