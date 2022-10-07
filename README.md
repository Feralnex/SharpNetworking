<h1 align="center">Notice</h1>
SharpNetworking is still under development, not fully tested and currently is intended as only an example of how you can design server in C# or as a basis for creating your own.

# SharpNetworking
SharpNetworking is a C# networking library. It can be used in .NET environments.

Server provides functionality for establishing connections and sending data back and forth, leaving control over sending and receiving packets to you.

## Installation
Grab the SharpNetworking.dll file from the [latest release](https://github.com/Feralnex/SharpNetworking/releases/latest) and simply add a reference inside your project.

## Getting Started
The server life cycle consists of seven stages:
- `Idle` - server was just created
- `Initializing` - Initialize method was called
- `Initialized` - the Initialize method was successful
- `Launching` - Launch method was called
- `Launched` - the Launch method was successful
- `Terminating` - Terminate method was called
- `Terminated` - the Terminate method was successful

Core methods:
- `Initialize` - creates new TcpListener and UdpListener each time when invoked, creates IdPool and Clients instances only when invoked the first time. Can be invoked only when server status equals Idle or Terminated
- `Launch` - starts TcpListener and UdpListener that listen on their corresponding ports. Can be invoked only when server status equals Initialized
- `Terminate` -  stops TcpListener and UdpListener and disconnects all clients. Can be invoked only when server status equals Launched

### Initial Setup
You can subscribe to the following events:
- Server
  - `NewMessage` - invoked when the server broadcasts a new message (about internal things that happen inside server)
  - `StatusChanged` - invoked when the server changed its status
  - `Initializing` - invoked when initializing the server (used to run your own logic before changing status to Initialized)
  - `Launching` - invoked when launching the server (used to run your own logic before changing status to Launched)
  - `Terminating` - invoked when terminating the server (used to run your own logic before changing status to Terminated)
  - `PacketReceived` - invoked when packet is received from a client
  - `UdpPacketReceived` - invoked when UDP packet is received from unbound client
  - `Bound` - invoked when the connection with the client has been established and an identifier (_int_) has been assigned to the client
  - `Disconnected` - invoked when a client disconnectsint
  - `FailedToBind` - invoked when failed to get NetworkStream from TcpSocket (connection is dropped)
  - `FailedToSendPacket` - invoked when failed to send packet to client
  - `FailedToSendUdpPacket` - invoked when failed to send UDP packet to unbound client
  - `FailedToReceivePacket` - invoked when failed to receive packet from client
  - `FailedToReceiveUdpPacket` - invoked when failed to receive UDP packet from unbound client
- Client
  - `PacketReceived` - invoked when a packet is received from a server
  - `Connected` - invoked when a connection to the server is established
  - `Bound` - invoked when a connection to the server is established
  - `Disconnected` - invoked when disconnected from the server
  - `FailedToConnect` - invoked when failed to connect to the server
  - `FailedToBind` - invoked when failet to bind Client to Socket
  - `FailedToSend` - invoked when failed to send packet

### Creating and Sending Packets
Packets are created like:
```cs
Request request = new Request(<senderId>, <messageId>);
Response response = new Response(<request>, <senderId>, <messageId>);
```

- `<senderId>` corresponds to the Enum value that identifies the sender
- `<messageId>` corresponds to the Enum value that identifies a given packet
- `<request>` corresponds to the request to which this Response will be sent

To add data to your packet, use:
```cs
request.Write(value);
response.Write(value);
```

`value` can be any of the following types: `byte`, `byte[]`, `bool`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`, `float`, `double`, `string`, `BigInteger`, `Enum`, `DateTime`.

You can always create extension methods to support any custom type.

To send your packet, use one of the following:
```cs
client.Tcp.Send(packet); // Sends TCP packet to client
client.Udp.Send(packet); // Sends UDP packet to client
```

### Handling Packets
To handle packet, simply subscribe OnPacketReceived method to PacketReceived Server event. For example:
```cs
private void OnPacketReceived(Client client, Packet packet)
{
    if (packet is Request) Handle(client, packet as Request);
    else if (packet is Response) Handle(client, packet as Response);
}

private void Handle(Client client, Request request)
{
    int someInt = request.ReadInt();
    bool someBool = request.ReadBool();

    if (request.TryRead(out double someDouble))
    {
        // Do something
    }
    else
    {
        // Do something
    }
}

private void Handle(Client client, Response response)
{
    // Similar to the above
}
```

When handling packet remember that it contains additional information like from who and what packet it is (SenderId and Id, respectively)