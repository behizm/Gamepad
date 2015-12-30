using System.Security.Cryptography;
using System.Threading.Tasks;
using Gamepad.Service.Utilities.Cryptography;

namespace Gamepad.Service.Liberary
{
    class GamepadHashSystem
    {
        public static async Task<string> EncryptAsync(string code)
        {
            return
                await Symmetric<TripleDESCryptoServiceProvider>
                    .EncryptAsync(code, "gamepad:abxy:mobe", "jystk");

        }

        public static string Encrypt(string code)
        {
            return
                Symmetric<TripleDESCryptoServiceProvider>
                    .Encrypt(code, "gamepad:abxy:mobe", "jystk");

        }

        public static async Task<string> DecryptAsync(string token)
        {
            return
                await Symmetric<TripleDESCryptoServiceProvider>
                    .DecryptAsync(token, "gamepad:abxy:mobe", "jystk");

        }

        public static string Decrypt(string token)
        {
            return
                Symmetric<TripleDESCryptoServiceProvider>
                    .Decrypt(token, "gamepad:abxy:mobe", "jystk");

        }
    }
}
