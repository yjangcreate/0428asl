using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    None,                       // �ʱ�ȭ ����
    StartMenu,                  // ó�� ���۽� ������ ȭ��
    Character_HelloMyName,      // ĳ���Ͱ� ������� ����� ���� ȭ��
    Character_AskName,          // ĳ���Ͱ� ������� �̸��� ���� ȭ��
    User_InputName,             // ����ڰ� ����� �̸��� �Է��ϴ� ȭ��
    User_HowAreYou,             // ����ڰ� ����� ����� ���� ȭ��
    Character_AnswerFeeling,    // ĳ���Ͱ� ������� ����� ���� ȭ��
    User_InputFeeling,          // ����ڰ� ����� ������ �Է��ϴ� ȭ��
    EndMenu,                    // ���� �ó������� ��ȯ�ϴ� ȭ��
}

public enum UIState
{
    None,               // �ʱ�ȭ ����
    Empty,              // �� ����
    StartMenu,          // ó�� ���۽� ������ ȭ��
    EndMenu,            // ���� �ó������� ��ȯ�ϴ� ȭ��
    SubTitle            // �ڸ� ǥ��
}

public class GlobalManager : MonoBehaviour
{
    [Header("State")]
    public GameState CurrentGameState = GameState.None; // ���� ���� ����
    public UIState CurrentUIState = UIState.None; // ���� UI ����

    [Header("GameObject")]
    public Character Character;
    public Animator CharacterAnimator; // ĳ���� ������Ʈ

    [Header("UI")]
    public GameObject StartMenu;
    public GameObject EndMenu;
    public GameObject SubTitle;

    [Space(10)]
    public Button StartButton;
    public TMP_InputField UserNameField; // ������� �̸��� �Է��ϴ� �ʵ�

    public Button RestartButton;

    protected void Awake()
    {
        SetGameState(GameState.StartMenu);

        StartButton.onClick.AddListener(OnClickStartButton);
        RestartButton.onClick.AddListener(OnClickRestartButton);
    }

    public void SetGameState(GameState state)
    {
        CurrentGameState = state;
        switch (state)
        {
            case GameState.StartMenu:
                Debug.Log("GlobalManager: ���� ���¸� StartMenu�� �����մϴ�.");
                SetUIState(UIState.StartMenu); // ���� �޴� UI Ȱ��ȭ
                break;
            case GameState.Character_HelloMyName:
                Debug.Log("GlobalManager: ���� ���¸� Character_HelloMyName���� �����մϴ�.");
                SetUIState(UIState.SubTitle);
                CharacterAnimator.SetTrigger("HelloMyNameTrigger"); // ĳ���� �ִϸ��̼� Ʈ���� ����

                // ĳ���� �ִϸ��̼� ���� �� ���� ���·� ��ȯ
                Character.OnIdle.AddListener(EndCharacterHellowMyName);
                break;
            case GameState.Character_AskName:
                Debug.Log("GlobalManager: ���� ���¸� Character_AskName���� �����մϴ�.");
                SetUIState(UIState.SubTitle);
                CharacterAnimator.SetTrigger("AskNameTrigger"); // ĳ���� �ִϸ��̼� Ʈ���� ����

                // ĳ���� �ִϸ��̼� ���� �� ���� ���·� ��ȯ
                Character.OnIdle.AddListener(EndCharacterAskName);
                break;
            case GameState.User_InputName:
                Debug.Log("GlobalManager: ���� ���¸� User_InputName���� �����մϴ�.");
                // TODO: Input System�� ����Ͽ� ������� �̸��� �Է¹޴� UI�� �����ؾ� �մϴ�.
                SetGameState(GameState.User_HowAreYou);
                break;
            case GameState.User_HowAreYou:
                Debug.Log("GlobalManager: ���� ���¸� User_HowAreYou���� �����մϴ�.");
                // TODO: Input System�� ����Ͽ� ������� ����� �Է¹޴� UI�� �����ؾ� �մϴ�.
                SetGameState(GameState.Character_AnswerFeeling);
                break;
            case GameState.Character_AnswerFeeling:
                Debug.Log("GlobalManager: ���� ���¸� Character_AnswerFeeling���� �����մϴ�.");
                CharacterAnimator.SetTrigger("AnswerFeelingTrigger"); // ĳ���� �ִϸ��̼� Ʈ���� ����

                // ĳ���� �ִϸ��̼� ���� �� ���� ���·� ��ȯ
                Character.OnIdle.AddListener(EndCharacterAnswerFeeling);
                break;
            case GameState.User_InputFeeling:
                Debug.Log("GlobalManager: ���� ���¸� User_InputFeeling���� �����մϴ�.");
                // TODO: Input System�� ����Ͽ� ������� ����� �Է¹޴� UI�� �����ؾ� �մϴ�.
                SetGameState(GameState.EndMenu);
                break;
            case GameState.EndMenu:
                Debug.Log("GlobalManager: ���� ���¸� EndMenu�� �����մϴ�.");
                SetUIState(UIState.EndMenu); // ���� �޴� UI Ȱ��ȭ
                break;
            default:
                Debug.LogWarning("GlobalManager: �߸��� ���� �����Դϴ�.");
                break;
        }
    }

    public void SetUIState(UIState uiState)
    {
        StartMenu.SetActive(false);
        EndMenu.SetActive(false);
        SubTitle.SetActive(false);

        switch (uiState)
        {
            case UIState.Empty:
                break;
            case UIState.StartMenu:
                StartMenu.SetActive(true);
                break;
            case UIState.EndMenu:
                EndMenu.SetActive(true);
                break;
            case UIState.SubTitle:
                SubTitle.SetActive(true);
                break;
            default:
                Debug.LogWarning("GlobalManager: �߸��� UI �����Դϴ�.");
                break;
        }
    }

    public void OnClickStartButton()
    {
        Debug.Log("GlobalManager: �ó������� �����մϴ�.");

        // ���� �ó������� ��ȯ
        SetGameState(GameState.Character_HelloMyName);
    }

    public void OnClickRestartButton()
    {
        Debug.Log("GlobalManager: �ó������� ������մϴ�.");

        SetGameState (GameState.StartMenu);
    }

    public void EndCharacterHellowMyName()
    {
        Debug.Log("GlobalManager: ĳ���Ͱ� HelloMyName �ִϸ��̼��� �����մϴ�.");
        // ĳ���� �ִϸ��̼� ���� �� ���� ���·� ��ȯ
        SetGameState(GameState.Character_AskName);
        Character.OnIdle.RemoveListener(EndCharacterHellowMyName); // ������ ����
    }

    public void EndCharacterAskName()
    {
        Debug.Log("GlobalManager: ĳ���Ͱ� AskName �ִϸ��̼��� �����մϴ�.");
        // ĳ���� �ִϸ��̼� ���� �� ���� ���·� ��ȯ
        SetGameState(GameState.User_InputName);
        Character.OnIdle.RemoveListener(EndCharacterAskName); // ������ ����
    }

    public void EndCharacterAnswerFeeling()
    {
        Debug.Log("GlobalManager: ĳ���Ͱ� AnswerFeeling �ִϸ��̼��� �����մϴ�.");
        // ĳ���� �ִϸ��̼� ���� �� ���� ���·� ��ȯ
        SetGameState(GameState.User_InputFeeling);
        Character.OnIdle.RemoveListener(EndCharacterAnswerFeeling); // ������ ����
    }
}
