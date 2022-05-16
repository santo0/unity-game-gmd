using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CreditsVideo : MonoBehaviour
{
    VideoPlayer videoPlayer;


    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += VideoFinished;
    }

    void VideoFinished(UnityEngine.Video.VideoPlayer videoPlayer)
    {
        GameManager.instance.LoadLevel("Menu");
    }

}
