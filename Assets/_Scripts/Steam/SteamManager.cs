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
    }
}
