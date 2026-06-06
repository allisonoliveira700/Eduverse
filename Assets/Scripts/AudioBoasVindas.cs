using System.Collections;
using UnityEngine;

public class AudioBoasVindas : MonoBehaviour
{
    [Header("Configuração de Áudio")]
    [Tooltip("Arraste o componente AudioSource para cá")]
    public AudioSource caixaDeSom;

    [Tooltip("Arraste o seu arquivo de áudio de introdução para cá")]
    public AudioClip audioIntroducao;

    [Header("Configuração de Tempo")]
    [Tooltip("Quantos segundos esperar antes do áudio começar?")]
    public float tempoDeEspera = 2f;

    private void Start()
    {
        // Assim que o jogo começa, ele verifica se está tudo configurado e inicia a contagem
        if (caixaDeSom != null && audioIntroducao != null)
        {
            StartCoroutine(TocarAudioComAtraso());
        }
        else
        {
            Debug.LogWarning("ALERTA: Faltou colocar o AudioSource ou o AudioClip no script AudioBoasVindas!");
        }
    }

    private IEnumerator TocarAudioComAtraso()
    {
        // Dá um pequeno tempo para o jogador "nascer" e se ambientar na sala
        yield return new WaitForSeconds(tempoDeEspera);

        // Toca a mensagem de boas-vindas e explicação do puzzle
        caixaDeSom.PlayOneShot(audioIntroducao);
    }
}