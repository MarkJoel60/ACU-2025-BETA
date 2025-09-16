// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// A CA transaction detail for the bank transaction for which a CA document will be created.
/// </summary>
[PXCacheName("CA Bank Transaction Detail")]
[Serializable]
public class CABankTranDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ICATranDetail
{
  protected int? _CostCodeID;

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (CABankTran.tranID))]
  [PXParent(typeof (Select<CABankTran, Where<CABankTran.tranType, Equal<Current<CABankTranDetail.bankTranType>>, And<CABankTran.tranID, Equal<Current<CABankTranDetail.bankTranID>>>>>))]
  [PXUIField(Visible = false)]
  public virtual int? BankTranID { get; set; }

  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (CABankTran.tranType))]
  [PXUIField(DisplayName = "Type", Visible = false)]
  public virtual 
  #nullable disable
  string BankTranType { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (CABankTran.lineCntrCA))]
  public virtual int? LineNbr { get; set; }

  [Account(typeof (CABankTranDetail.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<Where<PX.Objects.GL.Account.curyID, Equal<Current<CABankTran.curyID>>, Or<PX.Objects.GL.Account.curyID, IsNull>>>>>))]
  public virtual int? AccountID { get; set; }

  [SubAccount(typeof (CABankTranDetail.accountID), typeof (CABankTranDetail.branchID), false)]
  public virtual int? SubID { get; set; }

  [PXRestrictor(typeof (Where<CashAccount.branchID, Equal<Current<CABankTranDetail.branchID>>>), "This Cash Account does not match selected Branch", new System.Type[] {})]
  [PXRestrictor(typeof (Where<CashAccount.curyID, Equal<Current<CABankTran.curyID>>>), "Offset account must be a Cash Account in the same currency as current Cash Account", new System.Type[] {})]
  [CashAccountScalar]
  [PXDBScalar(typeof (Search<CashAccount.cashAccountID, Where<CashAccount.accountID, Equal<CABankTranDetail.accountID>, And<CashAccount.subID, Equal<CABankTranDetail.subID>, And<CashAccount.branchID, Equal<CABankTranDetail.branchID>>>>>))]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<CABankTranDetail.inventoryID>>>>))]
  public virtual string TaxCategoryID { get; set; }

  [PXDBInt]
  public virtual int? ReferenceID { get; set; }

  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (CABankTran.curyInfoID), PopulateParentCuryInfoID = true)]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (CABankTranDetail.curyInfoID), typeof (CABankTranDetail.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? CuryTranAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tran. Amount")]
  public virtual Decimal? TranAmt { get; set; }

  [PXCurrency(typeof (CABankTranDetail.curyInfoID), typeof (CABankTranDetail.taxableAmt), BaseCalc = false)]
  [PXDBScalar(typeof (Search2<CATax.curyTaxableAmt, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CATax.taxID>>>, Where<CATax.adjTranType, Equal<CABankTranDetail.bankTranType>, And<CATax.adjRefNbr, Equal<CABankTranDetail.bankTranID>, And<CATax.lineNbr, Equal<CABankTranDetail.lineNbr>, And<PX.Objects.TX.Tax.taxCalcLevel, Equal<CSTaxCalcLevel.inclusive>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>>>>>>, OrderBy<Asc<CATax.taxID>>>))]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  [PXBaseCury]
  [PXDBScalar(typeof (Search2<CATax.taxableAmt, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CATax.taxID>>>, Where<CATax.adjTranType, Equal<CABankTranDetail.bankTranType>, And<CATax.adjRefNbr, Equal<CABankTranDetail.bankTranID>, And<CATax.lineNbr, Equal<CABankTranDetail.lineNbr>, And<PX.Objects.TX.Tax.taxCalcLevel, Equal<CSTaxCalcLevel.inclusive>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>>>>>>, OrderBy<Asc<CATax.taxID>>>))]
  public virtual Decimal? TaxableAmt { get; set; }

  /// <summary>
  /// The amount of tax (VAT) associated with the line in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CABankTranDetail.curyInfoID), typeof (CABankTranDetail.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxAmt { get; set; }

  /// <summary>
  /// The amount of tax (VAT) associated with the line in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmt { get; set; }

  [NonStockItem]
  [PXUIField]
  [PXForeignReference(typeof (Field<CABankTranDetail.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitPrice { get; set; }

  [PXDBCurrencyPriceCost(typeof (CABankTranDetail.curyInfoID), typeof (CASplit.unitPrice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Price")]
  public virtual Decimal? CuryUnitPrice { get; set; }

  [ProjectDefault("CA")]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInCA, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase]
  public virtual int? ProjectID { get; set; }

  [ActiveProjectTask(typeof (CABankTranDetail.projectID), "CA")]
  public virtual int? TaskID { get; set; }

  [CostCode(null, typeof (CABankTranDetail.taskID), null)]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? NonBillable { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CABankTranDetail>.By<CABankTranDetail.bankTranID, CABankTranDetail.bankTranType, CABankTranDetail.lineNbr>
  {
    public static CABankTranDetail Find(
      PXGraph graph,
      int? bankTranID,
      string bankTranType,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (CABankTranDetail) PrimaryKeyOf<CABankTranDetail>.By<CABankTranDetail.bankTranID, CABankTranDetail.bankTranType, CABankTranDetail.lineNbr>.FindBy(graph, (object) bankTranID, (object) bankTranType, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.branchID>
    {
    }

    public class BankTransaction : 
      PrimaryKeyOf<CABankTran>.By<CABankTran.tranID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.bankTranID>
    {
    }

    public class OffsetAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.subID>
    {
    }

    public class OffsetCashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.cashAccountID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.taxCategoryID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.curyInfoID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.inventoryID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.projectID, CABankTranDetail.taskID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.costCodeID>
    {
    }

    public class BusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<CABankTranDetail>.By<CABankTranDetail.referenceID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDetail.branchID>
  {
  }

  public abstract class bankTranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDetail.bankTranID>
  {
  }

  public abstract class bankTranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranDetail.bankTranType>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDetail.lineNbr>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDetail.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDetail.subID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDetail.cashAccountID>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranDetail.taxCategoryID>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDetail.referenceID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranDetail.tranDesc>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CABankTranDetail.curyInfoID>
  {
  }

  public abstract class curyTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranDetail.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranDetail.tranAmt>
  {
  }

  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranDetail.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranDetail.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranDetail.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranDetail.taxAmt>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDetail.inventoryID>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranDetail.qty>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranDetail.unitPrice>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranDetail.curyUnitPrice>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDetail.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDetail.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranDetail.costCodeID>
  {
  }

  public abstract class nonBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranDetail.nonBillable>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankTranDetail.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankTranDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankTranDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranDetail.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankTranDetail.Tstamp>
  {
  }
}
