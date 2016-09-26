using System.Runtime.InteropServices;

namespace AkiVACO
{
    namespace Unions
    {
        /// <summary>
        /// Union sbyte-byte[1]
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = sizeof(sbyte))]
        public struct UnionSByte
        {
            public const int Size = sizeof(sbyte);

            // value
            [FieldOffset(0)]
            private sbyte m_value;

            // byte
            [FieldOffset(0)]
            public byte b0;

            public sbyte value
            {
                set { m_value = value; }
                get { return m_value; }
            }

            public byte[] bytes
            {
                get { return new byte[Size] { b0 }; }
            }

            public int SetBytes(byte[] table, int index)
            {
                if (table == null)
                {
                    throw new System.ArgumentNullException();
                }
                if (table.Length < index + Size)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                b0 = table[index + 0];

                return Size;
            }

            public static byte[] ToBytes(sbyte val)
            {
                UnionSByte bt = new UnionSByte();
                bt.value = val;
                return bt.bytes;
            }

            public static sbyte ToValue(byte[] table, int index)
            {
                UnionSByte bt = new UnionSByte();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }

        /// <summary>
        /// Union short-byte[2]
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = sizeof(short))]
        public struct UnionShort
        {
            public const int Size = sizeof(short);

            // value
            [FieldOffset(0)]
            private short m_value;

            // bytes
            [FieldOffset(0)]
            public byte b0;
            [FieldOffset(1)]
            public byte b1;

            public short value
            {
                set { m_value = value; }
                get { return m_value; }
            }

            public byte[] bytes
            {
                get { return new byte[Size] { b0, b1 }; }
            }

            public int SetBytes(byte[] table, int index)
            {
                if (table == null)
                {
                    throw new System.ArgumentNullException();
                }
                if (table.Length < index + Size)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                b0 = table[index + 0];
                b1 = table[index + 1];

                return Size;
            }

            public static byte[] ToBytes(short val)
            {
                UnionShort bt = new UnionShort();
                bt.value = val;
                return bt.bytes;
            }

            public static short ToValue(byte[] table, int index)
            {
                UnionShort bt = new UnionShort();
                bt.SetBytes(table, index);
                return bt.value;
            }

        }

        /// <summary>
        /// Union int-byte[4]
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = sizeof(int))]
        public struct UnionInt
        {
            public const int Size = sizeof(int);

            // value
            [FieldOffset(0)]
            private int m_value;

            // bytes
            [FieldOffset(0)]
            public byte b0;
            [FieldOffset(1)]
            public byte b1;
            [FieldOffset(2)]
            public byte b2;
            [FieldOffset(3)]
            public byte b3;

            public int value
            {
                set { m_value = value; }
                get { return m_value; }
            }

            public byte[] bytes
            {
                get { return new byte[Size] { b0, b1, b2, b3 }; }
            }

            public int SetBytes(byte[] table, int index)
            {
                if (table == null)
                {
                    throw new System.ArgumentNullException();
                }
                if (table.Length < index + Size)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                b0 = table[index + 0];
                b1 = table[index + 1];
                b2 = table[index + 2];
                b3 = table[index + 3];

                return Size;
            }

            public static byte[] ToBytes(int val)
            {
                UnionInt bt = new UnionInt();
                bt.value = val;
                return bt.bytes;
            }

            public static int ToValue(byte[] table, int index)
            {
                UnionInt bt = new UnionInt();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }

        /// <summary>
        /// Union long-byte[8]
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = sizeof(long))]
        public struct UnionLong
        {
            public const int Size = sizeof(long);

            // value
            [FieldOffset(0)]
            private long m_value;

            // bytes
            [FieldOffset(0)]
            public byte b0;
            [FieldOffset(1)]
            public byte b1;
            [FieldOffset(2)]
            public byte b2;
            [FieldOffset(3)]
            public byte b3;
            [FieldOffset(4)]
            public byte b4;
            [FieldOffset(5)]
            public byte b5;
            [FieldOffset(6)]
            public byte b6;
            [FieldOffset(7)]
            public byte b7;

            public long value
            {
                set { m_value = value; }
                get { return m_value; }
            }

            public byte[] bytes
            {
                get { return new byte[Size] { b0, b1, b2, b3, b4, b5, b6, b7 }; }
            }

            public int SetBytes(byte[] table, int index)
            {
                if (table == null)
                {
                    throw new System.ArgumentNullException();
                }
                if (table.Length < index + Size)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                b0 = table[index + 0];
                b1 = table[index + 1];
                b2 = table[index + 2];
                b3 = table[index + 3];
                b4 = table[index + 4];
                b5 = table[index + 5];
                b6 = table[index + 6];
                b7 = table[index + 7];

                return Size;
            }

            public static byte[] ToBytes(long val)
            {
                UnionLong bt = new UnionLong();
                bt.value = val;
                return bt.bytes;
            }

            public static long ToValue(byte[] table, int index)
            {
                UnionLong bt = new UnionLong();
                bt.SetBytes(table, index);
                return bt.value;
            }

        }

        /// <summary>
        /// Union byte-byte[1]
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = sizeof(byte))]
        public struct UnionByte
        {
            public const int Size = sizeof(byte);

            // value
            [FieldOffset(0)]
            private byte m_value;

            // byte
            [FieldOffset(0)]
            public byte b0;

            public byte value
            {
                set { m_value = value; }
                get { return m_value; }
            }

            public byte[] bytes
            {
                get { return new byte[Size] { b0 }; }
            }

            public int SetBytes(byte[] table, int index)
            {
                if (table == null)
                {
                    throw new System.ArgumentNullException();
                }
                if (table.Length < index + Size)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                b0 = table[index + 0];

                return Size;
            }

            public static byte[] ToBytes(byte val)
            {
                UnionByte bt = new UnionByte();
                bt.value = val;
                return bt.bytes;
            }

            public static byte ToValue(byte[] table, int index)
            {
                UnionByte bt = new UnionByte();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }

        /// <summary>
        /// Union ushort-byte[2]
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = sizeof(ushort))]
        public struct UnionUShort
        {
            public const int Size = sizeof(ushort);

            // value
            [FieldOffset(0)]
            private ushort m_value;

            // bytes
            [FieldOffset(0)]
            public byte b0;
            [FieldOffset(1)]
            public byte b1;

            public ushort value
            {
                set { m_value = value; }
                get { return m_value; }
            }

            public byte[] bytes
            {
                get { return new byte[Size] { b0, b1 }; }
            }

            public int SetBytes(byte[] table, int index)
            {
                if (table == null)
                {
                    throw new System.ArgumentNullException();
                }
                if (table.Length < index + Size)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                b0 = table[index + 0];
                b1 = table[index + 1];

                return Size;
            }

            public static byte[] ToBytes(ushort val)
            {
                UnionUShort bt = new UnionUShort();
                bt.value = val;
                return bt.bytes;
            }

            public static ushort ToValue(byte[] table, int index)
            {
                UnionUShort bt = new UnionUShort();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }

        /// <summary>
        /// Union uint-byte[4]
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = sizeof(uint))]
        public struct UnionUInt
        {
            public const int Size = sizeof(uint);

            // value
            [FieldOffset(0)]
            private uint m_value;

            // bytes
            [FieldOffset(0)]
            public byte b0;
            [FieldOffset(1)]
            public byte b1;
            [FieldOffset(2)]
            public byte b2;
            [FieldOffset(3)]
            public byte b3;

            public uint value
            {
                set { m_value = value; }
                get { return m_value; }
            }

            public byte[] bytes
            {
                get { return new byte[Size] { b0, b1, b2, b3 }; }
            }

            public int SetBytes(byte[] table, int index)
            {
                if (table == null)
                {
                    throw new System.ArgumentNullException();
                }
                if (table.Length < index + Size)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                b0 = table[index + 0];
                b1 = table[index + 1];
                b2 = table[index + 2];
                b3 = table[index + 3];

                return Size;
            }

            public static byte[] ToBytes(uint val)
            {
                UnionUInt bt = new UnionUInt();
                bt.value = val;
                return bt.bytes;
            }

            public static uint ToValue(byte[] table, int index)
            {
                UnionUInt bt = new UnionUInt();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }

        /// <summary>
        /// Union ulong-byte[8]
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = sizeof(ulong))]
        public struct UnionULong
        {
            public const int Size = sizeof(ulong);

            // value
            [FieldOffset(0)]
            private ulong m_value;

            // bytes
            [FieldOffset(0)]
            public byte b0;
            [FieldOffset(1)]
            public byte b1;
            [FieldOffset(2)]
            public byte b2;
            [FieldOffset(3)]
            public byte b3;
            [FieldOffset(4)]
            public byte b4;
            [FieldOffset(5)]
            public byte b5;
            [FieldOffset(6)]
            public byte b6;
            [FieldOffset(7)]
            public byte b7;

            public ulong value
            {
                set { m_value = value; }
                get { return m_value; }
            }

            public byte[] bytes
            {
                get { return new byte[Size] { b0, b1, b2, b3, b4, b5, b6, b7 }; }
            }

            public int SetBytes(byte[] table, int index)
            {
                if (table == null)
                {
                    throw new System.ArgumentNullException();
                }
                if (table.Length < index + Size)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                b0 = table[index + 0];
                b1 = table[index + 1];
                b2 = table[index + 2];
                b3 = table[index + 3];
                b4 = table[index + 4];
                b5 = table[index + 5];
                b6 = table[index + 6];
                b7 = table[index + 7];

                return Size;
            }

            public static byte[] ToBytes(ulong val)
            {
                UnionULong bt = new UnionULong();
                bt.value = val;
                return bt.bytes;
            }

            public static ulong ToValue(byte[] table, int index)
            {
                UnionULong bt = new UnionULong();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }

        /// <summary>
        /// Union float-byte[4]
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = sizeof(float))]
        public struct UnionFloat
        {
            public const int Size = sizeof(float);

            // value
            [FieldOffset(0)]
            private float m_value;

            // bytes
            [FieldOffset(0)]
            public byte b0;
            [FieldOffset(1)]
            public byte b1;
            [FieldOffset(2)]
            public byte b2;
            [FieldOffset(3)]
            public byte b3;

            public float value
            {
                set { m_value = value; }
                get { return m_value; }
            }

            public byte[] bytes
            {
                get { return new byte[Size] { b0, b1, b2, b3 }; }
            }

            public int SetBytes(byte[] table, int index)
            {
                if (table == null)
                {
                    throw new System.ArgumentNullException();
                }
                if (table.Length < index + Size)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                b0 = table[index + 0];
                b1 = table[index + 1];
                b2 = table[index + 2];
                b3 = table[index + 3];

                return Size;
            }

            public static byte[] ToBytes(float val)
            {
                UnionFloat bt = new UnionFloat();
                bt.value = val;
                return bt.bytes;
            }

            public static float ToValue(byte[] table, int index)
            {
                UnionFloat bt = new UnionFloat();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }

        /// <summary>
        /// Union double-byte[8]
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = sizeof(double))]
        public struct UnionDouble
        {
            public const int Size = sizeof(double);

            // value
            [FieldOffset(0)]
            private double m_value;

            // bytes
            [FieldOffset(0)]
            public byte b0;
            [FieldOffset(1)]
            public byte b1;
            [FieldOffset(2)]
            public byte b2;
            [FieldOffset(3)]
            public byte b3;
            [FieldOffset(4)]
            public byte b4;
            [FieldOffset(5)]
            public byte b5;
            [FieldOffset(6)]
            public byte b6;
            [FieldOffset(7)]
            public byte b7;

            public double value
            {
                set { m_value = value; }
                get { return m_value; }
            }

            public byte[] bytes
            {
                get { return new byte[Size] { b0, b1, b2, b3, b4, b5, b6, b7 }; }
            }

            public int SetBytes(byte[] table, int index)
            {
                if (table == null)
                {
                    throw new System.ArgumentNullException();
                }
                if (table.Length < index + Size)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                b0 = table[index + 0];
                b1 = table[index + 1];
                b2 = table[index + 2];
                b3 = table[index + 3];
                b4 = table[index + 4];
                b5 = table[index + 5];
                b6 = table[index + 6];
                b7 = table[index + 7];

                return Size;
            }

            public static byte[] ToBytes(double val)
            {
                UnionDouble bt = new UnionDouble();
                bt.value = val;
                return bt.bytes;
            }

            public static double ToValue(byte[] table, int index)
            {
                UnionDouble bt = new UnionDouble();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }

        /// <summary>
        /// Union char-byte[2] : Unicode
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = sizeof(char))]
        public struct UnionChar
        {
            public const int Size = sizeof(char);

            // value
            [FieldOffset(0)]
            private char m_value;

            // bytes
            [FieldOffset(0)]
            public byte b0;
            [FieldOffset(1)]
            public byte b1;

            public char value
            {
                set { m_value = value; }
                get { return m_value; }
            }

            public byte[] bytes
            {
                get { return new byte[Size] { b0, b1 }; }
            }

            public int SetBytes(byte[] table, int index)
            {
                if (table == null)
                {
                    throw new System.ArgumentNullException();
                }
                if (table.Length < index + Size)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                b0 = table[index + 0];
                b1 = table[index + 1];

                return Size;
            }

            public static byte[] ToBytes(char val)
            {
                UnionChar bt = new UnionChar();
                bt.value = val;
                return bt.bytes;
            }

            public static char ToValue(byte[] table, int index)
            {
                UnionChar bt = new UnionChar();
                bt.SetBytes(table, index);
                return bt.value;
            }

        }

    }   // End of namespace Unions

}