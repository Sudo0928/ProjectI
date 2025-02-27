using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    public Stat playerStat { private get; set; }

    [SerializeField] List<Image> hearts = new List<Image>();
    [Space(10)]
    [SerializeField] Sprite fullHeart;            // 가득 찬 하트 스프라이트
    [SerializeField] Sprite halfHeart;            // 반 찬 하트 스프라이트
    [SerializeField] Sprite emptyHeart;           // 빈 하트 스프라이트
    [SerializeField] Sprite soulHeart;
    [SerializeField] Sprite halfSoulHeart;
    [SerializeField] Sprite halfBlackHeart;
    [SerializeField] Sprite blackHeart;

    int bombCount = 00;
    int keyCount = 00;
    int coinCount = 00;

    [Space(10)]
    [SerializeField] Text coinCntText;
    [SerializeField] Text boomCntText;
    [SerializeField] Text keyCntText;

    [Space(10)]
    [SerializeField] Image activeItemSprite;
    [SerializeField] GameObject gauge;

    [Space(10)]
    [SerializeField] List<GameObject> gaugeLevels = new List<GameObject>();
    [SerializeField] Image gaugeFill;

    private void Start()
    {

        // playerStat의 Heart 값이 변경될 때 UpdateHealthBar() 호출
        playerStat.AddListener(DesignEnums.Option.MaxHeart, UpdateHealthBar);
        playerStat.AddListener(DesignEnums.Option.CurHeart, UpdateHealthBar);
        playerStat.AddListener(DesignEnums.Option.Coin, UpdateItemText);
        playerStat.AddListener(DesignEnums.Option.Boom, UpdateItemText);
        playerStat.AddListener(DesignEnums.Option.Key, UpdateItemText); 
        playerStat.onChangeGauge.AddListener(UpdateGaugeLevel);
        playerStat.onChangeGauge.AddListener(UpdateGaugeState);
         
        // 처음 상태 업데이트
        UpdateItemText();
        UpdateHealthBar();
        UpdateGaugeLevel();
        UpdateGaugeState();
    }

    // 1. ItemInfo를 인자로 받아서, 아이템의 이미지를 activeItemSprite에 넣어주기
    public void UpdateActiveItem(ItemInfo item)
    {
        activeItemSprite.sprite = GameManager.Instance.GetItemSprite(item);
    }

    // 2. 게이지 값 업데이트 (게이지의 비율을 계산하여 fillAmount 적용)
    void UpdateGaugeState()
    {
        int maxGauge = playerStat.MaxGauge;  // 최대 게이지
        int curGauge = playerStat.CurGauge;  // 최대 게이지 

        // 게이지의 비율을 계산하여 0과 1 사이로 설정 
        gaugeFill.fillAmount = curGauge / maxGauge;
    }

    // 3. 아이템에 맞는 게이지 레벨에 따라 gaugeLevels 활성화/비활성화
    void UpdateGaugeLevel()
    {
        // 아이템에 맞는 게이지 개수를 가져오기 (아이템에 필요한 게이지 레벨을 가져옴)
        int requiredGaugeLevel = playerStat.MaxGauge;

        activeItemSprite.gameObject.SetActive(requiredGaugeLevel > 0);
		gauge.gameObject.SetActive(requiredGaugeLevel > 0);

		 
        // gaugeLevels는 여러 개의 이미지나 오브젝트들 리스트
        // 그중에서 필요한 만큼만 활성화하고 나머지는 비활성화
        for (int i = 0; i < gaugeLevels.Count; i++)
        {
                gaugeLevels[i].SetActive(i == requiredGaugeLevel-1);  // 해당 레벨은 활성화
        } 
    }

    // 4. 체력바 업데이트 
    public void UpdateHealthBar()
    { 
        float maxHealth = playerStat.GetStat(DesignEnums.Option.MaxHeart);  // 최대 체력
        float currentHealth = playerStat.GetStat(DesignEnums.Option.CurHeart);  // 현재 체력

        int cnt = (int)Mathf.Ceil(maxHealth);
		// 체력 UI 업데이트
		for (int i = 1; i <= cnt; i++) 
        {
            if (i < currentHealth)  // 체력이 가득 찬 하트로 채우기
            {
                hearts[i-1].sprite = fullHeart;
            }
            else if ((float)i - currentHealth == 0.5f)  // 반 칸 체력
            {
                hearts[i-1].sprite = halfHeart;
            }
            else  // 빈 하트
            {
                hearts[i-1].sprite = emptyHeart;
            }
			hearts[i-1].gameObject.SetActive(true);
		}

        // 최대 체력에 맞게 하트 개수 조정
        for (int i = cnt; i < hearts.Count; i++)
        {
            hearts[i].gameObject.SetActive(false);  // 남는 하트는 비활성화
        }
    }

    // 아이템 수치 텍스트 업데이트
    void UpdateItemText()
    {
        // 세 자리 숫자 제한, 99를 넘지 않도록 처리
        coinCntText.text = coinCount.ToString("D2");
        boomCntText.text = bombCount.ToString("D2");
        keyCntText.text = keyCount.ToString("D2");
    }


    public void UpdateHealthBarWithSoulAndBlackHearts()
    {
        float maxHealth = playerStat.GetStat(DesignEnums.Option.MaxHeart);  // 최대 체력
        float currentHealth = playerStat.GetStat(DesignEnums.Option.CurHeart);  // 현재 체력

        // 최대 체력에 맞게 하트 슬롯 생성
        for (int i = 0; i < maxHealth; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }

        for (int i = (int)maxHealth; i < hearts.Count; i++)
            hearts[i].gameObject.SetActive(false);
    } 
}

