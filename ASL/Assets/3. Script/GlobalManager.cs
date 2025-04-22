using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    EndMenu,                    // 다음 시나리오로 전환하는 화면
}

public enum UIState
{
    None,               // 초기화 상태
    Empty,              // 빈 상태
    StartMenu,          // 처음 시작시 나오는 화면
    EndMenu,            // 다음 시나리오로 전환하는 화면
    SubTitle            // 자막 표시
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
    public GameObject EndMenu;
    public GameObject SubTitle;

    [Space(10)]
    public Button StartButton;
    public TMP_InputField UserNameField; // 사용자의 이름을 입력하는 필드

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
                Debug.Log("GlobalManager: 게임 상태를 StartMenu로 설정합니다.");
                SetUIState(UIState.StartMenu); // 시작 메뉴 UI 활성화
                break;
            case GameState.Character_HelloMyName:
                Debug.Log("GlobalManager: 게임 상태를 Character_HelloMyName으로 설정합니다.");
                SetUIState(UIState.SubTitle);
                CharacterAnimator.SetTrigger("HelloMyNameTrigger"); // 캐릭터 애니메이션 트리거 설정

                // 캐릭터 애니메이션 종료 후 다음 상태로 전환
                Character.OnIdle.AddListener(EndCharacterHellowMyName);
                break;
            case GameState.Character_AskName:
                Debug.Log("GlobalManager: 게임 상태를 Character_AskName으로 설정합니다.");
                SetUIState(UIState.SubTitle);
                CharacterAnimator.SetTrigger("AskNameTrigger"); // 캐릭터 애니메이션 트리거 설정

                // 캐릭터 애니메이션 종료 후 다음 상태로 전환
                Character.OnIdle.AddListener(EndCharacterAskName);
                break;
            case GameState.User_InputName:
                Debug.Log("GlobalManager: 게임 상태를 User_InputName으로 설정합니다.");
                // TODO: Input System을 사용하여 사용자의 이름을 입력받는 UI를 구현해야 합니다.
                SetGameState(GameState.User_HowAreYou);
                break;
            case GameState.User_HowAreYou:
                Debug.Log("GlobalManager: 게임 상태를 User_HowAreYou으로 설정합니다.");
                // TODO: Input System을 사용하여 사용자의 기분을 입력받는 UI를 구현해야 합니다.
                SetGameState(GameState.Character_AnswerFeeling);
                break;
            case GameState.Character_AnswerFeeling:
                Debug.Log("GlobalManager: 게임 상태를 Character_AnswerFeeling으로 설정합니다.");
                CharacterAnimator.SetTrigger("AnswerFeelingTrigger"); // 캐릭터 애니메이션 트리거 설정

                // 캐릭터 애니메이션 종료 후 다음 상태로 전환
                Character.OnIdle.AddListener(EndCharacterAnswerFeeling);
                break;
            case GameState.User_InputFeeling:
                Debug.Log("GlobalManager: 게임 상태를 User_InputFeeling으로 설정합니다.");
                // TODO: Input System을 사용하여 사용자의 기분을 입력받는 UI를 구현해야 합니다.
                SetGameState(GameState.EndMenu);
                break;
            case GameState.EndMenu:
                Debug.Log("GlobalManager: 게임 상태를 EndMenu로 설정합니다.");
                SetUIState(UIState.EndMenu); // 종료 메뉴 UI 활성화
                break;
            default:
                Debug.LogWarning("GlobalManager: 잘못된 게임 상태입니다.");
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
                Debug.LogWarning("GlobalManager: 잘못된 UI 상태입니다.");
                break;
        }
    }

    public void OnClickStartButton()
    {
        Debug.Log("GlobalManager: 시나리오를 시작합니다.");

        // 다음 시나리오로 전환
        SetGameState(GameState.Character_HelloMyName);
    }

    public void OnClickRestartButton()
    {
        Debug.Log("GlobalManager: 시나리오를 재시작합니다.");

        SetGameState (GameState.StartMenu);
    }

    public void EndCharacterHellowMyName()
    {
        Debug.Log("GlobalManager: 캐릭터가 HelloMyName 애니메이션을 종료합니다.");
        // 캐릭터 애니메이션 종료 후 다음 상태로 전환
        SetGameState(GameState.Character_AskName);
        Character.OnIdle.RemoveListener(EndCharacterHellowMyName); // 리스너 제거
    }

    public void EndCharacterAskName()
    {
        Debug.Log("GlobalManager: 캐릭터가 AskName 애니메이션을 종료합니다.");
        // 캐릭터 애니메이션 종료 후 다음 상태로 전환
        SetGameState(GameState.User_InputName);
        Character.OnIdle.RemoveListener(EndCharacterAskName); // 리스너 제거
    }

    public void EndCharacterAnswerFeeling()
    {
        Debug.Log("GlobalManager: 캐릭터가 AnswerFeeling 애니메이션을 종료합니다.");
        // 캐릭터 애니메이션 종료 후 다음 상태로 전환
        SetGameState(GameState.User_InputFeeling);
        Character.OnIdle.RemoveListener(EndCharacterAnswerFeeling); // 리스너 제거
    }
}
