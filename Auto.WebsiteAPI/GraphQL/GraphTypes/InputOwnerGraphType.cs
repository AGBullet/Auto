using Auto.Data.Entities;
using GraphQL.Types;

namespace Auto.Website.GraphQL.GraphTypes;

public class InputOwnerGraphType : InputObjectGraphType<Owner>
{
    public InputOwnerGraphType()
    {
        Name = "InputOwnerType";
        Field<NonNullGraphType<StringGraphType>>("FirstName");
        Field<NonNullGraphType<StringGraphType>>("LastName");
        Field<NonNullGraphType<StringGraphType>>("Email");
        Field<NonNullGraphType<StringGraphType>>("vehicleCode");
    }
    
    
}