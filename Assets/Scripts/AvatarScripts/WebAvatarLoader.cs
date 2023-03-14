using ReadyPlayerMe.AvatarLoader;
using ReadyPlayerMe.Core;
using ReadyPlayerMe.Core.Data;
using UnityEngine;

public class WebAvatarLoader : MonoBehaviour
{
    private const string TAG = nameof(WebAvatarLoader);
    private GameObject avatar;
    private string avatarUrl = "";
    private GameObject avatarForUI;

    private void Start()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        CoreSettings partner = Resources.Load<CoreSettings>("CoreSettings");
        
        WebInterface.SetupRpmFrame(partner.Subdomain);
#endif
    }
    
    public void OnWebViewAvatarGenerated(string generatedUrl)
    {
        var avatarLoader = new AvatarObjectLoader();
        avatarUrl = generatedUrl;
        avatarLoader.OnCompleted += OnAvatarLoadCompleted;
        avatarLoader.OnFailed += OnAvatarLoadFailed;
        avatarLoader.LoadAvatar(avatarUrl);



       
    }

    private void OnAvatarLoadCompleted(object sender, CompletionEventArgs args)
    {
        if (avatar) Destroy(avatar);
        avatar = args.Avatar;
        if (args.Metadata.BodyType == BodyType.HalfBody)
        {
            avatar.transform.position = new Vector3(0, 1, 0);
        }

        avatar.GetComponent<Animator>().enabled = false;
        if (AvatarHolderManager.instance.avatar == null) AvatarHolderManager.instance.avatar = args.Avatar;
        avatarForUI = Instantiate(avatar) as GameObject;
        OnAvatarLoaded();
    }

    private void OnAvatarLoadFailed(object sender, FailureEventArgs args)
    {
        SDKLogger.Log(TAG,$"Avatar Load failed with error: {args.Message}");
    }
    private void OnAvatarLoaded()
    {

        if (AvatarHolderManager.instance.avatar == null) AvatarHolderManager.instance.avatar = this.avatar;
        if (AvatarHolderManager.instance.avatarForUI == null) AvatarHolderManager.instance.avatarForUI = this.avatarForUI;
        AvatarHolderManager.instance.LoadAvatars();
        Scene1Manager.Instance.GenderButton.gameObject.SetActive(false);

    }
}
