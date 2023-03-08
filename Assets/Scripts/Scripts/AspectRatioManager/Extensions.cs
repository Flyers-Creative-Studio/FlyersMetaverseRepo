using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum CropDirection { None, Horizontal, Vertical }

public static class Extensions
{
    public static Texture2D ToTexture2D(this RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public static void SetImageTexture(this UnityEngine.UI.Image theImage, Texture theTexture)
    {
        theImage.sprite = Sprite.Create((Texture2D)theTexture, new Rect(0, 0, theTexture.width, theTexture.height), new Vector2(theTexture.width / 2, theTexture.height / 2));
    }

    public static Texture2D CropTexture(this Texture2D originalTexture, CropDirection cropDirection, double cropRateWithRespectToInversePorposionalDirection)
    {
        int cropOffsetX = 0;
        int cropOffsetY = 0;
        int blockWidth = 0;
        int blockHeight = 0;

        if (cropDirection == CropDirection.Horizontal)
        {
            blockHeight = originalTexture.height;
            blockWidth = (int)(originalTexture.height * cropRateWithRespectToInversePorposionalDirection);
            cropOffsetX = (originalTexture.width - blockWidth) / 2;
        }
        else if (cropDirection == CropDirection.Vertical)
        {
            blockHeight = (int)(originalTexture.width * cropRateWithRespectToInversePorposionalDirection);
            blockWidth = originalTexture.width;
            cropOffsetY = (originalTexture.height - blockHeight) / 2;
        }

        try
        {
            Color[] textureCroppedData = originalTexture.GetPixels(cropOffsetX, cropOffsetY, blockWidth, blockHeight);
            Texture2D textureToReturn = new Texture2D(blockWidth, blockHeight);
            textureToReturn.SetPixels(textureCroppedData);
            textureToReturn.Apply();
            return textureToReturn;
        }
        catch
        {
            return originalTexture;
        }
    }

    public static Texture GetCroppedTexture(this UnityEngine.UI.Image theImage, Texture theTexture)
    {
        Sprite sprite1 = Sprite.Create((Texture2D)theTexture, new Rect(0, 0, theTexture.width, theTexture.height), Vector2.zero);
        theImage.sprite = sprite1;
        theImage.GetComponent<AspectRatioFitter>().enabled = false;
        theImage.rectTransform.anchorMin = new Vector2(0, 0);
        theImage.rectTransform.anchorMax = new Vector2(1, 1);
        theImage.rectTransform.sizeDelta = new Vector2(0, 0);

        HelperUtil.CallAfterDelay(() =>
        {
            theImage.GetComponent<AspectRatioFitter>().enabled = true;
        }, 0.01f);

		if ((float)theTexture.height > (float)theTexture.width)
		{
			theImage.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
			theImage.GetComponent<AspectRatioFitter>().aspectRatio = ((float)theTexture.height / (float)theTexture.width) / 2;
			return sprite1.texture;
		}
		else if ((float)theTexture.width > (float)theTexture.height)
		{
			theImage.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
			theImage.GetComponent<AspectRatioFitter>().aspectRatio = ((float)theTexture.width / (float)theTexture.height);
			return sprite1.texture;
		}
        else 
        {
            theImage.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
            theImage.GetComponent<AspectRatioFitter>().aspectRatio = 1;
            return sprite1.texture;
        }
    }
    public static Texture GetCroppedTexture(this RawImage theImage, Texture theTexture)
    {
        theImage.texture = theTexture;
        if (theTexture.height > theTexture.width)
        {
            theImage.GetComponent<AspectRatioFitter>().enabled = false;
            theImage.rectTransform.anchorMin = new Vector2(0, 0);
            theImage.rectTransform.anchorMax = new Vector2(1, 1);
            theImage.rectTransform.sizeDelta = new Vector2(0, 0);

            HelperUtil.CallAfterDelay(() =>
            {
                theImage.GetComponent<AspectRatioFitter>().enabled = true;
            }, 0.01f);
            theImage.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
            theImage.GetComponent<AspectRatioFitter>().aspectRatio = (float)theTexture.width / (float)theTexture.height;
            return theImage.texture;
        }
        else if (theTexture.width > theTexture.height)
        {
            theImage.GetComponent<AspectRatioFitter>().enabled = false;
            theImage.rectTransform.anchorMin = new Vector2(0, 0);
            theImage.rectTransform.anchorMax = new Vector2(1, 1);
            theImage.rectTransform.sizeDelta = new Vector2(0, 0);

            HelperUtil.CallAfterDelay(() =>
            {
                theImage.GetComponent<AspectRatioFitter>().enabled = true;
            }, 0.01f);
            theImage.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            theImage.GetComponent<AspectRatioFitter>().aspectRatio = ((float)theTexture.width / (float)theTexture.height) < 1.6F ? 1.715F : (float)theTexture.width / (float)theTexture.height;
            return theImage.texture;
        }
        else if (theTexture.width == theTexture.height)
        {
            theImage.GetComponent<AspectRatioFitter>().enabled = false;
            theImage.rectTransform.anchorMin = new Vector2(0, 0);
            theImage.rectTransform.anchorMax = new Vector2(1, 1);
            theImage.rectTransform.sizeDelta = new Vector2(0, 0);

            HelperUtil.CallAfterDelay(() =>
            {
                theImage.GetComponent<AspectRatioFitter>().enabled = true;
            }, 0.01f);
            theImage.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
            theImage.GetComponent<AspectRatioFitter>().aspectRatio = 1;
            return theImage.texture;
        }
        return theImage.texture;

    }
    public static void AddIfNotAvailable<KEY, VALUE>(this IDictionary theDictionary, KEY theKey, VALUE theValue)
    {
        if (!theDictionary.Contains(theKey)) theDictionary.Add(theKey, theValue);
    }

    public static void AddIfNotAvailable<VALUE>(this IList theList, VALUE theValue)
    {
        if (!theList.Contains(theValue)) theList.Add(theValue);
    }

    public static T GetTypeInParents<T>(this Transform theTansform)
    {
        while (true)
        {
            if (theTansform.parent != null)
            {
                if (theTansform.parent.GetComponent<T>() != null)
                {
                    return theTansform.parent.GetComponent<T>();
                }
                else
                {
                    theTansform = theTansform.parent;
                }
            }
            else return default(T);
        }
    }
}
