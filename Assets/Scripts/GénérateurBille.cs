using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GénérateurBille : MonoBehaviour
{
    [SerializeField] int NbParalleles;
    [SerializeField] int NbPoints; //Le nombre de points sur chaque parallele
    [SerializeField] float Rayon;
    [SerializeField] int VitesseMax;

    Mesh Maillage { get; set; }
    Vector3[] Sommets { get; set; }
    int[] ListeTriangles { get; set; }

    void Awake()
    {
        GénérerMaillage();
        GetComponent<Rigidbody>().maxAngularVelocity = VitesseMax;
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
            float phi = Mathf.PI * ((float)(cptParalleles + 1) / (float)(NbParalleles + 1));
            for(int cptPoint = 0; cptPoint < NbPoints; cptPoint++)
            {
                float theta = (2* Mathf.PI) * ((float)cptPoint / (float)NbPoints);
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
        int nbTriangles = (2 * NbPoints) + ((2 * NbPoints) * (NbParalleles - 1)); //Top & Bottom + Body de la bille
        int cptPosTriangle = 0;
        ListeTriangles = new int[nbTriangles * 3];
        
        //Top de la bille
        for(int i = 0; i < NbPoints; i++)
        {
            if (i == NbPoints - 1)
            {
                ListeTriangles[cptPosTriangle++] = i + 1;
                ListeTriangles[cptPosTriangle++] = 0;   //"Pole Nord"
                ListeTriangles[cptPosTriangle++] = 1;
            }

            else
            {
                ListeTriangles[cptPosTriangle++] = i + 1;
                ListeTriangles[cptPosTriangle++] = 0;   //"Pole Nord"
                ListeTriangles[cptPosTriangle++] = i + 2; //Revenir au premier point
            }
        }

        //Body de la bille
        for(int i = 0; i < NbParalleles - 1; i++) //Cpt pour les paralleles
        {
            for(int j = 0; j < NbPoints; j++) //Cpt pour les points
            {
                if(j == NbPoints - 1)
                {
                    //Top triangle
                    ListeTriangles[cptPosTriangle++] = i * NbPoints + j + 1;
                    ListeTriangles[cptPosTriangle++] = i * NbPoints + 1;    //Revenir au premier point
                    ListeTriangles[cptPosTriangle++] = (i + 1) * NbPoints + 1; //Revenir au premier point bottom

                    //Bottom triangle
                    ListeTriangles[cptPosTriangle++] = i * NbPoints + 1; //Revenir au premier point
                    ListeTriangles[cptPosTriangle++] = (i + 1) * NbPoints + 1; //Revenir au premier point bottom
                    ListeTriangles[cptPosTriangle++] = (i + 1) * NbPoints + 1;
                }

                else
                {
                    //Top triangle
                    ListeTriangles[cptPosTriangle++] = i * NbPoints + j + 1;
                    ListeTriangles[cptPosTriangle++] = i * NbPoints + j + 2;
                    ListeTriangles[cptPosTriangle++] = (i + 1) * NbPoints + j + 1;

                    //Bottom triangle
                    ListeTriangles[cptPosTriangle++] = i * NbPoints + j + 2;
                    ListeTriangles[cptPosTriangle++] = (i + 1) * NbPoints + j + 2;
                    ListeTriangles[cptPosTriangle++] = (i + 1) * NbPoints + j + 1;
                }
            }
        }

        //Bottom de la bille
        for(int i = 0; i < NbPoints; i++)
        {
            if (i == NbPoints - 1)
            {
                ListeTriangles[cptPosTriangle++] = ((NbParalleles - 1) * NbPoints) + i + 1;
                ListeTriangles[cptPosTriangle++] = ((NbParalleles - 1) * NbPoints) + 1; //Revenir au premier point
                ListeTriangles[cptPosTriangle++] = Sommets.Length - 1; //"Pole Sud"
            }

            else
            {
                ListeTriangles[cptPosTriangle++] = ((NbParalleles - 1) * NbPoints) + i + 1;
                ListeTriangles[cptPosTriangle++] = ((NbParalleles - 1) * NbPoints) + i + 2;
                ListeTriangles[cptPosTriangle++] = Sommets.Length - 1; //"Pole Sud"
            }
        }
    }
}
