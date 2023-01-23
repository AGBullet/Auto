using Auto.Data;
using Auto.Data.Entities;
using Auto.Website.GraphQL.GraphTypes;
using GraphQL;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Mutations;

public class OwnerMutation : ObjectGraphType
{
    private readonly IAutoDatabase _db;

    public OwnerMutation(IAutoDatabase db)
    {
        _db = db;

        Field<OwnerGrahType>("createOwner").Argument<InputOwnerGraphType>("owner").Resolve(CreateOwner);
        Field<OwnerGrahType>("updateOwner").Argument<NonNullGraphType<IntGraphType>>("id").Argument<InputOwnerGraphType>("owner").Resolve(UpdateOwner);
        Field<OwnerGrahType>("deleteOwner").Argument<IntGraphType>("id").Resolve(DeleteOwner);
    }

    private Owner DeleteOwner(IResolveFieldContext<object> arg)
    {
        var owner = arg.GetArgument<Owner>("owner");
        var id = _db.CreateOwner(owner);
        return _db.FindOwner(id);
    }

    private Owner UpdateOwner(IResolveFieldContext<object> arg)
    {
        var updateOwner = arg.GetArgument<Owner>("owner");
        var id = arg.GetArgument<int>("id").ToString();
        var owner = _db.FindOwner(id);
        owner.FirstName = updateOwner.FirstName;
        owner.LastName = updateOwner.LastName;
        owner.Email = updateOwner.Email;
        owner.VehicleCode = updateOwner.VehicleCode;
        _db.UpdateOwner(owner);
        return _db.FindOwner(id);

    }

    private Owner CreateOwner(IResolveFieldContext<object> arg)
    {
        var owner = arg.GetArgument<Owner>("owner");
        var id = _db.CreateOwner(owner);
        return _db.FindOwner(id);
    }
    
}