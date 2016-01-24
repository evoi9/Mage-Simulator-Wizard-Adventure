using UnityEngine;
using System.Collections;

public class MonsterSpawn : MonoBehaviour {

	public GameObject monsterPrefab;
	float initTime = 0;
	float totalTime = 0;
	int n = 1;
	bool hasRan = false;

	void start(){
		initTime = Time.time;
	}

	// Update is called once per frame
	void Update(){
		totalTime = Time.time;
		Debug.Log ("total time " + totalTime);
		Debug.Log ("enemies spawned " + n);
		if (((totalTime-initTime) >= (n * 3) - 0.1) && ((totalTime- initTime) <= (n * 3) + 0.1) && hasRan == false)
		{
			GameObject.Instantiate(monsterPrefab, transform.position, Quaternion.Euler(0, 180, 0);
			hasRan = true;
			n++;
		}
		else
		{
			hasRan = false;
		}
	}
}
