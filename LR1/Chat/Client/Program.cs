using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

List<Client> clients;
IPAddress localAddress = IPAddress.Parse("127.0.0.1");
int localPort = 8080;
int remotePort = 8888;

string path = @"C:\\ITandDDP\LR1\Chat\user.json";
string username;
bool exist = false;
Console.Write("Enter your name: ");
username = Console.ReadLine();

if (JsonConvert.DeserializeObject<List<Client>>(File.ReadAllText(path)) == null)
{
    clients = new List<Client>();
}
else
{
    clients = JsonConvert.DeserializeObject<List<Client>>(File.ReadAllText(path));
}

exist = clients.Any(c => c.Name == username);
if (exist)
{
    Console.WriteLine(clients.First(c => c.Name == username).History);
}
else
{
    clients.Add(new Client(username));
    File.WriteAllText(path, JsonConvert.SerializeObject(clients));
}


Task.Run(ReceiveMessageAsync);
await SendMessageAsync();

async Task SendMessageAsync()
{
    using Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    Console.WriteLine("Enter your message");

    while (true)
    {
        var message = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(message))
        {
            break;
        }
        RefreshFile(message);
        message = $"{username}: {message}";
        
        byte[] data = Encoding.UTF8.GetBytes(message);
        await sender.SendToAsync(data, new IPEndPoint(localAddress, remotePort));
    }
}

async Task ReceiveMessageAsync()
{
    byte[] data = new byte[65535];
    using Socket receiver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    receiver.Bind(new IPEndPoint(localAddress, localPort));
    while (true)
    {
        var result = await receiver.ReceiveFromAsync(data, new IPEndPoint(IPAddress.Any, 0));
        var message = Encoding.UTF8.GetString(data, 0, result.ReceivedBytes);

        RefreshFile(message);
        Console.WriteLine(message);
    }
}

void RefreshFile(string text)
{
    clients = JsonConvert.DeserializeObject<List<Client>>(File.ReadAllText(path));
    foreach (Client c in clients)
    {
        if (c.Name == username)
        {
            exist = true;
            c.History += text + "\n";

            break;
        }
    }
    File.WriteAllText(path, JsonConvert.SerializeObject(clients));
}

class Client
{
    public string Name { get; set; }
    public string History { get; set; } = "";
    public Client(string name)
    {
        Name = name;
    }
}