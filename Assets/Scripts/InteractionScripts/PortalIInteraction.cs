using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class PortalIInteraction : InteractableObject
{
    public SceneType sceneType;
    public PlayerManager player;
    //public AvatarLoadingManager AvatarLoadingManagerRef;

    public override void OnInteraction(InteractionType interactionType, string tag)
    {
        if (interactionType == InteractionType.enter)
        {
            player.SetInteraction(() =>
            {
                var asyncSceneRef = SceneManager.LoadSceneAsync(StaticLibrary.SceneName.DownloadingScene, LoadSceneMode.Single);
                asyncSceneRef.completed += (data) =>
                {
                    HelperUtil.ShowLoading();
                    HelperUtil.CallAfterDelay(() =>
                    {
                        HelperUtil.LoadSceneWithAdressable((asynceRef) =>
                        {
                            HelperUtil.HideDownloadLoading();
                            HelperUtil.HideLoading();
                            if (asynceRef.Status == AsyncOperationStatus.Succeeded)
                            {
                              
                            }
                            if (asynceRef.Status == AsyncOperationStatus.Failed)
                            {
                                HelperUtil.ShowMessage(GameMessage.InternetNotWorking);
                            }
                        }, sceneType.ToString());
                    }, 10f);
                };
            }, "Explore");
        }
        else if (interactionType == InteractionType.exit)
        {
            player.HideInteractionUI();
        }
    }
    public AssetReference GetNextScene(SceneType sceneType)
    {

        // if (AvatarLoadingManagerRef.Avatar != null) AvatarLoadingManagerRef.Avatar.transform.SetParent(AvatarHolderManager.instance.transform);

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
