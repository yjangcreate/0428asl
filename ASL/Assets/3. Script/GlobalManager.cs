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
    SubTitle,           // 자막 표시
    NameInput,
    HowAreYouInput,
    FeelingInput,
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
    public GameObject NameInput;
    public GameObject HowAreYouInput;
    public GameObject FeelingInput;

    [Space(10)]
    public Button StartButton;
    public TMP_InputField UserNameField; // 사용자의 이름을 입력하는 필드

    [Space(10)]
    public int InputNameIndex;
    public Button NameInputSkipButton;
    public List<GameObject> AlphabetObjectList;

    [Space]
    public int HowAreYouStepCount;
    public Button HowAreYouInputSkipButton;
    public List<GameObject> HowAreYouObjectList;

    [Space(10)]
    public Button FeelingInputSkipButton;

    public Button RestartButton;

    protected void Awake()
    {
        SetGameState(GameState.StartMenu);

        StartButton.onClick.AddListener(OnClickStartButton);
        RestartButton.onClick.AddListener(OnClickRestartButton);

        NameInputSkipButton.onClick.AddListener(StepInputName);
        HowAreYouInputSkipButton.onClick.AddListener(StepHowAreYou);
        FeelingInputSkipButton.onClick.AddListener(StepInputFeeling);

        // 스타트 버튼 활성화 조건
        UserNameField.onValueChanged.AddListener((str) => { 
            StartButton.enabled = !string.IsNullOrEmpty(str); // 텍스트가 입력되어야만 넘어감
        });
    }

    public void SetGameState(GameState state)
    {
        CurrentGameState = state;
        switch (state)
        {
            case GameState.StartMenu:
                // 초기화
                UserNameField.text = ""; 

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
                SetUIState(UIState.NameInput);
                StartInputName();
                break;
            case GameState.User_HowAreYou:
                Debug.Log("GlobalManager: 게임 상태를 User_HowAreYou으로 설정합니다.");
                SetUIState(UIState.HowAreYouInput);
                StartHowAreYou();
                break;
            case GameState.Character_AnswerFeeling:
                Debug.Log("GlobalManager: 게임 상태를 Character_AnswerFeeling으로 설정합니다.");
                SetUIState(UIState.SubTitle);
                CharacterAnimator.SetTrigger("AnswerFeelingTrigger"); // 캐릭터 애니메이션 트리거 설정

                // 캐릭터 애니메이션 종료 후 다음 상태로 전환
                Character.OnIdle.AddListener(EndCharacterAnswerFeeling);
                break;
            case GameState.User_InputFeeling:
                Debug.Log("GlobalManager: 게임 상태를 User_InputFeeling으로 설정합니다.");
                SetUIState(UIState.FeelingInput);
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
        CurrentUIState = uiState;

        StartMenu.SetActive(false);
        EndMenu.SetActive(false);
        SubTitle.SetActive(false);
        NameInput.SetActive(false);
        HowAreYouInput.SetActive(false);
        FeelingInput.SetActive(false);

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
            case UIState.NameInput:
                NameInput.SetActive(true);
                break;
            case UIState.HowAreYouInput:
                HowAreYouInput.SetActive(true);
                break;
            case UIState.FeelingInput:
                FeelingInput.SetActive(true);
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

    public void StartInputName()
    {
        InputNameIndex = -1;
        StepInputName();
    }

    public void StepInputName()
    {
        string userName = UserNameField.text;
        InputNameIndex += 1;

        AlphabetObjectList.ForEach(item => { item.SetActive(false); });
        if (InputNameIndex < userName.Length)
        {
            // 알파벳을 대문자로 변경
            char character = char.ToUpper(userName[InputNameIndex]);
            AlphabetObjectList[character - 'A'].SetActive(true);
        }
        else
        {
            // 다음으로 넘어감
            SetGameState(GameState.User_HowAreYou);
        }
    }

    public void StartHowAreYou()
    {
        HowAreYouStepCount = -1;
        StepHowAreYou();
    }

    public void StepHowAreYou()
    {
        HowAreYouStepCount += 1;

        HowAreYouObjectList.ForEach(item => { item.SetActive(false); });
        if (HowAreYouStepCount < HowAreYouObjectList.Count)
        {
            HowAreYouObjectList[HowAreYouStepCount].SetActive(true);
        }
        else
        {
            SetGameState(GameState.Character_AnswerFeeling);
        }
    }

    public void StepInputFeeling()
    {
        SetGameState(GameState.EndMenu);
    }

    public void EndCharacterHellowMyName()
    {
        Debug.Log("GlobalManager: 캐릭터가 HelloMyName 애니메이션을 종료합니다.");
        // 캐릭터 애니메이션 종료 후 다음 상태로 전환
        Character.OnIdle.RemoveListener(EndCharacterHellowMyName); // 리스너 제거
        SetGameState(GameState.Character_AskName);
    }

    public void EndCharacterAskName()
    {
        Debug.Log("GlobalManager: 캐릭터가 AskName 애니메이션을 종료합니다.");
        // 캐릭터 애니메이션 종료 후 다음 상태로 전환
        Character.OnIdle.RemoveListener(EndCharacterAskName); // 리스너 제거
        SetGameState(GameState.User_InputName);
    }

    public void EndCharacterAnswerFeeling()
    {
        Debug.Log("GlobalManager: 캐릭터가 AnswerFeeling 애니메이션을 종료합니다.");
        // 캐릭터 애니메이션 종료 후 다음 상태로 전환
        Character.OnIdle.RemoveListener(EndCharacterAnswerFeeling); // 리스너 제거
        SetGameState(GameState.User_InputFeeling);
    }
}
