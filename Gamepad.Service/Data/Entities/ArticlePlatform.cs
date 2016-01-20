using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class ArticlePlatform : BaseEntity
    {
        public ArticlePlatform() : base()
        {
        }

        public ArticlePlatform(GamePlatform platform)
        {
            GamePlatform = platform;
        }

        public GamePlatform GamePlatform { get; set; }

        [ForeignKey("Article")]
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }

    public enum GamePlatform
    {
        Windows = 0,
        XboxOn = 1,
        Ps4 = 2
    }
}