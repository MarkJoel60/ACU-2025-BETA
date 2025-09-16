// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.BAccountSelectorBase
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CR.MassProcess;
using PX.Objects.EP;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.FS;

[CRCacheIndependentPrimaryGraphList(new System.Type[] {typeof (CustomerMaint), typeof (EmployeeMaint), typeof (VendorMaint), typeof (BusinessAccountMaint)}, new System.Type[] {typeof (Select<PX.Objects.AR.Customer, Where<Current<PX.Objects.CR.BAccount.bAccountID>, Less<Zero>, Or<PX.Objects.AR.Customer.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>>), typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>), typeof (Select<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>), typeof (Select<PX.Objects.CR.BAccount, Where2<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.prospectType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.vendorType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>>>, And<Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>, Or<Current<PX.Objects.CR.BAccount.bAccountID>, Less<Zero>>>>>>)})]
[Serializable]
public class BAccountSelectorBase : PX.Objects.CR.BAccount
{
  [PXDimensionSelector("BIZACCT", typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public override 
  #nullable disable
  string AcctCD { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXFieldDescription]
  [PXMassMergableField]
  [PXUIField]
  public override string AcctName { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [EmployeeType.List]
  public override string Type { get; set; }

  public new class PK : PrimaryKeyOf<BAccountSelectorBase>.By<BAccountSelectorBase.acctCD>
  {
    public static BAccountSelectorBase Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (BAccountSelectorBase) PrimaryKeyOf<BAccountSelectorBase>.By<BAccountSelectorBase.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new class UK : PrimaryKeyOf<PX.Objects.CR.BAccount>.By<BAccountSelectorBase.acctCD>
  {
    public static PX.Objects.CR.BAccount Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (PX.Objects.CR.BAccount) PrimaryKeyOf<PX.Objects.CR.BAccount>.By<BAccountSelectorBase.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new static class FK
  {
    public class Class : 
      PrimaryKeyOf<CRCustomerClass>.By<CRCustomerClass.cRCustomerClassID>.ForeignKeyOf<PX.Objects.CR.BAccount>.By<BAccountSelectorBase.classID>
    {
    }

    public class ParentBusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<PX.Objects.CR.BAccount>.By<BAccountSelectorBase.parentBAccountID>
    {
    }

    public class Address : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<PX.Objects.CR.BAccount>.By<BAccountSelectorBase.defAddressID>
    {
    }

    public class ContactInfo : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<PX.Objects.CR.BAccount>.By<BAccountSelectorBase.defContactID>
    {
    }

    public class PrimaryContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<PX.Objects.CR.BAccount>.By<BAccountSelectorBase.primaryContactID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<PX.Objects.CR.BAccount>.By<BAccountSelectorBase.taxZoneID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<PX.Objects.EP.EPEmployee>.By<PX.Objects.EP.EPEmployee.bAccountID>.ForeignKeyOf<PX.Objects.CR.BAccount>.By<BAccountSelectorBase.ownerID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<PX.Objects.CR.BAccount>.By<BAccountSelectorBase.workgroupID>
    {
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountSelectorBase.bAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountSelectorBase.acctCD>
  {
  }

  public new abstract class acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BAccountSelectorBase.acctName>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountSelectorBase.type>
  {
  }

  public new abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountSelectorBase.classID>
  {
  }

  public new abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccountSelectorBase.parentBAccountID>
  {
  }

  public new abstract class defAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccountSelectorBase.defAddressID>
  {
  }

  public new abstract class defContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccountSelectorBase.defContactID>
  {
  }

  public new abstract class primaryContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccountSelectorBase.primaryContactID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountSelectorBase.taxZoneID>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountSelectorBase.ownerID>
  {
  }

  public new abstract class workgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccountSelectorBase.workgroupID>
  {
  }
}
