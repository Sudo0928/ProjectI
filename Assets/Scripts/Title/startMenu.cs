using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowMenuNavigation : MonoBehaviour
{
    public GameObject[] arrowImages;  // 화살표 이미지들 (4개)
    public Transform[] menuItems;     // 메뉴 항목들의 Transform (5개 메뉴)
    public float arrowOffset = 50f;   // 화살표가 메뉴 항목 옆으로 이동하는 간격

    private int selectedIndex = 0;  // 현재 선택된 메뉴 항목 인덱스

    public GameObject uiPanel;  // UI 패널 (Vertical Layout Group 포함)



    void Start()
    {
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

            arrowImages[selectedIndex].transform.position = new Vector3(targetPosition.x - arrowOffset + 700, targetPosition.y + 780, targetPosition.z);
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

            arrowImages[selectedIndex].transform.position = new Vector3(targetPosition.x - arrowOffset -220, targetPosition.y -60, targetPosition.z);
        }
    }

    // 선택된 메뉴로 씬 전환
    void SelectMenu()
    {
        switch (selectedIndex)
        {
            case 0:
                Debug.Log("Menu 1 selected");
                // 씬 전환 코드 예시
                // SceneManager.LoadScene("Scene1");
                break;
            case 1:
                Debug.Log("Menu 2 selected");
                // SceneManager.LoadScene("Scene2");
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
}
