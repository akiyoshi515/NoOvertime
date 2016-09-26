using UnityEngine;
using System.Collections;

namespace AkiVACO
{
    namespace Unions
    {

        public static class UnionsUtil
        {
            public static byte[] ToBytes(sbyte val)
            {
                return (UnionSByte.ToBytes(val));
            }

            public static byte[] ToBytes(byte val)
            {
                return (UnionByte.ToBytes(val));
            }

            public static byte[] ToBytes(short val)
            {
                return (UnionShort.ToBytes(val));
            }

            public static byte[] ToBytes(ushort val)
            {
                return (UnionUShort.ToBytes(val));
            }

            public static byte[] ToBytes(int val)
            {
                return (UnionInt.ToBytes(val));
            }

            public static byte[] ToBytes(uint val)
            {
                return (UnionUInt.ToBytes(val));
            }

            public static byte[] ToBytes(long val)
            {
                return (UnionLong.ToBytes(val));
            }

            public static byte[] ToBytes(ulong val)
            {
                return (UnionULong.ToBytes(val));
            }

            public static byte[] ToBytes(float val)
            {
                return (UnionFloat.ToBytes(val));
            }

            public static byte[] ToBytes(double val)
            {
                return (UnionDouble.ToBytes(val));
            }

            public static byte[] ToBytes(char val)
            {
                return (UnionChar.ToBytes(val));
            }

            // UnityUnions
            public static byte[] ToBytes(Vector2 val)
            {
                return (UnionVector2.ToBytes(val));
            }

            public static byte[] ToBytes(Vector3 val)
            {
                return (UnionVector3.ToBytes(val));
            }

            public static byte[] ToBytes(Vector4 val)
            {
                return (UnionVector4.ToBytes(val));
            }

            public static byte[] ToBytes(Quaternion val)
            {
                return (UnionQuaternion.ToBytes(val));
            }

            // Ex string : size = sizeof(short) + sizeof(char) * num
            public static byte[] ToBytes(string val)
            {
                short byteSize = (short)(val.Length * sizeof(char));
                byte[] bt = new byte[byteSize + sizeof(short)];
                int idx = 0;

                // Set ByteSize field
                UnionShort uniSize = new UnionShort();
                uniSize.value = byteSize;
                bt[idx++] = uniSize.b0;
                bt[idx++] = uniSize.b1;

                // Set String field
                UnionChar uniCh = new UnionChar();
                foreach (char ch in val.ToCharArray())
                {
                    uniCh.value = ch;
                    foreach (byte b in uniCh.bytes)
                    {
                        bt[idx++] = b;
                    }
                }

                return bt;
            }


            public static int ToValue(out sbyte ret, byte[] bytes, int index)
            {
                ret = UnionSByte.ToValue(bytes, index);
                return UnionSByte.Size;
            }

            public static int ToValue(out byte ret, byte[] bytes, int index)
            {
                ret = UnionByte.ToValue(bytes, index);
                return UnionByte.Size;
            }

            public static int ToValue(out short ret, byte[] bytes, int index)
            {
                ret = UnionShort.ToValue(bytes, index);
                return UnionShort.Size;
            }

            public static int ToValue(out ushort ret, byte[] bytes, int index)
            {
                ret = UnionUShort.ToValue(bytes, index);
                return UnionUShort.Size;
            }

            public static int ToValue(out int ret, byte[] bytes, int index)
            {
                ret = UnionInt.ToValue(bytes, index);
                return UnionInt.Size;
            }

            public static int ToValue(out uint ret, byte[] bytes, int index)
            {
                ret = UnionUInt.ToValue(bytes, index);
                return UnionUInt.Size;
            }

            public static int ToValue(out long ret, byte[] bytes, int index)
            {
                ret = UnionLong.ToValue(bytes, index);
                return UnionLong.Size;
            }

            public static int ToValue(out ulong ret, byte[] bytes, int index)
            {
                ret = UnionULong.ToValue(bytes, index);
                return UnionULong.Size;
            }

            public static int ToValue(out float ret, byte[] bytes, int index)
            {
                ret = UnionFloat.ToValue(bytes, index);
                return UnionFloat.Size;
            }

            public static int ToValue(out double ret, byte[] bytes, int index)
            {
                ret = UnionDouble.ToValue(bytes, index);
                return UnionDouble.Size;
            }

            public static int ToValue(out char ret, byte[] bytes, int index)
            {
                ret = UnionChar.ToValue(bytes, index);
                return UnionChar.Size;
            }

            // UnityUnions
            public static int ToValue(out Vector2 ret, byte[] bytes, int index)
            {
                ret = UnionVector2.ToValue(bytes, index);
                return UnionVector2.Size;
            }

            public static int ToValue(out Vector3 ret, byte[] bytes, int index)
            {
                ret = UnionVector3.ToValue(bytes, index);
                return UnionVector3.Size;
            }

            public static int ToValue(out Vector4 ret, byte[] bytes, int index)
            {
                ret = UnionVector4.ToValue(bytes, index);
                return UnionVector4.Size;
            }

            public static int ToValue(out Quaternion ret, byte[] bytes, int index)
            {
                ret = UnionQuaternion.ToValue(bytes, index);
                return UnionQuaternion.Size;
            }
            
            // Ex string : size = sizeof(short) + sizeof(char) * num
            public static int ToValue(out string ret, byte[] bytes, int index)
            {
                if (bytes.Length - index < sizeof(short))
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                short byteSize;
                int idx = UnionsUtil.ToValue(out byteSize, bytes, index);
                if (bytes.Length <  index + sizeof(short) + byteSize)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                int strLength = byteSize / sizeof(char);

                ret = "";

                char ch;
                for (int i = 0; i < strLength; ++i )
                {
                    idx += UnionsUtil.ToValue(out ch, bytes, idx);
                    ret += ch;
                }

                return (int)(byteSize);
            }


        }

    }   // End of namespace Unions
}   // End of namespace AkiVACO
