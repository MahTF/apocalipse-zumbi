using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int quantidadeCura;
    private int tempoDestruicao = 10;

    private void OnTriggerEnter(Collider objetoColisao)
    {
        if(objetoColisao.CompareTag("Player"))
        {
            GerarValorCuraAleatorio();
            objetoColisao.GetComponent<ControlaJogador>().CurarVida(quantidadeCura);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(gameObject, tempoDestruicao);
    }
    private void GerarValorCuraAleatorio()
    {
        quantidadeCura = Random.Range(10, 21);
    }
}
