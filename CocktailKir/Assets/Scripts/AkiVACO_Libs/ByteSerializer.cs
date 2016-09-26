using System.Runtime.InteropServices;
using System.Collections;

namespace AkiVACO
{
    namespace Serializer
    {
        public interface IByteSerializer
        {
            byte[] ToBytes();
        }

        public class ByteSerializer
            : IByteSerializer
        {
            protected ArrayList m_array = null;

            public byte[] bytes
            {
                get { return ToBytes(); }
            }

            public byte[] ToBytes()
            {
                return (byte[])(m_array.ToArray(typeof(byte)));
            }

            ~ByteSerializer()
            {
                m_array = null;
            }

            public ByteSerializer()
            {
                m_array = new ArrayList();
            }

            public ByteSerializer(byte[] args)
            {
                Initialize(args);
            }

            public void Initialize(byte[] args)
            {
                m_array = null;
                m_array = new ArrayList(args);
            }

            public void Clear()
            {
                m_array = new ArrayList();
            }

            public void Add(sbyte val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(byte val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(short val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(ushort val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(int val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(uint val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(long val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(ulong val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(float val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(double val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(char val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(string val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

        }   // End of class ByteSerializer

        public class ByteDeserializer : IByteSerializer
        {
            protected byte[] m_bytes = null;
            protected int m_index = 0;

            public byte[] bytes
            {
                get { return ToBytes(); }
            }

            public byte[] ToBytes()
            {
                return m_bytes;
            }

            public ByteDeserializer(byte[] args)
            {
                m_bytes = args;
                m_index = 0;
            }

            ~ByteDeserializer()
            {
                m_bytes = null;
            }

            public void Reset()
            {
                m_index = 0;
            }

            public sbyte GetSByte()
            {
                sbyte ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public byte GetByte()
            {
                byte ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public short GetShort()
            {
                short ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public ushort GetUShort()
            {
                ushort ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public int GetInt()
            {
                int ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public uint GetUInt()
            {
                uint ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public long GetLong()
            {
                long ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public ulong GetULong()
            {
                ulong ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public float GetFloat()
            {
                float ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public double GetDouble()
            {
                double ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public char GetChar()
            {
                char ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public string GetString()
            {
                string ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public sbyte PopSByte()
            {
                sbyte ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public byte PopByte()
            {
                byte ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public short PopShort()
            {
                short ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public ushort PopUShort()
            {
                ushort ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public int PopInt()
            {
                int ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public uint PopUInt()
            {
                uint ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public long PopLong()
            {
                long ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public ulong PopULong()
            {
                ulong ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public float PopFloat()
            {
                float ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public double PopDouble()
            {
                double ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public char PopChar()
            {
                char ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public string PopString()
            {
                string ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

        }   // End of class ByteDeserializer

    }   // End of namespace Serializer

}