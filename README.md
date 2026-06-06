# 🖥️ HackWeb — Escape Room em Unity

> Um jogo de puzzle em primeira pessoa com temática de hacking, onde o jogador precisa resolver enigmas de segurança para avançar pelos andares de uma instalação digital.

\---

## 📋 Índice

* [Sobre o Projeto](#-sobre-o-projeto)
* [Tecnologias Utilizadas](#-tecnologias-utilizadas)
* [Pacotes Unity](#-pacotes-unity)
* [Estrutura de Pastas](#-estrutura-de-pastas)
* [Arquitetura dos Scripts](#-arquitetura-dos-scripts)

  * [PlayerInteraction.cs](#1-playerinteractioncs--sistema-de-interação)
  * [TerminalEnigma.cs](#2-terminalenigmacs--puzzle-do-terminal)
  * [PuzzleChaves.cs](#3-puzzlechavescs--puzzle-das-chaves-criptográficas)
  * [WallHologramFeedback.cs](#4-wallhologramfeedbackcs--feedback-holográfico)
* [Fluxo de Gameplay](#-fluxo-de-gameplay)
* [Configuração do Projeto](#-configuração-do-projeto)
* [Como Configurar no Unity Editor](#-como-configurar-no-unity-editor)
* [Convenções de Tags](#-convenções-de-tags)
* [Contribuindo](#-contribuindo)
* [Roadmap](#-roadmap)
* [Licença](#-licença)

\---

## 🎮 Sobre o Projeto

**HackWeb** é um jogo de exploração em primeira pessoa desenvolvido na **Unity 6**, ambientado em um universo inspirado por conceitos de segurança digital, criptografia e invasão de sistemas.

O objetivo do jogador é explorar o ambiente, interpretar pistas e resolver desafios interativos para desbloquear novas áreas e avançar na experiência. Cada puzzle foi projetado com base em conceitos reais da área de tecnologia, transformando temas como autenticação, criptografia e controle de acesso em mecânicas de gameplay acessíveis e envolventes.

O projeto combina elementos de exploração, raciocínio lógico e interação com o ambiente, proporcionando uma experiência inspirada no estilo *escape room digital*.

### ✨ Principais Funcionalidades

* 🔐 Sistema de terminal interativo com autenticação por senha
* 🔑 Puzzle baseado em conceitos de criptografia assimétrica
* 📡 Feedback visual holográfico para orientação do jogador
* 🎯 Sistema de interação por Raycast em primeira pessoa
* 🎮 Controle dinâmico de câmera e interface durante os desafios
* 🚪 Desbloqueio progressivo de áreas conforme a resolução dos puzzles

\---


## 🎥 Demonstração

Uma demonstração completa do projeto pode ser visualizada no vídeo abaixo:

[▶️ Assistir demonstração no YouTube](https://youtu.be/tPwTM0gcZp8?si=DjDIWzS6D2yex8bE)

---

## 🛠️ Tecnologias Utilizadas

|Tecnologia|Versão|Finalidade|
|-|-|-|
|**Unity**|6.x (URP)|Engine principal do jogo|
|**C#**|.NET Standard 2.1|Linguagem de scripting|
|**Universal Render Pipeline (URP)**|17.3.0|Pipeline de renderização com suporte a efeitos visuais modernos|
|**Unity Input System**|1.19.0|Leitura de input do mouse/teclado (nova API)|
|**TextMeshPro (TMP)**|via ugui 2.0.0|Renderização de texto de alta qualidade na UI|
|**Cinemachine**|3.1.2|Sistema de câmera dinâmica|
|**AI Navigation**|2.0.12|Navegação de NPCs (NavMesh)|

\---

## 📦 Pacotes Unity

Todos os pacotes são gerenciados pelo **Unity Package Manager** e estão declarados em `Packages/manifest.json`.

### Pacotes Principais (instalados manualmente)

```json
{
  "com.unity.ai.navigation": "2.0.12",
  "com.unity.cinemachine": "3.1.2",
  "com.unity.inputsystem": "1.19.0",
  "com.unity.render-pipelines.universal": "17.3.0",
  "com.unity.timeline": "1.8.12",
  "com.unity.ugui": "2.0.0",
  "com.unity.visualscripting": "1.9.11"
}
```

### Por que URP?

O **Universal Render Pipeline** foi escolhido por oferecer:

* Suporte nativo a **efeitos de brilho (Bloom)** e **pós-processamento** — essenciais para a estética cyberpunk
* Melhor performance em uma gama maior de hardware comparado ao HDRP
* Compatibilidade com shaders de holograma e emisssão de luz

\---

## 📁 Estrutura de Pastas

```
HackWeb/
│
├── Assets/
│   ├── Scripts/                  # Todos os scripts C# do projeto
│   │   ├── PlayerInteraction.cs  # Gerencia o raycast e input do jogador
│   │   ├── TerminalEnigma.cs     # Lógica do puzzle de terminal/senha
│   │   ├── PuzzleChaves.cs       # Lógica do puzzle de chaves criptográficas
│   │   └── WallHologramFeedback.cs # Feedback visual holográfico nas paredes
│   │
│   ├── Scenes/                   # Cenas do jogo
│   │   └── MainScene.unity       # Cena principal (a ser adicionada)
│   │
│   ├── Prefabs/                  # Objetos reutilizáveis
│   │   ├── UI/                   # Canvas e elementos de interface
│   │   └── Props/                # Objetos interativos (computador, abajures)
│   │
│   ├── Materials/                # Materiais URP (incluindo shaders holográficos)
│   ├── Audio/                    # Efeitos sonoros e trilha
│   └── Fonts/                    # Fontes TMP (estilo terminal/monospace)
│
├── Packages/
│   ├── manifest.json             # Declaração de dependências do UPM
│   └── packages-lock.json        # Versões travadas de todos os pacotes
│
├── ProjectSettings/              # Configurações do projeto Unity
└── README.md
```

> \*\*Nota:\*\* As pastas `Scenes/`, `Prefabs/`, `Materials/`, `Audio/` e `Fonts/` são sugeridas como boas práticas de organização e podem variar conforme o desenvolvimento avança.

\---

## 🧠 Arquitetura dos Scripts

Os quatro scripts seguem um princípio de **responsabilidade única**: cada um gerencia exatamente um sistema do jogo, comunicando-se entre si através de referências diretas e chamadas de método público.

```
PlayerInteraction
      │
      │ Raycast (Physics.Raycast)
      │
      ├──► TerminalEnigma.AbrirTerminal()
      │         └── Gerencia Canvas UI + Mouse + Movimento do Jogador
      │
      └──► PuzzleChaves.InteragirComAbajur()
                └── Gerencia Sequência de Fases + Barreira de Escada

WallHologramFeedback
      └── Trigger Collider (OnTriggerEnter)
                └── Exibe Canvas Holográfico por tempo determinado
```

\---

### 1\. `PlayerInteraction.cs` — Sistema de Interação

**Responsabilidade:** Detectar quando o jogador clica com o mouse e verificar se o objeto à sua frente é interagível.

**Como funciona:**

A cada frame, o script verifica se o botão esquerdo do mouse foi pressionado usando a nova **Input System API** (`Mouse.current.leftButton.wasPressedThisFrame`). Quando pressionado, ele dispara um `Raycast` a partir da câmera principal na direção em que o jogador está olhando.

```csharp
// O raio parte do centro da câmera e vai até a distância máxima de interação
Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanciaInteracao)
```

Se o raio colidir com um objeto marcado com a **Tag `Interactable`**, o script verifica qual componente de puzzle está presente nesse objeto e chama o método correspondente.

**Parâmetros configuráveis no Inspector:**

|Campo|Tipo|Padrão|Descrição|
|-|-|-|-|
|`distanciaInteracao`|`float`|`4f`|Distância máxima (em unidades Unity) que o jogador consegue interagir|

**Pontos técnicos importantes:**

* Usa `Mouse.current` da nova Input System — **não** o legado `Input.GetMouseButtonDown(0)`
* O raycast detecta **apenas** objetos com a Tag `Interactable`, evitando interações acidentais
* Suporta múltiplos tipos de puzzle no mesmo objeto: verifica `TerminalEnigma` e `PuzzleChaves` de forma independente com `GetComponent<>()`

\---

### 2\. `TerminalEnigma.cs` — Puzzle do Terminal

**Responsabilidade:** Gerenciar toda a interação com o computador/terminal: abrir/fechar a interface, receber a senha digitada e, se correta, remover a barreira que bloqueia o jogador.

**Fluxo completo:**

```
Jogador clica no PC
        ↓
AbrirTerminal()
  • Ativa o Canvas do computador
  • Libera o cursor do mouse (CursorLockMode.None)
  • Desativa o script de movimento da câmera
  • Limpa o campo de senha e coloca foco nele
        ↓
Jogador digita a senha e clica em "Confirmar"
        ↓
ConfirmarSenha()
  • Sanitiza a string (remove zero-width spaces \\u200B e espaços)
  • Compara com senhaCorreta
        ↓
  ┌─ ERRADA ──► Exibe "TENTE NOVAMENTE" em vermelho
  └─ CORRETA ──► AcessoConcedidoRoutine() (Coroutine)
                    • Exibe "ACESSO CONCEDIDO" em verde
                    • Aguarda 2 segundos (yield return new WaitForSeconds(2f))
                    • Destroi a paredeInvisivel
                    • Chama FecharTerminal()
```

**Parâmetros configuráveis no Inspector:**

|Campo|Tipo|Descrição|
|-|-|-|
|`canvasComputador`|`GameObject`|O painel de UI que representa a tela do computador|
|`inputSenha`|`TMP\_InputField`|Campo de texto onde o jogador digita a senha|
|`paredeInvisivel`|`GameObject`|A barreira que será destruída ao acertar a senha|
|`senhaCorreta`|`string`|A senha que o jogador precisa descobrir (padrão: `"2008"`)|
|`feedbackText`|`TextMeshProUGUI`|Texto que exibe "ACESSO CONCEDIDO" ou "TENTE NOVAMENTE"|
|`scriptDeMovimento`|`Behaviour`|Referência ao script de câmera/movimento do jogador|

**Detalhe técnico — Proteção contra "Clique Fantasma":**

Sem proteção, o mesmo clique que fecha o terminal poderia imediatamente reabri-lo no próximo frame. Para evitar isso, `AbrirTerminal()` verifica logo no início se o canvas **já está ativo** e, se estiver, encerra a função imediatamente:

```csharp
public void AbrirTerminal()
{
    // Proteção: ignora o clique se o terminal já estiver aberto
    if (canvasComputador != null \&\& canvasComputador.activeSelf)
    {
        return;
    }
    // ... resto da lógica
}
```

**Detalhe técnico — Sanitização da Senha:**

A comparação de senha remove caracteres invisíveis antes de comparar, evitando falhas silenciosas causadas por zero-width spaces que podem ser inseridos por alguns teclados virtuais ou sistemas de auto-complete:

```csharp
string senhaDigitada = inputSenha.text.Replace("\\u200B", "").Trim();
string senhaAlvo = senhaCorreta.Replace("\\u200B", "").Trim();
```

\---

### 3\. `PuzzleChaves.cs` — Puzzle das Chaves Criptográficas

**Responsabilidade:** Implementar um puzzle de sequência onde o jogador deve interagir com dois objetos (abajures/mesas) na ordem correta — simulando o conceito de **chave pública + chave privada** da criptografia assimétrica.

**Mecânica de Sequência:**

O puzzle tem 3 estados controlados por variáveis **estáticas** (compartilhadas entre todas as instâncias do script):

```
faseAtualDoPuzzle = 0  →  Aguardando Mesa 1 (Chave Pública)
faseAtualDoPuzzle = 1  →  Mesa 1 ativada, aguardando Mesa 3 (Chave Privada)
faseAtualDoPuzzle = 2  →  Puzzle concluído
```

```
Estado Inicial (fase 0)
        ↓
Jogador interage com Mesa 1
  ├─ CORRETO: fase → 1, acende luz da Mesa 1, exibe mensagem amarela
  └─ ERRADO (outra mesa): ErroNaSequencia() → fase → 0, apaga luzes
        ↓
Jogador interage com Mesa 3
  ├─ CORRETO: PuzzleConcluidoRoutine()
  │     • Acende luz da Mesa 3
  │     • Exibe "CAMINHO SEGURO ESTABELECIDO!" em verde
  │     • Aguarda 1.5s
  │     • Destroi barreiraEscada
  └─ ERRADO (outra mesa): ErroNaSequencia() → reinicia do zero
```

**Por que variáveis `static`?**

Cada abajur é um GameObject separado na cena, mas precisa compartilhar o mesmo estado de progresso. Usar `static` garante que todos os objetos `PuzzleChaves` na cena leiam e escrevam no mesmo estado global, sem necessidade de um GameManager externo.

```csharp
private static int faseAtualDoPuzzle = 0;
private static bool puzzleResolvido = false;
private static PuzzleChaves abajurMesa1;
private static PuzzleChaves abajurMesa3;
```

> ⚠️ \*\*Atenção:\*\* Por usar `static`, as variáveis são resetadas no `Awake()` de cada instância. Isso garante que ao recarregar a cena, o puzzle comece do zero.

**Sistema de Mensagens Temporárias:**

O script implementa um sistema próprio de mensagens com tempo de exibição, evitando que mensagens se sobreponham:

```csharp
private static Coroutine rotinaDeTexto;

private void MostrarMensagem(string texto, Color cor, float tempo)
{
    // Cancela a mensagem anterior antes de exibir a nova
    if (rotinaDeTexto != null) StopCoroutine(rotinaDeTexto);
    rotinaDeTexto = StartCoroutine(ApagarMensagemDepoisDeTempo(tempo));
}
```

**Parâmetros configuráveis no Inspector:**

|Campo|Tipo|Descrição|
|-|-|-|
|`numeroDestaMesa`|`int`|Identificador da mesa (use `1` para Chave Pública, `3` para Chave Privada)|
|`indicadorVisualLuz`|`GameObject`|Objeto visual (luz/partícula) que acende ao ativar a mesa|
|`barreiraEscada`|`GameObject`|Barreira que bloqueia a escada, destruída ao completar o puzzle|
|`feedbackText`|`TextMeshProUGUI`|Texto de feedback (pode ser o mesmo do TerminalEnigma ou separado)|

\---

### 4\. `WallHologramFeedback.cs` — Feedback Holográfico

**Responsabilidade:** Exibir um painel holográfico na parede quando o jogador entra em uma zona de trigger, mantendo o display ativo por um tempo configurável e depois desligando automaticamente.

**Funcionamento:**

Utiliza o sistema de **Trigger Colliders** do Unity — o objeto que contém este script deve ter um `Collider` com `Is Trigger = true`. Quando o jogador (Tag `Player`) entra na zona, o canvas holográfico é ativado e um temporizador é iniciado.

```csharp
private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        feedbackCanvas.SetActive(true);
        
        // Reinicia o timer se o jogador entrar/sair/entrar antes do tempo acabar
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideCanvasAfterTime());
    }
}
```

**Parâmetros configuráveis no Inspector:**

|Campo|Tipo|Padrão|Descrição|
|-|-|-|-|
|`feedbackCanvas`|`GameObject`|—|O Canvas com o conteúdo holográfico a ser exibido|
|`displayTime`|`float`|`10f`|Tempo em segundos que o holograma permanece visível|

\---

## 🎮 Fluxo de Gameplay

```
INÍCIO DA CENA
      │
      ▼
\[Exploração Livre em FPS]
      │
      ├──────────────────────────────────────────────┐
      │                                              │
      ▼                                              ▼
\[Jogador entra em zona de Trigger]         \[Jogador clica em objeto Interactable]
      │                                              │
      ▼                                              ├──► \[É um Terminal PC?]
\[WallHologramFeedback ativa]               │              │
\[Holograma exibe pista/dica]               │              ▼
\[Desaparece após 10 segundos]              │        \[TerminalEnigma abre]
                                           │        \[Mouse liberado]
                                           │        \[Movimento desativado]
                                           │        \[Jogador digita senha]
                                           │              │
                                           │         ┌────┴────┐
                                           │         ▼         ▼
                                           │      ERRADA    CORRETA
                                           │         │         │
                                           │      Feedback  "ACESSO
                                           │      vermelho  CONCEDIDO"
                                           │               Parede destruída
                                           │
                                           └──► \[É um Abajur/Chave?]
                                                     │
                                                     ▼
                                           \[PuzzleChaves verifica sequência]
                                                     │
                                              ┌──────┴──────┐
                                              ▼             ▼
                                           ERRADO        CORRETO
                                              │             │
                                           Reset         Avança fase
                                           Luzes         Até completar:
                                           apagam        Barreira da
                                                         escada destruída
```

\---

## ⚙️ Configuração do Projeto

### Pré-requisitos

* **Unity Hub** instalado
* **Unity 6** (versão `6000.x.x` ou superior)
* **Git** instalado na máquina

### Clonando o Repositório

```bash
git clone https://github.com/seu-usuario/hackweb.git
cd hackweb
```

### Abrindo no Unity

1. Abra o **Unity Hub**
2. Clique em **"Add project from disk"**
3. Navegue até a pasta clonada e selecione-a
4. Certifique-se de que a versão do Unity selecionada é a **Unity 6**
5. Clique em **"Open"**

> O Unity resolverá automaticamente todos os pacotes listados em `Packages/manifest.json` na primeira abertura. Isso pode levar alguns minutos.

\---

## 🔧 Como Configurar no Unity Editor

### Configurando o `PlayerInteraction`

1. Selecione o **GameObject do Jogador** (seu Character Controller ou Capsule)
2. Adicione o componente `PlayerInteraction`
3. O campo `distanciaInteracao` já tem valor padrão de `4` — ajuste conforme o tamanho do seu ambiente

### Configurando o `TerminalEnigma`

1. Selecione o **GameObject do Computador** na cena
2. Adicione o componente `TerminalEnigma`
3. Preencha os campos no Inspector:

   * `Canvas Computador` → arraste o Canvas de UI do terminal
   * `Input Senha` → arraste o `TMP\_InputField` de digitação
   * `Parede Invisivel` → arraste o GameObject da barreira a ser destruída
   * `Senha Correta` → digite a senha (ex: `2008`)
   * `Feedback Text` → arraste o `TextMeshProUGUI` de feedback
   * `Script De Movimento` → arraste o componente de câmera (ex: `PlayerLook`, `FirstPersonController`)
4. Adicione a **Tag `Interactable`** ao GameObject do computador
5. No Canvas, conecte o botão "Confirmar" ao método `TerminalEnigma.ConfirmarSenha()`
6. Conecte o botão "Fechar/ESC" ao método `TerminalEnigma.FecharTerminal()`

### Configurando o `PuzzleChaves`

1. Selecione o **GameObject da Mesa 1** (Chave Pública)
2. Adicione o componente `PuzzleChaves`
3. Defina `Numero Desta Mesa` = `1`
4. Arraste o indicador visual de luz para `Indicador Visual Luz`
5. Repita para a **Mesa 3** (Chave Privada), definindo `Numero Desta Mesa` = `3`
6. Em **um** dos dois objetos, arraste a barreira da escada para `Barreira Escada`
7. Adicione a **Tag `Interactable`** aos dois GameObjects das mesas

### Configurando o `WallHologramFeedback`

1. Crie um **GameObject vazio** na parede desejada
2. Adicione um `Box Collider` com **`Is Trigger = true`**
3. Adicione o componente `WallHologramFeedback`
4. Arraste o Canvas do holograma para `Feedback Canvas`
5. Ajuste `Display Time` conforme desejado

\---

