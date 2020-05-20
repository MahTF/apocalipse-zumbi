﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade = 20;
    public AudioClip SomMorteZumbi;
    private Rigidbody rigidbodyBala;

    void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbodyBala.MovePosition(rigidbodyBala.position + transform.forward * Velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider objetoColisao)
    {
        if(objetoColisao.CompareTag("Inimigo"))
        {
            objetoColisao.GetComponent<ControlaInimigo>().TomarDano(1);
        }

        Destroy(gameObject);
    }
}
