using System;
using UnityEngine;

public static class PlayfabAuth
{
    // Const - Save email/password
    public const string PlayfabAuthPlayerPrefsKeyUsername = "playfab_auth_username";
    public const string PlayfabAuthPlayerPrefsKeyEmail = "playfab_auth_email";
    public const string PlayfabAuthPlayerPrefsKeyPassword = "playfab_auth_password";

    // Getter
    public static bool IsLoggedIn
    {
        get
        {
            // TODO: Implement check that we are logged in
            return PlayFab.PlayFabClientAPI.IsClientLoggedIn();
        }
    }

    // Functions
    public static void TryRegisterWithEmail(string email, string password, Action registerResultCallback, Action errorCallback)
    {
        PlayfabAuth.TryRegisterWithEmail(email, password, email, registerResultCallback, errorCallback);
    }

    public static void TryRegisterWithEmail(string email, string password, string username, Action registerResultCallback, Action errorCallback)
    {
        // TODO: Request playfab for registration
        var request = new PlayFab.ClientModels.RegisterPlayFabUserRequest
        {
            Email = email,
            Password = password,
            Username = username,
            RequireBothUsernameAndEmail = true,
        };
        PlayFab.PlayFabClientAPI.RegisterPlayFabUser(request, (result) =>
        {
            Debug.Log("Successfully registered user account with PlayFab.");
            registerResultCallback.Invoke();
        }, (error) =>
        {
            Debug.LogError("Something went wrong with your first API call.  :(");
            Debug.LogError("Here's some debug information:");
            Debug.LogError(error.GenerateErrorReport());
            errorCallback.Invoke();
        });
        // -------------------------------
        // >> For the moment, we will consider it to be a success
        //registerResultCallback.Invoke();
    }

    public static void TryLoginWithEmail(string email, string password, Action loginResultCallback, Action errorCallback)
    {
        // TODO: Request playfab for login
        var request = new PlayFab.ClientModels.LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password,
        };
        PlayFab.PlayFabClientAPI.LoginWithEmailAddress(request, (result) =>
        {
            Debug.Log("Successfully logged in user account with PlayFab.");
            loginResultCallback.Invoke();
        }, (error) =>
        {
            Debug.LogError("Something went wrong with your first API call.  :(");
            Debug.LogError("Here's some debug information:");
            Debug.LogError(error.GenerateErrorReport());
            errorCallback.Invoke();
        });
        // -------------------------------
        // >> For the moment, we will consider it to be a success
        //loginResultCallback.Invoke();
    }

    // Logout
    public static void Logout(Action logoutResultCallback, Action errorCallback)
    {
        // Clear all keys from PlayerPrefs
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyUsername);
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyEmail);
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyPassword);

        // Callback
        logoutResultCallback.Invoke();
    }
}
