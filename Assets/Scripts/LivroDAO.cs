using System.Collections;
using UnityEngine;
using TMPro;

public class LivroDAO : MonoBehaviour
{
    [Header("Configuração")]
    public TextMeshProUGUI textoDicaInterface;
    [TextArea]
    public string mensagemDoLivro = "O ataque ao The DAO ocorreu em um ano que mudou tudo. Encontre o terminal de saída e digite este ano para escapar.";

    [Header("Configurações de Áudio (NOVO)")]
    [Tooltip("Arraste o AudioSource do livro para cá")]
    public AudioSource caixaDeSom;
    [Tooltip("Som de folhear páginas ou abrir um livro antigo")]
    public AudioClip somAbrirLivro;

    public void LerLivro()
    {
        if (textoDicaInterface != null)
        {
            textoDicaInterface.text = mensagemDoLivro;
            textoDicaInterface.color = Color.cyan;

            // TOCA O SOM DO LIVRO AQUI
            if (caixaDeSom != null && somAbrirLivro != null)
            {
                caixaDeSom.PlayOneShot(somAbrirLivro);
            }

            StopAllCoroutines();
            StartCoroutine(LimparMensagem());
        }
    }

    private IEnumerator LimparMensagem()
    {
        yield return new WaitForSeconds(8f);
        if (textoDicaInterface != null) textoDicaInterface.text = "";
    }
}