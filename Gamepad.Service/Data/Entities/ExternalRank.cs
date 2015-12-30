using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class ExternalRank : BaseEntity
    {
        public ExternalRankType Type { get; set; }

        [Range(0, 100)]
        [Index("IX_Score")]
        public short Score { get; set; }

        [Required]
        public string Url { get; set; }



        [ForeignKey("Article")]
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }

    public enum ExternalRankType
    {
        Metacritic = 0,
        Imdb = 1,
        Gamespot = 2
    }
}
