using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameObject healthSlotPrefab;  // ��Ʈ ���� ������ (��Ʈ �̹����� ������ UI ���)
    public Transform healthBar;         // ��Ʈ ������ ���� �θ� ������Ʈ (UI���� ǥ�õ� ��ġ)
    public Sprite fullHeart;            // ���� �� ��Ʈ ��������Ʈ
    public Sprite halfHeart;            // �� �� ��Ʈ ��������Ʈ
    public Sprite emptyHeart;           // �� ��Ʈ ��������Ʈ

    private float currentHealth = 6f;   // ���� ü�� (�ִ� 6)
    private float maxHealth = 6f;       // �ִ� ü�� (6)

    private Image[] healthSlots;        // ü�� ���Ե��� ������ �迭

    void Start()
    {
        // ������ �� �ִ� ü�¿� �´� ��Ʈ ������ ����
        CreateHealthSlots();
        UpdateHealthBar();
    }

    // ü�� �� ������Ʈ �Լ�
    public void UpdateHealthBar()
    {
        // ���� ü�¿� �°� �� ��Ʈ ������ ������Ʈ
        for (int i = 0; i < healthSlots.Length; i++)
        {
            if (currentHealth >= i + 1)
            {
                healthSlots[i].sprite = fullHeart;  // ���� �� ��Ʈ
            }
            else if (currentHealth >= i + 0.5f)
            {
                healthSlots[i].sprite = halfHeart;  // �� ĭ ��Ʈ
            }
            else
            {
                healthSlots[i].sprite = emptyHeart; // �� ��Ʈ
            }
        }
    }

    // ü�� �ٸ� �ִ� ü�¿� �°� �������� ����
    private void CreateHealthSlots()
    {
        // ������ ������ ��Ʈ ������ ���� (����)
        foreach (Transform child in healthBar)
        {
            Destroy(child.gameObject);
        }

        // �ִ� ü�¿� �°� ��Ʈ ���� ����
        healthSlots = new Image[(int)(maxHealth)];
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject healthSlot = Instantiate(healthSlotPrefab, healthBar);
            healthSlots[i] = healthSlot.GetComponent<Image>();
        }
    }

    // ���� ��ź ����

    // ü���� �����ϴ� �Լ� (�� ĭ ����)
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0f;  // ü���� 0 �̸��� ���� �ʵ��� ó��
        UpdateHealthBar();
    }

    // ü���� ȸ���ϴ� �Լ� (�� ĭ ȸ��)
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;  // ü���� �ִ븦 ���� �ʵ��� ó��
        UpdateHealthBar();
    }

    // �ִ� ü�� ���� �Լ�
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;  // �ִ� ü�� ����
        CreateHealthSlots();   // �ִ� ü�¿� �°� ��Ʈ ������ �ٽ� ����
        if (currentHealth > maxHealth) currentHealth = maxHealth;  // ���� ü���� �ִ븦 ���� �ʵ��� ó��
        UpdateHealthBar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) // H Ű�� ���� �ޱ� (�� ĭ ����)
        {
            TakeDamage(0.5f); // �� ĭ ����
        }
        if (Input.GetKeyDown(KeyCode.J)) // J Ű�� ü�� ȸ�� (�� ĭ ȸ��)
        {
            Heal(0.5f); // �� ĭ ȸ��
        }

        // ���÷� �ִ� ü���� ������Ű�� �׽�Ʈ (P Ű�� �ִ� ü�� ����)
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseMaxHealth(1f); // �ִ� ü���� 1 ����
        }
    }
}
