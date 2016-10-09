using UnityEngine;
using System.Collections;

public class TestBalletLines
{
    public static void Draw(
        Transform trans, float power, float mass, float timeSlice, float hitPointY,
        Transform transHitPoint, System.Action<int, Vector3> lineRender)
    {
        float halfGravity = Physics.gravity.y * 0.5f;
        
        float s = power / mass;

        Vector3 forward = trans.forward;

        Vector3 start = trans.position;
        Vector3 prev = start;

        Vector3 vecNearPos = transHitPoint.position;
        float minDistance = float.MaxValue;

        int idx = 0;
        lineRender.Invoke(idx++, start);

        for (int i = 0; i < 30; ++i)
        {
            float t = timeSlice * (i+1);
            Vector3 vecPower = forward * (t * s);
            float y = halfGravity * Mathf.Pow(t, 2);
            Vector3 vec = vecPower + Vector3.up * y;
            Vector3 nextPos = start + vec;
            lineRender.Invoke(idx++, nextPos);

            Vector3 offset = nextPos - prev;
            RaycastHit[] hitInfo = Physics.RaycastAll(prev, offset, offset.magnitude);
            foreach (RaycastHit hit in hitInfo)
            {
                if (hit.collider.tag != "Ballet")
                {
                    if (hit.distance < minDistance)
                    {
                        minDistance = hit.distance;
                        vecNearPos = hit.point;
                    }
                }
            }
            if (minDistance != float.MaxValue)
            {
                break;
            }
            
            prev = nextPos;
        }
        transHitPoint.position = vecNearPos;
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
