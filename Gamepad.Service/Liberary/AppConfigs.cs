using System.Collections.Generic;
using System.Linq;

namespace Gamepad.Service.Liberary
{
    public static class AppConfigs
    {
        public static string Sitename { get; } = GpServices.Config.GetValue("Sitename");
        public static string SwearWords { get; } = GpServices.Config.GetValue("SwearWords");
        public static List<string> SwearWordList { get; } = SwearWords?.Split(':').Select(x => x.Trim()).ToList();
        public static string SwearWordsFa { get; } = GpServices.Config.GetValue("SwearWordsFa");
        public static List<string> SwearWordFaList { get; } = SwearWordsFa?.Split(':').Select(x => x.Trim()).ToList();
    }
}
