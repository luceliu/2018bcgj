using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Overworld
{

    public class FadeHackScript : MonoBehaviour
    {
        public bool StartFaded = false;
        private RawImage Img;

        void Start()
        {
            if (Img == null)
                Img = GetComponent<RawImage>();

            Img.enabled = true;

            if (StartFaded)
                Img.canvasRenderer.SetAlpha(1.0f);
            else
                Img.canvasRenderer.SetAlpha(0.0f);
        }

        void Update()
        {

        }

        public void ExecuteFadeout(float fadeTime)
        {
            Img.CrossFadeAlpha(1.0f, fadeTime, false);
        }

        public void ExecuteFadein(float fadeTime)
        {
            Img.CrossFadeAlpha(0, fadeTime, false);
        }
    }
}