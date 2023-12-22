using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct CameraShakeData
{
    [Range(0f, 1f)]
    public float Magnitued;
    [Range(0f, 1f)]
    public float Duration;
}

public class CameraEffects : Instancable<CameraEffects>
{
    private Camera cam;

    [Header("Zoom Control")]
    [Range(0f, 1f)]
    public float ZoomSpeed;
    public int ZoomNormal;
    public int ZoomMax;
    public int ZoomMin;

    [Header("Shake Controll")]
    public CameraShakeData shakeData;

    [Header("Fade Control")]
    public GameObject FadeObj;
    public float fadeSpeed;
    public float fadeTimeSpeed;

    //Private variables
    IEnumerator activeZoom;
    IEnumerator activeFade;

    public void OnEnable()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        if (FadeObj != null)
        {
            FadeOut();
        }
        ZoomToNormal();
    }

    /// <summary>
    /// Zoom's in or out based on the given zoom amount
    /// </summary>
    /// <param name="zoomamn">The given zoom amount</param>
    public void Zoom(int zoomamn)
    {
        if (activeZoom != null)
        {
            StopCoroutine(activeZoom);
        }
        activeZoom = zoom(zoomamn);
        StartCoroutine(activeZoom);
    }

    /// <summary>
    /// Zoom's to the given normal value in the Unity ui
    /// </summary>
    public void ZoomToNormal()
    {
        Zoom(ZoomNormal);
    }

    /// <summary>
    /// Zoom's to the given max value in the Unity ui
    /// </summary>
    public void ZoomToMax()
    {
        Zoom(ZoomMax);
    }

    /// <summary>
    /// Zoom's to the given min value in the Unity ui
    /// </summary>
    public void ZoomToMin()
    {
        Zoom(ZoomMin);
    }

    /// <summary>
    /// Fade's the level in based on the Unity ui values
    /// </summary>
    public void FadeIn()
    {
        if (activeFade != null)
        {
            StopCoroutine(activeFade);
        }
        activeFade = FadeToIn();
        StartCoroutine(activeFade);
    }

    /// <summary>
    /// Fades the level out based on the Unity ui values
    /// </summary>
    public void FadeOut()
    {
        if (activeFade != null)
        {
            StopCoroutine(activeFade);
        }
        activeFade = FadeToOut();
        StartCoroutine(activeFade);
    }

    /// <summary>
    /// Shake's the camera based on the given values
    /// </summary>
    /// <param name="duration">How long to shake the camera</param>
    /// <param name="magnitude">How much to shake the camera</param>
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(shake(duration, magnitude));
    }

    /// <summary>
    /// Shake's the camera based on the given values in the Unity ui
    /// </summary>
    public void Shake()
    {
        StartCoroutine(shake(shakeData.Duration, shakeData.Magnitued));
    }

    #region Helper methods (ignore if you can)
    private IEnumerator zoom(int zoomTo)
    {
        while ((int)Mathf.Lerp(cam.orthographicSize, zoomTo, ZoomSpeed) != zoomTo)
        {
            yield return cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomTo, ZoomSpeed);
        }
    }

    private IEnumerator FadeToOut()
    {
        CanvasGroup group = FadeObj.GetComponent<CanvasGroup>();

        while (group.alpha > 0)
        {
            group.alpha -= fadeSpeed;
            yield return new WaitForSeconds(fadeTimeSpeed);
        }
        group.alpha = 0;
        yield return null;
    }

    private IEnumerator FadeToIn()
    {
        CanvasGroup group = FadeObj.GetComponent<CanvasGroup>();

        while (group.alpha < 1)
        {
            group.alpha += fadeSpeed;
            yield return new WaitForSeconds(fadeTimeSpeed);
        }
        group.alpha = 1;
        yield return null;
    }

    private IEnumerator shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
    }
    #endregion
}
