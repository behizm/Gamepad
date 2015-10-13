using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    internal class PostContent
    {
        public string Content { get; set; }


        [Key, ForeignKey("Post")]
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
