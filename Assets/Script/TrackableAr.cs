using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackableAr : DefaultObserverEventHandler
{
    private bool marker;

    protected override void OnTrackingFound(){
        base.OnTrackingFound();
        marker = true;
    }

    protected override void OnTrackingLost(){
        base.OnTrackingLost();
        marker = false;
    }

    public bool GetMarker()
    {
        return marker;
    }
}
