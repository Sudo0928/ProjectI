using UnityEngine;
using UnityEngine.Events;

public enum MonsterState
{
    Idle,
    Move,
    Trace,
    Attack
}

public class MonsterBasic : MonoBehaviour
{
    // 몬스터 상태
   
    [SerializeField] protected MonsterState monsterState = MonsterState.Idle;

    protected Rigidbody2D rigid;
    protected Animator anim;

    [Space]
    [SerializeField, Tooltip("몬스터 최대 체력")] protected int monsterMaxHP = 5;
    // 몬스터 현재 체력
    protected float monsterCurrentHP = 0;
    [SerializeField, Tooltip("몬스터 공격력")] protected float monsterAtk = 0.5f;

    public UnityEvent onDie = new UnityEvent();
    protected void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        monsterCurrentHP = monsterMaxHP;
    }
     
    public void GetDamage(float _damage)
    {
        if (gameObject.activeSelf == false)
            return;

        monsterCurrentHP -= _damage;
        if (monsterCurrentHP <= 0)
        {
            onDie?.Invoke();

            MonsterDeadEvent e = new MonsterDeadEvent(this);
            EventManager.DispatchEvent(e);

            gameObject.SetActive(false);
        }
    }
	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{

        if (collision.gameObject.CompareTag("Tear"))
        {
            var tear = collision.gameObject.GetComponent<BaseTear>();
            if (tear != null && tear.Owner.CompareTag("Player"))
            {
                GetDamage(tear.Damage);
                Debug.Log("몬스터 한대 맞음");
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyAttackEvent e = new EnemyAttackEvent(monsterAtk, collision);
            EventManager.DispatchEvent(e);
        }
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
        

	}
}
