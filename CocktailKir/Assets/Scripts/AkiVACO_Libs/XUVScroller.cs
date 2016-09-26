
// Author     : Atuki Yoshinaga

using UnityEngine;
using System.Collections;

namespace AkiVACO
{

public class XUVScroller
{
    private Vector2 m_moveSpeed = Vector2.zero;
    public Material m_material = null;

    private const float CoordinateValue = 1.0f;

    public Vector2 moveSpeed
    {
        get { return m_moveSpeed; }
        set { m_moveSpeed = value; }
    }

    public float u
    {
        get { return m_moveSpeed.x; }
        set { m_moveSpeed.x = value; }
    }

    public float v
    {
        get { return m_moveSpeed.y; }
        set { m_moveSpeed.y = value; }
    }

    public XUVScroller(MeshRenderer renderer)
    {
        m_material = renderer.material;
        XLogger.LogValidObject(m_material, "Component is not bound <renderer.material>");

    }

    public XUVScroller(Material material)
    {
        m_material = material;
        XLogger.LogValidObject(m_material, "Component is not bound <renderer.material>");

    }

    public void Update()
    {
        Vector2 vec = m_material.GetTextureOffset("_MainTex");
        vec += (m_moveSpeed * XTime.deltaTime);
        Coordinate(ref vec);
        m_material.SetTextureOffset("_MainTex", vec);
    }

    void Coordinate(ref Vector2 vec)
    {
        if (vec.x > CoordinateValue)
        {
            vec.x -= CoordinateValue;
        }
        else if (vec.x < -CoordinateValue)
        {
            vec.x += CoordinateValue;
        }

        if (vec.y > CoordinateValue)
        {
            vec.y -= CoordinateValue;
        }
        else if (vec.y < -CoordinateValue)
        {
            vec.y += CoordinateValue;
        }
    }

}

}