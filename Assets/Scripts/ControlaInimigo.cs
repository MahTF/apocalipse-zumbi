using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    public int Velocidade = 5;
    public GameObject Jogador;
    private Rigidbody rigidbodyZumbi;
    private Animator animatorZumbi;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Player");
        int skinZumbi = Random.Range(1, 28);
        transform.GetChild(skinZumbi).gameObject.SetActive(true);
        rigidbodyZumbi = GetComponent<Rigidbody>();
        animatorZumbi = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 direcao = Jogador.transform.position - transform.position;
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        //olhar para o personagem
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rigidbodyZumbi.MoveRotation(novaRotacao);

        if (distancia > 2.5)
        {
            rigidbodyZumbi.MovePosition(rigidbodyZumbi.position + direcao.normalized * Velocidade * Time.deltaTime);

            animatorZumbi.SetBool("Atacando", false);
        }
        else
        {
            //Atacar
            animatorZumbi.SetBool("Atacando", true);
        }

    }

    void AtacaJogador()
    {
        Time.timeScale = 0;
        Jogador.GetComponent<ControlaJogador>().TextoGameOver.SetActive(true);
        Jogador.GetComponent<ControlaJogador>().Vivo = false;
    }
}
