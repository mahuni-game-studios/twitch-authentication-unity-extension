# Unity Twitch Authentication Extension by Mahuni Game Studios

[![Downloads](https://img.shields.io/github/downloads/mahuni-game-studios/twitch-authentication-unity-extension/total.svg)](https://github.com/mahuni-game-studios/twitch-authentication-unity-extension/releases/)
[![Latest Version](https://img.shields.io/github/v/release/mahuni-game-studios/twitch-authentication-unity-extension)](https://github.com/mahuni-game-studios/twitch-authentication-unity-extension/releases/tag/v1.0)

An extension to authenticate your Twitch application and start using the Twitch API via Unity. Includes authorization and local storage of the token.

## Code Snippet Examples

The simplest implementation to give your game permission to access and use the Twitch API!

### Authentication

```cs
public class YourUnityClass : MonoBehaviour
{
    private void Start()
    {        
        // Register to authentication finished event
        TwitchAuthentication.OnAuthenticated += OnAuthenticated;
        
        // Set relevant information to the connection
        TwitchAuthentication.ConnectionInformation infos = new("your-client-id", new List<string>(){TwitchWebRequestAuthentication.ConnectionInformation.CHANNEL_MANAGE_REDEMPTIONS});
        
        // Start authentication
        TwitchAuthentication.StartAuthenticationValidation(this, infos);
    }
    
    // Authentication has finished
    private void OnAuthenticated(bool success)
    {
        if (success)
        {
            // TODO: Start using the Twitch API from here!
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

To use the provided `TwitchAuthenticationExtension_Demo` scene, the `TextMeshPro` package is required. If you do not have it yet imported into your project, simply opening the `TwitchAuthenticationExtension_Demo.scene` will ask if you want to import it. Select the `Import TMP Essentials` option, close the `TMP Importer` and you are good to go.

### Setup project
1. Either open this project directly or import it to your own project in the Unity Editor
2. Start using the `TwitchAuthentication` script right away, or take a look into the `TwitchAuthenticationExtension_Demo` scene to find an easy example implementation.