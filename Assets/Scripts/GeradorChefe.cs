using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    public GameObject Chefe;
    public float TempoEntreGeracoes = 30;
    private float tempoProximaGeracao = 0;

    private void Start()
    {
        tempoProximaGeracao = TempoEntreGeracoes;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > tempoProximaGeracao)
        {
            Instantiate(Chefe, transform.position, Quaternion.identity);
            tempoProximaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }
}
