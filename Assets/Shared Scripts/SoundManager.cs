using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//maybe it doesn't actually need to be a MonoBehaviour? eh
public class SoundManager : MonoBehaviour
{
    public const string SHOOT_SOUND = "shoot";
    public const string SHOOTBOOK_SOUND = "shootbook";
    public const string JUMP_SOUND = "jump";
    public const string EXPLOSION_SOUND = "explosion";
    public const string PAPER_FIRE_SOUND = "paper";
    public const string PAPER_HIT_SOUND = "fail";
    public const string BOMB_SOUND = "bomb";

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
