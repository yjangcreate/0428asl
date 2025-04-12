using UnityEngine;
using UnityEngine.UI;

public class SubtitlePopup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Slider subtitleSizeSlider;
    [SerializeField] private Toggle autoScrollToggle;
    [SerializeField] private Toggle showTranslationToggle;
    [SerializeField] private Dropdown subtitlePositionDropdown;
    
    private float subtitleSize = 1f;
    private bool autoScrollEnabled = true;
    private bool showTranslation = true;
    private int subtitlePosition = 0;

    private void Start()
    {
        // 버튼 이벤트 설정
        closeButton.onClick.AddListener(OnCloseButtonClick);
        
        // 슬라이더 이벤트 설정
        subtitleSizeSlider.onValueChanged.AddListener(OnSubtitleSizeChanged);
        
        // 토글 이벤트 설정
        autoScrollToggle.onValueChanged.AddListener(OnAutoScrollChanged);
        showTranslationToggle.onValueChanged.AddListener(OnShowTranslationChanged);
        
        // 드롭다운 이벤트 설정
        subtitlePositionDropdown.onValueChanged.AddListener(OnSubtitlePositionChanged);

        // 초기값 설정
        subtitleSizeSlider.value = subtitleSize;
        autoScrollToggle.isOn = autoScrollEnabled;
        showTranslationToggle.isOn = showTranslation;
        subtitlePositionDropdown.value = subtitlePosition;
    }

    private void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }

    private void OnSubtitleSizeChanged(float value)
    {
        subtitleSize = value;
        // 여기에 자막 크기 변경 로직 추가
        Debug.Log($"자막 크기: {subtitleSize}");
    }

    private void OnAutoScrollChanged(bool value)
    {
        autoScrollEnabled = value;
        // 여기에 자동 스크롤 설정 변경 로직 추가
        Debug.Log($"자동 스크롤: {(autoScrollEnabled ? "활성화" : "비활성화")}");
    }

    private void OnShowTranslationChanged(bool value)
    {
        showTranslation = value;
        // 여기에 번역 표시 설정 변경 로직 추가
        Debug.Log($"번역 표시: {(showTranslation ? "활성화" : "비활성화")}");
    }

    private void OnSubtitlePositionChanged(int value)
    {
        subtitlePosition = value;
        // 여기에 자막 위치 변경 로직 추가
        Debug.Log($"자막 위치: {subtitlePosition}");
    }
} 