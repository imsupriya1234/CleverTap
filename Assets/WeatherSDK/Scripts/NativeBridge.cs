using UnityEngine;
using UnityEngine.EventSystems;

public class NativeBridge : MonoBehaviour, IPointerClickHandler
{
    private string message = "Temperature: ";

    public void OnPointerClick(PointerEventData eventData)
    {
        ToastManager.Show($"{message} {WeatherUI.temperature}°C");
    }
}
