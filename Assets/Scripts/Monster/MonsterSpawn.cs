using UnityEngine;
using System.Collections;

public class MonsterSpawn : MonoBehaviour {

	public GameObject monsterPrefab;
	float totalTime = 0;
	int n = 1;
	bool hasRan = false;

	// Update is called once per frame
	void Update(){
		Debug.Log ("total time " + totalTime);
		Debug.Log ("enemies spawned " + n);
		totalTime = Time.fixedTime;
		if ((totalTime >= n * 2 - 0.1) && (totalTime <= n * 2 + 0.1) && hasRan == false)
		{
			GameObject.Instantiate(monsterPrefab, transform.position, Quaternion.identity);
			hasRan = true;
			n++;
		}
		else
		{
			//hasRan = false;
		}
	}
}
