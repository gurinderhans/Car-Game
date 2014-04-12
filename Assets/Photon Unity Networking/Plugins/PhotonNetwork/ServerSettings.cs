
using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>Currently available cloud regions as enum.</summary>
/// <remarks>Must match order in CloudServerRegionNames and CloudServerRegionPrefixes.</remarks>
public enum CloudServerRegion { EU, US, Asia, Japan };


/// <summary>
/// Collection of connection-relevant settings, used internally by PhotonNetwork.ConnectUsingSettings.
/// </summary>
[Serializable]
public class ServerSettings : ScriptableObject
{
    public const string DefaultCloudServerUrl = "app-eu.exitgamescloud.com";
    public const string DefaultServerAddress = "127.0.0.1";
    public const int DefaultMasterPort = 5055;              // default port for MasterServer
    public const int DefaultNameServerPort = 5058;          // default port for NameServer
    public const string DefaultAppID = "Master";

    // per region name and server-prefix. must match order in CloudServerRegion enum! (see above)
    public static readonly string[] CloudServerRegionPrefixes = new string[] {"app-eu", "app-us", "app-asia", "app-jp"};

    public enum HostingOption { NotSet, PhotonCloud, SelfHosted, OfflineMode }

    public HostingOption HostType = HostingOption.NotSet;

    public string ServerAddress = DefaultServerAddress;     // the address to be used (including region-suffix)
    public int ServerPort = 5055;
    public string AppID = "";
    public bool PingCloudServersOnAwake = false;
    public List<string> RpcList;

    [HideInInspector]
    public bool DisableAutoOpenWizard;

    public string Region { get { return ExtractRegionFromAddress(this.ServerAddress); } }

    public static int FindRegionForServerAddress(string server)
    {
        int result = 0;

        for (int i = 0; i < CloudServerRegionPrefixes.Length; i++)
        {
            if (server.StartsWith(CloudServerRegionPrefixes[i]))
            {
                return i;
            }
        }
        
        return result;
    }

    public static string ExtractRegionFromAddress(string address)
    {
        if (address == null)
        {
            return null;
        }

        int dotIndex = address.IndexOf('.');
        if (dotIndex < 5)
        {
            return null;
        }

        string region = address.Substring(4, dotIndex-4);
        Debug.Log("Extracted region: " + region);
        return region;
    }

    public static string FindServerAddressForRegion(int regionIndex)
    {
        return DefaultCloudServerUrl.Replace("app-eu", CloudServerRegionPrefixes[regionIndex]);
    }

    public static string FindServerAddressForRegion(CloudServerRegion regionIndex)
    {
        return DefaultCloudServerUrl.Replace("app-eu", CloudServerRegionPrefixes[(int)regionIndex]);
    }

    /// <summary>
    /// Tries to convert a region shortcut to a value of CloudServerRegion enum. Defaults to CloudServerRegion.US, if that fails.
    /// </summary>
    /// <returns>If conversion is successful.</returns>
    public static bool TryParseCloudServerRegion(string regionShortcut, out CloudServerRegion region)
    {
        region = CloudServerRegion.US;
        try
        {
            region = (CloudServerRegion)Enum.Parse(typeof(CloudServerRegion), regionShortcut, true);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public void UseCloud(string cloudAppid, int regionIndex)
    {
        this.HostType = HostingOption.PhotonCloud;
        this.AppID = cloudAppid;
        this.ServerAddress = FindServerAddressForRegion(regionIndex);
        this.ServerPort = DefaultMasterPort;
    }

    public void UseMyServer(string serverAddress, int serverPort, string application)
    {
        this.HostType = HostingOption.SelfHosted;
        this.AppID = (application != null) ? application : DefaultAppID;
        this.ServerAddress = serverAddress;
        this.ServerPort = serverPort;
    }

    public override string ToString()
    {
        return "ServerSettings: " + HostType + " " + ServerAddress;
    }
}
