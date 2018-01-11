using System;
using System.IO;
using System.Text;

namespace Encrypt.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceString = "123456789012345 This is a signing 98765 dafh with longer data this is a message!";

            Console.WriteLine($"Sample - {Crypto.GetKey()}");


            Console.WriteLine($"SRC : {sourceString}");
            var rawBytes = Encoding.UTF8.GetBytes(sourceString);


            var
            serverKey = "fa8c2uO4F8KB0DOfvQuf2w=="; //Example Server Key
            serverKey = "XatFFUYIk76n3DBuyQOUWom6pxAUhNwl";
            serverKey = "nJH2Kxs7msnnlFmthXTRkmHT6s+QbJnR8TtMetUK/qw=";

            var
            clientKey = "saumtCBlRTy9WWnqRHB8fg=="; //Example Client Key
            clientKey = "KzadjQKEqfLA3s+f6nIhhHhl0kvPafbm";
            clientKey = "hdsAWMkzmtCF4YbuMTvrkAOXKAMESuYj9zOprnZkqOc=";

            var sourceData = Crypto.Encrypt(serverKey, sourceString);
            var totalEncryptedBytes = sourceData.Data.Length;

            SensitiveValue destinationData;

            string output;

            using (var ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, sourceData);
                output = Convert.ToBase64String(ms.ToArray());

                ms.Seek(0, SeekOrigin.Begin);
                destinationData = ProtoBuf.Serializer.Deserialize<SensitiveValue>(ms);
            }

            Console.WriteLine($"OriginalBytes = {rawBytes.Length} Total EncryptedBytes = {totalEncryptedBytes}");
            Console.WriteLine($"Data to Store {output}");

            var resultMessage = Crypto.Decrypt(serverKey, destinationData);


            Console.WriteLine($"DST : {resultMessage} {resultMessage.Length}");
            Console.ReadLine();

        }
    }
}
