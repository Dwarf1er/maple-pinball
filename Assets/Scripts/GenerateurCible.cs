using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]


public class GenerateurCible : MonoBehaviour
{
    [SerializeField] Texture texture;
    [SerializeField] Vector3 dimension;

    protected Mesh Maillage { get; private set; }
    MeshRenderer mshRenderer { get; set; }

    protected Vector3[] Sommets { get; set; }  // Le tableau qui contiendra les différents sommets (vertices) de la primitive

    protected int[] Triangles { get; set; }

    Vector4[] Tangentes { get; set; }



    void Awake()
    {
        GénérerMaillage();
        AppliquerTexture();
        GenererSommets();
        GenererTriangles();
        GenererUV();

    }

    private void GénérerMaillage()
    {
        Maillage = new Mesh();
        GetComponent<MeshFilter>().mesh = Maillage;
        Maillage.name = gameObject.name;
        GenererSommets();
        GenererTriangles();
        Maillage.vertices = Sommets;
        Maillage.triangles = Triangles;
        Maillage.RecalculateNormals();
        if (Sommets != null)
        {
            CalculerTangentes();
            Maillage.tangents = Tangentes;
        }
    }

    void AppliquerTexture()
    {
        mshRenderer = GetComponent<MeshRenderer>();
        mshRenderer.material.mainTexture = texture;
    }

    void GenererSommets()
    {
        Sommets = Maillage.vertices = new Vector3[]
        {
            // face avant
            new Vector3(0,dimension.y,0), //0
            new Vector3(dimension.x,dimension.y,0), //1
            new Vector3(dimension.x,0,0), //2
            new Vector3(0,0,0), //3
            //face droite
            new Vector3(dimension.x,dimension.y,0), //4
            new Vector3(dimension.x,dimension.y,dimension.z), //5
            new Vector3(dimension.x,0,dimension.z), //6
            new Vector3(dimension.x,0,0), //7
            //face arriere
            new Vector3(dimension.x,dimension.y,dimension.z), //8
            new Vector3(0,dimension.y,dimension.z), //9
            new Vector3(0,0,dimension.z), //10
            new Vector3(dimension.x,0,dimension.z), //11
            //face gauche
            new Vector3(0,dimension.y,dimension.z), //12
            new Vector3(0,dimension.y,0), //13
            new Vector3(0,0,0), //14
            new Vector3(0,0,dimension.z), //15
            //face haut
            new Vector3(0,dimension.y,dimension.z), //16
            new Vector3(dimension.x,dimension.y,dimension.z), //17
            new Vector3(dimension.x,dimension.y,0), //18
            new Vector3(0,dimension.y,0), //19
            //face bas
            new Vector3(0,0,0), //20
            new Vector3(dimension.x,0,0), //21
            new Vector3(dimension.x,0,dimension.z), //22
            new Vector3(0,0,dimension.z), //23
        };
    }

    void GenererTriangles()
    {
        Triangles = Maillage.triangles = new int[]
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


        Maillage.uv = new Vector2[]
        {
            // face avant
            new Vector2(0,0),
            new Vector2(1,0),
            new Vector2(1,1),
            new Vector2(0,1),
            //face droite
            new Vector2(1,1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(0,0),
            // face arriere
            new Vector2(0,0),
            new Vector2(1,0),
            new Vector2(1,1),
            new Vector2(0,1),
            //face gauche
            new Vector2(1,1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(0,0),
            //haut
            new Vector2(1,1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(0,0),
            // bas
            new Vector2(1,1),
            new Vector2(1,0),
            new Vector2(0,1),
            new Vector2(0,0),
        };
    }

    private void CalculerTangentes()
    {
        // Le tableau Tangentes contiendra la description du vecteur tangente 
        // associée à chacun des sommets de la primitive
        Tangentes = new Vector4[Sommets.Length];
        Vector4 tangenteDeBase = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0; i < Tangentes.Length; ++i)
        {
            Tangentes[i] = tangenteDeBase;
        }
    }

    void OnValidate()
    {
        Sommets = null;
        Triangles = null;
        Awake();
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

