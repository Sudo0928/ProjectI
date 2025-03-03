using UnityEngine;

public class Familiar_DryBaby : MonoBehaviour
{
    public PlayerController player { private get; set; }
	Animator anim;
	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		Vector3 dir =  player.transform.position - transform.position;

		if (dir.magnitude > 2.0f)
			transform.position = player.transform.position;

		else if (dir.magnitude > 1.0f)
			transform.position += dir * player.Stat.GetStat(DesignEnums.Option.Speed) * Time.fixedDeltaTime * 0.8f;

	} 

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Tear"))
		{
			BaseTear tear = collision.gameObject.GetComponent<BaseTear>();
			if (tear.Owner.gameObject.CompareTag("Monster"))
			{
				//tear.Remove();
				anim.Play("Hit");
			}
		}
	} 
}
