using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    public static AudioSource Instancia;
    private AudioSource fonteAudio;
    //Awake roda antes do start.
    void Awake()
    {
        fonteAudio = GetComponent<AudioSource>();
        Instancia = fonteAudio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
