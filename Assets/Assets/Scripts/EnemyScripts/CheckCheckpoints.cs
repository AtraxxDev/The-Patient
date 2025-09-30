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
                _instance = new CheckCheckpoints();

            // Refresh every time we access it (so new scene reloads work)
            _instance._checkpointsObjects = GameObject.FindGameObjectsWithTag("Checkpoint")
                .OrderBy(waypoint => waypoint.name)
                .ToList();

            _instance._safeZone = GameObject.FindGameObjectWithTag("Safe");

            return _instance;

        }
    }
}
