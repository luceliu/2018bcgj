using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bad but the caffeine hasn't really kicked in yet
public static class VideoModeManager
{
    static readonly Vector2Int OverworldDefaultResolution = new Vector2Int(1440, 1080);
    static readonly Vector2Int OverworldFallbackResolution = new Vector2Int(800, 600);
    static readonly Vector2Int OverworldWindowedResolution = new Vector2Int(1024, 768);

    static readonly Vector2Int DreamworldDefaultResolution = new Vector2Int(1920, 1080);
    static readonly Vector2Int DreamworldFallbackResolution = new Vector2Int(1280, 720);
    static readonly Vector2Int DreamworldWindowedResolution = new Vector2Int(1280, 720);

    private static bool FullScreen;
    private static bool UseFallbacks;
    private static bool IsSet;

    public static void Init()
    {
        if (IsSet)
            return;

        FullScreen = Screen.fullScreen; //broken?

        Resolution[] allResolutions = Screen.resolutions;
        int maxWidth = 0;
        int maxHeight = 0;
        foreach(Resolution r in allResolutions)
        {
            if (r.width > maxWidth)
                maxWidth = r.width;
            if (r.height > maxHeight)
                maxHeight = r.height;
        }
        if (maxWidth >= DreamworldDefaultResolution.x && maxHeight >= DreamworldDefaultResolution.y)
            UseFallbacks = false;
        else
            UseFallbacks = true;

        IsSet = true;
    }

    public static void SetDreamworld()
    {
        Vector2Int r;

        if (!FullScreen)
            r = DreamworldWindowedResolution;
        else
        {
            if (UseFallbacks)
                r = DreamworldFallbackResolution;
            else
                r = DreamworldDefaultResolution;
        }

        if (IsEditor)
            return;

        Screen.SetResolution(r.x, r.y, FullScreen);
    }

    public static void SetOverworld()
    {
        Vector2Int r;

        if (!FullScreen)
            r = OverworldWindowedResolution;
        else
        {
            if (UseFallbacks)
                r = OverworldFallbackResolution;
            else
                r = OverworldDefaultResolution;
        }

        if (IsEditor)
            return;

        Screen.SetResolution(r.x, r.y, FullScreen);
    }

    public static void SetMaximum()
    {
        var res = Screen.resolutions[Screen.resolutions.Length - 1];

        if (IsEditor)
            return;

        if (FullScreen)
            Screen.SetResolution(res.width, res.height, true);
    }


    public static string GetNicelyFormattedString()
    {
        return string.Format("Fullscreen: {0} | UseFallbacks: {1}", FullScreen.ToString(), UseFallbacks.ToString());
    }

    private static bool IsEditor
    {
        get
        {
            RuntimePlatform rtp = Application.platform;
            return (rtp == RuntimePlatform.WindowsEditor || rtp == RuntimePlatform.OSXEditor || rtp == RuntimePlatform.LinuxEditor);
        }
    }

}
