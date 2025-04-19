using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,                       // 초기화 상태
    StartMenu,                  // 처음 시작시 나오는 화면
    Character_HelloMyName,      // 캐릭터가 사용자의 기분을 묻는 화면
    Character_AskName,          // 캐릭터가 사용자의 이름을 묻는 화면
    User_InputName,             // 사용자가 수어로 이름을 입력하는 화면
    User_HowAreYou,             // 사용자가 수어로 기분을 묻는 화면
    Character_AnswerFeeling,    // 캐릭터가 사용자의 기분을 묻는 화면
    User_InputFeeling,          // 사용자가 수어로 감정을 입력하는 화면
}

public enum UIState
{
    None,               // 초기화 상태
    Empty,              // 빈 상태
    StartMenu,          // 처음 시작시 나오는 화면
    NextPopup,          // 다음 시나리오로 전환하는 화면
}

public enum WorldUIState
{
    None,               // 초기화 상태
    Empty,              // 빈 상태
    Display,            // 화면에 표시되는 상태
}

public class GlobalManager : MonoBehaviour
{
    [Header("State")]
    public GameState CurrentGameState = GameState.None; // 현재 게임 상태
    public UIState CurrentUIState = UIState.None; // 현재 UI 상태

    [Header("GameObject")]
    public Character Character;
    public Animator CharacterAnimator; // 캐릭터 오브젝트

    [Header("UI")]
    public GameObject StartMenu;
    public GameObject NextPopup;

    [Header("WorldUI")]
    public GameObject WorldUI; // 월드 UI 오브젝트

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
                SetUIState(UIState.StartMenu); // 시작 메뉴 UI 활성화
                SetWorldUIState(WorldUIState.Empty); // 월드 UI 비활성화
                break;
            case GameState.Character_HelloMyName:
                SetUIState(UIState.Empty); // 빈 UI 상태로 설정
                SetWorldUIState(WorldUIState.Display); // 월드 UI 활성화
                CharacterAnimator.SetTrigger("HelloMyNameTrigger"); // 캐릭터 애니메이션 트리거 설정

                Character.OnIdle += () =>
                {
                    // 캐릭터가 Idle 상태일 때 호출되는 이벤트
                    Debug.Log("캐릭터가 Idle 상태입니다.");
                    SetGameState(GameState.Character_AskName); // 다음 게임 상태로 전환
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
                Debug.LogWarning("GlobalManager: 잘못된 게임 상태입니다.");
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
                Debug.LogWarning("GlobalManager: 잘못된 UI 상태입니다.");
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
                Debug.LogWarning("GlobalManager: 잘못된 월드 UI 상태입니다.");
                break;
        }
    }

    public void OnClickStartButton()
    {
        Debug.Log("GlobalManager: 시나리오를 시작합니다.");

        // 다음 시나리오로 전환
        SetGameState(GameState.Character_HelloMyName);
    }
}
