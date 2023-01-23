using Grpc.Core;

namespace Auto.OwnerService.Services;

public class PhoneService : Phoner.PhonerBase
{
    private readonly ILogger<PhoneService> _logger;

    public PhoneService(ILogger<PhoneService> logger)
    {
        this._logger = logger;
    }
    public override Task<PhoneReply> GetPhone(PhoneRequest request, ServerCallContext context)
    {
        return Task.FromResult(new PhoneReply() { Code = "+7 (RUS) ",Phone = "(921) 123 13 12" });
    }
}

