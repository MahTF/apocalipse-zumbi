using System.Collections;
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
        Destroy(gameObject, 15); //Caso a bala não bata em nada, ela é destroida após 15 segundos.
    }

    void FixedUpdate()
    {
        rigidbodyBala.MovePosition(rigidbodyBala.position + transform.forward * Velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider objetoColisao)
    {
        switch (objetoColisao.tag)
        {
            case "Inimigo":
                objetoColisao.GetComponent<ControlaInimigo>().TomarDano(1);
                break;

            case "ChefeFase":
                objetoColisao.GetComponent<ControlaChefe>().TomarDano(1);
                break;
        }

        Destroy(gameObject);
    }
}
