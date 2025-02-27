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
    [SerializeField] Sprite fullHeart;            // ���� �� ��Ʈ ��������Ʈ
    [SerializeField] Sprite halfHeart;            // �� �� ��Ʈ ��������Ʈ
    [SerializeField] Sprite emptyHeart;           // �� ��Ʈ ��������Ʈ
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

        // playerStat�� Heart ���� ����� �� UpdateHealthBar() ȣ��
        playerStat.AddListener(DesignEnums.Option.MaxHeart, UpdateHealthBar);
        playerStat.AddListener(DesignEnums.Option.CurHeart, UpdateHealthBar);
        playerStat.AddListener(DesignEnums.Option.Coin, UpdateItemText);
        playerStat.AddListener(DesignEnums.Option.Boom, UpdateItemText);
        playerStat.AddListener(DesignEnums.Option.Key, UpdateItemText); 
        playerStat.onChangeGauge.AddListener(UpdateGaugeLevel);
        playerStat.onChangeGauge.AddListener(UpdateGaugeState);
         
        // ó�� ���� ������Ʈ
        UpdateItemText();
        UpdateHealthBar();
        UpdateGaugeLevel();
        UpdateGaugeState();
    }

    // 1. ItemInfo�� ���ڷ� �޾Ƽ�, �������� �̹����� activeItemSprite�� �־��ֱ�
    public void UpdateActiveItem(ItemInfo item)
    {
        activeItemSprite.sprite = GameManager.Instance.GetItemSprite(item);
    }

    // 2. ������ �� ������Ʈ (�������� ������ ����Ͽ� fillAmount ����)
    void UpdateGaugeState()
    {
        int maxGauge = playerStat.MaxGauge;  // �ִ� ������
        int curGauge = playerStat.CurGauge;  // �ִ� ������ 

        // �������� ������ ����Ͽ� 0�� 1 ���̷� ���� 
        gaugeFill.fillAmount = curGauge / maxGauge;
    }

    // 3. �����ۿ� �´� ������ ������ ���� gaugeLevels Ȱ��ȭ/��Ȱ��ȭ
    void UpdateGaugeLevel()
    {
        // �����ۿ� �´� ������ ������ �������� (�����ۿ� �ʿ��� ������ ������ ������)
        int requiredGaugeLevel = playerStat.MaxGauge;

        activeItemSprite.gameObject.SetActive(requiredGaugeLevel > 0);
		gauge.gameObject.SetActive(requiredGaugeLevel > 0);

		 
        // gaugeLevels�� ���� ���� �̹����� ������Ʈ�� ����Ʈ
        // ���߿��� �ʿ��� ��ŭ�� Ȱ��ȭ�ϰ� �������� ��Ȱ��ȭ
        for (int i = 0; i < gaugeLevels.Count; i++)
        {
                gaugeLevels[i].SetActive(i == requiredGaugeLevel-1);  // �ش� ������ Ȱ��ȭ
        } 
    }

    // 4. ü�¹� ������Ʈ 
    public void UpdateHealthBar()
    { 
        float maxHealth = playerStat.GetStat(DesignEnums.Option.MaxHeart);  // �ִ� ü��
        float currentHealth = playerStat.GetStat(DesignEnums.Option.CurHeart);  // ���� ü��

        int cnt = (int)Mathf.Ceil(maxHealth);
		// ü�� UI ������Ʈ
		for (int i = 1; i <= cnt; i++) 
        {
            if (i < currentHealth)  // ü���� ���� �� ��Ʈ�� ä���
            {
                hearts[i-1].sprite = fullHeart;
            }
            else if ((float)i - currentHealth == 0.5f)  // �� ĭ ü��
            {
                hearts[i-1].sprite = halfHeart;
            }
            else  // �� ��Ʈ
            {
                hearts[i-1].sprite = emptyHeart;
            }
			hearts[i-1].gameObject.SetActive(true);
		}

        // �ִ� ü�¿� �°� ��Ʈ ���� ����
        for (int i = cnt; i < hearts.Count; i++)
        {
            hearts[i].gameObject.SetActive(false);  // ���� ��Ʈ�� ��Ȱ��ȭ
        }
    }

    // ������ ��ġ �ؽ�Ʈ ������Ʈ
    void UpdateItemText()
    {
        // �� �ڸ� ���� ����, 99�� ���� �ʵ��� ó��
        coinCntText.text = coinCount.ToString("D2");
        boomCntText.text = bombCount.ToString("D2");
        keyCntText.text = keyCount.ToString("D2");
    }


    public void UpdateHealthBarWithSoulAndBlackHearts()
    {
        float maxHealth = playerStat.GetStat(DesignEnums.Option.MaxHeart);  // �ִ� ü��
        float currentHealth = playerStat.GetStat(DesignEnums.Option.CurHeart);  // ���� ü��

        // �ִ� ü�¿� �°� ��Ʈ ���� ����
        for (int i = 0; i < maxHealth; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }

        for (int i = (int)maxHealth; i < hearts.Count; i++)
            hearts[i].gameObject.SetActive(false);
    } 
}

