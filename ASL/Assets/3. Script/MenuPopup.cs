using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPopup : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button signLanguageToggleButton;
    [SerializeField] private Button subtitleToggleButton;
    
    private bool isSignLanguageEnabled = true;
    private bool isSubtitleEnabled = true;

    private void Start()
    {
        // 종료 버튼 설정
        exitButton.onClick.AddListener(OnExitButtonClick);
        
        // 수어 활성화/비활성화 버튼 설정
        signLanguageToggleButton.onClick.AddListener(OnSignLanguageToggleButtonClick);
        
        // 자막 활성화/비활성화 버튼 설정
        subtitleToggleButton.onClick.AddListener(OnSubtitleToggleButtonClick);
    }

    private void OnExitButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void OnSignLanguageToggleButtonClick()
    {
        isSignLanguageEnabled = !isSignLanguageEnabled;
        // 여기에 수어 인식 기능 활성화/비활성화 로직 추가
        Debug.Log($"수어 인식: {(isSignLanguageEnabled ? "활성화" : "비활성화")}");
    }

    private void OnSubtitleToggleButtonClick()
    {
        isSubtitleEnabled = !isSubtitleEnabled;
        // 여기에 자막 표시 기능 활성화/비활성화 로직 추가
        Debug.Log($"자막: {(isSubtitleEnabled ? "활성화" : "비활성화")}");
    }
} 