using GETTER.Domain.Common;

namespace GETTER.Domain.Organizations;

public static class OrganizationErrors
{
    public static readonly Error InvalidName =
        new("Organization.InvalidName", "Organization name must be between 3 and 255 characters long.");
}
