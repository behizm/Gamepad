using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class Poll : BaseEntity
    {
        [Required, StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }

        [Required, StringLength(500, MinimumLength = 2)]
        public string Body { get; set; }

        [StringLength(500, MinimumLength = 2)]
        public string Description { get; set; }

        [Range(1, 100)]
        public short MaxChoiceCount { get; set; }

        public bool IsVisible { get; set; }

        // expire date for participating in poll
        public DateTime? ExpiredDate { get; set; }


        [ForeignKey("Article")]
        public Guid? ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public ICollection<PollChoice> PollChoices { get; set; }

        public ICollection<PollUserAnswer> PollUserAnswers { get; set; }
    }
}
