// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.ContactAddress.Document
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.Extensions.ContactAddress;

/// <exclude />
public class Document : PXMappedCacheExtension
{
  public virtual int? ContactID { get; set; }

  public virtual int? DocumentContactID { get; set; }

  public virtual int? DocumentAddressID { get; set; }

  public virtual int? ShipContactID { get; set; }

  public virtual int? ShipAddressID { get; set; }

  public virtual int? LocationID { get; set; }

  public virtual int? BAccountID { get; set; }

  public virtual bool? AllowOverrideContactAddress { get; set; }

  public virtual bool? AllowOverrideShippingContactAddress { get; set; }

  public virtual int? ProjectID { get; set; }

  public abstract class contactID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  Document.contactID>
  {
  }

  public abstract class documentContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.documentContactID>
  {
  }

  public abstract class documentAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.documentAddressID>
  {
  }

  public abstract class shipContactID : IBqlField, IBqlOperand
  {
  }

  public abstract class shipAddressID : IBqlField, IBqlOperand
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.locationID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.bAccountID>
  {
  }

  public abstract class allowOverrideContactAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Document.allowOverrideContactAddress>
  {
  }

  public abstract class allowOverrideShippingContactAddress : IBqlField, IBqlOperand
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.projectID>
  {
  }
}
