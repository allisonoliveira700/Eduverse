using System.Collections;
using UnityEngine;
using TMPro;

public class TerminalSaida : MonoBehaviour
{
    [Header("Interface do PC")]
    public GameObject canvasComputador;
    public TMP_InputField inputSenha;

    [Header("Progresso do Jogo")]
    public string senhaCorreta = "2016";

    [Header("Animação da Porta")]
    public Animator animadorPorta;
    public string nomeDoTrigger = "Abrir";

    [Header("Feedback Visual")]
    public TextMeshProUGUI feedbackText;

    [Header("Congelar Jogador")]
    public Behaviour scriptDeMovimento;

    [Header("Configurações de Áudio (ATUALIZADO)")]
    public AudioSource caixaDeSom;
    public AudioClip somSucesso;
    public AudioClip somErro;
    [Tooltip("Som da porta pesada se destrancando/abrindo")]
    public AudioClip somPortaAbrindo; // <--- NOVO ÁUDIO DA PORTA

    private void Start()
    {
        if (canvasComputador != null) canvasComputador.SetActive(false);
        if (feedbackText != null) feedbackText.text = "";
    }

    public void AbrirTerminal()
    {
        if (canvasComputador != null && canvasComputador.activeSelf) return;

        if (canvasComputador != null)
        {
            canvasComputador.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (scriptDeMovimento != null) scriptDeMovimento.enabled = false;

            if (feedbackText != null)
            {
                feedbackText.text = "";
                feedbackText.color = Color.white;
            }

            if (inputSenha != null)
            {
                inputSenha.text = "";
                inputSenha.Select();
                inputSenha.ActivateInputField();
            }
        }
    }

    public void FecharTerminal()
    {
        if (canvasComputador != null)
        {
            canvasComputador.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (scriptDeMovimento != null) scriptDeMovimento.enabled = true;
        }
    }

    public void ConfirmarSenha()
    {
        string senhaDigitada = inputSenha.text.Replace("\u200B", "").Trim();
        string senhaAlvo = senhaCorreta.Replace("\u200B", "").Trim();

        if (senhaDigitada == senhaAlvo)
        {
            if (caixaDeSom != null && somSucesso != null)
                caixaDeSom.PlayOneShot(somSucesso);

            StartCoroutine(AcessoConcedidoRoutine());
        }
        else
        {
            if (caixaDeSom != null && somErro != null)
                caixaDeSom.PlayOneShot(somErro);

            if (feedbackText != null)
            {
                feedbackText.text = "ACESSO NEGADO. TENTE NOVAMENTE.";
                feedbackText.color = Color.red;
            }
        }
    }

    private IEnumerator AcessoConcedidoRoutine()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "SISTEMA DESBLOQUEADO. A PORTA À SUA ESQUERDA ESTÁ ABERTA.";
            feedbackText.color = Color.green;
        }

        if (animadorPorta != null)
        {
            animadorPorta.SetTrigger(nomeDoTrigger);

            // TOCA O SOM DA PORTA ABRINDO AQUI!
            if (caixaDeSom != null && somPortaAbrindo != null)
            {
                caixaDeSom.PlayOneShot(somPortaAbrindo);
            }
        }

        yield return new WaitForSeconds(2f);
        FecharTerminal();
    }
}