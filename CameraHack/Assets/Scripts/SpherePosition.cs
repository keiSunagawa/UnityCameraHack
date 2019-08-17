using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Positioning;

public class SpherePosition : MonoBehaviour, Positionable
{
    private Position thispos;
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        var modPos = this.transform.position;
        modPos.x = this.thispos.x;
        modPos.y = this.thispos.y;
        this.transform.position = modPos;
    }

    public void setPosition(Position pos) {
        thispos = pos;
    }
}
