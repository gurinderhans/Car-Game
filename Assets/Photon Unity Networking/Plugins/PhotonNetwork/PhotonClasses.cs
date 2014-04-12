// ----------------------------------------------------------------------------
// <copyright file="PhotonClasses.cs" company="Exit Games GmbH">
//   PhotonNetwork Framework for Unity - Copyright (C) 2011 Exit Games GmbH
// </copyright>
// <summary>
//   
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Lite;
using UnityEngine;


/// <summary>Class for constants. Defines photon-event-codes for PUN usage.</summary>
internal class PunEvent
{
    public const byte RPC = 200; 
    public const byte SendSerialize = 201;
    public const byte Instantiation = 202;
    public const byte CloseConnection = 203;
    public const byte Destroy = 204;
    public const byte RemoveCachedRPCs = 205;
    public const byte SendSerializeReliable = 206;  // TS: added this but it's not really needed anymore
    public const byte DestroyPlayer = 207;  // TS: added to make others remove all GOs of a player
    public const byte AssignMaster = 208;  // TS: added to assign someone master client (overriding the current)
    public const byte OwnershipRequest = 209;
    public const byte OwnershipTransfer = 210;
    public const byte VacantViewIds = 211;
}


/// <summary>
/// Options of lobby types available. Lobby types might be implemented in certain Photon versions and won't be available on older servers.
/// </summary>
public enum LobbyType :byte
{
    /// <summary>This lobby is used unless another is defined by game or JoinRandom. Room-lists will be sent and JoinRandomRoom can filter by matching properties.</summary>
    Default = 1,
    /// <summary>This lobby type lists rooms like Default but JoinRandom has a parameter for SQL-like "where" clauses for filtering. This allows bigger, less, or and and combinations.</summary>
    SqlLobby = 2
}


/// <summary>Refers to a specific lobby (and type) on the server.</summary>
public class TypedLobby
{
    public string Name;
    public LobbyType Type;

    public static readonly TypedLobby Default = new TypedLobby();
    public bool IsDefault { get { return this.Type == LobbyType.Default && string.IsNullOrEmpty(this.Name); } }

    public TypedLobby()
    {
        this.Name = string.Empty;
        this.Type = LobbyType.Default;
    }

    public TypedLobby(string name, LobbyType type)
    {
        this.Name = name;
        this.Type = type;
    }

    public override string ToString()
    {
        return string.Format("lobby '{0}'[{1}]", this.Name, this.Type);
    }
}


/// <summary>Aggregates several less-often used options for operation RaiseEvent. See field descriptions for usage details.</summary>
public class RaiseEventOptions
{
    /// <summary>Default options: CachingOption: DoNotCache, InterestGroup: 0, targetActors: null, receivers: Others, sequenceChannel: 0.</summary>
    public readonly static RaiseEventOptions Default = new RaiseEventOptions();

    /// <summary>Defines if the server should simply send the event, put it in the cache or remove events that are like this one.</summary>
    /// <remarks>
    /// When using option: SliceSetIndex, SlicePurgeIndex or SlicePurgeUpToIndex, set a CacheSliceIndex. All other options except SequenceChannel get ignored.
    /// </remarks>
    public EventCaching CachingOption;

    /// <summary>The number of the Interest Group to send this to. 0 goes to all users but to get 1 and up, clients must subscribe to the group first.</summary>
    public byte InterestGroup;

    /// <summary>A list of PhotonPlayer.IDs to send this event to. You can implement events that just go to specific users this way.</summary>
    public int[] TargetActors;

    /// <summary>Sends the event to All, MasterClient or Others (default). Be careful with MasterClient, as the client might disconnect before it got the event and it gets lost.</summary>
    public ReceiverGroup Receivers;

    /// <summary>Events are ordered per "channel". If you have events that are independent of others, they can go into another sequence or channel.</summary>
    public byte SequenceChannel;

    /// <summary>Events can be forwarded to Webhooks, which can evaluate and use the events to follow the game's state.</summary>
    public bool ForwardToWebhook;

