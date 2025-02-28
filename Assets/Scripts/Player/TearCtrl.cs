
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Diagnostics;

public class TearCtrl : MonoBehaviour
{
    Vector2 moveDir;
    float moveDist = 0;
    float targetDist;
    float speed;

	public float circleRadius = 5f;     // ȸ�� �ݰ� (���� ũ��)
	public float rotationSpeed = 360f;  // ȸ�� �ӵ� (���ӵ�, deg/s)
	private float angle = 0f;           // ���� ȸ�� ����

	public void InitTear(Vector2 dir, float distacne, float speed )
    { 
		this.speed = speed;
        moveDir = dir;
		targetDist = distacne;

		circleRadius = Random.Range(1f, 5f);
		rotationSpeed = Random.Range(90f, 360f);
	}

	// Update is called once per frame
	void Update()
    {
		// ����� ��� (���ӵ� * �ð� = ȸ�� ���� ����)
		angle += rotationSpeed * Time.deltaTime;
		float radian = angle * Mathf.Deg2Rad;

		// ���� ���� źȯ �̵� (���� �߽��� �������� ��ġ ����)
		Vector2 circularOffset = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * circleRadius;

		// ���� �̵� + ��� �ռ�
		transform.position += (Vector3)(moveDir * speed * Time.deltaTime) + (Vector3)circularOffset * Time.deltaTime;
	}
}


