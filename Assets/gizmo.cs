using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(0, 0, 0), .5f);
        Gizmos.DrawSphere(new Vector3(1, 0, 0), .5f);
        Gizmos.DrawSphere(new Vector3(1, 1, 0), .5f);
        Gizmos.DrawSphere(new Vector3(0, 1, 0), .5f);
    }
}
