﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoArma;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") == true)
        {
            //Atirar
            Instantiate(Bala, CanoArma.transform.position, CanoArma.transform.rotation);
        }
    }
}
