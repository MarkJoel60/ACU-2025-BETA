// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.MISC1099EFileProcessingInfoRaw
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXPrimaryGraph(typeof (MISC1099EFileProcessing))]
[PXProjection(typeof (Select5<AP1099History, InnerJoin<AP1099Box, On<AP1099Box.boxNbr, Equal<AP1099History.boxNbr>>, InnerJoin<Vendor, On<Vendor.bAccountID, Equal<AP1099History.vendorID>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Vendor.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<Vendor.defContactID>>>, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<Vendor.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<Vendor.defLocationID>>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>, And<PX.Objects.CR.Location.defAddressID, Equal<PX.Objects.CR.Address.addressID>>>, InnerJoin<PX.Objects.GL.Branch, On<AP1099History.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<PX.Objects.GL.Ledger, On<PX.Objects.GL.Branch.ledgerID, Equal<PX.Objects.GL.Ledger.ledgerID>>, LeftJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>>>>>>>, Where<CurrentValue<MISC1099EFileFilter.box7>, Equal<MISC1099EFileFilter.box7.box7All>, Or<CurrentValue<MISC1099EFileFilter.box7>, Equal<MISC1099EFileFilter.box7.box7Equal>, And<AP1099History.boxNbr, Equal<MISC1099EFileFilter.box7.box7Nbr>, Or<CurrentValue<MISC1099EFileFilter.box7>, Equal<MISC1099EFileFilter.box7.box7NotEqual>, And<AP1099History.boxNbr, NotEqual<MISC1099EFileFilter.box7.box7Nbr>>>>>>, Aggregate<GroupBy<Vendor.bAccountID, GroupBy<AP1099History.branchID, GroupBy<AP1099History.finYear, Sum<AP1099History.histAmt>>>>>>), Persistent = false)]
[Serializable]
public class MISC1099EFileProcessingInfoRaw : AP1099History
{
  [PXUIField(DisplayName = "Vendor")]
  [PXDBString(30, IsUnicode = true, InputMask = "", BqlField = typeof (Vendor.acctCD))]
  public virtual 
  #nullable disable
  string VAcctCD { get; set; }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (Vendor.acctName))]
  [PXUIField(DisplayName = "Vendor Name")]
  public virtual string VAcctName { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Location.taxRegistrationID))]
  [PXUIField(DisplayName = "Tax Registration ID")]
  public virtual string LTaxRegistrationID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.GL.DAC.Organization.organizationID))]
  [PXDimensionSelector("COMPANY", typeof (Search<PX.Objects.GL.DAC.Organization.organizationID>), typeof (PX.Objects.GL.DAC.Organization.organizationCD))]
  [PXUIField(DisplayName = "Payer Company")]
  public virtual int? PayerOrganizationID { get; set; }

  [PXDBInt(BqlField = typeof (AP1099History.branchID))]
  [PXDimensionSelector("BRANCH", typeof (Search<PX.Objects.GL.Branch.branchID>), typeof (PX.Objects.GL.Branch.branchCD))]
  [PXUIField(DisplayName = "Payer Branch")]
  public virtual int? PayerBranchID { get; set; }

  [PXInt]
  [PXDimensionSelector("BIZACCT", typeof (Search<PX.Objects.CR.BAccount.bAccountID>), typeof (PX.Objects.CR.BAccount.acctCD))]
  [PXUIField(DisplayName = "Payer")]
  public virtual int? PayerBAccountID { get; set; }

  [PXDBString(100, BqlField = typeof (PX.Objects.CR.Address.countryID))]
  public virtual string CountryID { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Address.state))]
  public virtual string State { get; set; }

  public abstract class vAcctCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileProcessingInfoRaw.vAcctCD>
  {
  }

  public abstract class vAcctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileProcessingInfoRaw.vAcctName>
  {
  }

  public abstract class lTaxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileProcessingInfoRaw.lTaxRegistrationID>
  {
  }

  public abstract class payerOrganizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MISC1099EFileProcessingInfoRaw.payerOrganizationID>
  {
  }

  public abstract class payerBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MISC1099EFileProcessingInfoRaw.payerBranchID>
  {
  }

  public abstract class payerBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MISC1099EFileProcessingInfoRaw.payerBAccountID>
  {
  }

  public abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileProcessingInfoRaw.countryID>
  {
  }

  public abstract class state : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileProcessingInfoRaw.state>
  {
  }

  public new abstract class finYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileProcessingInfoRaw.finYear>
  {
  }

  public new abstract class boxNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MISC1099EFileProcessingInfoRaw.boxNbr>
  {
  }
}
