using ReadyPlayerMe.AvatarLoader;
using ReadyPlayerMe.Core;
using UnityEngine;


 public class RuntimeTest1 : MonoBehaviour    {
    [SerializeField]
    private string avatarUrl = "https://d1a370nemizbjq.cloudfront.net/28aa3030-18ce-428d-b0f1-5491e98f3b85.glb";
    [SerializeField] private string avatarUrl2 = "https://d1a370nemizbjq.cloudfront.net/209a1bc2-efed-46c5-9dfd-edc8a1d9cbe4.glb";
    private GameObject avatar;
    private GameObject avatarForUI;

    private void Start()
        {
        var avatarLoader = new AvatarObjectLoader();
        // use the OnCompleted event to set the avatar and setup animator
        avatarLoader.OnCompleted += (_, args) =>
        {
            avatar = args.Avatar;
            avatar.GetComponent<Animator>().enabled = false;
            if (AvatarHolderManager.instance.avatar == null) AvatarHolderManager.instance.avatar = args.Avatar;
            avatarForUI = Instantiate(avatar) as GameObject;
             OnAvatarLoaded();

        };
     
        if (AvatarHolderManager.instance.MaleAvatar)
        {
            avatarLoader.LoadAvatar(avatarUrl);
        }
       
    }

    private void OnAvatarImported(GameObject avatar) {
            Debug.Log($"Avatar imported. [{Time.timeSinceLevelLoad:F2}]");
    }

    private void OnAvatarLoaded()
    {
       
       if(AvatarHolderManager.instance.avatar==null) AvatarHolderManager.instance.avatar = this.avatar;
       if(AvatarHolderManager.instance.avatarForUI==null) AvatarHolderManager.instance.avatarForUI = this.avatarForUI;
       AvatarHolderManager.instance.LoadAvatars();
       Scene1Manager.Instance.GenderButton.gameObject.SetActive(false);
       
    }
 }

