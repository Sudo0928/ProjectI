using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
	Stat playerStat;

    [SerializeField] List<Image> hearts = new List<Image>();
	[Space(10)]
	[SerializeField] Sprite fullHeart;            // ���� �� ��Ʈ ��������Ʈ
	[SerializeField] Sprite halfHeart;            // �� �� ��Ʈ ��������Ʈ
	[SerializeField] Sprite emptyHeart;           // �� ��Ʈ ��������Ʈ
	[SerializeField] Sprite soulHeart;      
	[SerializeField] Sprite halfSoulHeart;      
	[SerializeField] Sprite harlfBlackHeart;         
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

	[Space(10)]
	[SerializeField] List<GameObject> gaugeLevels = new List<GameObject>();
	[SerializeField] Image gaugeFill;


	private void Start()
	{
		
		playerStat.AddListener(DesignEnums.Option.Heart, UpdateHealthBar);
		//playerStat.AddListener()
	}

	// 1. ItemInfo�� ���ڷ� �޾Ƽ�, �������� �̹����� activeItemSprite�� �־��ֱ�
	// 2. �������� ���� �ٲ� ��, gaugeFill.fillAmount �� 0 ~ 1 ���̷� �ٲ��ֱ�
	// 3. �������� �ʿ��� �������� ������ ���� gaugeLevels�߿� �ϳ� �����ϱ�
	// 4. UpdateHealthBar() ���� �����ϱ�

	 
	// 1�� -> 

	public void UpdateActiveItem(ItemInfo item)
	{
		activeItemSprite.sprite = GameManager.Instance.GetItemSprite(item);
	}

	// 2 �� ->
	void UpdateGaugeState()
	{
		// �÷��̾� ������ ���� ������ ������, �ִ� ������ ���� �ҷ�����
//		gaugeFill.fillAmount = ���� ������ ������ / �ִ� ������ ����
	}



	void UpdateItemText()
	{
		// �� �ڸ� ���� ����, 99�� ���� �ʵ��� ó��
		coinCntText.text = coinCount.ToString("D2"); 
		boomCntText.text = bombCount.ToString("D2");
		keyCntText.text = keyCount.ToString("D2");
	}

	// stat���� ���� ü�°� �ִ� ü���� �ҷ��� ��, 
	// ü�� UI ������Ʈ, -> �ҿ� ��Ʈ, �� ��Ʈ���� ����
	public void UpdateHealthBar()
	{
		float maxHealth = playerStat.GetStat(DesignEnums.Option.Heart);

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

