using System.Collections;
using System.Collections.Generic;  
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gamein
{
    [CreateAssetMenu(fileName = "GameLibrary", menuName = "Gamein/GameLibrary", order = 1)]
    public class GameLibrary : ScriptableObject
    {
        [SerializeField] private bool useLocalScenes;
        public bool UseLocalScenes
        {
            get { return useLocalScenes; }
        }
        [SerializeField] private string localSceneName;
        public string LocalSceneName
        {
            get { return localSceneName; }
        }

        #region SPRITE_DICTIONARY

        [Header("References")]
        [SerializeField] private List<SpriteData> spriteLibrary;
        private Dictionary<string, Sprite> spriteDictionary = null;
        public Sprite GetSprite(string spriteID)
        {
            if (spriteDictionary == null)
            {
                spriteDictionary = new Dictionary<string, Sprite>();

                for (int i = 0; i < spriteLibrary.Count; i++)
                {
                    if (!spriteDictionary.ContainsKey(spriteLibrary[i].uniqueID))
                    {
                        spriteDictionary.Add(spriteLibrary[i].uniqueID, spriteLibrary[i].sprite);
                    }
                    else
                    {
                        Debug.LogError("ERROR: Sprite ID : " + spriteLibrary[i].uniqueID + " already exists in the sprite dictionary.");
                    }
                }
            }
            if (spriteDictionary.ContainsKey(spriteID))
            {
                return spriteDictionary[spriteID];
            }
            else
            {
                Debug.LogError("ERROR: Sprite ID : " + spriteID + " does not exists in the sprite dictionary.");
                return null;
            }
        }

        #endregion

        #region Default Data
        public Material defaultMaterial;
        public Sprite defaultImage;
        public Sprite defaultImageSqure;
        public Sprite defaultProfileImage;
        public Sprite defaultVideoSprite;
        public Sprite defaultextSprite;
        public Sprite defaulaudioSprite;
        public Sprite litBoader;
        public Sprite darkBoader;
        public Font boldFont;
        public Font litFont;
        public Sprite defaultAudioBackground;
        public List<CountryData> countryData = new List<CountryData>();
        public List<Font> TotalFont = new List<Font>();
        #endregion

        #region AddressableScene Reference
        public AssetReference LobbySceneref;
        public AssetReference HealtheeRef;
        public AssetReference MechanoRef;
        public AssetReference QuestyRef;
        public AssetReference ExtroverseRef;

		#endregion

		#region  T&C And Configuration
        [Header("Url Links")]
        public string termsconditions_URL="Enter the url or links";
        public string privacyPolicy_URL="Enter the url or links";
        #endregion
	}

	[System.Serializable]
    public class CountryData
    {
        public string name;
        public int countryCode;
        public Sprite flagImage;
    }

    [System.Serializable]
    public struct SpriteData
    {
        public string uniqueID;
        public Sprite sprite;
    }
}