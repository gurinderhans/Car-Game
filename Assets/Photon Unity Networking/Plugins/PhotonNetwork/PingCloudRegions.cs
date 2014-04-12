#if !(UNITY_WINRT || UNITY_WP8 || UNITY_PS3 || UNITY_WIIU)

using System;
using System.Collections;
using System.Net;
using UnityEngine;

/// <summary>
/// This script is automatically added to the PhotonHandler gameobject by PUN.
/// It will auto-ping the ExitGames cloud regions via Awake.
/// This is done only once per client and the result is saved in PlayerPrefs. 
/// Use PhotonNetwork.ConnectToBestCloudServer(gameVersion) to connect to cloud region with best ping.
/// </summary>
public class PingCloudRegions : MonoBehaviour
{
    public static string ClosestRegion;
    public static bool ClosestRegionAvailable { get { return !string.IsNullOrEmpty(ClosestRegion); } }
    
    public static PingCloudRegions Instance;

    private bool isPinging = false;
    private int lowestRegionAverage = -1;
    private const string PlayerPrefsKey = "PUNCloudBestRegion";

    public void Awake()
    {
        Instance = this;

        // load settings and ping only if none available AND if we should ping on Awake
        bool loadedRegion = LoadRegion(out ClosestRegion);  // this sets ClosestRegion. no need to set it on success!
        if (!loadedRegion && PhotonNetwork.PhotonServerSettings.PingCloudServersOnAwake)
        {
            RefreshCloudServerRating();
        }
    }

    public static void RefreshCloudServerRating()
    {
        if (Instance != null)
        {
            if (Instance.isPinging)
            {
                Debug.Log("RefreshCloudServerRating already in process. Skipping this call.");
                return;
            }

            Instance.StartCoroutine(Instance.PingAllRegions());
        }
    }

    // Ping all PUN cloud regions (unless offline mode)
    private IEnumerator PingAllRegions()
    {
        ServerSettings settings = (ServerSettings)Resources.Load(PhotonNetwork.serverSettingsAssetFile, typeof(ServerSettings));
        if (settings.HostType == ServerSettings.HostingOption.OfflineMode)
            yield break;

        ClosestRegion = null;
        isPinging = true;
        lowestRegionAverage = -1;
        foreach (CloudServerRegion region in System.Enum.GetValues(typeof(CloudServerRegion)))
        {
            yield return StartCoroutine(PingRegion(region));
        }
        isPinging = false;
    }

    /// <summary>
    /// Pings a server several times to get an average RTT. If that's lower than the current best, the region becomes closestRegion.
    /// </summary>
    /// <param name="region"></param>
    /// <returns></returns>
    IEnumerator PingRegion(CloudServerRegion region)
    {
        string hostname = ServerSettings.FindServerAddressForRegion(region);
        string regionIp = ResolveHost(hostname);

        if (string.IsNullOrEmpty(regionIp))
        { 
            Debug.LogError("Could not resolve host: " + hostname);
            yield break;
        }

        int pingSum = 0;
        int tries = 3;
        int skipped = 0;
        float timeout = 0.700f; // 700 milliseconds is our max, after this we assume a timeout.
        for (int i = 0; i < tries; i++)
        {
            float startTime = Time.time;
            Ping ping = new Ping(regionIp);
            while (!ping.isDone && Time.time < startTime + timeout)
            { 
                // sometimes Unity ping never returns, so we use a timeout here
                yield return 0;
            }
            if (ping.time == -1)
            {
                if (skipped > 5)
                {
                    pingSum += (int)(timeout * 1000) * tries;
                    break;
                }
                else
                {
                    i -= 1; //Sometimes Unity ping doesnt return, we therefor retry a few times..
                    skipped++;
                    continue;
                }
            }

            pingSum += ping.time;
        }

        int pingAverage = pingSum / tries;
        //Debug.LogWarning (hostname + ": " + regionAverage + "ms");
        if (pingAverage < lowestRegionAverage || lowestRegionAverage == -1)
        {
            lowestRegionAverage = pingAverage;
            SaveAndSetRegion(region.ToString());
        }
    }

    public static void ConnectToBestRegion()
    {
        Instance.StartCoroutine(Instance.ConnectToBestRegionInternal());
    }

    IEnumerator ConnectToBestRegionInternal()
    {
        CloudServerRegion bestRegion;
        if (!ClosestRegionAvailable || !ServerSettings.TryParseCloudServerRegion(ClosestRegion, out bestRegion))
        {
            RefreshCloudServerRating();
        }

        while (isPinging)
        {
            yield return 0; // wait until pinging finished (offline mode won't ping)
        }

        ServerSettings.TryParseCloudServerRegion(ClosestRegion, out bestRegion);
        string bestServerAddress = ServerSettings.FindServerAddressForRegion(bestRegion);
        string bestServerFullAddress = bestServerAddress + ":" + ServerSettings.DefaultMasterPort;
        PhotonNetwork.networkingPeer.Connect(bestServerFullAddress, ServerConnection.MasterServer);
    }

    /// <summary>
    /// Loads the region shortcut from PlayerPrefs or initializes the region as empty string.
    /// </summary>
    /// <returns>True of any player pref was set and loaded.</returns>
    private static bool LoadRegion(out string region)
    {
        region = PlayerPrefs.GetString(PlayerPrefsKey, "");
        return !string.IsNullOrEmpty(region);
    }

    /// <summary>Sets ClosestRegion and saves it in a PlayerPref setting.</summary>
    private static void SaveAndSetRegion(string region)
    {
        ClosestRegion = region;
        PlayerPrefs.SetString(PlayerPrefsKey, region);
    }

    /// <summary>Removes the PlayerPref setting for "best region".</summary>
    public static void DeleteRegionPref()
    {
        if (Instance != null && Instance.isPinging)
        {
            Debug.LogWarning("DeleteRegionPref can't delete region while pining is going on.");
            return;
        }

        ClosestRegion = null;
        PlayerPrefs.DeleteKey(PlayerPrefsKey);
    }

    public static void OverrideRegion(CloudServerRegion region)
    {
        SaveAndSetRegion(region.ToString());
    }

    /// <summary>
    /// Attempts to resolve a hostname into an IP string or returns empty string if that fails.
    /// </summary>
    /// <param name="hostName">Hostname to resolve.</param>
    /// <returns>IP string or empty string if resolution fails</returns>
    public static string ResolveHost(string hostName)
    {
        try
        {
            IPAddress[] address = Dns.GetHostAddresses(hostName);
            if (address.Length == 1) 
            {
                return address[0].ToString();
            }

            // if we got more addresses, try to pick a IPv4 one
            for (int index = 0; index < address.Length; index++)
            {
                IPAddress ipAddress = address[index];
                if (ipAddress != null)
                {
                    string ipString = ipAddress.ToString();
                    if (ipString.IndexOf('.') >= 0)
                    {
                        return ipString;
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Exception caught! " + e.Source + " Message: " + e.Message);
        }

        return string.Empty;
    }
}
#endif
