using UnityEngine;

public class MenuController : MonoBehaviour
{
    // 메뉴 항목을 관리하는 변수
    public GameObject[] menuSprites;           // 메뉴로 사용할 스프라이트 이미지들
    public Sprite[] allSprites;                // 11개의 이미지를 위한 스프라이트 배열
    public GameObject arrow;                   // 화살표 (현재 선택된 메뉴를 나타내는 화살표)
    public float arrowSpeed = 10f;             // 화살표가 이동하는 속도

    private int currentMenuIndex = 0;          // 현재 선택된 메뉴 항목 인덱스
    private int currentSpriteIndex = 0;        // 현재 선택된 스프라이트 인덱스 (0 ~ 10, 총 11개의 이미지)

    void Start()
    {
        UpdateMenu(); // 메뉴와 화살표 상태 초기화
    }

    void Update()
    {
        // 화살표를 위로 올리고 아래로 내리는 입력
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentMenuIndex = (currentMenuIndex - 1 + menuSprites.Length) % menuSprites.Length;
            UpdateMenu();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentMenuIndex = (currentMenuIndex + 1) % menuSprites.Length;
            UpdateMenu();
        }

        // 좌우 화살표 입력 (스프라이트 변경)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentSpriteIndex = (currentSpriteIndex - 1 + allSprites.Length) % allSprites.Length;  // 왼쪽으로 이미지 변경
            UpdateMenu();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % allSprites.Length;  // 오른쪽으로 이미지 변경
            UpdateMenu();
        }

        // 화살표 애니메이션 (위/아래로 이동)
        MoveArrow();
    }

    // 메뉴와 스프라이트 업데이트
    void UpdateMenu()
    {
        // 메뉴 스프라이트의 SpriteRenderer를 찾아 이미지를 전환
        SpriteRenderer spriteRenderer = menuSprites[currentMenuIndex].GetComponent<SpriteRenderer>();

        // 현재 메뉴에 해당하는 이미지를 `allSprites` 배열에서 가져옴
        spriteRenderer.sprite = allSprites[currentSpriteIndex];
    }

    // 화살표 이동 (위/아래)
    void MoveArrow()
    {
        // 메뉴 스프라이트 왼쪽에 화살표를 배치
        Vector3 targetPosition = menuSprites[currentMenuIndex].transform.position + new Vector3(-200f, 0f, 0f);
        arrow.transform.position = Vector3.Lerp(arrow.transform.position, targetPosition, Time.deltaTime * arrowSpeed);
    }
}
