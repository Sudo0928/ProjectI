using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; // �� ���ӽ����̽��� �߰����ݴϴ�.

public class ArrowMenuNavigation : MonoBehaviour
{
    public Image fadeImage;   // �̹��� ������Ʈ
    public float fadeDuration = 2f;  // õõ�� ��Ÿ��/����� �ð�
    private CanvasGroup canvasGroup;

    public GameObject[] arrowImages;  // ȭ��ǥ �̹����� (4��)
    public Transform[] menuItems;     // �޴� �׸���� Transform (5�� �޴�)
    public float arrowOffset = 50f;   // ȭ��ǥ�� �޴� �׸� ������ �̵��ϴ� ����

    private int selectedIndex = 0;  // ���� ���õ� �޴� �׸� �ε���

    public GameObject uiPanel;  // UI �г� (Vertical Layout Group ����)

    void Start()
    {
        canvasGroup = fadeImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = fadeImage.gameObject.AddComponent<CanvasGroup>();
        }

        // ó������ ������ �����ϰ� ����
        canvasGroup.alpha = 0f;

        // ���� �ε�Ǹ� UI �ʱ�ȭ
        uiPanel.SetActive(true);  // ó������ UI�� ��Ȱ��ȭ
        foreach (GameObject arrow in arrowImages)
        {
            arrow.SetActive(true);
        }

        selectedIndex = 0;  // ù ��° �޴� �׸��� ���õ� ���·� �ʱ�ȭ
        ArrowPosition();  // ù ��° �޴� �׸� ���� ȭ��ǥ ǥ��
    }

    void Update()
    {
        // 'W' �Ǵ� 'Up Arrow' Ű�� �޴� ���� �̵�
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = Mathf.Clamp(selectedIndex - 1, 0, menuItems.Length - 1);
            UpdateArrowPosition();
        }

        // 'S' �Ǵ� 'Down Arrow' Ű�� �޴� �Ʒ��� �̵�
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = Mathf.Clamp(selectedIndex + 1, 0, menuItems.Length - 1);

            // ������ �޴� �׸񿡼� �Ʒ��� �̵� �� ù ��° �޴��� ���ư�����
            if (selectedIndex >= menuItems.Length)
            {
                selectedIndex = 0;
            }

            UpdateArrowPosition();
        }

        // Enter Ű�� �޴� ����
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMenu();
        }
    }

    void ArrowPosition()
    {
        // ���õ� �ε����� �´� ȭ��ǥ�� Ȱ��ȭ
        if (arrowImages.Length > selectedIndex)
        {
            arrowImages[selectedIndex].SetActive(true);

            // ���õ� �޴� �׸� ������ ȭ��ǥ ��ġ �̵�
            Vector3 targetPosition = menuItems[selectedIndex].position;

            arrowImages[selectedIndex].transform.position = new Vector3(targetPosition.x - arrowOffset, targetPosition.y, targetPosition.z);
        }
    }

    // ȭ��ǥ ��ġ ������Ʈ
    void UpdateArrowPosition()
    {
        // ���õ� �ε����� �´� ȭ��ǥ�� Ȱ��ȭ
        if (arrowImages.Length > selectedIndex)
        {
            arrowImages[selectedIndex].SetActive(true);

            // ���õ� �޴� �׸� ������ ȭ��ǥ ��ġ �̵�
            Vector3 targetPosition = menuItems[selectedIndex].position;

            arrowImages[selectedIndex].transform.position = new Vector3(targetPosition.x - arrowOffset - 200, targetPosition.y - 40, targetPosition.z);
        }
    }

    // ���õ� �޴��� �� ��ȯ
    void SelectMenu()
    {
        switch (selectedIndex)
        {
            case 0:
                Debug.Log("Menu 1 selected");

                // "Menu 1" ���� �� ���̵� ��, ���̵� �ƿ� ȿ�� �߰�
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

    // FadeIn�� FadeOut�� ó���ϴ� �ڷ�ƾ
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

        // ��� ��� (���̵� �� �Ϸ� �� ��� ���)
        yield return new WaitForSeconds(1f);

        // Fade Out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1 - Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        // �� ��ȯ
        SceneManager.LoadScene(sceneName);
    }
} 
