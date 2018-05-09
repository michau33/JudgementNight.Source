using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PathFollowing
{
    public class PathFollower : MonoBehaviour
    {
        List<Waypoint> waypoints = new List<Waypoint>();
        
        Waypoint currentWaypoint;
        public Waypoint CurrentWaypoint
        {
            get { return currentWaypoint; }
            set { currentWaypoint = value; }
        }

        public event EventHandler<EventArgs> WaypointChanged;


        void Awake()
        {
        
        }

        void Start()
        {

        }

        protected virtual void OnWaypointChanged(EventArgs args)
        {
            if (WaypointChanged != null)
                WaypointChanged.Invoke(this, args);
        }
    }

    [System.Serializable]
    public class Waypoint 
    {
        [SerializeField] public Transform Position;
        [SerializeField] public bool IsReached;
    }
}