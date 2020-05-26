using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ControlaInterface : MonoBehaviour
{
    public Slider SliderVidaJogador;
    public GameObject PainelGameOver;
    public Text TextoTempoSobrevivencia;
    public Text TextoTempoMaximoSobrevivencia;
    public Text TextoQtZumbiMortos;
    private float tempoPontuacaoSalvo;
    private ControlaJogador scriptControlaJogador;
    private int qtZumbiMortos;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Player").GetComponent<ControlaJogador>();

        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.VidaInicial;
        AtualizarSliderVidaJogador();
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }


    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        AtualizarTexto(minutos, segundos);
        AjustarPontuacaoMaxima(minutos, segundos);
        PainelGameOver.SetActive(true);
    }

    private void AtualizarTexto(int minutos, int segundos)
    {
        if (minutos <= 0)
        {
            TextoTempoSobrevivencia.text = $"Você sobreviveu por {segundos} segundos!";
        }
        else
        {
            TextoTempoSobrevivencia.text = $"Você sobreviveu por {minutos} minutos e {segundos} segundos!";
        }
    }

    void AjustarPontuacaoMaxima(int minutos, int segundos)
    {
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            if (minutos <= 0)
            {
                TextoTempoMaximoSobrevivencia.text = string.Format("Seu passou seu melhor tempo com {0} segundos!", segundos);
            }
            else
            {
                TextoTempoMaximoSobrevivencia.text = string.Format("Seu passou seu melhor tempo com {0} minutos e {1} segundos!", minutos, segundos);
            }
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }

        if(TextoTempoMaximoSobrevivencia.text == "")
        {
            minutos = (int)tempoPontuacaoSalvo / 60;
            segundos = (int)tempoPontuacaoSalvo % 60;
            if (minutos <= 0)
            {
                TextoTempoMaximoSobrevivencia.text = string.Format("Seu melhor tempo é {0} segundos!", segundos);
            }
            else
            {
                TextoTempoMaximoSobrevivencia.text = string.Format("Seu melhor tempo é {0} minutos e {1} segundos!", minutos, segundos);
            }
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
        Time.timeScale = 1;
    }

    public void AtualizarQtZumbiMortos()
    {
        qtZumbiMortos++;
        AtualizarScore();
    }

    private void AtualizarScore()
    {
        TextoQtZumbiMortos.gameObject.SetActive(true);
        TextoQtZumbiMortos.text = string.Format("x {0}", qtZumbiMortos.ToString());
    }
}
