using UnityEngine;
using System.Collections;

public class PlaneTexture : MonoBehaviour
{
    public float rotateSpeed = 30f;
    public void Update()
    {
        var rot = Quaternion.Euler(0, 0, Time.time * rotateSpeed);
        var m = Matrix4x4.TRS(Vector3.zero, rot, Vector3.one);
        // Gets the Propeller Material
        GetComponent<Renderer>().materials[1].SetMatrix("_TextureRotation", m);
    }
}
