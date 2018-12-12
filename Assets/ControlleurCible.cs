using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshCollider))]
 [RequireComponent(typeof(MeshRenderer))]
 [RequireComponent(typeof(MeshFilter))]

public class ControlleurCible : MonoBehaviour
{
    [SerializeField]
    Texture texture;

    [SerializeField]
    int largeurTuile = 64;
    [SerializeField]
    int hauteurTuile = 64;

    [SerializeField]
    int idTuileX = 0;
    [SerializeField]
    int idTuileY = 0;
    Mesh Maillage { get; set; }
    MeshRenderer AfficheurMaillage { get; set; }
   
    void Start()
    {
        InitRef();
        AppliquerTexture();
        GenererSommets();
        GenererTriangles();
        GenererUVs();

        Maillage.RecalculateNormals();
    }

    

    void Update()
    {
        
    }

    void InitRef()
    {
        Maillage = GetComponent<MeshFilter>().mesh;
        AfficheurMaillage = GetComponent<MeshRenderer>();
    }
    private void AppliquerTexture()
    {
        AfficheurMaillage.material.mainTexture = texture;
        
    }

    void GenererSommets()
    {
        Maillage.vertices = new Vector3[]
        {
            new Vector3(0,1,0),//0
            new Vector3(1,1,0),//1
            new Vector3(1,0,0),//2
            new Vector3(0,0,0),//3

            new Vector3(0,1,0),//4
            new Vector3(1,1,0),//5
            new Vector3(1,0,0),//6
            new Vector3(0,0,0)//7
        };
    }
    void GenererTriangles()
    {
        Maillage.triangles = new int[]
        {
            0,1,2,
            0,2,3,
            
        };
    }
    void GenererUVs()
    {
        (int largeurTexture, int hauteurTexture) = ChercherDimensionsTexture(texture);

        int nbTuilesLignes = largeurTexture / largeurTuile;
        int nbTuilesColonne = hauteurTexture / hauteurTuile;

        //éviter la répétition de transtypage
        float nbTuilesLignesF = nbTuilesLignes;
        float nbTuilesColonnesF = nbTuilesColonne;

        Maillage.uv = new Vector2[]
        {
            new Vector2(idTuileX/nbTuilesLignesF,         (idTuileY + 1)/nbTuilesColonnesF),
            new Vector2((idTuileX + 1)/nbTuilesLignesF,   (idTuileY + 1)/nbTuilesColonnesF),
            new Vector2((idTuileX + 1)/nbTuilesLignesF,   idTuileY/nbTuilesColonnesF),
            new Vector2(idTuileX/nbTuilesLignesF,         idTuileY/nbTuilesColonnesF),
        };
    }
    public static (int, int) ChercherDimensionsTexture(Texture texture)
    {
        if (texture == null)
            throw new ArgumentNullException();
        return (texture.width, texture.height);
    }
}
