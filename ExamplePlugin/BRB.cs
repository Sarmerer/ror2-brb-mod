using BepInEx;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using UnityEngine;
using UnityEngine.UI;

namespace BRB
{

    [BepInDependency(R2API.Networking.NetworkingAPI.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class BRB : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "sarmerer";
        public const string PluginName = "BRB";
        public const string PluginVersion = "1.0.0";

        private Text pauseNotificationText;

        public void Awake()
        {
            Log.Init(Logger);

            NetworkingAPI.RegisterMessageType<SyncPause>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && NetworkUser.readOnlyLocalPlayersList.Count > 0)
            {
                bool newPauseState = RoR2.PauseManager.isPaused;
                Log.Info($"Sending pause state: {newPauseState}");

                new SyncPause(newPauseState).Send(NetworkDestination.Clients);
            }
        }

        public static void SetGamePauseState(bool isPaused)
        {
            if (RoR2.PauseManager.isPaused == isPaused)
            {
                return;
            }

            RoR2.PauseManager.CCTogglePause(new()
            {
                userArgs = ["pause"],
                sender = FindObjectOfType<NetworkUser>(),
                localUserSender = LocalUserManager.GetFirstLocalUser(),
                commandName = "pause"
            });

            Time.timeScale = isPaused ? 0f : 1f;
        }
    }
}