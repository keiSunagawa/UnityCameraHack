using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KUtil;
using Positioning;

public class MyCamera : MonoBehaviour, PositionCalculater
{
    private WebCamTexture webCamTexture = null;
    Color32[] colors = null;
    FaceSearcher faces = new MockFaceSearcher();

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return Application.RequestUserAuthorization( UserAuthorization.WebCam );
        bool canUseCamera = Application.HasUserAuthorization( UserAuthorization.WebCam );

        switch (getDevice(canUseCamera)){
            case Right<string, WebCamDevice> r:
                WebCamDevice userCameraDevice = r.val;
                webCamTexture = new WebCamTexture(userCameraDevice.name, 1280, 720);
                GetComponent<Renderer>().material.mainTexture = webCamTexture;
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
        if(webCamTexture != null) {
            colors = new Color32[webCamTexture.width * webCamTexture.height];
            webCamTexture.GetPixels32(colors);
            var pp = faces.search(colors);
            var p = calc(pp);
            var sp = GameObject.Find("Sphere");
            var inst = sp.GetComponent<SpherePosition>();
            inst.setPosition(p);
        }
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

    public Position calc(ParPosition par) {
        Position p;

        var bx = GetComponent<Renderer>().bounds.size.x;
        var by = GetComponent<Renderer>().bounds.size.y;

        p.x = (bx * (par.xp / 100.0f)) - (bx / 2);
        p.y = (by * (par.yp / 100.0f)) - (by / 2);
        return p;
    }
}

class MockFaceSearcher : FaceSearcher {
    // 画像解析処理を作るのはめんどくさそうなので特に意味はない適当な数値を返す
    public ParPosition search(Color32[] cs) {
        int xtmp = 0;
        int ytmp = 0;
        foreach (Color32 c in cs) {
            xtmp = xtmp + c.b + c.g - c.r;
            ytmp = ytmp + c.r + c.g - c.b;
        }
        ParPosition pp;
        pp.xp = Math.Abs(xtmp) % 100;
        pp.yp = Math.Abs(ytmp) % 100;
        return pp;
    }
}
