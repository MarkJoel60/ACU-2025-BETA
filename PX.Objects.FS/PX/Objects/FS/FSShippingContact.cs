// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSShippingContact
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Field Service Shipping Contact")]
[Serializable]
public class FSShippingContact : FSContact
{
  [PXExtraKey]
  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public override bool? IsDefaultContact
  {
    get => this._IsDefaultContact;
    set => this._IsDefaultContact = value;
  }

  [PXBool]
  [PXUIField]
  public override bool? OverrideContact
  {
    [PXDependsOnFields(new System.Type[] {typeof (FSShippingContact.isDefaultContact)})] get
    {
      return base.OverrideContact;
    }
    set => base.OverrideContact = value;
  }

  public new class PK : PrimaryKeyOf<
  #nullable disable
  FSShippingContact>.By<FSShippingContact.contactID>
  {
    public static FSShippingContact Find(PXGraph graph, int? contactID, PKFindOptions options = 0)
    {
      return (FSShippingContact) PrimaryKeyOf<FSShippingContact>.By<FSShippingContact.contactID>.FindBy(graph, (object) contactID, options);
    }
  }

  public new static class FK
  {
    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<FSShippingContact>.By<FSShippingContact.bAccountID>
    {
    }
  }

  public new abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSShippingContact.contactID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSShippingContact.bAccountID>
  {
  }

  public new abstract class bAccountContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSShippingContact.bAccountContactID>
  {
  }

  public new abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSShippingContact.revisionID>
  {
  }

  public new abstract class isDefaultContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSShippingContact.isDefaultContact>
  {
  }

  public new abstract class overrideContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSShippingContact.overrideContact>
  {
  }
}
