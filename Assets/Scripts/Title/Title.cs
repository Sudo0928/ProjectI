using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필요

public class SceneTransition : MonoBehaviour
{

    // UI 버튼을 클릭하면 호출되는 함수
    public void OnStartButtonClicked()
    {
        // "StartScene" 씬으로 전환
        SceneManager.LoadScene("StartScene");
    }

    void Update()
    {
        // Enter 키를 눌렀을 때 씬 전환
        if (Input.GetKeyDown(KeyCode.Return)) // Enter 키
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
