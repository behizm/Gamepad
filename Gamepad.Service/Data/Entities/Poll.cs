using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    internal class Poll : BaseEntity
    {
        [Required, StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }

        [Required, StringLength(500, MinimumLength = 2)]
        public string Body { get; set; }

        [Required, StringLength(500, MinimumLength = 2)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public short MaxChoiceCount { get; set; }

        public DateTime? ExpiredDate { get; set; }


        [ForeignKey("Article")]
        public Guid? ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public ICollection<PollChoice> PollChoices { get; set; }

        public ICollection<PollUserAnswer> PollUserAnswers { get; set; }
    }
}
