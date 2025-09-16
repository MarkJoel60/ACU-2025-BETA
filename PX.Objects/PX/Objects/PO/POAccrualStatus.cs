// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("PO Accrual Status")]
[Serializable]
public class POAccrualStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? LineNbr { get; set; }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault]
  [POAccrualType.List]
  public virtual 
  #nullable disable
  string Type { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  public virtual string LineType { get; set; }

  [PXDBString(2, IsFixed = true)]
  [POOrderType.List]
  [PXDefault]
  public virtual string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  public virtual string OrderNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  public virtual int? OrderLineNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [POReceiptType.List]
  [PXDefault]
  public virtual string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  public virtual string ReceiptNbr { get; set; }

  [Vendor]
  [PXDBDefault]
  public virtual int? VendorID { get; set; }

  [BasePayToVendor]
  [PXDefault]
  public virtual int? PayToVendorID { get; set; }

  [AnyInventory]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [SubItem]
  [PXDefault]
  public virtual int? SubItemID { get; set; }

  [Site]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  [Account]
  [PXDefault]
  public virtual int? AcctID { get; set; }

  [SubAccount]
  [PXDefault]
  public virtual int? SubID { get; set; }

  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  public virtual string OrigUOM { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOrigQty { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  public virtual string OrigCuryID { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigCost { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigCost { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigDiscAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDiscAmt { get; set; }

  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  public virtual string ReceivedUOM { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ReceivedQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseReceivedQty { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ReceivedCost { get; set; }

  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  public virtual string BilledUOM { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseBilledQty { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  public virtual string BillCuryID { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBilledAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBilledCost { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledCost { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBilledDiscAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledDiscAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PPVAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ReceivedTaxAdjCost { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBilledTaxAdjCost { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledTaxAdjCost { get; set; }

  [PXDBBool]
  [PXFormula(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualStatus.isAccountAffected, Equal<False>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualStatus.unreleasedReceiptCntr, Equal<int0>>>>, And<BqlOperand<POAccrualStatus.unreleasedPPVAdjCntr, IBqlInt>.IsEqual<int0>>>, And<BqlOperand<POAccrualStatus.unreleasedTaxAdjCntr, IBqlInt>.IsEqual<int0>>>, And<BqlOperand<Add<POAccrualStatus.receivedCost, POAccrualStatus.receivedTaxAdjCost>, IBqlDecimal>.IsEqual<BqlOperand<POAccrualStatus.billedCost, IBqlDecimal>.Add<BqlOperand<POAccrualStatus.pPVAmt, IBqlDecimal>.Add<POAccrualStatus.billedTaxAdjCost>>>>>>.And<BqlOperand<Switch<Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualStatus.receivedQty, IsNotNull>>>, And<BqlOperand<POAccrualStatus.billedQty, IBqlDecimal>.IsNotNull>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualStatus.receivedUOM, IsNull>>>>.And<BqlOperand<POAccrualStatus.billedUOM, IBqlString>.IsNull>>>>.Or<BqlOperand<POAccrualStatus.receivedUOM, IBqlString>.IsEqual<POAccrualStatus.billedUOM>>>>, BqlOperand<True, IBqlBool>.When<BqlOperand<POAccrualStatus.receivedQty, IBqlDecimal>.IsEqual<POAccrualStatus.billedQty>>.Else<False>>, BqlOperand<True, IBqlBool>.When<BqlOperand<POAccrualStatus.baseReceivedQty, IBqlDecimal>.IsEqual<POAccrualStatus.baseBilledQty>>.Else<False>>, IBqlBool>.IsEqual<True>>>))]
  public virtual bool? IsClosed { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the accrual account is affected
  /// by this <see cref="T:PX.Objects.PO.POAccrualStatus" /> record.
  /// If the value is <see langword="false" />, the accrual account will not be affected
  /// by this <see cref="T:PX.Objects.PO.POAccrualStatus" /> record, and this record will have
  /// <see cref="P:PX.Objects.PO.POAccrualStatus.IsClosed" /> equal to <see langword="true" />.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualStatus.orderType, NotEqual<POOrderType.projectDropShip>>>>>.Or<BqlOperand<POAccrualStatus.dropshipExpenseRecording, IBqlString>.IsNotEqual<DropshipExpenseRecordingOption.onBillRelease>>>>>.And<BqlOperand<POAccrualStatus.lineType, IBqlString>.IsNotIn<POLineType.service, POLineType.freight>>))]
  public virtual bool? IsAccountAffected { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMSetup.DropshipExpenseRecording" />
  [PXDBString(1)]
  public virtual string DropshipExpenseRecording { get; set; }

  [PXString(6, IsUnicode = true)]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBString(6, IsFixed = true)]
  public virtual string MaxFinPeriodID { get; set; }

  [PXDBString(6, IsFixed = true)]
  [PXFormula(typeof (BqlOperand<POAccrualStatus.maxFinPeriodID, IBqlString>.When<BqlOperand<POAccrualStatus.isClosed, IBqlBool>.IsEqual<True>>.Else<Null>))]
  public virtual string ClosedFinPeriodID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? UnreleasedReceiptCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? UnreleasedPPVAdjCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? UnreleasedTaxAdjCntr { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<POAccrualStatus>.By<POAccrualStatus.refNoteID, POAccrualStatus.lineNbr, POAccrualStatus.type>
  {
    public static POAccrualStatus Find(
      PXGraph graph,
      Guid? refNoteID,
      int? lineNbr,
      string type,
      PKFindOptions options = 0)
    {
      return (POAccrualStatus) PrimaryKeyOf<POAccrualStatus>.By<POAccrualStatus.refNoteID, POAccrualStatus.lineNbr, POAccrualStatus.type>.FindBy(graph, (object) refNoteID, (object) lineNbr, (object) type, options);
    }

    public static POAccrualStatus FindDirty(
      PXGraph graph,
      Guid? refNoteID,
      int? lineNbr,
      string type)
    {
      return PXResultset<POAccrualStatus>.op_Implicit(PXSelectBase<POAccrualStatus, PXSelect<POAccrualStatus, Where<POAccrualStatus.refNoteID, Equal<Required<POAccrualStatus.refNoteID>>, And<POAccrualStatus.lineNbr, Equal<Required<POAccrualStatus.lineNbr>>, And<POAccrualStatus.type, Equal<Required<POAccrualStatus.type>>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[3]
      {
        (object) refNoteID,
        (object) lineNbr,
        (object) type
      }));
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.orderType, POAccrualStatus.orderNbr>
    {
    }

    public class OrderLine : 
      PrimaryKeyOf<POLine>.By<POLine.orderType, POLine.orderNbr, POLine.lineNbr>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.orderType, POAccrualStatus.orderNbr, POAccrualStatus.orderLineNbr>
    {
    }

    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.receiptType, POAccrualStatus.receiptNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.siteID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.vendorID>
    {
    }

    public class PayToVendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.payToVendorID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.acctID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.subID>
    {
    }

    public class OriginalCurrency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.origCuryID>
    {
    }

    public class BillingCurrency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<POAccrualStatus>.By<POAccrualStatus.billCuryID>
    {
    }
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POAccrualStatus.refNoteID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualStatus.lineNbr>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.type>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.lineType>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.orderNbr>
  {
  }

  public abstract class orderLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualStatus.orderLineNbr>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.receiptNbr>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualStatus.vendorID>
  {
  }

  public abstract class payToVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualStatus.payToVendorID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualStatus.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualStatus.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualStatus.siteID>
  {
  }

  public abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualStatus.acctID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualStatus.subID>
  {
  }

  public abstract class origUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.origUOM>
  {
  }

  public abstract class origQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualStatus.origQty>
  {
  }

  public abstract class baseOrigQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.baseOrigQty>
  {
  }

  public abstract class origCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.origCuryID>
  {
  }

  public abstract class curyOrigAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.curyOrigAmt>
  {
  }

  public abstract class origAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualStatus.origAmt>
  {
  }

  public abstract class curyOrigCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.curyOrigCost>
  {
  }

  public abstract class origCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualStatus.origCost>
  {
  }

  public abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.curyOrigDiscAmt>
  {
  }

  public abstract class origDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.origDiscAmt>
  {
  }

  public abstract class receivedUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.receivedUOM>
  {
  }

  public abstract class receivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.baseReceivedQty>
  {
  }

  public abstract class receivedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.receivedCost>
  {
  }

  public abstract class billedUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.billedUOM>
  {
  }

  public abstract class billedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualStatus.billedQty>
  {
  }

  public abstract class baseBilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.baseBilledQty>
  {
  }

  public abstract class billCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.billCuryID>
  {
  }

  public abstract class curyBilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.curyBilledAmt>
  {
  }

  public abstract class billedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualStatus.billedAmt>
  {
  }

  public abstract class curyBilledCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.curyBilledCost>
  {
  }

  public abstract class billedCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualStatus.billedCost>
  {
  }

  public abstract class curyBilledDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.curyBilledDiscAmt>
  {
  }

  public abstract class billedDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.billedDiscAmt>
  {
  }

  public abstract class pPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualStatus.pPVAmt>
  {
  }

  public abstract class receivedTaxAdjCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.receivedTaxAdjCost>
  {
  }

  public abstract class curyBilledTaxAdjCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.curyBilledTaxAdjCost>
  {
  }

  public abstract class billedTaxAdjCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualStatus.billedTaxAdjCost>
  {
  }

  public abstract class isClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAccrualStatus.isClosed>
  {
  }

  public abstract class isAccountAffected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POAccrualStatus.isAccountAffected>
  {
  }

  public abstract class dropshipExpenseRecording : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualStatus.dropshipExpenseRecording>
  {
  }

  [Obsolete]
  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualStatus.uOM>
  {
    public class PreventEditINUnitIfExist : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<INUnit.unitMultDiv, INUnit.unitRate, INUnit.fromUnit>>.On<InventoryItemMaint>.IfExists<Select<POAccrualStatus, Where<POAccrualStatus.inventoryID, Equal<Current<INUnit.inventoryID>>, And<Current<INUnit.fromUnit>, In3<POAccrualStatus.origUOM, POAccrualStatus.receivedUOM, POAccrualStatus.billedUOM>, And<POAccrualStatus.isClosed, Equal<False>>>>>>
    {
      public static bool IsActive() => true;

      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        POAccrualStatus poAccrualStatus = (POAccrualStatus) firstPreventingEntity;
        return poAccrualStatus.Type == "R" ? PXMessages.LocalizeFormat("The item's conversion rule for the purchase unit cannot be changed because the item has not been fully received, billed, or invoiced in the {0} document of the {1} type: {2}. Use the Purchase Accrual Balance by Period (PO402000) inquiry to view the complete list of such documents.", new object[3]
        {
          (object) "Purchase Receipt",
          ((PXCache) GraphHelper.Caches<POAccrualStatus>(arg.Graph)).GetStateExt<POAccrualStatus.receiptType>((object) poAccrualStatus),
          (object) poAccrualStatus.ReceiptNbr
        }) : PXMessages.LocalizeFormat("The item's conversion rule for the purchase unit cannot be changed because the item has not been fully received, billed, or invoiced in the {0} document of the {1} type: {2}. Use the Purchase Accrual Balance by Period (PO402000) inquiry to view the complete list of such documents.", new object[3]
        {
          (object) "Purchase Order",
          ((PXCache) GraphHelper.Caches<POAccrualStatus>(arg.Graph)).GetStateExt<POAccrualStatus.orderType>((object) poAccrualStatus),
          (object) poAccrualStatus.OrderNbr
        });
      }

      public virtual void _(PX.Data.Events.RowDeleting<INUnit> e)
      {
        if (((EditPreventor<TypeArrayOf<IBqlField>.FilledWith<INUnit.unitMultDiv, INUnit.unitRate, INUnit.fromUnit>>.On<InventoryItemMaint>) this).AllowEditInsertedRecords && (((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<INUnit>>) e).Cache.GetStatus((object) e.Row) == 2 || ((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<INUnit>>) e).Cache.Locate((object) e.Row) == null))
          return;
        string preventingReason = ((EditPreventor<TypeArrayOf<IBqlField>.FilledWith<INUnit.unitMultDiv, INUnit.unitRate, INUnit.fromUnit>>.On<InventoryItemMaint>) this).GetEditPreventingReason(new GetEditPreventingReasonArgs(((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<INUnit>>) e).Cache, typeof (INUnit.unitRate), (object) e.Row, (object) e.Row.UnitRate, false));
        if (!string.IsNullOrEmpty(preventingReason))
          throw new PXException(preventingReason);
      }
    }
  }

  public abstract class maxFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualStatus.maxFinPeriodID>
  {
  }

  public abstract class closedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualStatus.closedFinPeriodID>
  {
  }

  public abstract class unreleasedReceiptCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POAccrualStatus.unreleasedReceiptCntr>
  {
  }

  public abstract class unreleasedPPVAdjCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POAccrualStatus.unreleasedPPVAdjCntr>
  {
  }

  public abstract class unreleasedTaxAdjCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POAccrualStatus.unreleasedTaxAdjCntr>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAccrualStatus.createdDateTime>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POAccrualStatus.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualStatus.createdByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAccrualStatus.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POAccrualStatus.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualStatus.lastModifiedByScreenID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POAccrualStatus.Tstamp>
  {
  }
}
