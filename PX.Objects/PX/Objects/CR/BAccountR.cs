// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BAccountR
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXHidden]
[Serializable]
public class BAccountR : BAccount
{
  [PXDimensionSelector("BIZACCT", typeof (Search2<BAccountR.acctCD, LeftJoin<Contact, On<Contact.bAccountID, Equal<BAccountR.bAccountID>, And<Contact.contactID, Equal<BAccountR.defContactID>>>, LeftJoin<Address, On<Address.bAccountID, Equal<BAccountR.bAccountID>, And<Address.addressID, Equal<BAccountR.defAddressID>>>>>, Where2<Where<BAccountR.type, Equal<BAccountType.customerType>, Or<BAccountR.type, Equal<BAccountType.prospectType>, Or<BAccountR.type, Equal<BAccountType.combinedType>, Or<BAccountR.type, Equal<BAccountType.vendorType>>>>>, And<Match<Current<AccessInfo.userName>>>>>), typeof (BAccountR.acctCD), new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.acctName), typeof (BAccountR.type), typeof (BAccountR.classID), typeof (BAccountR.status), typeof (Contact.phone1), typeof (Address.city), typeof (Address.countryID), typeof (Contact.eMail)}, DescriptionField = typeof (BAccountR.acctName))]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public override 
  #nullable disable
  string AcctCD
  {
    get => this._AcctCD;
    set => this._AcctCD = value;
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  [PXSelector(typeof (Search<Contact.contactID>))]
  [PXUIField]
  public override int? DefContactID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "View In CRM")]
  public new virtual bool? ViewInCrm { get; set; }

  /// <summary>
  /// The flag is used to identify whether a BAccount is associated with a Customer (AR).
  /// It is set to true within the CustomerAttribute
  /// and utilized in the PrimaryGraphList logic of BAccount to determine the appropriate navigation behavior.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? ViewInAr { get; set; }

  /// <summary>
  /// The flag is used to identify whether a BAccount is associated with a Vendor (AP).
  /// It is set to true within the VendorAttribute
  /// and utilized in the PrimaryGraphList logic of BAccount to determine the appropriate navigation behavior.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? ViewInAp { get; set; }

  public new class PK : PrimaryKeyOf<BAccount>.By<BAccountR.bAccountID>
  {
    public static BAccount Find(PXGraph graph, int bAccountID, PKFindOptions options = 0)
    {
      return (BAccount) PrimaryKeyOf<BAccount>.By<BAccountR.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public new class UK : PrimaryKeyOf<BAccount>.By<BAccountR.acctCD>
  {
    public static BAccount Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (BAccount) PrimaryKeyOf<BAccount>.By<BAccountR.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new static class FK
  {
    public class ParentBAccount : 
      PrimaryKeyOf<BAccount>.By<BAccountR.bAccountID>.ForeignKeyOf<BAccount>.By<BAccountR.parentBAccountID>
    {
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountR.bAccountID>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountR.acctName>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BAccountR.noteID>
  {
  }

  public new abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountR.defLocationID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountR.type>
  {
  }

  public new abstract class isCustomerOrCombined : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BAccountR.isCustomerOrCombined>
  {
  }

  public new abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccountR.parentBAccountID>
  {
  }

  public new abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountR.defAddressID>
  {
  }

  public new abstract class cOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountR.cOrgBAccountID>
  {
  }

  public new abstract class vOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountR.vOrgBAccountID>
  {
  }

  public new abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountR.baseCuryID>
  {
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountR.classID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountR.acctCD>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountR.defContactID>
  {
  }

  public new abstract class viewInCrm : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccountR.viewInCrm>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountR.status>
  {
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.Active\" instead")]
    public const string Active = "A";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.Hold\" instead")]
    public const string Hold = "H";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.Inactive\" instead")]
    public const string Inactive = "I";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.OneTime\" instead")]
    public const string OneTime = "T";
    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.CreditHold\" instead")]
    public const string CreditHold = "C";

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.ListAttribute\" instead")]
    public class ListAttribute : CustomerStatus.BusinessAccountNonCustomerListAttribute
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.active\" instead")]
    public class active : CustomerStatus.active
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.hold\" instead")]
    public class hold : CustomerStatus.hold
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.inactive\" instead")]
    public class inactive : CustomerStatus.inactive
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.oneTime\" instead")]
    public class oneTime : CustomerStatus.oneTime
    {
    }

    [Obsolete("Will be removed in Acumatica 2021 R2. Use \"CustomerStatus.creditHold\" instead")]
    public class creditHold : CustomerStatus.creditHold
    {
    }
  }

  public new abstract class vStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountR.vStatus>
  {
  }

  public abstract class viewInAr : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccountR.viewInAr>
  {
  }

  public abstract class viewInAp : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccountR.viewInAp>
  {
  }
}
