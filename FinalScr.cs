using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Vuforia;
using Newtonsoft.Json;

/*
POST JSON
{
    LightStatus = 0/1
    WMStatus = 0/1
    WMTime = 1
}
GET JSON
{
    LightStatus = 0/1
    WMStatus = 0/1
    WMTime = 1
    PeakTime = 0/1
}
*/

public class FinalScr : MonoBehaviour
{
    // InputField field;
    // InputField Hum;
    
    public VirtualButtonBehaviour Vb_off;
    public VirtualButtonBehaviour vb_on;
    public string url = "http://192.168.1.100:80/rpi";
    
    string bodyJsonString = "{\"temperature\": \"200\", \"humidity\": \"500\" }";
 
    void Start()
    {
        // field = GameObject.Find("InputField").GetComponent<InputField>();
        
        // Hum = GameObject.Find("InputField1").GetComponent<InputField>();

        Vb_off.RegisterOnButtonPressed(OnButtonPressed_off);
        vb_on.RegisterOnButtonPressed(OnButtonPressed_on);
        
    }

    public void OnButtonPressed_off(VirtualButtonBehaviour Vb_off)
    {
        StartCoroutine(Post(url, bodyJsonString));
        Debug.Log("Click OFF");
        // field.text = "Turned OFF";
        
    }

    public void OnButtonPressed_on(VirtualButtonBehaviour Vb_on)
    {
        // GetData_tem();
        // GetData_hum();
        StartCoroutine(GetDatInfo(url));
        Debug.Log("Click ON");
        // field.text = "Turned ON";
    }

    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
    }
    IEnumerator GetDatInfo(string url)
    {
        UnityWebRequest requesting = UnityWebRequest.Get(url);
        yield return requesting.SendWebRequest();

        if (requesting.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + requesting.error);
        }
        else
        {
            Debug.Log(requesting.downloadHandler.text);
        }

    }
 
}
