using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor 
{
	EnemySpawner spawner;
	SerializedObject serializedTarget;

	#region Properties
	SerializedProperty m_spawnPoints;

	#endregion

	void OnEnable()
	{
		spawner = (target as EnemySpawner);
		serializedTarget = new SerializedObject(spawner);
		m_spawnPoints = serializedTarget.FindProperty("spawnPoints");
	}

	public override void OnInspectorGUI()
	{
		// TODO build whole custom inspector
		// base.OnInspectorGUI();

		serializedTarget.Update();

		GUILayout.Space(10f);
		GUILayout.Label("SPAWNPOINT MANAGER v.1.0", Styles.HeaderStyle);

		GUILayout.BeginVertical(Styles.Background);

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("ADD NEW", GUILayout.Height(30f)))
		{
			if (spawner.spawnPoints == null)
			{
				spawner.spawnPoints = new List<SpawnPoint>();
			}
			spawner.spawnPoints.Add(new SpawnPoint(Vector3.zero));
		}

		if (GUILayout.Button("REMOVE ALL", GUILayout.Height(30f)))
		{
			spawner.spawnPoints.Clear();
		}

		GUILayout.EndHorizontal();

		GUILayout.Space(10f);

		for (int i = 0; i < m_spawnPoints.arraySize; i++)
		{
			var el = m_spawnPoints.GetArrayElementAtIndex(i);

			SerializedProperty location = el.FindPropertyRelative("location");
			SerializedProperty spawnDuration = el.FindPropertyRelative("spawnDuration");
			SerializedProperty timeBetweenSpawns = el.FindPropertyRelative("timeBetweenSpawns");
			SerializedProperty prefabsToSpawn = el.FindPropertyRelative("prefabsToSpawn");
		
			Styles.SpawnPointStyle.normal.textColor = Color.blue;
			GUILayout.Label("Spawnpoint " + (i + 1), Styles.SpawnPointStyle);

			GUILayout.BeginHorizontal();

			// remove current spawnpoint
			if (GUILayout.Button("Remove", GUILayout.Width(80f), GUILayout.ExpandWidth(true))) 
			{
				spawner.spawnPoints.RemoveAt(i);
				m_spawnPoints.DeleteArrayElementAtIndex(i);
			}

			// clear all information about current spawnpoint
			if (GUILayout.Button("Clear", GUILayout.Width(80f), GUILayout.ExpandWidth(true))) 
			{
				var curr = spawner.spawnPoints[i];
			
				curr.location = Vector3.zero;
				curr.prefabsToSpawn.Clear();
				curr.spawnDuration = 0f;
				curr.timeBetweenSpawns = 0f;
			}

			GUILayout.EndHorizontal();
		
				// check if current spawn point is still accesible (prevents argument out of range error)
			if (spawner.spawnPoints.ElementAtOrDefault(i) != null)
			{
				GUI.backgroundColor = spawner.spawnPoints[i].location != Vector3.zero ? Color.green : Color.red; 
					EditorGUILayout.PropertyField(location, new GUIContent("Position:", "Set position of spawnpoint."));
				GUI.backgroundColor = spawner.spawnPoints[i].spawnDuration > 0f ? Color.green : Color.red; 
					EditorGUILayout.PropertyField(spawnDuration, new GUIContent("Spawn duration:", "Time in which spawner will be active."));
				GUI.backgroundColor = spawner.spawnPoints[i].timeBetweenSpawns > 0f ? Color.green : Color.red; 
					EditorGUILayout.PropertyField(timeBetweenSpawns, new GUIContent("Time between waves:", "Time between waves of enemies."));
				GUI.backgroundColor = Color.white;
			

				GUILayout.Label("Enemies Prefabs", Styles.EnemyPrefabsStyle);

				if (GUILayout.Button("Add Enemy", GUILayout.Width(80f), GUILayout.ExpandWidth(true))) 
				{
					spawner.spawnPoints[i].prefabsToSpawn.Add(null);
				}

				// prefabs to spawn properties
				for(int j = 0; j < prefabsToSpawn.arraySize; j++)
				{
					GUILayout.BeginHorizontal();

					GUI.backgroundColor = 
						spawner.spawnPoints[i].prefabsToSpawn.ElementAtOrDefault(j) != null && 
						spawner.spawnPoints[i].prefabsToSpawn.ElementAtOrDefault(j).GetComponent(typeof(ISpawnable)) != null
						? Color.green : Color.red; 
					EditorGUILayout.PropertyField(prefabsToSpawn.GetArrayElementAtIndex(j), new GUIContent("Enemy (" + j + ")"));
					GUI.backgroundColor = Color.white;
					
					if (GUILayout.Button("Remove", GUILayout.Width(80f))) 
					{
						prefabsToSpawn.DeleteArrayElementAtIndex(i);
						spawner.spawnPoints[i].prefabsToSpawn.RemoveAt(j);
					}
					
					GUILayout.EndHorizontal();
				}
			}
		}

		serializedTarget.ApplyModifiedProperties();

		GUILayout.EndVertical();

		GUILayout.Space(30f);
		GUILayout.Label("Custom editor script made by MichalDev®");
	}

	void OnSceneGUI()
	{
		SceneView.RepaintAll();

		foreach(SpawnPoint point in spawner.spawnPoints)
		{
			EditorGUI.BeginChangeCheck();

			bool isProperlyConfigured = point.IsConfiguredProperly;
			
			// set handle color to green if everything is correctly set in inspector 
			Handles.color = isProperlyConfigured ? Color.green : Color.red;

			// add movable handle on every point's location
			Vector3 newPos = Handles.FreeMoveHandle(
				point.location,
				Quaternion.identity,
				isProperlyConfigured ? 1.5f : 1f,
				Vector3.one * 0.5f,
				Handles.SphereHandleCap
			);

			// set new spawnpoint location after mouse release
			if (EditorGUI.EndChangeCheck())
			{
				point.location = newPos;
			}
		}

		
		if (Event.current.type == EventType.Layout)
		{
			// it prevents context changing when you click in the scene view
			HandleUtility.AddDefaultControl(0);
		}

		// create a spawnpoint when left mouse button is clicked in the scene view
		if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && !Event.current.alt)
        {
			Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            SpawnPoint temp = new SpawnPoint(new Vector3(ray.origin.x, 0f, ray.origin.z));
            
            spawner.spawnPoints.Add(temp);
        }
	}



	public static class Styles {
		public static GUIStyle HeaderStyle {
			get {
				return new GUIStyle {
					fontSize = 20,
					fontStyle = FontStyle.BoldAndItalic,
					padding = new RectOffset(0, 0, 5, 5),
					alignment = TextAnchor.MiddleCenter,
					normal = new GUIStyleState {
						textColor = Color.black,
						background = CreateTexture(200, 20, "#FFEEF2"),
					}
				};
			} 
		} 

		public static GUIStyle SpawnPointStyle {
			get {
				return new GUIStyle {
					padding = new RectOffset(0, 0, 5, 5),
					fontSize = 18,
					fontStyle = FontStyle.Bold,
					alignment = TextAnchor.MiddleCenter,
					normal = new GUIStyleState {
						textColor = Color.white,
						background = CreateTexture(200, 20, "#595758"),
					}
				};
			}
		}

		public static GUIStyle EnemyPrefabsStyle {
			get {
				return new GUIStyle {
					padding = new RectOffset(0, 0, 5, 5),
					fontSize = 12,
					fontStyle = FontStyle.Bold,
					alignment = TextAnchor.MiddleCenter,
					normal = new GUIStyleState {
						textColor = Color.black,
					}
				};
			}
		}

		public static GUIStyle Background {
			get {
				return new GUIStyle {
					normal = new GUIStyleState {
						background = CreateTexture(200, 20, "#FFEEF2")
					}
				};
			}
		}

		private static Texture2D CreateTexture(int width, int height, string hexColor)
		{
			Color[] pix = new Color[width*height];
			Color col;

			ColorUtility.TryParseHtmlString(hexColor, out col);
	
			for(int i = 0; i < pix.Length; i++)
				pix[i] = col;
	
			Texture2D result = new Texture2D(width, height);
			result.SetPixels(pix);
			result.Apply();
	
			return result;
		}
	}
}
