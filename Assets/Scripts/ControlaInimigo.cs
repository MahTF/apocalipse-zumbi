﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    public GameObject Jogador;
    public AudioClip SomMorte;
    private Status statusZumbi;
    private MovimentoPersonagem movimentoInimigo;
    private AnimacaoPersonagem animacaoInimigo;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Player");
        
        movimentoInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        statusZumbi = GetComponent<Status>();
        AleatorizarZumbi();
    }


    void FixedUpdate()
    {
        Vector3 direcao = Jogador.transform.position - transform.position;
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        movimentoInimigo.Rotacionar(direcao);

        if (distancia > 2.5)
        {
            movimentoInimigo.Movimentar(direcao, statusZumbi.Velocidade);

            animacaoInimigo.Atacar(false);
        }
        else
        {
            //Atacar
            animacaoInimigo.Atacar(true);
        }

    }

    void AtacaJogador()
    {
        int dano = Random.Range(20,31);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbi()
    {
        int skinZumbi = Random.Range(1, 28);
        transform.GetChild(skinZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusZumbi.Vida -= dano;

        if (statusZumbi.Vida <=0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject);
        ControlaAudio.Instancia.PlayOneShot(SomMorte);
    }
}
