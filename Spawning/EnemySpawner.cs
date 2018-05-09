using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	public List<SpawnPoint> spawnPoints;

	void Awake()
	{
		foreach(SpawnPoint point in spawnPoints)
		{
			if (point.IsConfiguredProperly)
			{
				IEnumerator spawnCoroutine = SpawnEntities(point);
				StartCoroutine(spawnCoroutine);
				IEnumerator timerCoroutine = Timer(point, spawnCoroutine);
				StartCoroutine(timerCoroutine);
			}
			else 
			{
				Debug.LogWarning("Spawnpoint " + spawnPoints.IndexOf(point) + " is not properly configured.");
			}
		}
	}

	IEnumerator SpawnEntities(SpawnPoint p)
	{
		while (true)
		{
			yield return new WaitForSeconds(p.timeBetweenSpawns);

			Instantiate(
				p.prefabsToSpawn[Random.Range(0, p.prefabsToSpawn.Count)],
				p.location,
				Quaternion.identity
			);
		}
	}

		
	IEnumerator Timer(SpawnPoint p, IEnumerator spawnCoroutine)
	{
		float time = 0.0f;

		while (time < p.spawnDuration)
		{
			time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		StopCoroutine(spawnCoroutine);
	}
}