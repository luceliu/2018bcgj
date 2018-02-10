using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{

    public class ZoneController : MonoBehaviour
    {
        public OverworldSceneController SceneController;
        public ZoneEnvironment Environment;

        public bool PlayerInZone { get { return InZone; } }
        private bool InZone;

        // Use this for initialization
        void Start()
        {
            if(SceneController == null)
            {
                SceneController = transform.root.GetComponent<OverworldSceneController>();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SignalCrossBoundary()
        {
            InZone = !InZone;

            //Debug.Log(string.Format("Player crossed boundary of {0}, now {1}", gameObject.name, InZone.ToString()));

            if (InZone)
                SceneController.ZonePlayerIsIn = this;
            else
                SceneController.ZonePlayerIsIn = null;
        }
    }
}