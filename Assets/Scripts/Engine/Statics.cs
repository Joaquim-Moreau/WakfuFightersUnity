using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics 
{
    public static Vector3 GetVector(Vector3 selfPosition, Vector3 otherPosition)
    {
        Vector3 displacement = otherPosition - selfPosition;
        displacement.z = 0f;
        return displacement;
    }
    
    public static float GetDistance(Vector3 selfPosition, Vector3 otherPosition)
    {
        Vector3 displacement = GetVector(selfPosition, otherPosition);
        float distance = Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y);

        return distance;
    }

    public static Vector3 GetDirection(Vector3 selfPosition, Vector3 otherPosition)
    {
        Vector3 displacement = GetVector(selfPosition, otherPosition);
        if (displacement == new Vector3(0, 0, 0))
        {
            return new Vector3(0, 0, 0);
        }
        float distance = Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y);

        return displacement / distance;
    }
    
    public static Vector3 GetVector(Entity self, Entity other)
    {
        Vector3 displacement = other.transform.position - self.transform.position;;
        displacement.z = 0f;
        return displacement;
    }
    
    public static float GetDistance(Entity self, Entity other)
    {
        Vector3 displacement = GetVector(self, other);
        float distance = Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y);

        return distance;
    }
    
    public static Vector3 GetDirection(Entity self, Entity other)
    {
        Vector3 displacement = GetVector(self, other);
        if (displacement == new Vector3(0, 0, 0))
        {
            return new Vector3(0, 0, 0);
        }
        float distance = Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y);

        return displacement / distance;
    }
    
    public static Vector3 GetVector(Holder holder, Entity other)
    {
        Vector3 displacement = other.transform.position - holder.transform.position;;
        displacement.z = 0f;
        return displacement;
    }
    
    public static float GetDistance(Holder holder, Entity other)
    {
        Vector3 displacement = GetVector(holder, other);
        float distance = Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y);

        return distance;
    }

    public static Vector3 GetDirection(Holder holder, Entity other)
    {
        Vector3 displacement = GetVector(holder, other);
        if (displacement == new Vector3(0, 0, 0))
        {
            return new Vector3(0, 0, 0);
        }
        float distance = Mathf.Sqrt(displacement.x * displacement.x + displacement.y * displacement.y);

        return displacement / distance;
    }

    public static float Angle(Vector3 from, Vector3 to)
    {
        return Mathf.Atan2(from.x * to.y - from.y * to.x, from.x * to.x + from.y + to.y); 
    }
}

