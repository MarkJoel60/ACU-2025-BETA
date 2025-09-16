// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementHelpers.GeneralInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA.BankStatementHelpers;

[PXHidden]
public class GeneralInvoice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Branch(null, null, true, true, true)]
  public int? BranchID { get; set; }

  [PXUIField]
  [PXString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  public 
  #nullable disable
  string RefNbr { get; set; }

  [PXString(2, IsFixed = true)]
  [PXUIField]
  [BatchModule.FullList]
  public virtual string OrigModule { get; set; }

  [PXString(3, IsKey = true, IsFixed = true)]
  [PXUIField]
  public virtual string DocType { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [APOpenPeriod(typeof (PX.Objects.AP.APRegister.docDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID { get; set; }

  [PXDefault]
  [PXInt]
  [PXSelector(typeof (BAccountR.bAccountID), SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  [PXUIField(DisplayName = "Customer/Vendor", Enabled = false)]
  public virtual int? BAccountID { get; set; }

  [PXInt]
  [PXSelector(typeof (BAccountR.bAccountID), SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  [PXUIField(DisplayName = "Vendor", Enabled = false)]
  public virtual int? VendorBAccountID => this.BAccountID;

  [PXInt]
  [PXSelector(typeof (BAccountR.bAccountID), SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  [PXUIField(DisplayName = "Customer", Enabled = false)]
  public virtual int? CustomerBAccountID => this.BAccountID;

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<GeneralInvoice.bAccountID>>, And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>))]
  public virtual int? LocationID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCury(typeof (GeneralInvoice.curyID))]
  [PXUIField]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXCury(typeof (GeneralInvoice.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryDocBal { get; set; }

  [PXString(1, IsFixed = true)]
  [PXUIField]
  public virtual string Status { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? DueDate { get; set; }

  [PXUIField]
  [PXString(40, IsUnicode = true)]
  public string ExtRefNbr { get; set; }

  [PXUIField]
  [PXString(40, IsUnicode = true)]
  public string APExtRefNbr => this.ExtRefNbr;

  [PXUIField]
  [PXString(40, IsUnicode = true)]
  public string ARExtRefNbr => this.ExtRefNbr;

  [PXString]
  [PXUIField]
  public virtual string Description { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GeneralInvoice.branchID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GeneralInvoice.refNbr>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GeneralInvoice.origModule>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GeneralInvoice.docType>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GeneralInvoice.docDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GeneralInvoice.finPeriodID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GeneralInvoice.bAccountID>
  {
  }

  public abstract class vendorBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GeneralInvoice.vendorBAccountID>
  {
  }

  public abstract class customerBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GeneralInvoice.customerBAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GeneralInvoice.locationID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GeneralInvoice.curyID>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GeneralInvoice.curyOrigDocAmt>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GeneralInvoice.curyDocBal>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GeneralInvoice.status>
  {
  }

  public abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GeneralInvoice.dueDate>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GeneralInvoice.extRefNbr>
  {
  }

  public abstract class apExtRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GeneralInvoice.apExtRefNbr>
  {
  }

  public abstract class arExtRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GeneralInvoice.arExtRefNbr>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GeneralInvoice.description>
  {
  }
}
