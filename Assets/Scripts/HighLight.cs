using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLight : MonoBehaviour
{
    public Material NextMaterial;


    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
    }

    public void SwapMaterial()
    {
        Material tmpMaterial = meshRenderer.material;
        meshRenderer.material = NextMaterial;
        NextMaterial = tmpMaterial;
    }


}
