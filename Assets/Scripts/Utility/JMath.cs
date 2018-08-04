using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JMath {
    
    public static float RoundToNearest(float numToRound, float roundTo) {
        return Mathf.Ceil(numToRound / roundTo) / roundTo;
    }

	public static Vector3 Average (Vector3[] vectors) {
		if (vectors.Length == 0) {
			return Vector3.zero;
		}
		float x = 0f;
		float y = 0f;
		float z = 0f;
		foreach (Vector3 pos in vectors) {
			x += pos.x;
			y += pos.y;
			z += pos.z;
		}
		return new Vector3(x / vectors.Length, y / vectors.Length, z / vectors.Length);
	}

	public static float Average (float[] floats) {
		if (floats.Length == 0) {
			return 0f;
		}
		float i = 0f;
		for (int j = 0; j < floats.Length; j++) {
			i += floats[j];
		}
		return i / floats.Length;
	}

    public static Vector2 RadianToVector2(float radian) {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree) {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}
