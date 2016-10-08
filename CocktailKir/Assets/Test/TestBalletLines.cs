using UnityEngine;
using System.Collections;

public class TestBalletLines
{
    public static Vector3 Draw(Transform trans, float power, float mass, float timeSlice, float hitPointY)
    {
        float halfGravity = Physics.gravity.y * 0.5f;
        
        float s = power / mass;

        Vector3 forward = trans.forward;

        Vector3 start = trans.position;
        Vector3 prev = start;

        for (int i = 0; i < 10;++i )
        {
            float t = timeSlice * (i+1);
            Vector3 vecPower = forward * (t * s);
            float y = halfGravity * Mathf.Pow(t, 2);
            Vector3 vec = vecPower + Vector3.up * y;
            Vector3 nextPos = start + vec;
            Debug.DrawLine(prev, nextPos);

            if (nextPos.y <= hitPointY)
            {
                return CalcHitPoist(prev, nextPos, hitPointY);
            }

            prev = nextPos;
        }

        return prev;
    }

    private static Vector3 CalcHitPoist(Vector3 prev, Vector3 next, float hitPointY)
    {
        Vector3 vec = next - prev;
        if (vec.y == 0.0f)
        {
            return vec;
        }

        float f = hitPointY - prev.y;
        float rate = f / vec.y;

        return prev + (vec * rate);
    }

}
