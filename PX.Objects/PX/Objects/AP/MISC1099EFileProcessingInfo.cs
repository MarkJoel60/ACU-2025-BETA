// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.MISC1099EFileProcessingInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXPrimaryGraph(typeof (MISC1099EFileProcessing))]
[Serializable]
public class MISC1099EFileProcessingInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXInt(IsKey = true)]
  public virtual int? PayerOrganizationID { get; set; }

  [PXInt(IsKey = true)]
  public virtual int? PayerBranchID { get; set; }

  [BAccount(IsDBField = false)]
  [PXUIField(DisplayName = "Payer")]
  public virtual int? PayerBAccountID { get; set; }

  [PXInt]
  [PXDimensionSelector("COMPANY", typeof (Search<PX.Objects.GL.DAC.Organization.organizationID>), typeof (PX.Objects.GL.DAC.Organization.organizationCD))]
  [PXUIField(DisplayName = "Payer Company")]
  public virtual int? DisplayOrganizationID { get; set; }

  [PXInt]
  [PXDimensionSelector("BRANCH", typeof (Search<PX.Objects.GL.Branch.branchID>), typeof (PX.Objects.GL.Branch.branchCD))]
  [PXUIField(DisplayName = "Payer Branch")]
  public virtual int? DisplayBranchID { get; set; }

  [PXInt(IsKey = true)]
  public virtual int? VendorID { get; set; }

  [PXString(4, IsKey = true, IsFixed = true)]
  public virtual 
  #nullable disable
  string FinYear { get; set; }

  [PXShort(IsKey = true)]
  public virtual short? BoxNbr { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual Decimal? HistAmt { get; set; }

  [PXUIField(DisplayName = "Vendor")]
  [PXString(30, IsUnicode = true, InputMask = "")]
  public virtual string VAcctCD { get; set; }

  [PXString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Vendor Name")]
  public virtual string VAcctName { get; set; }

  [PXString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Registration ID")]
  public virtual string LTaxRegistrationID { get; set; }

  [PXString(100)]
  public virtual string CountryID { get; set; }

  [PXString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (MISC1099EFileFilter.countryID))]
  public virtual string State { get; set; }

  public abstract class selected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.selected>
  {
  }

  public abstract class payerOrganizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.payerOrganizationID>
  {
  }

  public abstract class payerBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.payerBranchID>
  {
  }

  public abstract class payerBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.payerBAccountID>
  {
  }

  public abstract class displayOrganizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.payerOrganizationID>
  {
  }

  public abstract class displayBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.payerBranchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MISC1099EFileProcessingInfo.vendorID>
  {
  }

  public abstract class finYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.finYear>
  {
  }

  public abstract class boxNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  MISC1099EFileProcessingInfo.boxNbr>
  {
  }

  public abstract class histAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.histAmt>
  {
  }

  public abstract class vAcctCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.vAcctCD>
  {
  }

  public abstract class vAcctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.vAcctName>
  {
  }

  public abstract class lTaxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.lTaxRegistrationID>
  {
  }

  public abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MISC1099EFileProcessingInfo.countryID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MISC1099EFileProcessingInfo.state>
  {
  }
}
