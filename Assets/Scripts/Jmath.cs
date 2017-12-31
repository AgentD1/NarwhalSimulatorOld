using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Jmath {
    
    public static float RoundToNearest(float numToRound, float roundTo) {
        return Mathf.Ceil(numToRound / roundTo) / roundTo;
    }

	// found this on the internet somewhere probably goes with highscore getter on the unite page
    public static string Md5Sum(string strToEncrypt) {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++) {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
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
}
