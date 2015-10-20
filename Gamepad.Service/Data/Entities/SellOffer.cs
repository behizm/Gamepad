using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class SellOffer : BaseEntity
    {
        public int Price { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public string Link { get; set; }



        [ForeignKey("Article")]
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }

        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }
        public virtual User Owner { get; set; }
    }
}
