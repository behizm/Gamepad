using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class PollChoice : BaseEntity
    {
        [Required, StringLength(200)]
        public string Title { get; set; }

        public bool IsCurrect { get; set; }


        [ForeignKey("Poll")]
        public Guid PollId { get; set; }
        public virtual Poll Poll { get; set; }
    }
}
