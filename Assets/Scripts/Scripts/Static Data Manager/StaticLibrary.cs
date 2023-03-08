using System.Text;
using UnityEngine;
using System;
using System.Collections.Generic;

public static class ResourcesPath 
{
    public static class UI 
    {
        public const string WorldSliderPrefab = "UI/WorldSliderData";
        public const string DesktopWorldSliderPrefab = "UI/WorldSliderDataDesktop";
        public const string HomeScreen_Created_Entry= "UI/Created_Item";
        public const string HomeScreen_OnSale_Entry= "UI/On_Sale_Item";
        public const string HomeScreen_Owned_Entry= "UI/Owned_Item";
        public const string HomeScreen__Entry= "UI/On_Sale_Item";
        public const string NFC_History_Item_Entry = "UI/NFC_History_Item";
        public const string Memory_Post_Entry = "UI/MemoryPost";
        public const string MemoryInGameScene = "UI/MemoryInGameScene";
        public const string Item_Topic = "UI/Topic_Prefab";
        public const string Comment_Entry = "UI/comment_Prefabe";
        public const string Comment_Entry_GameScene = "UI/comment_PrefabeInGame";

        public const string Reply_Entry = "UI/reply_Prefabe";
        public const string Reply_InGame = "UI/ReplyInGame";
        public const string Comment_Reply_Entry = "UI/reply_inputBox";
        public const string Comment_Reply_Entry_InGame = "UI/reply_inputBoxInGame";
        public const string TagButton = "UI/Tag_Button";
        public const string CusstomTag = "UI/CustomTagPanelPrefab";
        public const string ObjectsInRoom = "UI/ObjectsInRoom";
        public const string ObjectsInMiniMap = "UI/ObjectInMiniMap";
        public const string MarkerOnMap = "UI/ObjectMarker";
    }
}

public static class UI_StaticData
{
  
    public static class Screen 
    {
        public static class Register 
        {
            public const string Email = "email";
            public const string Password = "password";
            public const string ConfirmPassword = "confirmPassword";
        }
    }
}

public static class GameMessage
{
    public const string ObjectPlaced = "Object placed successfully.";
    public const string ZipHierarchyNotPerfect = "Zip hierarchy is not perfect.";
    public const string BuyLandFeatureNotAvaialble = "Buy feature is not impemented yet.";
    public const string LandAlreadyOwned = "You cannot interact with this land as it is owned by someone else.";
    public const string FeedbackDone = "Feedback request saved successfully";
    public const string RequestDone = "Support request saved successfully";
    public const string UnsupportedAudioFormat = "This audio format is not supported !";
    public const string InternetNotWorking = "Please check your internet connection.";
    public const string NFCNotFound = "There is no object associated with this tag.";
    public const string Message_1 = "";
    public const string Message_2 = "";
    public const string something_went_wrong = "Something went wrong on the server, kindly try again after some time.";
    public const string formateissue = "Please check your format.";
    public const string logoutWarrning = "Are you sure you want logout.";
    public const string SelectTag = "Select Tag";
    public static string PleaseEnterEntry(string Entry) => "Please Enter " + Entry + ".";
    public static string MemeoryCountInObject(int memeoryCount) => "This object contains " + memeoryCount + " memories.";
    public static string defaultlocationMessage= "Location(Optional)";
    public static string faceNotSupport = "Device does not support biometric authentication";
    public static string OnlyForIOS = "Game only support biometric authentication in IOS";
    public static string fingerprintBiometric = "Please verify Fingerprint authentication";
    public static string fingerprintBiometricTryAgain = "Please try again Fingerprint authentication";
    public static string fingerprintBiometricfailed = "Failed Fingerprint authentication";
    public static string addItemFirst = "Item is Empty.";
    public static string audioRecordingFinished = "Your audio has been Recorded successfully.";
    public static string objectUploaded = "Your Zip has been Selected successfully.";
    public static string objectUploadedFail = "Your Zip has been Fail to Select.";
    public static string fileUploaded = "Your file has been Selected successfully.";
    public static string pleaseSelectZip = "Please select .zip file.";
    public static string zipLimit = "Please select a zip file under 30 Mb.";
    public static string buySuccessfully = "Object bought successfully.";
    public static string ObjectSelected(string objectFile) => "Your "+objectFile+" file has been Selected successfully.";
    public static string FileInstruction(string fileType) => "Please select " + fileType +" file.";
    public static string clickImage = "Please click Image.";
    public static string recordVideo = "Please record Video.";
    public static string AudioError = "Audio file extension not supported!";
    public static class NFCMessages
    {
        public const string WriteTag = "Hold the NFC tag against the device to write the object into the tag.";
        public const string UpdateTag = "Hold the NFC tag against the device to Update the object into the tag.";
        public const string UpdateSuccess = "Successfully added the object to the NFC tag.";
        public const string UpdateFailed = "Failed to add the object into the NFC tag.";
        public const string NFCFound="NFC Readed Successfully with the ID: ";
        public const string Tag_Invalid = "Tag not found, kindly try a new tag.";
        public const string SaveObjectFirst = "In order to overwrite, Kindly save an object first.";
        public const string UpdateObjectFirst = "In order to overwrite, Kindly Update an object first.";
        public const string WriteNFC = "Click on Write button to write the NFC into the tag.";
        public const string ItemSaved = "Item saved on the server, you can write the data.";
        public const string WriteSuccess = "Data has been successfully writen into the NFC tag.";
  
    }
    public static class SelectSort
    {
        public const string EnterValueforsort = "No Result Found.";
    }

