﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbi : MonoBehaviour
{
    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaGeracao = 3;
    private float distanciaJogadorGeracao = 20;
    private GameObject jogador;
    private int qtMaxZumbiVivos = 3;
    private int qtZumbiVivosAtual;
    private float tempoProximoAumentoDificuldade = 15;
    private float contadorAumentarDificuldade;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Player");
        contadorAumentarDificuldade = tempoProximoAumentoDificuldade;
    }

    void Update()
    {
        bool gerarZumbisDistancia = Vector3.Distance(transform.position, jogador.transform.position) > distanciaJogadorGeracao;

        if (gerarZumbisDistancia && qtZumbiVivosAtual <= qtMaxZumbiVivos)
        {
            contadorTempo += Time.deltaTime;
            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }

        if(Time.timeSinceLevelLoad > contadorAumentarDificuldade )
        {
            qtMaxZumbiVivos++;
            contadorAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDificuldade;
        }
    }

    private IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoCriacao, 1, LayerZumbi);

        while(colisores.Length > 0)
        {
            posicaoCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoCriacao, 1, LayerZumbi);
            yield return null;
        }
        ControlaInimigo zumbi = Instantiate(Zumbi, posicaoCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.meuGerador = this;
        qtZumbiVivosAtual++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, distanciaGeracao);
    }

    private Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaGeracao;
        posicao += transform.position;
        posicao.y = 0;
        return posicao;
    }

    public void DiminuirQtZumbiVivos()
    {
        qtZumbiVivosAtual--;
    }
}
