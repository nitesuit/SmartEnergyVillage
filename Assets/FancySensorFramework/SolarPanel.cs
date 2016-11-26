using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public  class SolarPanel : MonoBehaviour
{
	public  WebCamTexture webCamText;

	public  float GetCameraLevel()
	{
		var colorArr = webCamText.GetPixels();
		var avgColor = new Color();
		
		foreach (var c in colorArr)
		{
			avgColor += c;
		}
		avgColor = avgColor / colorArr.Length;
		
		//if (testImage != null)
		//{
		//	testImage.material.color = avgColor;
		//}

		return (avgColor.r + avgColor.g + avgColor.b)/3f;
		
	}

	 void StartCameraSample()
	{
		webCamText = new WebCamTexture();
		webCamText.Play();
	}

	 void StopCameraSample()
	{
		webCamText.Stop();
	}

}