    public static class OTPVerification
    {
        public const int TotalTimeronOTP =120;
        public const string OTPVerificationTimeOver = "please try again or resend the OTP.";
        public const string EmailVerificationTimeOver = "timer has been expired.";
        public const string OTPVerificationResendOTP = "Resend OTP";
        public const string OTPVerificationBackTologin = "Back to login";
        public const string OTP = "OTP";

    }
  
    public static class InputMessages
    {
        public const string Email = "Email";
        public const string MobileNo = "MobileNo";
        public const string FirstName = "First Name";
        public const string LastName = "Last Name";
        public const string Password = "Password";
        public const string ConfirmPassword = "Confirm Password";
        public const string code = "Code";
        public const string EnterEmail = "Please Enter Email.";
        public const string EnterMobile = "Please Enter MobileNo.";
        public const string EnterFirstName = "Please Enter First Name.";
        public const string EnterLastName = "Please Enter Last Name.";
        public const string EnterPassword = "Please Enter Password.";
        public const string EnterConfirmPassword = "Please Enter Confirm Password.";
        public const string PasswordConfirmPasswordSame = "Password and Confirm password must be same.";

        public static string Minimumlenth(string field,int limit) => "Please enter minimum "+limit+" character " + field + ".";
    }
    public static class GamePlayMessages
    {
        public const string Exit = "Are you sure, you want to quit this game?";
        public const string EndTimeEqualToStartTime = "End time could not be the same as Start Time, We will set it Automaticlly";
        public const string EndTimeBeforeThanStartTime = "End time could not be Before the Start Time, We will set it Automaticlly";
    }

    public static string MessageConvertor(string originalMessage)
    {
        if (originalMessage == "Invalid Email") originalMessage = "Please enter a valid email.";
        if (originalMessage == "") originalMessage = "Something went wrong.";
        if (originalMessage == "Reset password reset code sent to email, please check!") originalMessage = "A code has been sent to your email to reset the password.";
        if (originalMessage == "You are not tenant of this room!") originalMessage = "You can't un-place this object from a public room";
        if (originalMessage == "You have to lease this room first") originalMessage = "You can't place this object in a public room";
        return originalMessage;
    }

    public static class NewObjectScreen
    {
        public const string DefaultSelectedItem = "Click here to choose your file to upload";
        public static string ItemSelectd(int totalItem)=>totalItem+" selected item";
        public static string EnterEntery(string entery) => "Please Enter " + entery + ".";
        public static string EnterMinimum(string field,int limit) => "Please enter minimum " + limit + " character " + field + ".";
        public const string Name = "Name";
        public const string Descripation = "Descripation";
        public const string Tag = "Tag";
        public const string ObjectCreated = "Your object has been created.";

    }
     public static string FileIncorrect(string field) => "You need to select a file first in order to create " + field + ".";

    public const string InvalidFile = "Something is wrong with your file, Please try again.";

    public static class NewMemoryScreen
    {
        public const string DefaultSelectedItem = "Click here to choose your file to upload";
        public static string ItemSelectd(int totalItem) => totalItem + " selected item";
        public static string EnterEntery(string entery) => "Please Enter " + entery + ".";
        public static string Select(string entery) => "Please Selecte " + entery + ".";
        public static string EnterMinimum(string field, int limit) => "Please enter minimum " + limit + " character " + field + ".";
        public const string Title = "Title";
        public const string Detail = "Detail";
        public const string Location = "Location";
        public const string Image = "Image";
        public const string Comment = "Comment";
        public static string TotalMemroy(int totalcount) => "This object contains"+totalcount+" memories";
    }
}

public static class RegexData
{
    public const string EmailRegex= @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

    public const string PhoneNumberRegex = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
}

public static class StaticData 
{
   public static StringBuilder ResetApiEmail=new StringBuilder();
   public static StringBuilder CodeApiEmail=new StringBuilder();
   public static StringBuilder commentID = new StringBuilder();
   public static StringBuilder commentscoller = new StringBuilder();
}

#region Enums

public enum SeeAllObjectType
{
    created,
    owned,
    onsale
}
public enum MenuButtonsType
{
    home,
    Profile,
    Support,
    Setting,
    Wallets,
    Notification,
    Marketplace,
    Logout,
    Admin,
    TokenDispenser,
    ////
    InviteFriend
    
}

public enum InviteFriendsType
{
    user,
    friend ,
    pending
}
public enum RegisterType
{
    email,
    mobile
}
public enum LoginType
{
    email,
    mobile
}
public enum ItemDetailBottomView
{
    none,
    comments,
    memory
}
public enum MapType
{
    get,
    set
}
public enum UploadDataType
{
    image,
    video,
    pdf,
    audio,
    text,
    customText,
    obj,
    unitypackage,
    any
}
public enum InteractionType
{
    enter,
    stay,
    exit
}
public enum SceneType
{
    Lobby,
    Healthee,
    Mechano,
    Questy,
    Extroverse
}

#region Environment Scene
public enum PlayerView
{
    tpv,
    fpv
}
#endregion

#endregion

[System.Serializable]
public struct CountryData
{
    public string countryCode;
    public Sprite countrySprite;
}