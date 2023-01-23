using System.Text.Json;
using Auto.Messages;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using JsonSerializer = System.Text.Json.JsonSerializer;



public class Program
{
    const string SIGNALR_HUB_URL = "http://localhost:7033/hub";
    private static HubConnection hub;

    static async Task Main(string[] args)
    {
        hub = new HubConnectionBuilder().WithUrl(SIGNALR_HUB_URL).Build();
        await hub.StartAsync();
        Console.WriteLine("Hub started!");
        Console.WriteLine("Press any key...");
        Console.WriteLine("Connected to bus! Listening newOwnerMessage");
        var amqp = "amqp://user:rabbitmq@localhost:7888";
        using var bus = RabbitHutch.CreateBus(amqp);
        await bus.PubSub.SubscribeAsync<NewOwnerPhoneMessage>("HubNotifier", HandleNewOwnerMessage);
        Console.ReadLine();
       
    }
    private static async Task HandleNewOwnerMessage(NewOwnerPhoneMessage message)
    {
        var csvRow = $"{message.PhoneCode} {message.NumberPhone} : {message.FirstName}  {message.LastName},  {message.Email}, {message.ListedAtUtc}";

        Console.WriteLine(csvRow);

        var json = JsonSerializer.Serialize(message, JsonSettings());

        await hub.SendAsync("NotifyWebUsers", "Auto.SignalRClient", json);
    }

 

    static JsonSerializerOptions JsonSettings() =>
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
}