using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{

    public class OverworldPlayerController : MonoBehaviour
    {
        const float MoveDeadzone = 0.1f;

        public Transform CameraTransform;
        public Animator PlayerAnimator;
        public CharacterController PlayerCC;

        public float MoveSpeedMult = 1.0f;
        public float RunFactor = 2.0f;
        public float RotateFactor = 30.0f;


        // Use this for initialization
        void Start()
        {
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
        }

        private void HandleMove()
        {
            bool moved = false;

            //Vector3 moveVector = new Vector3(0, 0, Input.GetAxis("Vertical"));
            float moveValue = Input.GetAxis("Vertical");

            float RunMult = 1.0f;
            if (Input.GetButton("Run"))
                RunMult = RunFactor;

            //do rotate separately
            float rotateValue = Input.GetAxis("Horizontal");

            if (Mathf.Abs(rotateValue) >= MoveDeadzone)
            {
                transform.Rotate(Vector3.up, rotateValue * RotateFactor * Time.deltaTime);
                moved = true;
            }

            if (Mathf.Abs(moveValue) >= MoveDeadzone)
            {
                PlayerCC.Move(transform.forward * moveValue * MoveSpeedMult * Time.deltaTime * RunMult);
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
            }
        }
    }
}