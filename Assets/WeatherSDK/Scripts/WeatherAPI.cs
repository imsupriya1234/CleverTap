using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

[System.Serializable]
public class DailyData
{
    public float[] temperature_2m_max;
}

[System.Serializable]
public class WeatherResponse
{
    public DailyData daily;
}

public class WeatherAPI : MonoBehaviour
{
    
    public string BuildUrl(float latitude, float longitude)
    {
        return $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max&timezone=auto";
    }

    public IEnumerator GetTemperatureCoroutine(float latitude, float longitude, System.Action<float, string> callback)
    {
        string url = BuildUrl(latitude, longitude);
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                callback(-999f, req.error);
                yield break;
            }

            try
            {
                WeatherResponse resp = JsonUtility.FromJson<WeatherResponse>(req.downloadHandler.text);
                if (resp != null && resp.daily != null && resp.daily.temperature_2m_max != null && resp.daily.temperature_2m_max.Length > 0)
                {
                    callback(resp.daily.temperature_2m_max[0], null);
                }
                else
                {
                    callback(-999f, "Invalid API response");
                }
            }
            catch (System.Exception ex)
            {
                callback(-999f, ex.Message);
            }
        }
    }
}
