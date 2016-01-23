using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class Config : BaseEntity
    {
        [Required, StringLength(100, MinimumLength = 2)]
        [Index("IX_Key")]
        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime? EditDate { get; set; }
    }
}