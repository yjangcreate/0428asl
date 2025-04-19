using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

public enum UIState
{
    None,               // �ʱ�ȭ ����
    Empty,              // �� ����
    StartMenu,          // ó�� ���۽� ������ ȭ��
    NextPopup,          // ���� �ó������� ��ȯ�ϴ� ȭ��
}

public enum WorldUIState
{
    None,               // �ʱ�ȭ ����
    Empty,              // �� ����
    Display,            // ȭ�鿡 ǥ�õǴ� ����
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
    public GameObject NextPopup;

    [Header("WorldUI")]
    public GameObject WorldUI; // ���� UI ������Ʈ

    protected void Awake()
    {
        SetGameState(GameState.StartMenu);
    }

    public void SetGameState(GameState state)
    {
        CurrentGameState = state;
        switch (state)
        {
            case GameState.StartMenu:
                SetUIState(UIState.StartMenu); // ���� �޴� UI Ȱ��ȭ
                SetWorldUIState(WorldUIState.Empty); // ���� UI ��Ȱ��ȭ
                break;
            case GameState.Character_HelloMyName:
                SetUIState(UIState.Empty); // �� UI ���·� ����
                SetWorldUIState(WorldUIState.Display); // ���� UI Ȱ��ȭ
                CharacterAnimator.SetTrigger("HelloMyNameTrigger"); // ĳ���� �ִϸ��̼� Ʈ���� ����

                Character.OnIdle += () =>
                {
                    // ĳ���Ͱ� Idle ������ �� ȣ��Ǵ� �̺�Ʈ
                    Debug.Log("ĳ���Ͱ� Idle �����Դϴ�.");
                    SetGameState(GameState.Character_AskName); // ���� ���� ���·� ��ȯ
                };
                break;
            case GameState.Character_AskName:
                break;
            case GameState.User_InputName:
                break;
            case GameState.User_HowAreYou:
                break;
            case GameState.Character_AnswerFeeling:
                break;
            case GameState.User_InputFeeling:
                break;
            default:
                Debug.LogWarning("GlobalManager: �߸��� ���� �����Դϴ�.");
                break;
        }
    }

    public void SetUIState(UIState uiState)
    {
        StartMenu.SetActive(false);
        NextPopup.SetActive(false);

        switch (uiState)
        {
            case UIState.Empty:
                break;
            case UIState.StartMenu:
                StartMenu.SetActive(true);
                break;
            case UIState.NextPopup:
                NextPopup.SetActive(true);
                break;
            default:
                Debug.LogWarning("GlobalManager: �߸��� UI �����Դϴ�.");
                break;
        }
    }

    public void SetWorldUIState(WorldUIState worldUIState)
    {
        WorldUI.SetActive(false);
        switch (worldUIState)
        {
            case WorldUIState.Empty:
                break;
            case WorldUIState.Display:
                WorldUI.SetActive(true);
                break;
            default:
                Debug.LogWarning("GlobalManager: �߸��� ���� UI �����Դϴ�.");
                break;
        }
    }

    public void OnClickStartButton()
    {
        Debug.Log("GlobalManager: �ó������� �����մϴ�.");

        // ���� �ó������� ��ȯ
        SetGameState(GameState.Character_HelloMyName);
    }
}
