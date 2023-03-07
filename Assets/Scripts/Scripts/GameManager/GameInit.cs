using UnityEngine;
using UnityEngine.SceneManagement;
public class GameInit : MonoBehaviour
{
	private void Start()
	{
		HelperUtil.ShowLoading();
		HelperUtil.CallAfterDelay(() =>
		{
			HelperUtil.HideLoading();
#if UNITY_WEBGL
            SceneManager.LoadScene(StaticLibrary.SceneName.Home);
#endif

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
			var asyncData = SceneManager.LoadSceneAsync(StaticLibrary.SceneName.Home);
			asyncData.completed += (data) =>
			  {
				  if (PlayerPrefs.GetString("LOGINDATA") == string.Empty) { UIManager.instance.ActivatePanel(ScreenData.ScreensName.LoginPanel); }
				  else { UIManager.instance.ActivatePanel(ScreenData.ScreensName.HomePanel); }
			  };
#endif
		}, 1f);
	}
}
