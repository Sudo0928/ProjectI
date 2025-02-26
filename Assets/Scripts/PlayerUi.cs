using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    // �������� ���� ����
    public int bombCount = 00;
    public int keyCount = 00;
    public int coinCount = 00;

    // UI Text�� UI Image�� �Ҵ��� ������
    public Text itemCountText;


    // Start�� ���� ���� �� ȣ���
    void Start()
    {
        UpdateItemText(); // ���� �� �ؽ�Ʈ ������Ʈ
    }

    // �������� ������ ����Ǿ��� �� ȣ��Ǵ� �Լ�
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
        UpdateItemText(); // ������ ���� ���� �� �ؽ�Ʈ ������Ʈ
    }

    // UI �ؽ�Ʈ�� �����ϴ� �Լ�
    void UpdateItemText()
    {
        // �� �ڸ� ���� �������� ǥ��
        string bombText = bombCount.ToString("D2"); // "D2"�� �� �ڸ��� ǥ��
        string keyText = keyCount.ToString("D2");
        string coinText = coinCount.ToString("D2");

        // �� �ڸ� ���� ����, 99�� ���� �ʵ��� ó��
        bombCount = Mathf.Min(bombCount, 99);
        keyCount = Mathf.Min(keyCount, 99);
        coinCount = Mathf.Min(coinCount, 99);

        // �ؽ�Ʈ�� ������ ������ ǥ��
        itemCountText.text =   coinText + "\n" +
                               bombText+ "\n" +
                               keyText;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // B Ű�� ������ ��ź �߰�
        {
            AddItem("Bomb");
        }
        if (Input.GetKeyDown(KeyCode.K)) // K Ű�� ������ ���� �߰�
        {
            AddItem("Key");
        }
        if (Input.GetKeyDown(KeyCode.C)) // C Ű�� ������ ���� �߰�
        {
            AddItem("Coin");
        }
    }

}