    /// <summary>Used along with CachingOption SliceSetIndex, SlicePurgeIndex or SlicePurgeUpToIndex if you want to set or purge a specific cache-slice.</summary>
    public int CacheSliceIndex;
}

/// <summary>Wraps up common room properties needed when you create rooms.</summary>
/// <remarks>This directly maps to what the fields in the Room class.</remarks>
public class RoomOptions
{
    public bool isVisible = true;
    public bool isOpen = true;
    public int maxPlayers;
    
    public bool cleanupCacheOnLeave = PhotonNetwork.autoCleanUpPlayerObjects;
    
    public Hashtable customRoomProperties;
    public string[] customRoomPropertiesForLobby = new string[0];
}

/// <summary>Enum of "target" options for RPCs. These define which remote clients get your RPC call. </summary>
/// \ingroup publicApi
public enum PhotonTargets
{
    /// <summary>Sends the RPC to everyone else and executes it immediately on this client. Player who join later will not execute this RPC.</summary>
    All,
    /// <summary>Sends the RPC to everyone else. This client does not execute the RPC. Player who join later will not execute this RPC.</summary>
    Others,
    /// <summary>Sends the RPC to MasterClient only. Careful: The MasterClient might disconnect before it executes the RPC and that might cause dropped RPCs.</summary>
    MasterClient,
    /// <summary>Sends the RPC to everyone else and executes it immediately on this client. New players get the RPC when they join as it's buffered (until this client leaves).</summary>
    AllBuffered,
    /// <summary>Sends the RPC to everyone. This client does not execute the RPC. New players get the RPC when they join as it's buffered (until this client leaves).</summary>
    OthersBuffered,
    /// <summary>Sends the RPC to everyone (including this client) through the server.</summary>
    /// <remarks>
    /// This client executes the RPC like any other when it received it from the server.
    /// Benefit: The server's order of sending the RPCs is the same on all clients.
    /// </remarks>
    AllViaServer,
    /// <summary>Sends the RPC to everyone (including this client) through the server and buffers it for players joining later.</summary>
    /// <remarks>
    /// This client executes the RPC like any other when it received it from the server.
    /// Benefit: The server's order of sending the RPCs is the same on all clients.
    /// </remarks>
    AllBufferedViaServer
}

/// <summary>Used to define the level of logging output created by the PUN classes. Either log errors, info (some more) or full.</summary>
/// \ingroup publicApi
public enum PhotonLogLevel { ErrorsOnly, Informational, Full }


namespace Photon
{
    /// <summary>
    /// This class adds the property photonView, while logging a warning when your game still uses the networkView.
    /// </summary>
    public class MonoBehaviour : UnityEngine.MonoBehaviour
    {
        public PhotonView photonView
        {
            get
            {
                return PhotonView.Get(this);
            }
        }

        new public PhotonView networkView
        {
            get
            {
                Debug.LogWarning("Why are you still using networkView? should be PhotonView?");
                return PhotonView.Get(this);
            }
        }
    }
}

/// <summary>
/// Container class for info about a particular message, RPC or update.
/// </summary>
/// \ingroup publicApi
public class PhotonMessageInfo
{
    private int timeInt;
    public PhotonPlayer sender;
    public PhotonView photonView;

    /// <summary>
    /// Initializes a new instance of the <see cref="PhotonMessageInfo"/> class. 
    /// To create an empty messageinfo only!
    /// </summary>
    public PhotonMessageInfo()
    {
        this.sender = PhotonNetwork.player;
        this.timeInt = (int)(PhotonNetwork.time * 1000);
        this.photonView = null;
    }

    public PhotonMessageInfo(PhotonPlayer player, int timestamp, PhotonView view)
    {
        this.sender = player;
        this.timeInt = timestamp;
        this.photonView = view;
    }

    public double timestamp
    {
        get { return ((double)(uint)this.timeInt) / 1000.0f; }
    }

    public override string ToString()
    {
        return string.Format("[PhotonMessageInfo: player='{1}' timestamp={0}]", this.timestamp, this.sender);
    }
}

