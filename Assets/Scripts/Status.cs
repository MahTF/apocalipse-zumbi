using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int VidaInicial = 100;
    public float Velocidade = 5;
    [HideInInspector]
    public int Vida;

    void Start()
    {
        Vida = VidaInicial;
    }
}
