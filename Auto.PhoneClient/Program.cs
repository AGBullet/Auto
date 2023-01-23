using Auto.Messages;
using Auto.OwnerService;

using EasyNetQ;
using Grpc.Net.Client;


class Program
{
    private static Phoner.PhonerClient grpcClient;
    private static IBus bus;
    static async Task Main(string[] args)
    { 
        Console.WriteLine("Starting Auto.PhoneClient"); 
        var amqp = "amqp://user:rabbitmq@localhost:7888";
        bus = RabbitHutch.CreateBus(amqp); 
        Console.WriteLine("Connected to bus; Listening for newOwnerMessages");
        var grpcAddress = "http://localhost:5125";
        using var channel = GrpcChannel.ForAddress(grpcAddress);
        grpcClient = new Phoner.PhonerClient(channel);
        Console.WriteLine($"Connected to gRPC on {grpcAddress}");
        await bus.PubSub.SubscribeAsync<NewOwnerMessage>("PhoneClient", HandleNewOwnerMessage); 
        Console.WriteLine("Press Enter to exit");
        Console.ReadLine();
    }
    private static async Task HandleNewOwnerMessage(NewOwnerMessage message)
    {
        Console.WriteLine($"new owner; {message.Id} {message.VehicleCode}");
        var req = new PhoneRequest()
        {
            FirstName = message.FirstName,
            LastName = message.LastName,
            Email = message.Email,
            VehicleCode = message.VehicleCode
        }; 
        var phoneReply = await grpcClient.GetPhoneAsync(req);
        Console.WriteLine($"Owner {message.VehicleCode} has {phoneReply.Code} {phoneReply.Phone}");
        var newOwnerPhoneMessage = new NewOwnerPhoneMessage(message, phoneReply.Code,phoneReply.Phone);
        await bus.PubSub.PublishAsync(newOwnerPhoneMessage);
        
    }
}

