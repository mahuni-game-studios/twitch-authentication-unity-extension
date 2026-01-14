// © Copyright 2026 Mahuni Game Studios

using System.Collections.Generic;
using Mahuni.Twitch.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// A class to demonstrate and test the authentication and calls using web requests to the Twitch API.
/// Use it together with the TwitchWebRequestExtension_Demo scene.
/// </summary>
public class TwitchAuthenticationExtensionDemoUI : MonoBehaviour
{
    public TMP_InputField channelNameText;
    public TMP_InputField twitchClientIdText;
    public TextMeshProUGUI authenticationDescriptionText;
    public Toggle scopeManageRedemptionsToggle, scopeReadChatToggle, scopeReadSubscriptionsToggle;
    public Button authenticateButton, resetAuthenticationButton;
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called for the first time.
    /// </summary>
    private void Start()
    {
        TwitchAuthentication.Reset();
        TwitchAuthentication.OnAuthenticated += OnAuthenticated;
        
        channelNameText.onValueChanged.AddListener(ValidateFields);
        twitchClientIdText.onValueChanged.AddListener(ValidateFields);
        scopeManageRedemptionsToggle.onValueChanged.AddListener(ValidateScopes);
        scopeReadChatToggle.onValueChanged.AddListener(ValidateScopes);
        scopeReadSubscriptionsToggle.onValueChanged.AddListener(ValidateScopes);
        authenticateButton.onClick.AddListener(OnAuthenticationButtonClicked);
        resetAuthenticationButton.onClick.AddListener(OnResetAuthenticationButtonClicked);
        resetAuthenticationButton.interactable = TwitchAuthentication.HasToken();
        authenticationDescriptionText.text = "";
        
        ValidateFields();
    }

    #region Authentication

    /// <summary>
    /// The authentication button was clicked by the user
    /// </summary>
    private void OnAuthenticationButtonClicked()
    {
        authenticationDescriptionText.text = "<color=\"orange\">Authentication ongoing...";
        TwitchAuthentication.ConnectionInformation infos = new(twitchClientIdText.text, GetScopes());
        TwitchAuthentication.StartAuthenticationValidation(this, infos);
    }

    /// <summary>
    /// The authentication returned with a result
    /// </summary>
    /// <param name="success">True if authentication was successful</param>
    private void OnAuthenticated(bool success)
    {
        if (success)
        {
            authenticationDescriptionText.text = "<color=\"green\">Authentication successful!";
        }
        else
        {
            authenticationDescriptionText.text = "<color=\"red\">Authentication failed!";
        }
        
        resetAuthenticationButton.interactable = success;
        ValidateFields();
    }

    /// <summary>
    /// The reset authentication button was clicked by the user
    /// </summary>
    private void OnResetAuthenticationButtonClicked()
    {
        authenticationDescriptionText.text = "<color=\"orange\">You cleared the token and need to authenticate again.";
        resetAuthenticationButton.interactable = false;
        TwitchAuthentication.Reset();
        ValidateFields();
    }

    #endregion

    #region Helpers
    
    /// <summary>
    /// Update is called every frame if the MonoBehaviour is enabled
    /// </summary>
    private void Update()
    {
        // Tab through formular
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (EventSystem.current.currentSelectedGameObject == null || EventSystem.current.currentSelectedGameObject == twitchClientIdText.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(channelNameText.gameObject, new BaseEventData(EventSystem.current));
            }
            else if (EventSystem.current.currentSelectedGameObject == channelNameText.gameObject)
            {
                EventSystem.current.SetSelectedGameObject(twitchClientIdText.gameObject, new BaseEventData(EventSystem.current));
            }
        }
    }

    /// <summary>
    /// Validate the UI elements and their interactivity
    /// </summary>
    private void ValidateFields(string value = "")
    {
        authenticateButton.interactable = !TwitchAuthentication.IsAuthenticated() && !string.IsNullOrEmpty(channelNameText.text) && !string.IsNullOrEmpty(twitchClientIdText.text) && AreScopesValid();
        
        // Channel name or Client ID input is missing
        if (!TwitchAuthentication.IsAuthenticated() && (string.IsNullOrEmpty(channelNameText.text) || string.IsNullOrEmpty(twitchClientIdText.text)))
        {
            authenticationDescriptionText.text = "<color=\"orange\">Please enter channel name, client ID and at least one scope!";
        }
        else
        {
            authenticationDescriptionText.text = "Click 'Authenticate' to start authentication!";
        }
    }

    /// <summary>
    /// If a scope toggle changed, re-validate all input
    /// </summary>
    private void ValidateScopes(bool value = true)
    {
        ValidateFields();
    }

    /// <summary>
    /// Get if the scope selection is valid (it needs to be at least one selected)
    /// </summary>
    /// <returns>True if the selection is valid</returns>
    private bool AreScopesValid()
    {
        return scopeManageRedemptionsToggle.isOn || scopeReadChatToggle.isOn || scopeReadSubscriptionsToggle.isOn;
    }

    /// <summary>
    /// Get the currently selected scopes as list
    /// </summary>
    /// <returns>The currently selected scopes as list</returns>
    private List<string> GetScopes()
    {
        List<string> scopes = new List<string>();
        if (scopeManageRedemptionsToggle.isOn) scopes.Add(TwitchAuthentication.ConnectionInformation.CHANNEL_MANAGE_REDEMPTIONS);
        if (scopeReadChatToggle.isOn) scopes.Add(TwitchAuthentication.ConnectionInformation.CHAT_READ);
        if (scopeReadSubscriptionsToggle.isOn) scopes.Add(TwitchAuthentication.ConnectionInformation.USER_READ_SUBSCRIPTIONS);
        return scopes;
    }

    #endregion
}