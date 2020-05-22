using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlaJogador : MonoBehaviour, IMatavel
{
    
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public ControlaInterface ScriptControlaInterface;
    public AudioClip SomDano;
    public Status statusJogador;
    private AnimacaoPersonagem animacaoJogador;
    private MovimentoJogador movimentoJogador;

    // Start is called before the first frame update
    void Start()
    {
        movimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        animacaoJogador.Movimentar(direcao.magnitude);

    }

    void FixedUpdate()
    {
        movimentoJogador.Movimentar(direcao, statusJogador.Velocidade);

        movimentoJogador.RotacaoJogador(MascaraChao);
    }

    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;
        ScriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.Instancia.PlayOneShot(SomDano);

        if(statusJogador.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer() 
    {
        Time.timeScale = 0;
        ScriptControlaInterface.GameOver();
    }
}
