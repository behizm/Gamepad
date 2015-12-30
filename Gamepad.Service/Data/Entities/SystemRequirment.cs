using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class SystemRequirment : BaseEntity
    {
        public SystemRequirmentType RequirmentType { get; set; }

        [ForeignKey("SystemHardware")]
        public Guid SystemHardwareId { get; set; }
        public virtual SystemHardware SystemHardware { get; set; }

        [ForeignKey("Article")]
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }

    public enum SystemRequirmentType
    {
        Minimum,
        Recommend
    }
}
