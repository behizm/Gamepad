using System.ComponentModel.DataAnnotations;

namespace Gamepad.Service.Data.Entities
{
    public class SystemHardware : BaseEntity
    {
        public SystemHardwareType HardwareType { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }

    public enum SystemHardwareType
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
