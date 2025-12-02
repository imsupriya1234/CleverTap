using UnityEngine;
using System.Collections;

public class LocationServiceComponent : MonoBehaviour
{
    public float latitude;
    public float longitude;
    public bool hasLocation = false;

    private void Start()
    {
        
    }

    public void GetCoordiates() 
    {
        StartCoroutine(FetchLocation());
    }
    IEnumerator FetchLocation()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("Location service not enabled by user.");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        int waited = 0;
        while (Input.location.status == LocationServiceStatus.Initializing && waited < maxWait)
        {
            yield return new WaitForSeconds(1f);
            waited += 1;
        }

        if (waited >= maxWait || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("Unable to determine device location.");
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        hasLocation = true;
        Debug.Log($"Location acquired: {latitude}, {longitude}");
    }

    private void OnDisable()
    {
        Input.location.Stop();
    }
}
