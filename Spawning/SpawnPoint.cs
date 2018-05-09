using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces;

[System.Serializable]
public class SpawnPoint 
{
	[SerializeField] public Vector3 location;
	[SerializeField] public List<GameObject> prefabsToSpawn = new List<GameObject>();
	[SerializeField] public float spawnDuration;
	[SerializeField] public float timeBetweenSpawns;

	// checks whether there are enemies to spawn and they are not empty objects, whether spawn duration and time between spawns aren't 0
	public bool IsConfiguredProperly 
	{
		get 
		{ 
			return (prefabsToSpawn.Count > 0 && prefabsToSpawn.All(e => e != null) && 
					prefabsToSpawn.All(e => e.GetComponent(typeof(ISpawnable)) != null) &&
					spawnDuration > 0f && timeBetweenSpawns > 0f);
		}
	}

	public SpawnPoint(Vector3 location)
	{
		this.location = location;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(this.location, 0.5f);
	}
}