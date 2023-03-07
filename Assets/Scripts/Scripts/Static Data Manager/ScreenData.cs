using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenData : MonoBehaviour
{
    [SerializeField] private List<InputFieldData> inputFieldReferences;

    [SerializeField] private List<InputFieldData> backbuttonReferences;

    private Dictionary<string, InputField> inputFieldDictionary = new Dictionary<string, InputField>();

    void InitInputFieldDictionary()
    {
        foreach (var item in inputFieldReferences)
        {
            inputFieldDictionary.Add(item.key, item.inputField);
        }
    }

    private void Start()
    {
        InitInputFieldDictionary();
    }

    public InputField GetInputField(string inputFieldKey) => inputFieldDictionary[inputFieldKey];

    public List<InputFieldData> GetAllInputField(string screenName)
    {
        if (this.name == screenName)
        {
            return inputFieldReferences;
        }
        else
        {
            return null;
        }
    }

    //Constant keys.
    public static class RegisterKeys
    {
        public const string RegisterFirstName = "firstName";
        public const string RegisterLastName = "lastName";
        public const string RegiseterEmail = "Registeremail";
        public const string RegisterPassword = "Registerpassword";
        public const string ConfirmPassword = "RegisterconfirmPassword";
        public const string RegistermobileFirstName = "mobilefirstName";
        public const string RegistermobileLastName = "mobilelastName";
        public const string RegisetermobileEmail = "Registermobileno";
        public const string RegistermobilePassword = "Registermobilepassword";
        public const string ConfirmobilePassword = "RegisterconfirmmobilePassword";
    }

    public static class LoginKeys
    {
        public const string LoginEmail = "loginemail";
        public const string LoginPassword = "loginPassword";
        public const string mobilNumber = "mobilNumber";
        public const string mobilPassword = "mobilPassword";
    }
    public static class HomeKeys
    {
        public const string shortSearch = "shortSearch";
    }
    public static class ResetPassword
    {
        public const string newpassword = "newpassword";
        public const string confirmpassword = "confirmpassword";
    }
    public static class EmailVerification
    {
        public const string emailverificationcode = "emailverificationcode";
    }
    public static class NewObjectKeys
    {
        public const string Name = "name";
        public const string Descripition = "descripition";
    }
    public static class AddNewMemoryKeys
    {
        public const string Title = "title";
        public const string Descripition = "descripition";
        public const string Location = "location";
    }
    public static class selectsortKeys
    {
        public const string sort = "Sort";
    }
    public static class ScreensName
    {
        public const string RegisterPanel = "Register";
        public const string LoginPanel = "Login";
        public const string ForgotPasswordScreen = "ForgotPassword";
        public const string ForgotPasswordVerificationScreen = "ForgotPasswordVerification";
        public const string ResetPassword = "ResetPassword";
        public const string ForgotPasswordEmailVerificationtScreen = "EmailVerificationt";
        public const string OTPVerificationtScreen = "OTPVerificationt";
        public const string HomePanel = "Home";
        public const string DetailItems = "DetailItems";
        public const string ItemShort = "ItemShort";
        public const string FaceRecognitionStartScreen = "FaceRecognition";
        public const string NFCScanScreen = "NFCScan";
        public const string NFCScanHistoryScreen = "NFCScanHistory";
        public const string AddNewObject = "AddNewObject";
        public const string AddNewMemory = "AddNewMemory";
        public const string ObjectDetail = "ObjectDetail";
        public const string MemoryDetail = "MemoryDetail";
        public const string FullObjectDetail = "FullObjectDetail";
        public const string FullMemoryDetail = "FullMemoryDetail";
        public const string ObjectMemories = "ObjectMemories";
        public const string UpdateObject = "UpdateObject";
        public const string UpdateMemroy = "UpdateMemory";
        public const string Gameplay = "GameplayScene";
        public const string TokenDispenserPanel = "TokenDispenserPanel";
        public const string ProfilePanel = "ProfilePanel";
        public const string EditProfilePopupPanel = "EditProfilePopupPanel";
        public const string PublishBuildPanel = "PublishBuildPanel";
        public const string Support = "Support";
        public const string AdminPanel = "AdminPanel";
        public const string DeveloperPanel = "DeveloperPanel";
        public const string NotificationAdminPanel = "NotificationAdminPanel";
        public const string NotificationDevPanel = "NotificationDevPanel";
        public const string ItemPurchasePanel = "ObjectPurcahseDetail_Panel";
        public const string MintItemPanel = "Minting_Panel";
        public const string AuctionPanel = "AuctionDetail";
        public const string ObjectPlacement = "ObjectPlacementController";
        public const string LoadingPanel = "LoadingPanel"; 
        public const string InviteFriendsPanel = "InviteFriendsPanel";
       
        [Header("Desktop Panels References")]
        public const string desktopRegisterPanel = "Sign Up Panel";
        public const string desktopLoginPanel = "Sign in Panel";
        public const string desktopForgotPasswordScreen = "Forgot Password Panel";
        public const string desktopResetPasswordScreen = "Reset Password Panel";
        public const string desktopLoginRabbitHoleScreen = "Login Rabbit Hole Panel";
        public const string desktopLogoutPanelScreen = "Logout Panel";
        public const string desktopLoginCollectorScreen = "Login Collector Panel";
        public const string DesktopForgotPasswordEmailVerificationtScreen = "Verification Code Panel";


        [Header("Metaverse")]
        public const string MainControllerScreen = "MainController";
        public const string ItemDetailScreen = "ObjectDetail";
        public const string NewItemScreen = "NewItem";
        public const string MyItemScreen = "Inventory";
        public const string ItemPlaceScreen = "ItemPlaceDataScreen";
        public const string ObjectMapScreen = "MapScreen";
        public const string HouseDetailScreen = "HouseDetailScreen";
        public const string LeaseEditScreen = "LeaseEditScreen";
        public const string ItemMapScreen = "ItemMapScreen";
    }
    public static class PopupName
    {
        public const string AudioRecorder = "AudioRecorder";
        public const string CreateText = "CreateText";
    }
}
[System.Serializable]
public struct InputFieldData
{
    public string key;
    public InputField inputField;
}

[System.Serializable]
public struct BackButtonData
{
    public string key;
    public Button backbutton;
}