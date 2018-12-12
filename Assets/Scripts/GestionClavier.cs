using UnityEngine;


public class GestionClavier : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
   {
      if (Input.GetKey(KeyCode.Escape))
      {
         Application.Quit();
      }
   }

}
