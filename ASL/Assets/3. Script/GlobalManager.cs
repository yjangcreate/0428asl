using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    StartMenu,          // 처음 시작시 나오는 화면
    Hello,              // 캐릭터가 인사하는 화면
    HowAreYou,          // 캐릭터가 사용자의 기분을 묻는 화면
    InputFeeling,       // 사용자가 수어로 감정을 입력하는 화면
    CharacterQuestion,  // 캐릭터가 사용자가에게 이름을 묻는 화면
    InputName,          // 사용자가 수어로 이름을 입력하는 화면
    CharacterName,      // 캐릭터가 자기 이름을 말하는 화면
}

public class GlobalManager : MonoBehaviour
{
    public GameObject StartMenu;
    public GameObject NextPopup;

    protected void Awake()
    {
        SetUI(0);
    }

    public void SetUI(int index) // 임시로 인덱스로 동작
    {
        StartMenu.SetActive(false);
        NextPopup.SetActive(false);

        var list = new List<GameObject>();
        list.Add(StartMenu);
        list.Add(NextPopup);

        list[index].SetActive(true);
    }

    public void OnClickStartButton()
    {
        Debug.Log("GlobalManager: 시나리오를 시작합니다.");

        // 다음 시나리오로 전환
        SetUI(1);
    }
}
