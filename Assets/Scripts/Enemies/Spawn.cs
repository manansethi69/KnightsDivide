using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform spawnPoint;
	public GameObject spawnPrefab;
    public Animator animator;
	public void spawnEnemy ()
	{
        EnemyAI enemyAI = GetComponentInParent<EnemyAI>();
        GameObject player = enemyAI.player;
		GameObject spawn = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        spawn.GetComponent<EnemyAI>().player = player;
        spawn.GetComponentInChildren<Animator>().SetTrigger("spawn");
        spawn.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
	}

    public void spawn(){
        animator.SetTrigger("spawn");
    }
}
