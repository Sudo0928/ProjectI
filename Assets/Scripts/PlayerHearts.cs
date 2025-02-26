using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameObject healthSlotPrefab;  // 하트 슬롯 프리팹 (하트 이미지를 포함한 UI 요소)
    public Transform healthBar;         // 하트 슬롯을 담을 부모 오브젝트 (UI에서 표시될 위치)
    public Sprite fullHeart;            // 가득 찬 하트 스프라이트
    public Sprite halfHeart;            // 반 찬 하트 스프라이트
    public Sprite emptyHeart;           // 빈 하트 스프라이트

    private float currentHealth = 6f;   // 현재 체력 (최대 6)
    private float maxHealth = 6f;       // 최대 체력 (6)

    private Image[] healthSlots;        // 체력 슬롯들을 저장할 배열

    void Start()
    {
        // 시작할 때 최대 체력에 맞는 하트 슬롯을 생성
        CreateHealthSlots();
        UpdateHealthBar();
    }

    // 체력 바 업데이트 함수
    public void UpdateHealthBar()
    {
        // 현재 체력에 맞게 각 하트 슬롯을 업데이트
        for (int i = 0; i < healthSlots.Length; i++)
        {
            if (currentHealth >= i + 1)
            {
                healthSlots[i].sprite = fullHeart;  // 가득 찬 하트
            }
            else if (currentHealth >= i + 0.5f)
            {
                healthSlots[i].sprite = halfHeart;  // 반 칸 하트
            }
            else
            {
                healthSlots[i].sprite = emptyHeart; // 빈 하트
            }
        }
    }

    // 체력 바를 최대 체력에 맞게 동적으로 생성
    private void CreateHealthSlots()
    {
        // 기존에 생성된 하트 슬롯을 삭제 (리셋)
        foreach (Transform child in healthBar)
        {
            Destroy(child.gameObject);
        }

        // 최대 체력에 맞게 하트 슬롯 생성
        healthSlots = new Image[(int)(maxHealth)];
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject healthSlot = Instantiate(healthSlotPrefab, healthBar);
            healthSlots[i] = healthSlot.GetComponent<Image>();
        }
    }

    // 코인 폭탄 열쇠

    // 체력을 차감하는 함수 (반 칸 피해)
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0f;  // 체력이 0 미만이 되지 않도록 처리
        UpdateHealthBar();
    }

    // 체력을 회복하는 함수 (반 칸 회복)
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;  // 체력이 최대를 넘지 않도록 처리
        UpdateHealthBar();
    }

    // 최대 체력 증가 함수
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;  // 최대 체력 증가
        CreateHealthSlots();   // 최대 체력에 맞게 하트 슬롯을 다시 생성
        if (currentHealth > maxHealth) currentHealth = maxHealth;  // 현재 체력이 최대를 넘지 않도록 처리
        UpdateHealthBar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) // H 키로 피해 받기 (반 칸 피해)
        {
            TakeDamage(0.5f); // 반 칸 피해
        }
        if (Input.GetKeyDown(KeyCode.J)) // J 키로 체력 회복 (반 칸 회복)
        {
            Heal(0.5f); // 반 칸 회복
        }

        // 예시로 최대 체력을 증가시키는 테스트 (P 키로 최대 체력 증가)
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseMaxHealth(1f); // 최대 체력을 1 증가
        }
    }
}
