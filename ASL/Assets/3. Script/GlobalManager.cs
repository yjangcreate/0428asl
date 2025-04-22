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
    SubTitle,           // �ڸ� ǥ��
    NameInput,
    HowAreYouInput,
    FeelingInput,
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
    public GameObject NameInput;
    public GameObject HowAreYouInput;
    public GameObject FeelingInput;

    [Space(10)]
    public Button StartButton;
    public TMP_InputField UserNameField; // ������� �̸��� �Է��ϴ� �ʵ�

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

        // ��ŸƮ ��ư Ȱ��ȭ ����
        UserNameField.onValueChanged.AddListener((str) => { 
            StartButton.enabled = !string.IsNullOrEmpty(str); // �ؽ�Ʈ�� �ԷµǾ�߸� �Ѿ
        });
    }

    public void SetGameState(GameState state)
    {
        CurrentGameState = state;
        switch (state)
        {
            case GameState.StartMenu:
                // �ʱ�ȭ
                UserNameField.text = ""; 

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
                SetUIState(UIState.NameInput);
                StartInputName();
                break;
            case GameState.User_HowAreYou:
                Debug.Log("GlobalManager: ���� ���¸� User_HowAreYou���� �����մϴ�.");
                SetUIState(UIState.HowAreYouInput);
                StartHowAreYou();
                break;
            case GameState.Character_AnswerFeeling:
                Debug.Log("GlobalManager: ���� ���¸� Character_AnswerFeeling���� �����մϴ�.");
                SetUIState(UIState.SubTitle);
                CharacterAnimator.SetTrigger("AnswerFeelingTrigger"); // ĳ���� �ִϸ��̼� Ʈ���� ����

                // ĳ���� �ִϸ��̼� ���� �� ���� ���·� ��ȯ
                Character.OnIdle.AddListener(EndCharacterAnswerFeeling);
                break;
            case GameState.User_InputFeeling:
                Debug.Log("GlobalManager: ���� ���¸� User_InputFeeling���� �����մϴ�.");
                SetUIState(UIState.FeelingInput);
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
            // ���ĺ��� �빮�ڷ� ����
            char character = char.ToUpper(userName[InputNameIndex]);
            AlphabetObjectList[character - 'A'].SetActive(true);
        }
        else
        {
            // �������� �Ѿ
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
        Debug.Log("GlobalManager: ĳ���Ͱ� HelloMyName �ִϸ��̼��� �����մϴ�.");
        // ĳ���� �ִϸ��̼� ���� �� ���� ���·� ��ȯ
        Character.OnIdle.RemoveListener(EndCharacterHellowMyName); // ������ ����
        SetGameState(GameState.Character_AskName);
    }

    public void EndCharacterAskName()
    {
        Debug.Log("GlobalManager: ĳ���Ͱ� AskName �ִϸ��̼��� �����մϴ�.");
        // ĳ���� �ִϸ��̼� ���� �� ���� ���·� ��ȯ
        Character.OnIdle.RemoveListener(EndCharacterAskName); // ������ ����
        SetGameState(GameState.User_InputName);
    }

    public void EndCharacterAnswerFeeling()
    {
        Debug.Log("GlobalManager: ĳ���Ͱ� AnswerFeeling �ִϸ��̼��� �����մϴ�.");
        // ĳ���� �ִϸ��̼� ���� �� ���� ���·� ��ȯ
        Character.OnIdle.RemoveListener(EndCharacterAnswerFeeling); // ������ ����
        SetGameState(GameState.User_InputFeeling);
    }
}
