using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{

    public class OverworldPlayerController : MonoBehaviour
    {
        const float MoveDeadzone = 0.1f;

        public OverworldSceneController SceneController;
        public Transform CameraTransform;
        public Animator PlayerAnimator;
        public CharacterController PlayerCC;

        public float MoveSpeedMult = 1.0f;
        public float RunFactor = 2.0f;
        public float RotateFactor = 30.0f;

        public float EnergyLossPerSecond = 1.0f;
        
        public bool SleepBegan { get; private set; }
        public bool MoveLocked { get; private set; }

        // Use this for initialization
        void Start()
        {
            if (SceneController == null)
                SceneController = transform.root.GetComponent<OverworldSceneController>();

            if (CameraTransform == null)
                CameraTransform = Camera.main.transform;

            if (PlayerCC == null)
                PlayerCC = GetComponent<CharacterController>();

            if (PlayerAnimator == null)
                PlayerAnimator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleMove();
            HandleEnergy();
        }

        private void HandleEnergy()
        {
            if (SleepBegan)
                return;

            GameData.Instance.PlayerEnergy -= EnergyLossPerSecond * Time.deltaTime;

            if(GameData.Instance.PlayerEnergy < GameData.PlayerSleepThresholdFrac * GameData.PlayerMaxEnergy)
            {
                BeginSleep();
            }

            if(Input.GetButtonDown("Fire2"))
            {
                BeginSleep();
            }
        }

        public void BeginSleep()
        {
            SleepBegan = true;
            PlayerAnimator.Play("Idle"); //TODO get sleep animation

            //fadeout/coroutine/etc
            StartCoroutine(FadeoutAwaitCoroutine());            

        }

        IEnumerator FadeoutAwaitCoroutine()
        {
            const float interval = 1.0f;

            GetComponentInChildren<FadeHackScript>().ExecuteFadeout(interval);

            yield return new WaitForSeconds(interval);

            //then end it
            SceneController.ExitToDreamworld();
        }

        private void HandleMove()
        {
            if (MoveLocked || SleepBegan)
                return;

            bool moved = false;

            //Vector3 moveVector = new Vector3(0, 0, Input.GetAxis("Vertical"));
            float moveValue = Input.GetAxis("Vertical");

            float RunMult = 1.0f;
            if (Input.GetButton("Run"))
                RunMult = RunFactor;

            //do rotate separately
            float rotateValue = Input.GetAxis("Horizontal");

            if(Mathf.Abs(rotateValue) >= MoveDeadzone)
            {
                transform.Rotate(Vector3.up, rotateValue * RotateFactor * Time.deltaTime);
                moved = true;
            }

            if(Mathf.Abs(moveValue) >= MoveDeadzone)
            {
                PlayerCC.Move(transform.forward * moveValue * MoveSpeedMult * Time.deltaTime * RunMult);
                moved = true;
            }

            if(!moved)
            {
                PlayerAnimator.Play("Idle");
            }
            else
            {
                PlayerAnimator.Play("Run");
            }

            PlayerCC.Move(Physics.gravity);
            
        }
    }
}