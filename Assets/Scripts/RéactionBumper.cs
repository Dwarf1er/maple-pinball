using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RéactionBumper : MonoBehaviour
{
   [SerializeField]
   float force = 20;
   void OnCollisionEnter(Collision collision)
   {
      force = collision.relativeVelocity.magnitude * 2;
      Vector3 directionForce = -collision.contacts[0].normal;
      Debug.DrawRay(collision.contacts[0].point, directionForce, Color.red, 100);
      collision.rigidbody.AddForce(directionForce * force, ForceMode.VelocityChange);

   }
}
