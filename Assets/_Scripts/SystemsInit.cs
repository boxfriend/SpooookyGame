using UnityEngine;
using Serilog;
using Serilog.Sinks.Unity3D;
using System.IO;
using UnityEditor;
using System;

namespace Boxfriend
{
    internal static class SystemsInit
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)] 
        public static void Init() 
        {
            InitLogger();
#if !UNITY_EDITOR || STEAMTEST
            if(!InitSteam())
            {
                Log.Information("Unable to initialize steam. Shutting down.");
                Application.Quit();
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#endif
            }
#endif
        }

        private static void InitLogger ()
        {
            Log.Logger = new LoggerConfiguration()
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                .MinimumLevel.Verbose()
                .WriteTo.Unity3D()
#else
                .MinimumLevel.Information()
#endif
                .WriteTo.File(Path.Join(Application.dataPath, "Logs", $"{DateTime.Now:yyyy-MM-dd HHmm}.txt"),
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] ({ThreadId}) {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            Application.quitting += () => Log.CloseAndFlush();
            Log.Information("Serilog Initialized. Logging started.");
        }

        private static bool InitSteam ()
        {
            try
            {
                Steamworks.SteamClient.Init(1666460, true);

                Log.Information($"STEAMWORKS: Initialized. Logged in as {Steamworks.SteamClient.Name}");
                Application.quitting += () => Steamworks.SteamClient.Shutdown();
                return true;
            } catch (System.Exception e)
            {
                Log.Fatal($"STEAMWORKS: {e.Message}");
                return false;
            }
        }
    }
}
