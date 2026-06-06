using System.Collections;
using UnityEngine;
using TMPro;

public class PuzzleChaves : MonoBehaviour
{
    [Header("Configuração Individual")]
    public int numeroDestaMesa;
    public GameObject indicadorVisualLuz;

    [Header("Configurações Globais")]
    public GameObject barreiraEscada;
    public TextMeshProUGUI feedbackText;

    [Header("Configurações de Áudio (NOVO)")]
    [Tooltip("Arraste o AudioSource que vai tocar os sons globais do puzzle para cá")]
    public AudioSource caixaDeSom;
    [Tooltip("Som quando ativa um abajur na ordem certa (Ex: Clique de botão)")]
    public AudioClip somCliqueCerto;
    [Tooltip("Som de erro quando quebra a sequência")]
    public AudioClip somErroSequencia;
    [Tooltip("Som de vitória quando o puzzle inteiro é resolvido")]
    public AudioClip somPuzzleResolvido;

    private static int faseAtualDoPuzzle = 0;
    private static bool puzzleResolvido = false;
    private static PuzzleChaves abajurMesa1;
    private static PuzzleChaves abajurMesa3;
    private static Coroutine rotinaDeTexto;

    private void Awake()
    {
        faseAtualDoPuzzle = 0;
        puzzleResolvido = false;
        if (numeroDestaMesa == 1) abajurMesa1 = this;
        if (numeroDestaMesa == 3) abajurMesa3 = this;
    }

    public void InteragirComAbajur()
    {
        if (puzzleResolvido) return;

        if (faseAtualDoPuzzle == 0)
        {
            if (numeroDestaMesa == 1)
            {
                faseAtualDoPuzzle = 1;
                AcenderLuz(true);

                // TOCA SOM DE CLIQUE CERTO
                if (caixaDeSom != null && somCliqueCerto != null)
                    caixaDeSom.PlayOneShot(somCliqueCerto);

                MostrarMensagem("CHAVE PÚBLICA ENCONTRADA.\nAGUARDANDO CHAVE PRIVADA...", Color.yellow, 3f);
            }
            else
            {
                ErroNaSequencia();
            }
        }
        else if (faseAtualDoPuzzle == 1)
        {
            if (numeroDestaMesa == 3)
            {
                AcenderLuz(true);

                // TOCA SOM DE SUCESSO FINAL
                if (caixaDeSom != null && somPuzzleResolvido != null)
                    caixaDeSom.PlayOneShot(somPuzzleResolvido);

                StartCoroutine(PuzzleConcluidoRoutine());
            }
            else
            {
                ErroNaSequencia();
            }
        }
    }

    private void ErroNaSequencia()
    {
        faseAtualDoPuzzle = 0;

        if (abajurMesa1 != null) abajurMesa1.AcenderLuz(false);
        if (abajurMesa3 != null) abajurMesa3.AcenderLuz(false);

        // TOCA SOM DE ERRO NA SEQUÊNCIA
        if (caixaDeSom != null && somErroSequencia != null)
            caixaDeSom.PlayOneShot(somErroSequencia);

        MostrarMensagem("CONEXÃO FALHOU!\nSEQUÊNCIA INCORRETA.", Color.red, 3f);
    }

    private void AcenderLuz(bool estado)
    {
        if (indicadorVisualLuz != null) indicadorVisualLuz.SetActive(estado);
    }

    private void MostrarMensagem(string texto, Color cor, float tempo)
    {
        if (feedbackText != null)
        {
            feedbackText.text = texto;
            feedbackText.color = cor;
            if (rotinaDeTexto != null)
            {
                StopCoroutine(rotinaDeTexto);
            }
            rotinaDeTexto = StartCoroutine(ApagarMensagemDepoisDeTempo(tempo));
        }
    }

    private IEnumerator ApagarMensagemDepoisDeTempo(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        if (feedbackText != null) feedbackText.text = "";
    }

    private IEnumerator PuzzleConcluidoRoutine()
    {
        puzzleResolvido = true;
        faseAtualDoPuzzle = 2;
        MostrarMensagem("CAMINHO SEGURO ESTABELECIDO!", Color.green, 4f);
        yield return new WaitForSeconds(1.5f);
        if (barreiraEscada != null) Destroy(barreiraEscada);
    }
}