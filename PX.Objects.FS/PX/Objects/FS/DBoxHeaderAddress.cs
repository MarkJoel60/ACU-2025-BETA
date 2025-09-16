// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DBoxHeaderAddress
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Objects.CR;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.FS;

public class DBoxHeaderAddress
{
  public virtual string AddressLine1 { get; set; }

  public virtual string AddressLine2 { get; set; }

  public virtual string AddressLine3 { get; set; }

  public virtual string City { get; set; }

  public virtual string CountryID { get; set; }

  public virtual string State { get; set; }

  public virtual string PostalCode { get; set; }

  public static implicit operator DBoxHeaderAddress(CRAddress address)
  {
    if (address == null)
      return (DBoxHeaderAddress) null;
    return new DBoxHeaderAddress()
    {
      AddressLine1 = address.AddressLine1,
      AddressLine2 = address.AddressLine2,
      AddressLine3 = address.AddressLine3,
      City = address.City,
      CountryID = address.CountryID,
      State = address.State,
      PostalCode = address.PostalCode
    };
  }

  public static implicit operator DBoxHeaderAddress(PMSiteAddress address)
  {
    if (address == null)
      return (DBoxHeaderAddress) null;
    return new DBoxHeaderAddress()
    {
      AddressLine1 = address.AddressLine1,
      AddressLine2 = address.AddressLine2,
      AddressLine3 = address.AddressLine3,
      City = address.City,
      CountryID = address.CountryID,
      State = address.State,
      PostalCode = address.PostalCode
    };
  }
}
