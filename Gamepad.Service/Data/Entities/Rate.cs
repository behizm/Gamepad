using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class Rate : BaseEntity
    {
        public RateSource RateSource { get; set; }

        public RatingCategory Value { get; set; }

        [Required]
        public string Url { get; set; }



        public virtual ICollection<RateContent> RateContents { get; set; }

        [ForeignKey("Article")]
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }

    public enum RateSource
    {
        Esrb = 0,
        Pegi = 1,
        Farabi = 2
    }

    public enum RatingCategory
    {
        EsrbEc = 0,
        EsrbE = 1,
        EsrbE10 = 2,
        EsrbT = 3,
        EsrbM = 4,
        EsrbA = 5,
        EsrbRp = 6,
        Pegi3 = 10,
        Pegi7 = 11,
        Pegi12 = 12,
        Pegi16= 13,
        Pegi18 = 14
    }
}
