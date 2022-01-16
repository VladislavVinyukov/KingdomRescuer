using UnityEngine;

public class ParalaxCam : MonoBehaviour
{
    [SerializeField] private Transform[] paralaxLayers;
    [SerializeField] private float[] paralaxCoeff;

    private int layerCount;

    void Start()
    {
        layerCount = paralaxLayers.Length;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < layerCount; i++)
        {
                Vector2 posX = new Vector2(transform.position.x * paralaxCoeff[i], paralaxLayers[i].position.y);
            if (paralaxLayers[i].tag == "StaticBG")
            {
                Vector2 posStatic = new Vector2(transform.position.x, transform.position.y);
                paralaxLayers[i].position = posStatic;
            }
            else
                paralaxLayers[i].position = posX;

        }
    }
}
