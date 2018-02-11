using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{

    public class OverworldPlayerController : MonoBehaviour
    {
        const float MoveDeadzone = 0.1f;
        const float RotateDeadzone = 0.1f;

        const float RotateVertMax = 90;
        const float RotateVertMin = 45;

        public OverworldSceneController SceneController;
        public Transform CameraTransform;
        public Transform CameraRootTransform;
        public Animator PlayerAnimator;
        public CharacterController PlayerCC;

        public float MoveSpeedMult = 1.0f;
        public float RunFactor = 2.0f;
        public float RotateFactor = 30.0f;
        public float CameraRotateFactor = 90.0f;
        public float CameraElevateFactor = 60.0f;


        public float EnergyLossPerSecond = 1.0f;
        public bool SleepBegan { get; private set; }
        public bool MoveLocked { get; private set; }

        private float IntendedCameraRotation;
        private float IntendedCameraElevation;


        // Use this for initialization
        void Start()
        {
            if (SceneController == null)
                SceneController = transform.root.GetComponent<OverworldSceneController>();

            if (CameraTransform == null)
                CameraTransform = Camera.main.transform;

            if (CameraRootTransform == null)
                CameraRootTransform = transform.Find("CameraRoot");

            if (PlayerCC == null)
                PlayerCC = GetComponent<CharacterController>();

            if (PlayerAnimator == null)
                PlayerAnimator = GetComponent<Animator>();

            IntendedCameraElevation = CameraRootTransform.eulerAngles.x;
        }

        // Update is called once per frame
        void Update()
        {
            HandleCamera();
            HandleMove();
            HandleEnergy();
            HandleKeyDown();
        }

        private static void HandleKeyDown()
        {
            // for now, try to use item by pressing space
            if (Input.GetKeyDown("space"))
            {
                //Debug.Log("Space was pressed");
                GameData.Instance.CurrentInventory.Use("melatonin");
            }

            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                GameData.Instance.CurrentInventory.GetItemsInInventory();
            }
        }

        private void LateUpdate()
        {
            CameraRootTransform.eulerAngles = new Vector3(IntendedCameraElevation, IntendedCameraRotation, 0);
        }

        private void HandleCamera()
        {
            float rotateValue = Input.GetAxis("Mouse X");
            if (Mathf.Abs(rotateValue) > RotateDeadzone)
            {
                IntendedCameraRotation += (rotateValue * CameraRotateFactor * Time.deltaTime);
            }

            float elevateValue = Input.GetAxis("Mouse Y");
            if (Mathf.Abs(elevateValue) > RotateDeadzone)
            {
                IntendedCameraElevation += (elevateValue * CameraElevateFactor * Time.deltaTime);
                IntendedCameraElevation = Mathf.Clamp(IntendedCameraElevation, RotateVertMin, RotateVertMax);
            }
        }

        private void HandleEnergy()
        {
            if (SleepBegan)
                return;

            GameData.Instance.PlayerEnergy -= EnergyLossPerSecond * Time.deltaTime;

            if (GameData.Instance.PlayerEnergy < GameData.PlayerSleepThresholdFrac * GameData.PlayerMaxEnergy)
            {
                BeginSleep();
            }

            if (Input.GetButtonDown("Fire2"))
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

            DontDestroyOnLoad(Instantiate<GameObject>(Resources.Load<GameObject>("PFX_dreamtime")));

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

            Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            float RunMult = 1.0f;
            if (Input.GetButton("Run"))
                RunMult = RunFactor;

            if (moveVector.magnitude >= MoveDeadzone)
            {
                //translate directly
                Vector3 moveVec3 = new Vector3(moveVector.x, 0, moveVector.y);
                Vector3 fakeTransformVec = Quaternion.AngleAxis(IntendedCameraRotation, Vector3.up) * Vector3.forward;
                float angleOffset = Vector3.SignedAngle(Vector3.forward, fakeTransformVec, Vector3.up);
                moveVec3 = Quaternion.AngleAxis(angleOffset, Vector3.up) * moveVec3;
                transform.Translate(moveVec3 * MoveSpeedMult * RunMult * 2.0f * Time.deltaTime, Space.World);

                //rotate toward new facing
                Vector2 flatHeading = new Vector2(transform.forward.x, transform.forward.z);
                float angleDiff = Vector2.SignedAngle(flatHeading, new Vector2(moveVec3.x, moveVec3.z));
                float rotateValue = Mathf.Sign(angleDiff) * Mathf.Clamp(RotateFactor * Time.deltaTime, 0, Mathf.Abs(angleDiff));

                transform.Rotate(Vector3.down, rotateValue);

                moved = true;
            }


            if (!moved)
            {
                PlayerAnimator.Play("Idle");
            }
            else
            {
                PlayerAnimator.Play("Run");
            }

            PlayerCC.Move(Physics.gravity);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                // pick up item
                Item i = other.GetComponent<Item>();
                GameData.Instance.CurrentInventory.AddItem(i);
                GameData.Instance.CurrentInventory.GetItemsInInventory();
                Instantiate<GameObject>(Resources.Load<GameObject>("PFX_pickup"), other.transform.position, Quaternion.identity, transform.root);
            }
        }
    }
}