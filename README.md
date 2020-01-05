//Application.streamingAssetsPath
//public static string streamingAssetsPath;
//The path to the StreamingAssets folder (Read Only).

//Use the StreamingAssets folder to store Assets. At run time, Application.streamingAssetsPath provides the path to the folder. Add the //Asset name to Application.streamingAssetsPath. The built application can load the Asset at this address.

//It is not possible to access the StreamingAssets folder on WebGL and Android platforms. No file access is available on WebGL. Android uses a compressed .apk file. These platforms return a URL. 

//Use the UnityWebRequest class to access the Assets.

using UnityEngine;
using System.IO;
using UnityEngine.Video;

// Application-streamingAssetsPath example.
//
// Play a video and let the user stop/start it.
// The video location is StreamingAssets. The video is
// played on the camera background.

public class Example : MonoBehaviour
{
    private UnityEngine.Video.VideoPlayer videoPlayer;
    private string status;

    void Start()
    {
        GameObject cam = GameObject.Find("Main Camera");
        videoPlayer = cam.AddComponent<UnityEngine.Video.VideoPlayer>();

        // Obtain the location of the video clip.
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "SampleVideo_1280x720_5mb.mp4");

        // Restart from beginning when done.
        videoPlayer.isLooping = true;

        // Do not show the video until the user needs it.
        videoPlayer.Pause();

        status = "Press to play";
    }

    void OnGUI()
    {
        GUIStyle buttonWidth = new GUIStyle(GUI.skin.GetStyle("button"));
        buttonWidth.fontSize = 18 * (Screen.width / 800);

        if (GUI.Button(new Rect(Screen.width / 16, Screen.height / 16, Screen.width / 3, Screen.height / 8), status, buttonWidth))
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
                status = "Press to play";
            }
            else
            {
                videoPlayer.Play();
                status = "Press to pause";
            }
        }
    }
}
