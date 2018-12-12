using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testballe : MonoBehaviour
{
    [SerializeField]
    int velo;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().maxAngularVelocity = velo;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
