using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class ContrôleFlipper : MonoBehaviour
{
    [SerializeField]
    KeyCode touche;
    [SerializeField]
    float angleCible;
    HingeJoint HingeJoint { get; set; }
    void Awake()
    {
        HingeJoint = GetComponent<HingeJoint>();
        HingeJoint.useLimits = true;
        HingeJoint.useSpring = true;

    }
    void FixedUpdate()
    {
        JointSpring ressort = new JointSpring();
        ressort.spring = 8000f;
        ressort.damper = 10f;
        if (Input.GetKey(touche))
        {
            ressort.targetPosition = angleCible;
        }
        else
        {
            ressort.targetPosition = 0f;
        }
        HingeJoint.spring = ressort; 
    }
}
