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
        Quaternion rotacaoOposta = Quaternion.LookRotation(-transform.forward);
        switch (objetoColisao.tag)
        {
            case "Inimigo":
                ControlaInimigo inimigo = objetoColisao.GetComponent<ControlaInimigo>();
                inimigo.TomarDano(1);
                inimigo.ParticulaSangue(transform.position, rotacaoOposta);
                break;

            case "ChefeFase":
                ControlaChefe chefe = objetoColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(1);
                chefe.ParticulaSangue(transform.position, rotacaoOposta);
                break;
        }

        Destroy(gameObject);
    }
}
