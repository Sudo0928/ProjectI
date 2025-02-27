using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; // 이 네임스페이스를 추가해줍니다.

public class ArrowMenuNavigation : MonoBehaviour
{
    public Image fadeImage;   // 이미지 컴포넌트
    public float fadeDuration = 2f;  // 천천히 나타날/사라질 시간
    private CanvasGroup canvasGroup;

    public GameObject[] arrowImages;  // 화살표 이미지들 (4개)
    public Transform[] menuItems;     // 메뉴 항목들의 Transform (5개 메뉴)
    public float arrowOffset = 50f;   // 화살표가 메뉴 항목 옆으로 이동하는 간격

    private int selectedIndex = 0;  // 현재 선택된 메뉴 항목 인덱스

    public GameObject uiPanel;  // UI 패널 (Vertical Layout Group 포함)

    void Start()
    {
        canvasGroup = fadeImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = fadeImage.gameObject.AddComponent<CanvasGroup>();
        }

        // 처음에는 완전히 투명하게 설정
        canvasGroup.alpha = 0f;

        // 씬이 로드되면 UI 초기화
        uiPanel.SetActive(true);  // 처음에는 UI를 비활성화
        foreach (GameObject arrow in arrowImages)
        {
            arrow.SetActive(true);
        }

        selectedIndex = 0;  // 첫 번째 메뉴 항목을 선택된 상태로 초기화
        ArrowPosition();  // 첫 번째 메뉴 항목 옆에 화살표 표시
    }

    void Update()
    {
        // 'W' 또는 'Up Arrow' 키로 메뉴 위로 이동
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = Mathf.Clamp(selectedIndex - 1, 0, menuItems.Length - 1);
            UpdateArrowPosition();
        }

        // 'S' 또는 'Down Arrow' 키로 메뉴 아래로 이동
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = Mathf.Clamp(selectedIndex + 1, 0, menuItems.Length - 1);

            // 마지막 메뉴 항목에서 아래로 이동 시 첫 번째 메뉴로 돌아가도록
            if (selectedIndex >= menuItems.Length)
            {
                selectedIndex = 0;
            }

            UpdateArrowPosition();
        }

        // Enter 키로 메뉴 선택
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMenu();
        }
    }

    void ArrowPosition()
    {
        // 선택된 인덱스에 맞는 화살표를 활성화
        if (arrowImages.Length > selectedIndex)
        {
            arrowImages[selectedIndex].SetActive(true);

            // 선택된 메뉴 항목 옆으로 화살표 위치 이동
            Vector3 targetPosition = menuItems[selectedIndex].position;

            arrowImages[selectedIndex].transform.position = new Vector3(targetPosition.x - arrowOffset, targetPosition.y, targetPosition.z);
        }
    }

    // 화살표 위치 업데이트
    void UpdateArrowPosition()
    {
        // 선택된 인덱스에 맞는 화살표를 활성화
        if (arrowImages.Length > selectedIndex)
        {
            arrowImages[selectedIndex].SetActive(true);

            // 선택된 메뉴 항목 옆으로 화살표 위치 이동
            Vector3 targetPosition = menuItems[selectedIndex].position;

            arrowImages[selectedIndex].transform.position = new Vector3(targetPosition.x - arrowOffset - 200, targetPosition.y - 40, targetPosition.z);
        }
    }

    // 선택된 메뉴로 씬 전환
    void SelectMenu()
    {
        switch (selectedIndex)
        {
            case 0:
                Debug.Log("Menu 1 selected");

                // "Menu 1" 선택 시 페이드 인, 페이드 아웃 효과 추가
                StartCoroutine(FadeInOutScene("GY_TestScene"));
                break;

            case 1:
                Debug.Log("Menu 2 selected");
                // SceneManager.LoadScene("Scene2");
                StartCoroutine(FadeInOutScene("GY_TestScene"));
                break;

            case 2:
                Debug.Log("Menu 3 selected");
                // SceneManager.LoadScene("Scene3");
                break;

            case 3:
                Debug.Log("Menu 4 selected");
                // SceneManager.LoadScene("Scene4");
                break;

            case 4:
                Debug.Log("Menu 5 selected");
                // SceneManager.LoadScene("Scene5");
                break;

            default:
                break;
        }
    }

    // FadeIn과 FadeOut을 처리하는 코루틴
    IEnumerator FadeInOutScene(string sceneName)
    {
        // Fade In
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        // 잠시 대기 (페이드 인 완료 후 잠시 대기)
        yield return new WaitForSeconds(1f);

        // Fade Out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1 - Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        // 씬 전환
        SceneManager.LoadScene(sceneName);
    }
} 
