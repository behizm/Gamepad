using System;
using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Data.Entities
{
    internal abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }

        [Key]
        public Guid Id { get; private set; }

        public DateTime CreateDate { get; private set; }
    }
}
