using UnityEngine;
using System.Collections;

public class WaitAndDeactivateScript : MonoBehaviour
{
    public float TimeToStay;
    public GameObject ObjectToDeactivate;

    private float elapsed;

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= TimeToStay)
        {
            if (ObjectToDeactivate == null)
                this.gameObject.SetActive(false);
            else
            {
                ObjectToDeactivate.SetActive(false);
                this.enabled = false;
            }

        }

    }
}
