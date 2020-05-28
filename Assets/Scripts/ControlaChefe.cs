using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    public AudioClip SomMorte;
    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    private AnimacaoPersonagem animacao;
    private MovimentoPersonagem movimento;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Player").transform;
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        animacao = GetComponent<AnimacaoPersonagem>();
        movimento = GetComponent<MovimentoPersonagem>();
        agente.speed = statusChefe.Velocidade;
    }

    private void Update()
    {
        agente.SetDestination(jogador.position);
        animacao.Movimentar(agente.velocity.magnitude);

        if (agente.hasPath)
        {
            bool pertoJogador = agente.remainingDistance <= agente.stoppingDistance;
            if (pertoJogador)
            {
                animacao.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimento.Rotacionar(direcao);
            }
            else
            {
                animacao.Atacar(false);
            }
        }
    }

    private void AtacaJogador()
    {
        int dano = Random.Range(25, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        statusChefe.Vida -= dano;
        if(statusChefe.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        animacao.Morrer();
        movimento.Morrer();
        agente.enabled = false;
        this.enabled = false;
        ControlaAudio.Instancia.PlayOneShot(SomMorte);
        Destroy(gameObject, 2);

        /*
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarQtZumbiMortos();
        meuGerador.DiminuirQtZumbiVivos();*/
    }
}
