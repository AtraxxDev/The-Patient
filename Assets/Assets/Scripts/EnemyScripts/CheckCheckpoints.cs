using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public sealed class CheckCheckpoints 
{
    private static CheckCheckpoints _instance;
    private List<GameObject> _checkpointsObjects = new List<GameObject>();
    private GameObject _safeZone = new GameObject();

    public List<GameObject> CheckpointsObjects { get { return _checkpointsObjects; } }
    public GameObject GetSafeZone { get { return _safeZone; } }

    public static CheckCheckpoints Singleton
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CheckCheckpoints();
                _instance._checkpointsObjects.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));
                _instance._safeZone = GameObject.FindGameObjectWithTag("Safe");

                //Esto lo ordena en la lista por el nombre alfabéticos
                _instance._checkpointsObjects = _instance._checkpointsObjects.OrderBy(waypoint => waypoint.name).ToList();
            }
            return _instance;
        }
    }
}
