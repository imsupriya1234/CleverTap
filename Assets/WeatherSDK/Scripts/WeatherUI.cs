using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Android;

public class WeatherUI : MonoBehaviour
{
    public LocationServiceComponent locationService;
    public WeatherAPI weatherAPI;
    public GameObject toastButton; // optional: button that uses NativeBridge
    public static float temperature;

    private IEnumerator Start()
    {
        // Ask for Location Permission
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);

            // Wait until user responds
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }

        // Fetch The Loccation values
        locationService.GetCoordiates();


        // wait for location service to initialize (LocationServiceComponent.Start will run)
        float timeout = 15f;
        float waited = 0f;
        while (!locationService.hasLocation && waited < timeout)
        {
            waited += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }

        if (!locationService.hasLocation)
        {
            ToastManager.Show("Cannot get location");
            yield break;
        }

        FetchTheWeatherdata(); // Fetch the weather as per the location.
    }

    public void FetchTheWeatherdata()
    {
        
        StartCoroutine(weatherAPI.GetTemperatureCoroutine(locationService.latitude, locationService.longitude, (temp, err) =>
        {
            if (err != null)
            {
                ToastManager.Show("Weather API error");
            }
            else
            {
                temperature = temp;
                ToastManager.Show($"Weather loaded: {temp}°C");
            }
        }));
    }
}
