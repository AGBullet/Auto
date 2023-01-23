using System.Collections.Generic;
using System.Linq;
using Auto.Data;
using Auto.Data.Entities;
using Auto.Website.GraphQL.GraphTypes;
using GraphQL;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Queries;

public class VehicleQuery : ObjectGraphType
{
    private readonly IAutoDatabase _db;

    public VehicleQuery(IAutoDatabase db)
    {
        _db = db;
        Field<VehicleGraphType>("vehicle").Argument<string>("code").Resolve(GetVehicle);
        Field<ListGraphType<VehicleGraphType>>("vehicles").Argument<int>("count").Resolve(Vehicles);

    }

    private IEnumerable<Vehicle> Vehicles(IResolveFieldContext<object> arg)
    {
        var count = arg.GetArgument<int>("count");
        if (count==default)
        {
            count = 10;
            
        } 
        return _db.ListVehicles().Take(count);
    }

    private Vehicle GetVehicle(IResolveFieldContext<object> arg)
    {
        return _db.FindVehicle(arg.GetArgument<string>("code"));
    }
}