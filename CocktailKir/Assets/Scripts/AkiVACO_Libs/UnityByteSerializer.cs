using UnityEngine;
using System.Collections;

namespace AkiVACO
{
    namespace Serializer
    {
        public class UnityByteSerializer : ByteSerializer
        {
            public UnityByteSerializer()
                : base()
            {
            }

            public UnityByteSerializer(byte[] args)
                : base(args)
            {
            }

            ~UnityByteSerializer()
            {
            }

            public void Add(Vector2 val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(Vector3 val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }

            public void Add(Vector4 val)
            {
                m_array.AddRange(Unions.UnionsUtil.ToBytes(val));
            }
        }

        public class UnityByteDeserializer : ByteDeserializer
        {
            public UnityByteDeserializer(byte[] args)
                : base(args)
            {
            }

            ~UnityByteDeserializer()
            {
            }

            public Vector2 GetVector2()
            {
                Vector2 ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public Vector3 GetVector3()
            {
                Vector3 ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public Vector4 GetVector4()
            {
                Vector4 ret;
                Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public Vector2 PopVector2()
            {
                Vector2 ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public Vector3 PopVector3()
            {
                Vector3 ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

            public Vector4 PopVector4()
            {
                Vector4 ret;
                m_index += Unions.UnionsUtil.ToValue(out ret, m_bytes, m_index);
                return ret;
            }

        }

    }   // namespace Serializer
}   // namespace AkiVACO

