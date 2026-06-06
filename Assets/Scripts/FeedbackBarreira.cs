using System.Collections;
using UnityEngine;

public class FeedbackBarreira : MonoBehaviour
{
    [Header("Configuração de Áudio")]
    [Tooltip("Arraste o componente AudioSource desta barreira para cá")]
    public AudioSource caixaDeSom;

    [Tooltip("O áudio dizendo: 'É necessário resolver o enigma primeiro...' (Formatos: .mp3 ou .ogg)")]
    public AudioClip audioBloqueado;

    [Header("Controle de Spam (Anti-Repetição)")]
    [Tooltip("Tempo em segundos que o jogador deve esperar para o áudio poder tocar de novo (evita que o som toque 50 vezes seguidas se o jogador ficar batendo na parede)")]
    public float cooldownAudio = 5f;
    private bool podeTocarAudio = true;

    [Header("Referências dos Puzzles (Preencha APENAS o que bloqueia essa barreira)")]
    [Tooltip("Se esta barreira depende do Terminal Enigma (Senha 2008), arraste o terminal correspondente para cá")]
    public TerminalEnigma terminalEnigma;

    [Tooltip("Se esta barreira depende do Terminal Saída (Senha 2016), arraste o terminal correspondente para cá")]
    public TerminalSaida terminalSaida;

    // Esta função roda sozinha quando o jogador encosta no colisor invisível da barreira
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se quem encostou foi o jogador
        if (other.CompareTag("Player"))
        {
            // Se o áudio estiver liberado para tocar, vamos verificar o estado do puzzle
            if (podeTocarAudio)
            {
                VerificarEPlay();
            }
        }
    }

    private void VerificarEPlay()
    {
        bool puzzleResolvido = false;

        // 1. Se você arrastou um TerminalEnigma para o script:
        if (terminalEnigma != null)
        {
            // Se a parede invisível do terminal ainda existe, significa que o puzzle NÃO foi resolvido
            if (terminalEnigma.paredeInvisivel == null)
            {
                puzzleResolvido = true;
            }
        }
        // 2. Se você arrastou um TerminalSaida para o script:
        else if (terminalSaida != null)
        {
            // Como o TerminalSaida não deleta um objeto (ele ativa um Animator), 
            // ele só fecha o terminal. Mas podemos assumir que se o jogador chegou na barreira final 
            // e ela ainda existe/está fechada, precisamos avisá-lo. 
            // Nota: se a barreira já for ser destruída por outro script, este código nem vai rodar.
            puzzleResolvido = false;
        }

        // Se o puzzle ainda não foi resolvido, toca o áudio de aviso!
        if (!puzzleResolvido)
        {
            if (caixaDeSom != null && audioBloqueado != null)
            {
                caixaDeSom.PlayOneShot(audioBloqueado);
                StartCoroutine(AtivarCooldown());
            }
        }
    }

    // Corotina para impedir que o áudio fique repetindo sem parar se o jogador ficar esfregando na barreira
    private IEnumerator AtivarCooldown()
    {
        podeTocarAudio = false;
        yield return new WaitForSeconds(cooldownAudio);
        podeTocarAudio = true;
    }
}