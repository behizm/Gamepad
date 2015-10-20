using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Data.Entities
{
    public class SystemRequirment : BaseEntity
    {
        public bool IsMinimum { get; set; }

        public SystemPart SystemPart { get; set; }

        [Required, StringLength(100)]
        public string Value { get; set; }
    }

    public enum SystemPart
    {
        Os = 0,
        Cpu = 1,
        Ram = 2,
        Vga = 3,
        Hdd = 4,
        DirectX = 5,
        Psu = 6,
        Monitor = 8
    }
}
