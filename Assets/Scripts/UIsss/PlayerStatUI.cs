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
	[SerializeField] Sprite fullHeart;            // 가득 찬 하트 스프라이트
	[SerializeField] Sprite halfHeart;            // 반 찬 하트 스프라이트
	[SerializeField] Sprite emptyHeart;           // 빈 하트 스프라이트
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

	// 1. ItemInfo를 인자로 받아서, 아이템의 이미지를 activeItemSprite에 넣어주기
	// 2. 게이지의 값이 바뀔 때, gaugeFill.fillAmount 값 0 ~ 1 사이로 바꿔주기
	// 3. 아이템이 필요한 게이지의 레벨에 따라 gaugeLevels중에 하나 선택하기
	// 4. UpdateHealthBar() 마저 구현하기

	 
	// 1번 -> 

	public void UpdateActiveItem(ItemInfo item)
	{
		activeItemSprite.sprite = GameManager.Instance.GetItemSprite(item);
	}

	// 2 번 ->
	void UpdateGaugeState()
	{
		// 플레이어 스탯의 현재 게이지 정보와, 최대 게이지 정보 불러오기
//		gaugeFill.fillAmount = 현재 게이지 정보와 / 최대 게이지 정보
	}



	void UpdateItemText()
	{
		// 세 자리 숫자 제한, 99를 넘지 않도록 처리
		coinCntText.text = coinCount.ToString("D2"); 
		boomCntText.text = bombCount.ToString("D2");
		keyCntText.text = keyCount.ToString("D2");
	}

	// stat에서 현재 체력과 최대 체력을 불러온 후, 
	// 체력 UI 업데이트, -> 소울 하트, 블랙 하트까지 구현
	public void UpdateHealthBar()
	{
		float maxHealth = playerStat.GetStat(DesignEnums.Option.Heart);

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