public class PBitStream
{
    List<byte> streamBytes;
    private int currentByte;
    private int totalBits = 0;

    public int ByteCount
    {
        get { return BytesForBits(this.totalBits); }
    }

    public int BitCount
    {
        get { return this.totalBits; }
        private set { this.totalBits = value; }
    }

    public PBitStream()
    {
        this.streamBytes = new List<byte>(1);
    }

    public PBitStream(int bitCount)
    {
        this.streamBytes = new List<byte>(BytesForBits(bitCount));
    }

    public PBitStream(IEnumerable<byte> bytes, int bitCount)
    {
        this.streamBytes = new List<byte>(bytes);
        this.BitCount = bitCount;
    }

    public static int BytesForBits(int bitCount)
    {
        if (bitCount <= 0)
        {
            return 0;
        }

        return ((bitCount - 1) / 8) + 1;
    }

    public void Add(bool val)
    {
        int bytePos = this.totalBits / 8;
        if (bytePos > this.streamBytes.Count-1 || totalBits == 0)
        {
            this.streamBytes.Add(0);
        }

        if (val)
        {
            int currentByteBit = 7 - (this.totalBits % 8);
            this.streamBytes[bytePos] |= (byte)(1 << currentByteBit);
        }

        this.totalBits++;
    }

    public byte[] ToBytes()
    {
        return streamBytes.ToArray();
    }

    public int Position { get; set; }

    public bool GetNext()
    {
        if (this.Position > this.totalBits)
        {
            throw new Exception("End of PBitStream reached. Can't read more.");
        }

        return Get(this.Position++);
    }

    public bool Get(int bitIndex)
    {
        int byteIndex = bitIndex / 8;
        int bitInByIndex = 7 - (bitIndex % 8);
        return ((streamBytes[byteIndex] & (byte)(1 << bitInByIndex)) > 0);
    }

    public void Set(int bitIndex, bool value)
    {
        int byteIndex = bitIndex / 8;
        int bitInByIndex = 7 - (bitIndex % 8);
        this.streamBytes[byteIndex] |= (byte)(1 << bitInByIndex);
    }
}

/// <summary>
/// This container is used in OnPhotonSerializeView() to either provide incoming data of a PhotonView or for you to provide it.
/// </summary>
/// <remarks>
/// The isWriting property will be true if this client is the "owner" of the PhotonView (and thus the GameObject).
/// Add data to the stream and it's sent via the server to the other players in a room.
/// On the receiving side, isWriting is false and the data should be read.
/// 
/// Send as few data as possible to keep connection quality up. An empty PhotonStream will not be sent.
/// 
/// Use either Serialize() for reading and writing or SendNext() and ReceiveNext(). The latter two are just explicit read and 
/// write methods but do about the same work as Serialize(). It's a matter of preference which methods you use.
/// </remarks>
/// <seealso cref="PhotonNetworkingMessage"/>
/// \ingroup publicApi
public class PhotonStream
{
    bool write = false;
    internal List<object> data;
    byte currentItem = 0; //Used to track the next item to receive.

    /// <summary>
    /// Creates a stream and initializes it. Used by PUN internally.
    /// </summary>
    public PhotonStream(bool write, object[] incomingData)
    {
        this.write = write;
        if (incomingData == null)
        {
            this.data = new List<object>();
        }
        else
        {
            this.data = new List<object>(incomingData);
        }
    }
    
    /// <summary>If true, this client should add data to the stream to send it.</summary>
    public bool isWriting
    {
        get { return this.write; }
    }

    /// <summary>If true, this client should read data send by another client.</summary>
    public bool isReading
    {
        get { return !this.write; }
    }

    /// <summary>Count of items in the stream.</summary>
    public int Count
    {
        get
        {
            return data.Count;
        }
    }

    /// <summary>Read next piece of data from the stream when isReading is true.</summary>
    public object ReceiveNext()
    {
        if (this.write)
        {
            Debug.LogError("Error: you cannot read this stream that you are writing!");
            return null;
        }

        object obj = this.data[this.currentItem];
        this.currentItem++;
        return obj;
    }

