using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoArma;
    public AudioClip SomTiro;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") == true)
        {
            //Atirar
            Instantiate(Bala, CanoArma.transform.position, CanoArma.transform.rotation);
            ControlaAudio.Instancia.PlayOneShot(SomTiro);
        }
    }
}
