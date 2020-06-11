using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{
    public GameObject BotaoSair;

    private void Start()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
            BotaoSair.SetActive(true);
        #endif
    }

    public void Jogar()
    {
        StartCoroutine(MudarCena("game"));
    }

    public void Sair()
    {
        StartCoroutine(FecharJogo());
    }

    private IEnumerator MudarCena(string nome)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(nome);

    }

    private IEnumerator FecharJogo()
    {
        yield return new WaitForSeconds(0.2f);

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}
