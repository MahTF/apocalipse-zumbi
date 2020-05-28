using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        rigidBody.MovePosition(rigidBody.position + direcao.normalized * velocidade * Time.deltaTime);
    }

    public void Rotacionar(Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rigidBody.MoveRotation(novaRotacao);
    }

    public void Morrer()
    {
        rigidBody.constraints = RigidbodyConstraints.None;
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }
}
