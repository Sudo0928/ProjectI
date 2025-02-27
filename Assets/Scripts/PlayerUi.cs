using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    // 아이템의 개수 변수
    public int bombCount = 00;
    public int keyCount = 00;
    public int coinCount = 00;

    // UI Text와 UI Image를 할당할 변수들
    public Text itemCountText;


    // Start는 게임 시작 시 호출됨
    void Start()
    {
        UpdateItemText(); // 시작 시 텍스트 업데이트
    }

    // 아이템의 개수가 변경되었을 때 호출되는 함수
    public void AddItem(string itemType)
    {
        switch (itemType)
        {
            case "Bomb":
                if (bombCount < 99) bombCount++;
                break;
            case "Key":
                if (keyCount < 99) keyCount++;
                break;
            case "Coin":
                if (coinCount < 99) coinCount++;
                break;
            default:
                break;
        }
        UpdateItemText(); // 아이템 개수 변경 후 텍스트 업데이트
    }

    // UI 텍스트를 갱신하는 함수
    void UpdateItemText()
    {
        // 두 자리 숫자 형식으로 표시
        string bombText = bombCount.ToString("D2"); // "D2"로 두 자리로 표시
        string keyText = keyCount.ToString("D2");
        string coinText = coinCount.ToString("D2");

        // 세 자리 숫자 제한, 99를 넘지 않도록 처리
        bombCount = Mathf.Min(bombCount, 99);
        keyCount = Mathf.Min(keyCount, 99);
        coinCount = Mathf.Min(coinCount, 99);

        // 텍스트에 아이템 개수를 표시
        itemCountText.text =   coinText + "\n" +
                               bombText+ "\n" +
                               keyText;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // B 키를 눌러서 폭탄 추가
        {
            AddItem("Bomb");
        }
        if (Input.GetKeyDown(KeyCode.K)) // K 키를 눌러서 열쇠 추가
        {
            AddItem("Key");
        }
        if (Input.GetKeyDown(KeyCode.C)) // C 키를 눌러서 동전 추가
        {
            AddItem("Coin");
        }
    }

}
