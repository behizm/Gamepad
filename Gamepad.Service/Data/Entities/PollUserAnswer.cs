using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class PollUserAnswer : BaseEntity
    {
        [Required, StringLength(15, MinimumLength = 7)]
        public string ParticipantIp { get; set; }

        [ForeignKey("Poll")]
        public Guid PollId { get; set; }
        public virtual Poll Poll { get; set; }

        [ForeignKey("PollChoice")]
        public Guid? PollChoiceId { get; set; }
        public virtual PollChoice PollChoice { get; set; }

        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
