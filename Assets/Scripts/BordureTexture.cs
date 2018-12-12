using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class BordureTexture : MonoBehaviour
{
    [SerializeField] Texture texture;
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



    void Awake()
    {
        InitialiserReference();
        AppliquerTexture();
        GenererSommets();
        GenererTriangles();
        GenererUV();
        Maillage.RecalculateNormals();
    }

    void InitialiserReference()
    {
        Maillage = GetComponent<MeshFilter>().mesh;
        AfficheurMaillage = GetComponent<MeshRenderer>();
    }

    void AppliquerTexture()
    {
        AfficheurMaillage.material.mainTexture = texture;
    }

    void GenererSommets()
    {
        Maillage.vertices = new Vector3[]
        {
            // face avant
            new Vector3(0,1,0), //0
            new Vector3(1,1,0), //1
            new Vector3(1,0,0), //2
            new Vector3(0,0,0), //3
            //face droite
            new Vector3(1,1,0), //4
            new Vector3(1,1,1), //5
            new Vector3(1,0,1), //6
            new Vector3(1,0,0), //7
            //face arriere
            new Vector3(1,1,1), //8
            new Vector3(0,1,1), //9
            new Vector3(0,0,1), //10
            new Vector3(1,0,1), //11
            //face gauche
            new Vector3(0,1,1), //12
            new Vector3(0,1,0), //13
            new Vector3(0,0,0), //14
            new Vector3(0,0,1), //15
            //face haut
            new Vector3(0,1,1), //16
            new Vector3(1,1,1), //17
            new Vector3(1,1,0), //18
            new Vector3(0,1,0), //19
            //face bas
            new Vector3(0,0,0), //20
            new Vector3(1,0,0), //21
            new Vector3(1,0,1), //22
            new Vector3(0,0,1), //23
        };
    }

    void GenererTriangles()
    {
        Maillage.triangles = new int[]
        {
            0,1,2,
            0,2,3,
            4,5,6,
            4,6,7,
            8,9,10,
            8,10,11,
            12,13,14,
            12,14,15,
            16,17,18,
            16,18,19,
            20,21,22,
            20,22,23
        };
    }

    void GenererUV()
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

    void Update()
    {
        
    }
}

