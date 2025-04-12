using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    StartMenu,          // ó�� ���۽� ������ ȭ��
    Hello,              // ĳ���Ͱ� �λ��ϴ� ȭ��
    HowAreYou,          // ĳ���Ͱ� ������� ����� ���� ȭ��
    InputFeeling,       // ����ڰ� ����� ������ �Է��ϴ� ȭ��
    CharacterQuestion,  // ĳ���Ͱ� ����ڰ����� �̸��� ���� ȭ��
    InputName,          // ����ڰ� ����� �̸��� �Է��ϴ� ȭ��
    CharacterName,      // ĳ���Ͱ� �ڱ� �̸��� ���ϴ� ȭ��
}

public class GlobalManager : MonoBehaviour
{
    public GameObject StartMenu;
    public GameObject NextPopup;

    protected void Awake()
    {
        SetUI(0);
    }

    public void SetUI(int index) // �ӽ÷� �ε����� ����
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
        Debug.Log("GlobalManager: �ó������� �����մϴ�.");

        // ���� �ó������� ��ȯ
        SetUI(1);
    }
}
