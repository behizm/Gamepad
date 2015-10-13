using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    internal class ArticleInfo
    {
        [Required]
        public string Description { get; set; }

        public string Website { get; set; }

        public short? FinishTimeMain { get; set; }

        public short? FinishTimeAverage { get; set; }


        [Key, ForeignKey("Article")]
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
