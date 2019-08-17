using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KUtil;

public class MyCamera : MonoBehaviour
{
    private WebCamTexture webCamTexture = null;
    Texture2D texture;
    Color32[] colors = null;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return Application.RequestUserAuthorization( UserAuthorization.WebCam );
        bool canUseCamera = Application.HasUserAuthorization( UserAuthorization.WebCam );

        switch (getDevice(canUseCamera)){
            case Right<string, WebCamDevice> r:
                WebCamDevice userCameraDevice = r.val;
                webCamTexture = new WebCamTexture(userCameraDevice.name, 1000, 1000);
                GetComponent<Renderer> ().material.mainTexture = webCamTexture;
                webCamTexture.Play();
                break;
            case Left<string, WebCamDevice> l:
                Debug.LogFormat(l.val);
                break;
        }
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Either<string, WebCamDevice> getDevice(bool canUseCamera) {
        if(WebCamTexture.devices.Length == 0 )
        {
            return new Left<string, WebCamDevice>("camera device not found.");
        }

        if(!canUseCamera)
        {
            return new Left<string, WebCamDevice>("unauth camera device.");
        }

        return new Right<string, WebCamDevice>(WebCamTexture.devices[ 0 ]);
    }
}
