using System.Collections;
using UnityEngine;
using TMPro;

public class TerminalEnigma : MonoBehaviour
{
    [Header("Interface do PC")]
    public GameObject canvasComputador;
    public TMP_InputField inputSenha;

    [Header("Progresso do Jogo")]
    public GameObject paredeInvisivel;
    public string senhaCorreta = "2008";

    [Header("Feedback Visual")]
    public TextMeshProUGUI feedbackText;

    [Header("Congelar Jogador")]
    public Behaviour scriptDeMovimento;

    [Header("Configurações de Áudio (NOVO)")]
    [Tooltip("Arraste o AudioSource deste computador para cá")]
    public AudioSource caixaDeSom;
    [Tooltip("Som curto de acerto (Ex: Ding!)")]
    public AudioClip somSucesso;
    [Tooltip("Som curto de erro (Ex: Buzzer/Erro)")]
    public AudioClip somErro;

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
            if (feedbackText != null) feedbackText.text = "";

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
            // TOCA SOM DE SUCESSO
            if (caixaDeSom != null && somSucesso != null)
                caixaDeSom.PlayOneShot(somSucesso);

            StartCoroutine(AcessoConcedidoRoutine());
        }
        else
        {
            // TOCA SOM DE ERRO
            if (caixaDeSom != null && somErro != null)
                caixaDeSom.PlayOneShot(somErro);

            if (feedbackText != null)
            {
                feedbackText.text = "TENTE NOVAMENTE";
                feedbackText.color = Color.red;
            }
        }
    }

    private IEnumerator AcessoConcedidoRoutine()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "ACESSO CONCEDIDO";
            feedbackText.color = Color.green;
        }

        yield return new WaitForSeconds(2f);

        if (paredeInvisivel != null) Destroy(paredeInvisivel);
        FecharTerminal();
    }
}