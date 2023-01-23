namespace Auto.Messages;

public class NewOwnerMessage
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string VehicleCode { get; set; }
    public DateTime ListedAtUtc { get; set; }
}

public class NewOwnerPhoneMessage : NewOwnerMessage
{
    public string PhoneCode { get; set; }
    public string NumberPhone { get; set; }
    public NewOwnerPhoneMessage() { }

    public NewOwnerPhoneMessage(NewOwnerMessage message, string phoneCode, string numberPhone)
    {
        this.Id = message.Id;
        this.FirstName = message.FirstName;
        this.LastName = message.LastName;
        this.Email = message.Email;
        this.VehicleCode = message.VehicleCode;
        this.ListedAtUtc = message.ListedAtUtc;
        this.PhoneCode = phoneCode;
        this.NumberPhone = numberPhone;
    }
}

