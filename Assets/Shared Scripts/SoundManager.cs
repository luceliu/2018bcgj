using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//maybe it doesn't actually need to be a MonoBehaviour? eh
public class SoundManager : MonoBehaviour
{
    const string SHOOT_SOUND = "shoot";
    const string SHOOTBOOK_SOUND = "shootbook";
    const string JUMP_SOUND = "jump";
    const string EXPLOSION_SOUND = "explosion";
    const string PAPER_FIRE_SOUND = "paper";
    const string PAPER_HIT_SOUND = "fail";
    const string BOMB_SOUND = "bomb";

    public static SoundManager Instance
    {
        get
        {

            var smo = GameObject.Find("SoundManager");
            if (smo == null)
            {
                smo = Instantiate<GameObject>(Resources.Load<GameObject>("SoundManager"));
                smo.gameObject.name = "SoundManager";
            }

            return smo.GetComponent<SoundManager>();

        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void StartMusic()
    {
        var sp = Instantiate<GameObject>(Resources.Load<GameObject>("MusicBox"));
    }

    public void PlaySound(string sound)
    {
        var sp = Instantiate<GameObject>(Resources.Load<GameObject>("SoundPlayer"));
        var ac = sp.GetComponent<AudioSource>();

        string soundName = "DynamicSound/" + sound;
        ac.clip = Resources.Load<AudioClip>(soundName);
        ac.Play();
    }
}
