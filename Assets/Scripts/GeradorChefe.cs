using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    public GameObject Chefe;
    public float TempoEntreGeracoes = 30;
    public Transform[] PosicoesPossiveisGeracao;
    private float tempoProximaGeracao = 0;
    private ControlaInterface scriptInterface;
    private Transform jogador;

    private void Start()
    {
        tempoProximaGeracao = TempoEntreGeracoes;
        scriptInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > tempoProximaGeracao)
        {
            Vector3 posicaoGeracao = CalcularPosicaoMaisLonge();
            Instantiate(Chefe, posicaoGeracao, Quaternion.identity);
            scriptInterface.AparecerTextoChefeCriado();
            tempoProximaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }

    private Vector3 CalcularPosicaoMaisLonge()
    {
        Vector3 posicaoMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0; 

        foreach (Transform posicao in PosicoesPossiveisGeracao)
        {
            float distanciaEntreJogador = Vector3.Distance(posicao.position, jogador.position);
            if(distanciaEntreJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreJogador;
                posicaoMaiorDistancia = posicao.position;
            }
        }

        return posicaoMaiorDistancia;
    }
}
