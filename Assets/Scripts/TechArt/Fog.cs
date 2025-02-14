using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public Camera fogCamera;
    public float distance;
    public RenderTexture texture;
    // Start is called before the first frame update
    public float lagAmount = 0.5f;

    // Update is called once per frame

    private void Start()
    {
        Shader.SetGlobalVector("_PlayerPosition", PlayerMovement.instance.transform.position);
    }

    void Update()
    {

        transform.position = PlayerMovement.instance.transform.position;
        transform.position = new Vector3(transform.position.x, 0.01f, transform.position.z);

        var height = 2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad) * Camera.main.orthographicSize*2;
        var width = height * Screen.width / Screen.height;

        transform.localScale = new Vector3(width, 1, width);
        if (height > width)
        {
            transform.localScale = new Vector3(height, 1, height);
        }

        fogCamera.orthographicSize = width / 2f;
        Shader.SetGlobalTexture("_FogRenderTexture", texture);

        StartCoroutine(FogLagBehind());
    }

    private IEnumerator FogLagBehind()
    {
        Vector3 currentPosition = PlayerMovement.instance.transform.position;

        yield return new WaitForSeconds(lagAmount);

        Shader.SetGlobalVector("_PlayerPosition", currentPosition);
    }
}
