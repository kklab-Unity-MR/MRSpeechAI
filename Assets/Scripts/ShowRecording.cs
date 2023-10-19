using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRecording : MonoBehaviour
{
  
    [SerializeField] private MeshRenderer meshRenderer;

    public void setActiveTure()
    {
        meshRenderer.enabled = true;
    }

    public void setActiveFalse()
    {
        meshRenderer.enabled = false;
    }
}
