// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.PreventEditBAccountRestrictToBase`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Exceptions;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract;

public abstract class PreventEditBAccountRestrictToBase<TField, TGraph, TDocument, TSelect> : 
  EditPreventor<TypeArrayOf<IBqlField>.FilledWith<TField>>.On<TGraph>.IfExists<TSelect>
  where TField : class, IBqlField
  where TGraph : PXGraph
  where TDocument : class, IBqlTable
  where TSelect : BqlCommand, new()
{
  protected virtual string CreateEditPreventingReason(
    GetEditPreventingReasonArgs arg,
    object firstPreventingEntity,
    string fieldName,
    string currentTableName,
    string foreignTableName)
  {
    PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(arg.Row is PX.Objects.CR.BAccount row ? row.BAccountID : new int?());
    if (branchByBaccountId != null && EnumerableExtensions.IsIn<object>(arg.NewValue, (object) null, (object) 0))
      return (string) null;
    int? newValue = arg.NewValue as int?;
    string str = PXAccess.GetBranchByBAccountID(newValue)?.BaseCuryID ?? ((PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(newValue))?.BaseCuryID;
    TDocument document = firstPreventingEntity as TDocument;
    string baseCurrency = this.GetBaseCurrency(document);
    if (branchByBaccountId != null)
    {
      IBqlTable differentBaseCurrency = this.FindDocumentWithDifferentBaseCurrency(row.BAccountID, baseCurrency);
      if (differentBaseCurrency != null)
        return PXMessages.LocalizeFormatNoPrefix("This box must remain blank because documents with different base currencies exist for {2}: the {0}, the {1}.", new object[3]
        {
          (object) this.GetDocumentDescription((IBqlTable) document),
          (object) this.GetDocumentDescription(differentBaseCurrency),
          (object) row.AcctCD?.TrimEnd()
        });
    }
    return str == baseCurrency ? (string) null : this.GetErrorMessage(row, document, baseCurrency);
  }

  protected virtual string GetBaseCurrency(TDocument document)
  {
    long? curyInfoID = (long?) ((PXGraphExtension<TGraph>) this).Base.Caches[typeof (TDocument)].GetValue((object) document, "curyInfoID");
    return (PX.Objects.CM.CurrencyInfo.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, curyInfoID) ?? throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.CM.CurrencyInfo>((PXGraph) ((PXGraphExtension<TGraph>) this).Base), new object[1]
    {
      (object) curyInfoID
    })).BaseCuryID;
  }

  protected abstract string GetErrorMessage(
    PX.Objects.CR.BAccount baccount,
    TDocument document,
    string documentBaseCurrency);

  protected virtual IBqlTable FindDocumentWithDifferentBaseCurrency(
    int? baccountID,
    string currency)
  {
    INItemSite differentBaseCurrency1 = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<INItemSite.FK.Site>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.preferredVendorOverride, Equal<True>>>>, And<BqlOperand<INItemSite.preferredVendorID, IBqlInt>.IsEqual<BqlField<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.AsOptional>>>>.And<BqlOperand<PX.Objects.IN.INSite.baseCuryID, IBqlString>.IsNotEqual<BqlField<PX.Objects.CM.CurrencyInfo.baseCuryID, IBqlString>.AsOptional>>>>.Config>.Select((PXGraph) ((PXGraphExtension<TGraph>) this).Base, new object[2]
    {
      (object) baccountID,
      (object) currency
    }));
    if (differentBaseCurrency1 != null)
      return (IBqlTable) differentBaseCurrency1;
    PX.Objects.PO.POReceipt differentBaseCurrency2 = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceipt, PXViewOf<PX.Objects.PO.POReceipt>.BasedOn<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CM.CurrencyInfo>.On<BqlOperand<PX.Objects.PO.POReceipt.curyInfoID, IBqlLong>.IsEqual<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.receiptType, NotEqual<POReceiptType.transferreceipt>>>>, And<BqlOperand<PX.Objects.PO.POReceipt.vendorID, IBqlInt>.IsEqual<BqlField<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.AsOptional>>>>.And<BqlOperand<PX.Objects.CM.CurrencyInfo.baseCuryID, IBqlString>.IsNotEqual<BqlField<PX.Objects.CM.CurrencyInfo.baseCuryID, IBqlString>.AsOptional>>>>.Config>.Select((PXGraph) ((PXGraphExtension<TGraph>) this).Base, new object[2]
    {
      (object) baccountID,
      (object) currency
    }));
    if (differentBaseCurrency2 != null)
      return (IBqlTable) differentBaseCurrency2;
    POLandedCostDoc differentBaseCurrency3 = PXResultset<POLandedCostDoc>.op_Implicit(PXSelectBase<POLandedCostDoc, PXViewOf<POLandedCostDoc>.BasedOn<SelectFromBase<POLandedCostDoc, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CM.CurrencyInfo>.On<BqlOperand<POLandedCostDoc.curyInfoID, IBqlLong>.IsEqual<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLandedCostDoc.vendorID, Equal<BqlField<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.AsOptional>>>>>.And<BqlOperand<PX.Objects.CM.CurrencyInfo.baseCuryID, IBqlString>.IsNotEqual<BqlField<PX.Objects.CM.CurrencyInfo.baseCuryID, IBqlString>.AsOptional>>>>.Config>.Select((PXGraph) ((PXGraphExtension<TGraph>) this).Base, new object[2]
    {
      (object) baccountID,
      (object) currency
    }));
    if (differentBaseCurrency3 != null)
      return (IBqlTable) differentBaseCurrency3;
    return (IBqlTable) PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CM.CurrencyInfo>.On<BqlOperand<PX.Objects.SO.SOShipment.curyInfoID, IBqlLong>.IsEqual<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOShipment.shipmentType, NotEqual<INDocType.transfer>>>>, And<BqlOperand<PX.Objects.SO.SOShipment.customerID, IBqlInt>.IsEqual<BqlField<PX.Objects.CR.BAccount.bAccountID, IBqlInt>.AsOptional>>>>.And<BqlOperand<PX.Objects.CM.CurrencyInfo.baseCuryID, IBqlString>.IsNotEqual<BqlField<PX.Objects.CM.CurrencyInfo.baseCuryID, IBqlString>.AsOptional>>>>.Config>.Select((PXGraph) ((PXGraphExtension<TGraph>) this).Base, new object[2]
    {
      (object) baccountID,
      (object) currency
    })) ?? (IBqlTable) null;
  }

  protected virtual string GetDocumentDescription(IBqlTable document)
  {
    switch (document)
    {
      case PX.Objects.PO.POReceipt poReceipt:
        return $"{poReceipt.ReceiptNbr} {((PXCache) GraphHelper.Caches<PX.Objects.PO.POReceipt>((PXGraph) ((PXGraphExtension<TGraph>) this).Base)).DisplayName}";
      case POLandedCostDoc poLandedCostDoc:
        return $"{poLandedCostDoc.RefNbr} {((PXCache) GraphHelper.Caches<POLandedCostDoc>((PXGraph) ((PXGraphExtension<TGraph>) this).Base)).DisplayName}";
      case PX.Objects.SO.SOShipment soShipment:
        return $"{soShipment.ShipmentNbr} {((PXCache) GraphHelper.Caches<PX.Objects.SO.SOShipment>((PXGraph) ((PXGraphExtension<TGraph>) this).Base)).DisplayName}";
      case INItemSite inItemSite:
        return $"{PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, inItemSite.InventoryID).InventoryCD} {PX.Objects.IN.INSite.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, inItemSite.SiteID).SiteCD} {((PXCache) GraphHelper.Caches<INItemSite>((PXGraph) ((PXGraphExtension<TGraph>) this).Base)).DisplayName}";
      default:
        throw new NotImplementedException();
    }
  }
}
