﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    public GameObject Jogador;
    public AudioClip SomMorte;
    public GameObject KitMedico;
    public GameObject ParticulaSangueZumbi;
    [HideInInspector]
    public GeradorZumbi meuGerador;
    protected ControlaInterface scriptControlaInterface;
    private Status statusZumbi;
    private MovimentoPersonagem movimentoInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoGerarProxima = 4;
    private float porcentagemGerarKitMedico = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Player");

        movimentoInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        statusZumbi = GetComponent<Status>();
        AleatorizarZumbi();
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }


    void FixedUpdate()
    {

        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        movimentoInimigo.Rotacionar(direcao);

        animacaoInimigo.Movimentar(direcao.magnitude);

        if (distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5)
        {
            direcao = Jogador.transform.position - transform.position;
            movimentoInimigo.Movimentar(direcao, statusZumbi.Velocidade);

            animacaoInimigo.Atacar(false);
        }
        else
        {
            direcao = Jogador.transform.position - transform.position;
            //Atacar
            animacaoInimigo.Atacar(true);
        }

    }

    void AtacaJogador()
    {
        int dano = Random.Range(20, 31);
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

        if (statusZumbi.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject, 2);
        animacaoInimigo.Morrer();
        movimentoInimigo.Morrer();
        this.enabled = false;
        ControlaAudio.Instancia.PlayOneShot(SomMorte);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQtZumbiMortos();
        meuGerador.DiminuirQtZumbiVivos();
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(ParticulaSangueZumbi, posicao, rotacao);
    }

    private void Vagar()
    {
        contadorVagar -= Time.deltaTime;

        if (contadorVagar <= 0) 
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoGerarProxima + Random.Range(-1f, 1f);
        }

        bool pertoSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05f;
        if (pertoSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentoInimigo.Movimentar(direcao, statusZumbi.Velocidade);
        }

    }

    private Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = 0;
        return posicao;
    }

    void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if(Random.value <= porcentagemGeracao)
        {
            Instantiate(KitMedico, transform.position, Quaternion.identity);
        }
    }
}
