using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarFlies : MonoBehaviour
{
	[SerializeField] GameObject fly1;
	[SerializeField] GameObject fly2;

	[SerializeField] float dist = 1.0f;
	[SerializeField] float rotationSpeed = 100.0f; // ȸ�� �ӵ� (��/��)

	private float angle = 0.0f; // ���� ����
	private void Update()
    {
        // ������ �ð��� ���� ������Ŵ (�ݽð� ����)
        angle += rotationSpeed * Time.deltaTime;
        if (angle >= 360.0f) angle -= 360.0f; // ������ 0~360�� ������ ����

        // fly1�� ��ġ ���
        float rad = angle * Mathf.Deg2Rad; // ������ �������� ��ȯ
        fly1.transform.position = transform.position + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * dist;

        // fly2�� ��ġ ��� (fly1�� �ݴ��� ��ġ)
        fly2.transform.position = transform.position + new Vector3(Mathf.Cos(rad + Mathf.PI), Mathf.Sin(rad + Mathf.PI), 0) * dist;
    }
}
