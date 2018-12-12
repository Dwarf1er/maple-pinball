// Auteur : Vincent Echelard, 2017
// Note : Comme vous pouvez le constater, ce script permet de gérer la translation et 
//        la rotation de tout GameObject (pourvu d'un composant Transform) auquel il est rattaché.
//        Il peut donc s'appliquer à autre chose qu'une caméra...

using UnityEngine;

public class GestionCaméra : MonoBehaviour
{
   [SerializeField]              // Le champs VitesseLinéaire sera accessible en tant que paramètre du script dans l'éditeur de Unity.
   float VitesseLinéaire;        // L'usage de la commande [SeralizeField] permet d'éviter de mettre l'attribut public tout permettant de faire quand même le lien avec l'éditeur.
                                 // La vitesseLinéaire est le scalaire utilisé pour le calcul de la translation

   [SerializeField]              // Le champs VitesseAngulaire sera accessible en tant que paramètre du script dans l'éditeur de Unity.
   float VitesseAngulaire;       // L'usage de la commande [SeralizeField] permet d'éviter de mettre l'attribut public tout permettant de faire quand même le lien avec l'éditeur.
                                 // La VitesseAngulaire est le scalaire utilisé pour le calcul de la rotation

   //static string[] TouchesTranslation = { "w", "a", "s", "d" }; //les touches de déplacement de la caméra
   static KeyCode[] TouchesTranslation = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D }; //les touches de déplacement de la caméra
   static Vector3[] VecteursTranslation = { Vector3.forward, Vector3.left, Vector3.back, Vector3.right }; //les vecteurs unitaires associés aux différents déplacements de la caméra

   //static string[] TouchesRotation = { "up", "down", "right", "left", "page up", "page down" }; //les touches de rotation de la caméra
   static KeyCode[] TouchesRotation = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.PageUp, KeyCode.PageDown }; //les touches de rotation de la caméra
   static Vector3[] VecteursRotation = { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0), new Vector3(0, 0, 1), new Vector3(0, 0, -1) }; //les vecteurs associés aux différentes rotations de la caméra

   void Update()
   {
      if (Input.GetKey("right shift") || Input.GetKey("left shift")) // si une des deux touches shift (ou les deux) est enfoncée
      {
         GérerTouchesCaméra(); // on calcule le déplacement totale et la rotation totale pour un frame
      }
   }

   private void GérerTouchesCaméra()
   {
      Vector3 rotationFrame = InterpréterTouches(TouchesRotation, VecteursRotation, VitesseAngulaire); //on calcul la rotation totale du frame
      transform.Rotate(rotationFrame); // On applique la rotation totale calculée dans le Frame à la caméra
      Vector3 déplacementFrame = InterpréterTouches(TouchesTranslation, VecteursTranslation, VitesseLinéaire); //on calcule le déplacement total du frame
      transform.Translate(déplacementFrame); // On applique le déplacement total calculée dans le Frame à la caméra
   }

   private Vector3 InterpréterTouches(KeyCode[] touches, Vector3[] vecteurs, float vitesse)
   {
      Vector3 vecteurTransformationFrame = Vector3.zero;
      int nbTouches = touches.Length;
      for (int i = 0; i < nbTouches; ++i) // Pour chacune des touches
      {
         if (Input.GetKey(touches[i])) // Si la touche est enfoncée
         {
            vecteurTransformationFrame += vecteurs[i] * vitesse;
         }
      }
      return vecteurTransformationFrame;
   }
}
