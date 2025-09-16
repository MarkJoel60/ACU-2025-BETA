// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.APInvoiceEntryProjectExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.PM;

public class APInvoiceEntryProjectExternalTax : 
  PXGraphExtension<APInvoiceEntryExternalTax, APInvoiceEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>() && PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && new ProjectSettingsManager().CalculateProjectSpecificTaxes;
  }

  [PXOverride]
  public virtual IAddressLocation? GetToAddress(
    PX.Objects.AP.APInvoice? invoice,
    Func<PX.Objects.AP.APInvoice?, IAddressLocation?> baseMethod)
  {
    return (IAddressLocation) this.GetAddressFromProject((int?) invoice?.ProjectID) ?? baseMethod(invoice);
  }

  [PXOverride]
  public virtual IAddressLocation? GetToAddress(
    PX.Objects.AP.APInvoice? invoice,
    PX.Objects.AP.APTran? transaction,
    Func<PX.Objects.AP.APInvoice?, PX.Objects.AP.APTran?, IAddressLocation?> baseMethod)
  {
    if (transaction == null)
      return baseMethod(invoice, transaction);
    if (string.IsNullOrEmpty(transaction.POOrderType) || transaction.POOrderType == "RS")
      return (IAddressLocation) this.GetAddressFromProject(transaction.ProjectID) ?? baseMethod(invoice, transaction);
    if (transaction.POOrderType != "RO" || string.IsNullOrEmpty(transaction.PONbr))
      return baseMethod(invoice, transaction);
    return (IAddressLocation) PXResultset<POShipAddress>.op_Implicit(PXSelectBase<POShipAddress, PXViewOf<POShipAddress>.BasedOn<SelectFromBase<POShipAddress, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POOrder>.On<BqlOperand<PX.Objects.PO.POOrder.shipAddressID, IBqlInt>.IsEqual<POShipAddress.addressID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.orderType, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.PO.POOrder.orderNbr, IBqlString>.IsEqual<P.AsString>>>>.ReadOnly.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[2]
    {
      (object) transaction.POOrderType,
      (object) transaction.PONbr
    })) ?? baseMethod(invoice, transaction);
  }

  protected virtual PMAddress? GetAddressFromProject(int? projectID)
  {
    if (projectID.HasValue)
    {
      int? nullable1 = projectID;
      int? nullable2 = ProjectDefaultAttribute.NonProject();
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        return PXResultset<PMAddress>.op_Implicit(PXSelectBase<PMAddress, PXViewOf<PMAddress>.BasedOn<SelectFromBase<PMAddress, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMProject.siteAddressID, IBqlInt>.IsEqual<PMAddress.addressID>>>>.Where<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[1]
        {
          (object) projectID
        }));
    }
    return (PMAddress) null;
  }
}
