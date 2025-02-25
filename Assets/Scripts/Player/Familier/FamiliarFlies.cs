using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarFlies : MonoBehaviour
{
	[SerializeField] GameObject fly1;
	[SerializeField] GameObject fly2;

	[SerializeField] float dist = 1.0f;
	[SerializeField] float rotationSpeed = 100.0f; // 회전 속도 (도/초)

	private float angle = 0.0f; // 현재 각도
	private void Update()
    {
        // 각도를 시간에 따라 증가시킴 (반시계 방향)
        angle += rotationSpeed * Time.deltaTime;
        if (angle >= 360.0f) angle -= 360.0f; // 각도를 0~360도 범위로 유지

        // fly1의 위치 계산
        float rad = angle * Mathf.Deg2Rad; // 각도를 라디안으로 변환
        fly1.transform.position = transform.position + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * dist;

        // fly2의 위치 계산 (fly1과 반대쪽 위치)
        fly2.transform.position = transform.position + new Vector3(Mathf.Cos(rad + Mathf.PI), Mathf.Sin(rad + Mathf.PI), 0) * dist;
    }
}
