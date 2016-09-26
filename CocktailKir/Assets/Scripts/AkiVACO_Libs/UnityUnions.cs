using System.Runtime.InteropServices;
using UnityEngine;

namespace AkiVACO
{
    namespace Unions
    {
        /// <summary>
        /// Union Vector2-byte[8*2(x,y)]
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct UnionVector2
        {
            public const int Size = UnionFloat.Size * 2;

            // value
            private UnionFloat m_x;
            private UnionFloat m_y;

            public Vector2 value
            {
                set
                {
                    m_x.value = value.x;
                    m_y.value = value.y;
                }

                get
                {
                    return new Vector2(
                      m_x.value,
                      m_y.value);
                }
            }

            public byte[] bytes
            {
                get
                {
                    return new byte[Size] 
                    {
                        m_x.b0, m_x.b1, m_x.b2, m_x.b3,
                        m_y.b0, m_y.b1, m_y.b2, m_y.b3,
                    };
                }
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

                int idx = index;

                idx += m_x.SetBytes(table, idx);
                idx += m_y.SetBytes(table, idx);

                return Size;
            }

            public static byte[] ToBytes(Vector2 val)
            {
                UnionVector2 bt = new UnionVector2();
                bt.value = val;
                return bt.bytes;
            }

            public static Vector2 ToValue(byte[] table, int index)
            {
                UnionVector2 bt = new UnionVector2();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }   // End of UnionVector2

        /// <summary>
        /// Union Vector3-byte[8*3(x,y,z,w)]
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct UnionVector3
        {
            public const int Size = UnionFloat.Size * 3;

            // value
            private UnionFloat m_x;
            private UnionFloat m_y;
            private UnionFloat m_z;

            public Vector3 value
            {
                set
                {
                    m_x.value = value.x;
                    m_y.value = value.y;
                    m_z.value = value.z;
                }

                get
                {
                    return new Vector3(
                      m_x.value,
                      m_y.value,
                      m_z.value);
                }
            }

            public byte[] bytes
            {
                get
                {
                    return new byte[Size] 
                    {
                        m_x.b0, m_x.b1, m_x.b2, m_x.b3,
                        m_y.b0, m_y.b1, m_y.b2, m_y.b3,
                        m_z.b0, m_z.b1, m_z.b2, m_z.b3,
                    };
                }
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

                int idx = index;

                idx += m_x.SetBytes(table, idx);
                idx += m_y.SetBytes(table, idx);
                idx += m_z.SetBytes(table, idx);
                
                return Size;
            }

            public static byte[] ToBytes(Vector3 val)
            {
                UnionVector3 bt = new UnionVector3();
                bt.value = val;
                return bt.bytes;
            }

            public static Vector3 ToValue(byte[] table, int index)
            {
                UnionVector3 bt = new UnionVector3();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }   // End of UnionVector3

        /// <summary>
        /// Union Vector4-byte[8*4(x,y,z,w)]
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct UnionVector4
        {
            public const int Size = UnionFloat.Size * 4;

            // value
            private UnionFloat m_x;
            private UnionFloat m_y;
            private UnionFloat m_z;
            private UnionFloat m_w;

            public Vector4 value
            {
                set
                {
                    m_x.value = value.x;
                    m_y.value = value.y;
                    m_z.value = value.z;
                    m_w.value = value.w;
                }

                get
                {
                    return new Vector4(
                      m_x.value,
                      m_y.value,
                      m_z.value,
                      m_w.value);
                }
            }

            public byte[] bytes
            {
                get
                {
                    return new byte[Size] 
                    {
                        m_x.b0, m_x.b1, m_x.b2, m_x.b3,
                        m_y.b0, m_y.b1, m_y.b2, m_y.b3,
                        m_z.b0, m_z.b1, m_z.b2, m_z.b3,
                        m_w.b0, m_w.b1, m_w.b2, m_w.b3,
                    };
                }
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

                int idx = index;

                idx += m_x.SetBytes(table, idx);
                idx += m_y.SetBytes(table, idx);
                idx += m_z.SetBytes(table, idx);
                idx += m_w.SetBytes(table, idx);

                return Size;
            }

            public static byte[] ToBytes(Vector4 val)
            {
                UnionVector4 bt = new UnionVector4();
                bt.value = val;
                return bt.bytes;
            }

            public static Vector4 ToValue(byte[] table, int index)
            {
                UnionVector4 bt = new UnionVector4();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }   // End of UnionVector4

        /// <summary>
        /// Union Quaternion-byte[8*4(x,y,z,w)]
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct UnionQuaternion
        {
            public const int Size = UnionFloat.Size * 4;

            // value
            private UnionFloat m_x;
            private UnionFloat m_y;
            private UnionFloat m_z;
            private UnionFloat m_w;

            public Quaternion value
            {
                set
                {
                    m_x.value = value.x;
                    m_y.value = value.y;
                    m_z.value = value.z;
                    m_w.value = value.w;                    
                }

                get
                {
                    return new Quaternion(
                      m_x.value,
                      m_y.value,
                      m_z.value,
                      m_w.value);
                }
            }

            public byte[] bytes
            {
                get
                {
                    return new byte[Size] 
                    {
                        m_x.b0, m_x.b1, m_x.b2, m_x.b3,
                        m_y.b0, m_y.b1, m_y.b2, m_y.b3,
                        m_z.b0, m_z.b1, m_z.b2, m_z.b3,
                        m_w.b0, m_w.b1, m_w.b2, m_w.b3,
                    };
                }
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

                int idx = index;

                idx += m_x.SetBytes(table, idx);
                idx += m_y.SetBytes(table, idx);
                idx += m_z.SetBytes(table, idx);
                idx += m_w.SetBytes(table, idx);

                return Size;
            }

            public static byte[] ToBytes(Quaternion val)
            {
                UnionQuaternion bt = new UnionQuaternion();
                bt.value = val;
                return bt.bytes;
            }

            public static Quaternion ToValue(byte[] table, int index)
            {
                UnionQuaternion bt = new UnionQuaternion();
                bt.SetBytes(table, index);
                return bt.value;
            }
        }   // End of UnionQuaternion

    }   // End of namespace Unions

}