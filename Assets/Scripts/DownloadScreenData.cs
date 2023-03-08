using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownloadScreenData : MonoBehaviour
{
	public Image backGroundImage;
	public void Awake()
	{
		HelperUtil.SetTexture(HelperUtil.instance.converImageURL, (downloadedTexture) =>
		{
			Sprite sprite = Sprite.Create((Texture2D)downloadedTexture, new Rect(0, 0, downloadedTexture.width, downloadedTexture.height), new Vector2(downloadedTexture.width/2, downloadedTexture.height / 2));
			backGroundImage.sprite = sprite;
		});
	}
}
