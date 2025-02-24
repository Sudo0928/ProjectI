using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMonster : MonsterBasic, IMonsterMove, IMonsterAttack
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IMonsterMove.Move()
    {
        throw new System.NotImplementedException();
    }

    void IMonsterAttack.Attack()
    {
        throw new System.NotImplementedException();
    }
}
