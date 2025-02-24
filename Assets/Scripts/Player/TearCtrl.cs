using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class TearCtrl : MonoBehaviour
{
    Vector2 moveDir;
    float moveDist = 0;
    float targetDist;
    float speed;
	public void InitTear(Vector2 dir, float distacne, float speed)
    {
        moveDist = 0;

		this.speed = speed;
        moveDir = dir;
		targetDist = distacne;
	}

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * Time.deltaTime * speed;
        moveDist += Time.deltaTime * speed;

        if (moveDist >= targetDist)
            Destroy(gameObject);

	}

}
