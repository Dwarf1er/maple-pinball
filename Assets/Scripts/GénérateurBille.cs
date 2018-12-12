using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GénérateurBille : MonoBehaviour
{
    [SerializeField] int NbParalleles;
    [SerializeField] int NbPoints;
    [SerializeField] int Rayon;

    Mesh Maillage { get; set; }
    Vector3[] Sommets { get; set; }
    int[] ListeTriangles { get; set; }

    void Awake()
    {
        GénérerMaillage();
    }

    void GénérerMaillage()
    {
        Maillage = new Mesh();
        GetComponent<MeshFilter>().mesh = Maillage;
        Maillage.name = "Bille";
        GénérerListeSommets();
        GénérerListeTriangles();
        Maillage.vertices = Sommets;
        Maillage.triangles = ListeTriangles;
        Maillage.RecalculateNormals();
    }

    void GénérerListeSommets()
    {
        var listeSommets = new Vector3[NbParalleles * NbPoints + 2];
        int cptPositionPoint = 0;

        listeSommets[cptPositionPoint++] = new Vector3(0, 0, Rayon); //Ajout du "Pole Nord"

        for (int cptParalleles = 0; cptParalleles < NbParalleles; cptParalleles++)
        {
            float phi = Mathf.PI * ((cptParalleles + 1) / (NbParalleles + 1));
            for(int cptPoint = 0; cptPoint < NbPoints; cptPoint++)
            {
                float theta = Mathf.PI * (cptPoint / NbPoints);
                float x = Rayon * Mathf.Sin(phi) * Mathf.Cos(theta);
                float y = Rayon * Mathf.Sin(phi) * Mathf.Sin(theta);
                float z = Rayon * Mathf.Cos(phi);
                listeSommets[cptPositionPoint++] = new Vector3(x, y, z);
            }
        }

        listeSommets[cptPositionPoint] = new Vector3(0, 0, -Rayon); //Ajout du "Pole Sud"
        Sommets = listeSommets;
    }

    void GénérerListeTriangles()
    {

    }
}
