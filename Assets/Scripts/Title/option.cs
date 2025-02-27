using UnityEngine;

public class MenuController : MonoBehaviour
{
    // �޴� �׸��� �����ϴ� ����
    public GameObject[] menuSprites;           // �޴��� ����� ��������Ʈ �̹�����
    public Sprite[] allSprites;                // 11���� �̹����� ���� ��������Ʈ �迭
    public GameObject arrow;                   // ȭ��ǥ (���� ���õ� �޴��� ��Ÿ���� ȭ��ǥ)
    public float arrowSpeed = 10f;             // ȭ��ǥ�� �̵��ϴ� �ӵ�

    private int currentMenuIndex = 0;          // ���� ���õ� �޴� �׸� �ε���
    private int currentSpriteIndex = 0;        // ���� ���õ� ��������Ʈ �ε��� (0 ~ 10, �� 11���� �̹���)

    void Start()
    {
        UpdateMenu(); // �޴��� ȭ��ǥ ���� �ʱ�ȭ
    }

    void Update()
    {
        // ȭ��ǥ�� ���� �ø��� �Ʒ��� ������ �Է�
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

        // �¿� ȭ��ǥ �Է� (��������Ʈ ����)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentSpriteIndex = (currentSpriteIndex - 1 + allSprites.Length) % allSprites.Length;  // �������� �̹��� ����
            UpdateMenu();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % allSprites.Length;  // ���������� �̹��� ����
            UpdateMenu();
        }

        // ȭ��ǥ �ִϸ��̼� (��/�Ʒ��� �̵�)
        MoveArrow();
    }

    // �޴��� ��������Ʈ ������Ʈ
    void UpdateMenu()
    {
        // �޴� ��������Ʈ�� SpriteRenderer�� ã�� �̹����� ��ȯ
        SpriteRenderer spriteRenderer = menuSprites[currentMenuIndex].GetComponent<SpriteRenderer>();

        // ���� �޴��� �ش��ϴ� �̹����� `allSprites` �迭���� ������
        spriteRenderer.sprite = allSprites[currentSpriteIndex];
    }

    // ȭ��ǥ �̵� (��/�Ʒ�)
    void MoveArrow()
    {
        // �޴� ��������Ʈ ���ʿ� ȭ��ǥ�� ��ġ
        Vector3 targetPosition = menuSprites[currentMenuIndex].transform.position + new Vector3(-200f, 0f, 0f);
        arrow.transform.position = Vector3.Lerp(arrow.transform.position, targetPosition, Time.deltaTime * arrowSpeed);
    }
}
