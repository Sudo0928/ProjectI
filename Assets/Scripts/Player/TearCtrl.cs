
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

	public float circleRadius = 5f;     // 회전 반경 (원의 크기)
	public float rotationSpeed = 360f;  // 회전 속도 (각속도, deg/s)
	private float angle = 0f;           // 현재 회전 각도

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
		// 원운동을 계산 (각속도 * 시간 = 회전 각도 증가)
		angle += rotationSpeed * Time.deltaTime;
		float radian = angle * Mathf.Deg2Rad;

		// 원을 따라 탄환 이동 (원의 중심을 기준으로 위치 변경)
		Vector2 circularOffset = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * circleRadius;

		// 직진 이동 + 원운동 합성
		transform.position += (Vector3)(moveDir * speed * Time.deltaTime) + (Vector3)circularOffset * Time.deltaTime;
	}
}


