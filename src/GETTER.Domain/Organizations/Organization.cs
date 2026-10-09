using GETTER.Domain.Common;
using GETTER.Domain.Common.ValueObjects;

namespace GETTER.Domain.Organizations;

public sealed class Organization
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Nip? Nip { get; private set; }
    public Regon? Regon { get; private set; }
    public Krs? Krs { get; private set; }
    public Bdo? Bdo { get; private set; }
    public Eori? Eori { get; private set; }
    public Phone Phone { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Address Address { get; private set; } = null!;

    private Organization(Guid id, string name, Phone phone, Email email, Address address, Nip? nip, Regon? regon, Krs? krs, Bdo? bdo, Eori? eori)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
        Address = address;
        Nip = nip;
        Regon = regon;
        Krs = krs;
        Bdo = bdo;
        Eori = eori;
    }

    public static Organization Create(string name, Phone phone, Email email, Address address,
    Nip? nip = null, Regon? regon = null, Krs? krs = null, Bdo? bdo = null, Eori? eori = null)
    {

        ArgumentNullException.ThrowIfNull(phone);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(address);

        return new Organization(
            Guid.NewGuid(),
            ValidateName(StringNormalizer.Normalize(name)),
            phone,
            email,
            address,
            nip,
            regon,
            krs,
            bdo,
            eori);
    }
    private static string ValidateName(string? name)
    {
        if (name is null || name.Length is < 3 or > 255)
            throw new DomainException(OrganizationErrors.InvalidName);
        return name;
    }
}


