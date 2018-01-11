using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt.Application
{
    [ProtoContract]
    public class SensitiveValue
    {
        [ProtoMember(1)]
        public byte[] Vector { get; set; }

        [ProtoMember(2)]
        public byte[] Data { get; set; }

        internal void AllocateVector(byte[] iV)
        {
            Vector = new byte[iV.Length];
            Array.Copy(iV, Vector, iV.Length);
        }

        internal byte[] CopyVector()
        {
            var result = new byte[Vector.Length];
            Array.Copy(Vector, result, Vector.Length);
            return result;
        }
    }
}
