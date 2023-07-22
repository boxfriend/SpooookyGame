using Steamworks;
using UnityEngine;
using Serilog;

namespace Boxfriend.Steam
{
    public class SteamManager : MonoBehaviour
    {
        public void OpenInviteOverlay()
        {
            if(SteamClient.IsValid)
            {
                try
                {
                    SteamFriends.OpenGameInviteOverlay(SteamClient.SteamId);
                    Log.Verbose("STEAMWORKS: Invite overlay opened");
                }
                catch (System.Exception e)
                {
                    Log.Fatal($"STEAMWORKS: {e.Message}");
                }
            }
            else
            {
                Log.Information("STEAMWORKS: Steam not initialized.");
            }
        }

        public void SetRichPresence(string text)
        {
            if (SteamClient.IsValid)
            {
                try
                {
                    SteamFriends.SetRichPresence("test", text);
                    SteamFriends.SetRichPresence("steam_display", "#StatusFull");
                    Log.Information($"STEAMWORKS: Rich Presence set to {text}.");
                } catch (System.Exception e)
                {
                    Log.Fatal($"STEAMWORKS: {e.Message}");
                }
            } else
            {
                Log.Information("STEAMWORKS: Steam not initialized.");
            }
        }
    }
}
