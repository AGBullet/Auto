using Auto.Data.Entities;
using GraphQL.Types;

namespace Auto.Website.GraphQL.GraphTypes;

public class VehicleGraphType :  ObjectGraphType<Vehicle>
{
    public VehicleGraphType()
    {
        Name = "vehicle";
        Field(c => c.VehicleModel, nullable: false, typeof(ModelGraphType));
        Field(c => c.Owner, nullable: false, typeof(OwnerGrahType));
        Field(c => c.Registration);
        Field(c => c.Color);
        Field(c => c.Year);


    }
}