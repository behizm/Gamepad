using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    internal class Cast : BaseEntity
    {
        public CastType CastType { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string Value { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string FaValue { get; set; }


        [ForeignKey("Article")]
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }

    public enum CastType
    {
        Developer = 0,
        Publisher = 1,
        Brand = 2
    }
}