    /// <summary>Add another piece of data to send it when isWriting is true.</summary>
    public void SendNext(object obj)
    {
        if (!this.write)
        {
            Debug.LogError("Error: you cannot write/send to this stream that you are reading!");
            return;
        }

        this.data.Add(obj);
    }

    /// <summary>Turns the stream into a new object[].</summary>
    public object[] ToArray()
    {
        return this.data.ToArray();
    }

    /// <summary>
    /// Will read or write the value, depending on the stream's isWriting value.
    /// </summary>
    public void Serialize(ref bool myBool)
    {
        if (this.write)
        {
            this.data.Add(myBool);
        }
        else
        {
            if (this.data.Count > currentItem)
            {
                myBool = (bool)data[currentItem];
                this.currentItem++;
            }
        }
    }

    /// <summary>
    /// Will read or write the value, depending on the stream's isWriting value.
    /// </summary>
    public void Serialize(ref int myInt)
    {
        if (write)
        {
            this.data.Add(myInt);
        }
        else
        {
            if (this.data.Count > currentItem)
            {
                myInt = (int)data[currentItem];
                currentItem++;
            }
        }
    }

    /// <summary>
    /// Will read or write the value, depending on the stream's isWriting value.
    /// </summary>
    public void Serialize(ref string value)
    {
        if (write)
        {
            this.data.Add(value);
        }
        else
        {
            if (this.data.Count > currentItem)
            {
                value = (string)data[currentItem];
                currentItem++;
            }
        }
    }

    /// <summary>
    /// Will read or write the value, depending on the stream's isWriting value.
    /// </summary>
    public void Serialize(ref char value)
    {
        if (write)
        {
            this.data.Add(value);
        }
        else
        {
            if (this.data.Count > currentItem)
            {
                value = (char)data[currentItem];
                currentItem++;
            }
        }
    }

    /// <summary>
    /// Will read or write the value, depending on the stream's isWriting value.
    /// </summary>
    public void Serialize(ref short value)
    {
        if (write)
        {
            this.data.Add(value);
        }
        else
        {
            if (this.data.Count > currentItem)
            {
                value = (short)data[currentItem];
                currentItem++;
            }
        }
    }

    /// <summary>
    /// Will read or write the value, depending on the stream's isWriting value.
    /// </summary>
    public void Serialize(ref float obj)
    {
        if (write)
        {
            this.data.Add(obj);
        }
        else
        {
            if (this.data.Count > currentItem)
            {
                obj = (float)data[currentItem];
                currentItem++;
            }
        }
    }

    /// <summary>
    /// Will read or write the value, depending on the stream's isWriting value.
    /// </summary>
    public void Serialize(ref PhotonPlayer obj)
    {
        if (write)
        {
            this.data.Add(obj);
        }
        else
        {
            if (this.data.Count > currentItem)
            {
                obj = (PhotonPlayer)data[currentItem];
                currentItem++;
            }
        }
    }

    /// <summary>
    /// Will read or write the value, depending on the stream's isWriting value.
    /// </summary>
    public void Serialize(ref Vector3 obj)
    {
        if (write)
        {
            this.data.Add(obj);
        }
        else
        {
            if (this.data.Count > currentItem)
            {
                obj = (Vector3)data[currentItem];
                currentItem++;
            }
        }
    }

    /// <summary>
    /// Will read or write the value, depending on the stream's isWriting value.
    /// </summary>
    public void Serialize(ref Vector2 obj)
    {
        if (write)
        {
            this.data.Add(obj);
        }
        else
        {
            if (this.data.Count > currentItem)
            {
                obj = (Vector2)data[currentItem];
                currentItem++;
            }
        }
    }

    /// <summary>
    /// Will read or write the value, depending on the stream's isWriting value.
    /// </summary>
    public void Serialize(ref Quaternion obj)
    {
        if (write)
        {
            this.data.Add(obj);
        }
        else
        {
            if (this.data.Count > currentItem)
            {
                obj = (Quaternion)data[currentItem];
                currentItem++;
            }
        }
    }
}