using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{
    public class ZoneEntranceScript : MonoBehaviour
    {
        public ZoneController Zone;

        private bool inEntrance;

        void Start()
        {
            if(Zone == null)
            {
                Debug.LogError("ZoneEntranceScript on " + gameObject.name + " has no zone!");
            }
        }

        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                inEntrance = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(inEntrance)
            {
                Zone.SignalCrossBoundary();
                inEntrance = false;
            }
            else
            {
                Debug.LogWarning("Player left ZoneEntrance but never entered it!");
            }
        }
    }
}