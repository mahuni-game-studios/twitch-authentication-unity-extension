# Unity Twitch Web Request Extension by Mahuni Game Studios

[![Downloads](https://img.shields.io/github/downloads/mahuni-game-studios/twitch-web-request-unity-extension/total.svg)](https://github.com/mahuni-game-studios/twitch-web-request-unity-extension/releases/)
[![Latest Version](https://img.shields.io/github/v/release/mahuni-game-studios/twitch-web-request-unity-extension)](https://github.com/mahuni-game-studios/twitch-web-request-unity-extension/releases/tag/v1.0)

An extension to use the Twitch API directly via Unity web requests. Including authorization and local storage of the token and an example implementation showing reward creation / deletion via web request.

Available HTTP request types:
- GET
- POST
- DELETE
- PATCH
- PUT

## Code Snippet Examples

The simplest implementation to give your game permission to access and use the Twitch API with web requests!

### Authentication

```cs
public class YourUnityClass : MonoBehaviour
{
    private void Start()
    {        
        // Register to authentication finished event
        TwitchWebRequestAuthentication.OnAuthenticated += OnAuthenticated;
        
        // Set relevant information to the connection
        TwitchWebRequestAuthentication.ConnectionInformation infos = new(twitchClientIdText.text, new List<string>(){TwitchWebRequestAuthentication.ConnectionInformation.CHANNEL_MANAGE_REDEMPTIONS});
        
        // Start authentication
        TwitchWebRequestAuthentication.StartAuthenticationValidation(this, infos);
    }
    
    private void OnAuthenticated(bool success)
    {
        if (success)
        {
            // TODO: Start using the web requests to the Twitch API from here!
        }
    }
}
```

### Create a custom reward

```cs
public class YourUnityClass : MonoBehaviour
{
    private string channelName = "mychannelname";
    private string testRewardTitle = "My awesome reward";
    private long testRewardCost = 500;
    private TwitchWebRequests twitchWebRequests;
    
    private void Start()
    {
        // Initialize the web request by passing your channel name        
        twitchWebRequests = new TwitchWebRequests(channelName);
        
        // Create a reward asynchronously
        OnCreateReward(testRewardTitle, testRewardCost);
    }
      
    private async void OnCreateReward(string title, long cost)
    {
        var response = await twitchWebRequests.CreateRewardAwaitable(title, cost);        
        if (response.responseCode == TwitchResponseCode.OK)
        {
            TwitchCreateRewardResponse result = (TwitchCreateRewardResponse)JsonUtility.FromJson(response.responseBody, typeof(TwitchCreateRewardResponse));
            Debug.Log($"Reward with id {result.GetData().id} was successfully created!");
        }        
    }  
}
```

## Installation Guide

### Prerequisites

To be able to interact with the Twitch API, you need to register your Twitch application. You can follow how to do that with this [Guide from Twitch](https://dev.twitch.tv/docs/authentication/register-app/). In short:

1. Sign up to Twitch if you don't have already
2. Navigate to the [Twitch Developer Console](https://dev.twitch.tv/console/apps)
3. Create a new application and select an appropriate category, e.g. as Game Integration
4. Click on *Manage* on your application entry and you will be presented a `Client ID`. This ID will be needed to interact with Twitch.

<font color="red">The `Client ID` should stay secret, do not share or show it!</font>

#### Demo scene

To use the provided `TwitchWebRequestExtension_Demo` scene, the `TextMeshPro` package is required. If you do not have it yet imported into your project, simply opening the `TwitchWebRequestExtension_Demo.scene` will ask if you want to import it. Select the `Import TMP Essentials` option, close the `TMP Importer` and you are good to go.

### Setup project
1. Either open this project or your own project in the Unity Editor
2. Start using the `TwitchWebRequestAuthentication` and the `TwitchWebRequests` scripts right away, or take a look into the `TwitchWebRequestExtension_Demo` scene to find an easy example implementation.