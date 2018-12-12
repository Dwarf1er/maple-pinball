using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlleurLanceur : MonoBehaviour

{
    [SerializeField]
    float TailleMin;
    [SerializeField]
    float TailleMax;
    [SerializeField]
    float ForceLanceur;
    
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (transform.localScale.y > TailleMin)
                transform.localScale -= new Vector3(0,0.01f,0);
        }
        else if (transform.localScale.y < TailleMax)
        {           
           
            transform.localScale += new Vector3(0, 0.09f, 0);

            GetComponentInChildren<Rigidbody>().AddForce(new Vector3(0,0 , ForceLanceur));


           
            
        }
        
        
    }
    
}
