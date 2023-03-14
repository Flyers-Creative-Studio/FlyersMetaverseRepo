using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class PortalIInteraction : InteractableObject
{
    public SceneType sceneType;
    public PlayerManager player;
 
   // public AvatarLoadingManager AvatarLoadingManagerRef;

    public override void OnInteraction(InteractionType interactionType, string tag)
    {
        if (interactionType == InteractionType.enter)
        {
            player.SetInteraction(() =>
            {

               // HelperUtil.instance.LoadDownloadingScene(sceneType.ToString());
                LoadAddressable();

            }, "Explore");
        }
        else if (interactionType == InteractionType.exit)
        {
            player.HideInteractionUI();
        }
    }
    public void LoadAddressable()
    {
        if (AvatarHolderManager.instance.avatar!=null)
        {
            AvatarHolderManager.instance.avatar.transform.SetParent(AvatarHolderManager.instance.transform);
            AvatarHolderManager.instance.avatar.SetActive(false);
        }
        AvatarHolderManager.instance.SetParent_DefaultAvatars();


        Debug.Log(sceneType);
        // StartCoroutine(StartLoad());
        //  HelperUtil.LoadScenesWithAdressable(sceneType.ToString(),"Scenes");
         HelperUtil.instance.LoadDownloadingScene(sceneType.ToString());

       
    }
  

    public IEnumerator StartLoad()
    {
        HelperUtil.ShowLoading();

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Single);
       
        yield return handle;

        
    }
    public AssetReference GetNextScene(SceneType sceneType)
    {

         //if (AvatarLoadingManagerRef.Avatar != null) AvatarLoadingManagerRef.Avatar.transform.SetParent(AvatarHolderManager.instance.transform);

        if (sceneType == SceneType.Healthee)
        {
            return HelperUtil.GameLibrary.HealtheeRef;
        }
        else if (sceneType == SceneType.Mechano)
        {
            return HelperUtil.GameLibrary.MechanoRef;
        }
        else if (sceneType == SceneType.Questy)
        {
            return HelperUtil.GameLibrary.QuestyRef;
        }else if (sceneType == SceneType.Extroverse)
        {
            return HelperUtil.GameLibrary.ExtroverseRef;
        }
        return HelperUtil.GameLibrary.LobbySceneref;
    }

}
