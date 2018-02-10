using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Overworld;

//this is terrible and I regret everything but with only a few on screen it should be fine
public class ChaserScript : MonoBehaviour
{
    enum ChaserState
    {
        Boost, Glide, Terminal
    }

    public float BoostAngle = 45.0f;
    public float MaxVelocity = 3.0f;
    public float MaxAccel = 5.0f;
    public float BangFactor = 5.0f;
    public float BangError = 0.5f;
    public float FloatFactor = 0.05f;
    public float DestroyThreshold = 0.5f;

    private Transform SpawnerTransform;
    private Transform PlayerTransform;
    private ChaserState State;
    private Vector3 Velocity;

	void Start ()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        SpawnerTransform = transform.parent;
        Debug.Log(SpawnerTransform.gameObject.name);

        //initial velocity and state
        float iVelocity = Mathf.Clamp(MaxVelocity, 0, MaxAccel);
        Debug.Log(iVelocity);
        Vector3 vecToTarget = PlayerTransform.position - transform.position;
        Vector3 raisedVector = Quaternion.AngleAxis(BoostAngle, Vector3.left) * vecToTarget;
        //Vector3 raisedVector = vecToTarget;
        Velocity = raisedVector.normalized * iVelocity;
        Debug.Log(Velocity.ToString());

        State = ChaserState.Boost;

        transform.forward = Velocity.normalized;
	}
	
	void Update ()
    {
        Move();

        switch (State)
        {
            case ChaserState.Boost:
                DoBoostPhase();
                break;
            case ChaserState.Glide:
                DoGlidePhase();
                break;
            case ChaserState.Terminal:
                DoTerminalPhase();
                break;
        }

        CheckAndSwitchState();
    }

    private void Move()
    {
        //model velocity manually because fuck rigidbodies
        transform.Translate(Velocity * Time.deltaTime, Space.World);

        //point
        transform.forward = Velocity.normalized;
    }

    void DoBoostPhase()
    {       
        //model gravity manually
        transform.Translate(Physics.gravity * FloatFactor * Time.deltaTime, Space.World);

        //model acceleration (physically wrong but hopefully nobody will notice)
        float accel = MaxAccel * Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity + Velocity * accel, MaxVelocity);

    }

    void DoGlidePhase()
    {
        //adjust horizontal trajectory only

        //get angle
        Vector3 vecToTarget = PlayerTransform.position - transform.position;
        Vector2 flatVecToTarget = new Vector2(vecToTarget.x, vecToTarget.z);
        Vector2 flatVecVelocity = new Vector2(Velocity.x, Velocity.z);
        float flatAngle = Vector2.SignedAngle(flatVecVelocity, flatVecToTarget);

        //steer
        Vector3 fakeVecToTarget = new Vector3(flatVecToTarget.x, Velocity.y, flatVecToTarget.y);
        Velocity = Vector3.RotateTowards(Velocity.normalized, fakeVecToTarget.normalized, Mathf.Deg2Rad * BangError, 0) * Velocity.magnitude;

        //point
        transform.forward = Velocity.normalized;
    }

    void DoTerminalPhase()
    {
        //bang-bang terminal guidance
        float oldMag = Velocity.magnitude;

        Vector3 vecToTarget = (PlayerTransform.position + new Vector3(0, 0.5f, 0)) - transform.position;
        float fDist = Mathf.Abs(1.0f - Vector3.Dot(Velocity.normalized, vecToTarget.normalized));

        //Debug.Log(fDist);

        if(fDist > BangError)
        {
            //Debug.Log(f)
            Vector3 newVec = Vector3.RotateTowards(Velocity.normalized, vecToTarget.normalized, Mathf.Deg2Rad * BangFactor, 0);
            Velocity = newVec.normalized * oldMag;

            transform.forward = Velocity.normalized;
        }
               

    }

    void CheckAndSwitchState()
    {
        float distToTravel = GetFlatDistance(SpawnerTransform.position, PlayerTransform.position);
        float distTravelled = GetFlatDistance(SpawnerTransform.position, transform.position);

        if(State == ChaserState.Boost)
        {
            if (distTravelled >= distToTravel * 0.33f)
            {
                State = ChaserState.Glide;
                Debug.Log("Entering glide phase");
            }
                
        }
        else if(State == ChaserState.Glide)
        {
            if (distTravelled >= distToTravel * 0.66f)
            {
                State = ChaserState.Terminal;
                Debug.Log("Entering terminal phase");
            }                
        }

        //Debug.Log(distToTravel);

        //this doesn't work, distToTravel is something absurd
        if(distToTravel <= DestroyThreshold)
        {
            Destroy(this.gameObject);
            //TODO destroy effects?
        }
    }

    private static float GetFlatDistance(Vector3 pos0, Vector3 pos1)
    {
        return (new Vector3(pos0.x, 0, pos0.z) - new Vector3(pos1.x, 0, pos1.z)).magnitude;
    }
}
