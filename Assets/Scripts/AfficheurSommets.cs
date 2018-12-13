using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class AfficheurSommets : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        if (mesh == null)
            return;

        foreach (var v in mesh.vertices)
        {
            Gizmos.DrawSphere(transform.position + (Matrix4x4.Rotate(transform.rotation) * Matrix4x4.Scale(transform.localScale)).MultiplyVector(v), .05f);
        }
    }
}
