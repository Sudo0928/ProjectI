using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DesignEnums;

public class HarlequinBuddy : MonoBehaviour
{
    [SerializeField] GameObject tear;
	public PlayerController player { private get; set; }
	// Start is called before the first frame update
	void Update()
    {
		Vector3 dir = player.transform.position - transform.position;

		if (dir.magnitude > 2.0f)
			transform.position = player.transform.position;

		else if (dir.magnitude > 1.0f)
			transform.position += dir * player.Stat.GetStat(DesignEnums.Option.Speed) * Time.fixedDeltaTime * 0.8f;
	}
	  


	public void Attack(PlayerAttackEvent e)
	{
		PlayerController player = e.player;

		Vector2 velocity = player.Rigidbody2D.velocity;

		Vector2[] dirs = { RotateVector(e.direction, 15f), RotateVector(e.direction, -15f) };

		for (int i = 0; i < dirs.Length; i++) 
		{
			GameObject gameObject = Instantiate(tear);
			gameObject.transform.position = transform.position - new Vector3(0, 0.1f, 0);

			gameObject.GetComponent<BaseTear>().Init(
				player.gameObject,
                e.player.Stat.GetStat(Option.Attack) * 0.5f,
                5.0f, // 속도
				6.0f, // 거리
				0.5f, // 사이즈
				dirs[i],
				player.IsParbolic);
		}
		

	}

	Vector2 RotateVector(Vector2 v, float angle)
	{
		float rad = angle * Mathf.Deg2Rad; // 도(degree)를 라디안(radian)으로 변환
		float cos = Mathf.Cos(rad);
		float sin = Mathf.Sin(rad);

		// 2D 회전 변환 공식 적용
		return new Vector2(
			v.x * cos - v.y * sin,
			v.x * sin + v.y * cos
		);
	}
}
