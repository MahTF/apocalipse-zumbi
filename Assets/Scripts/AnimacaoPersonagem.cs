using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Atacar(bool estado)
    {
        animator.SetBool("Atacando", estado);
    }

    public void Movimentar(float valorMovimento)
    {
        animator.SetFloat("Movendo", valorMovimento);
    }
}
