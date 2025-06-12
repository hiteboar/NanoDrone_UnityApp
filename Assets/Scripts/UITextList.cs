using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextList : MonoBehaviour
{
    [Header("Configuración")]
    public RectTransform container; // Donde se colocan los elementos visuales
    public DeviceInfoListItem itemPrefab;   // Prefab con un TextMeshProUGUI

    private int count = 0;

    /// <summary>
    /// Agrega un nuevo elemento a la lista visual usando el nombre especificado.
    /// </summary>
    /// <param name="elementName">Nombre que se mostrará</param>
    public void AddElement(string elementAdress)
    {
        if (itemPrefab == null || container == null) {
            Debug.LogWarning("Faltan referencias en PrefabListDisplay.");
            return;
        }

        DeviceInfoListItem newItem = Instantiate<DeviceInfoListItem>(itemPrefab, container);
        newItem.InitDevice(elementAdress, BLEManager.Instance.GetDeviceName(elementAdress));
    }
}
