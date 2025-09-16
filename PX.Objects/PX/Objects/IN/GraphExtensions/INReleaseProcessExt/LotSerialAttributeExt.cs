// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INReleaseProcessExt.LotSerialAttributeExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN.DAC;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INReleaseProcessExt;

public class LotSerialAttributeExt : LotSerialGraphExtBase<INReleaseProcess>
{
  public PXSelect<ItemLotSerialAttributesHeader> itemLotSerialAttributesHeader;
  public PXSelect<INItemLotSerialAttributesHeader> inItemLotSerialAttributesHeader;
  public PXSelect<INRegisterItemLotSerialAttributesHeader> inRegisterItemLotSerialAttributesHeader;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.AccumulatedItemLotSerial(PX.Objects.IN.INTranSplit,PX.Objects.IN.INLotSerClass)" />
  /// </summary>
  [PXOverride]
  public virtual PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial AccumulatedItemLotSerial(
    INTranSplit split,
    INLotSerClass lsclass,
    Func<INTranSplit, INLotSerClass, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial> baseMethod)
  {
    ItemLotSerialAttributesHeader attributesHeader = new ItemLotSerialAttributesHeader();
    attributesHeader.InventoryID = split.InventoryID;
    attributesHeader.LotSerialNbr = split.LotSerialNbr;
    ((PXSelectBase<ItemLotSerialAttributesHeader>) this.itemLotSerialAttributesHeader).Insert(attributesHeader);
    return baseMethod(split, lsclass);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.PersistLotSerialAttributes(PX.Objects.IN.INRegister)" />
  /// </summary>
  [PXOverride]
  public virtual void PersistLotSerialAttributes(
    PX.Objects.IN.INRegister doc,
    Action<PX.Objects.IN.INRegister> base_PersistLotSerialAttributes)
  {
    base_PersistLotSerialAttributes(doc);
    bool? nullable1;
    if (EnumerableExtensions.IsNotIn<string>(doc.DocType, "R", "M"))
    {
      nullable1 = doc.IsCorrection;
      if (!nullable1.GetValueOrDefault())
        return;
    }
    if (doc.TransferNbr != null)
      return;
    HashSet<(int?, string)> valueTupleSet = new HashSet<(int?, string)>();
    PXViewOf<INTranSplit>.BasedOn<SelectFromBase<INTranSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTran>.On<INTranSplit.FK.Tran>>, FbqlJoins.Left<INRegisterItemLotSerialAttributesHeader>.On<INTranSplit.FK.RegisterItemLotSerialAttributesHeader>>, FbqlJoins.Left<INItemLotSerialAttributesHeader>.On<INRegisterItemLotSerialAttributesHeader.FK.INItemLotSerialAttributesHeader>>>.Where<KeysRelation<CompositeKey<Field<INTranSplit.docType>.IsRelatedTo<PX.Objects.IN.INRegister.docType>, Field<INTranSplit.refNbr>.IsRelatedTo<PX.Objects.IN.INRegister.refNbr>>.WithTablesOf<PX.Objects.IN.INRegister, INTranSplit>, PX.Objects.IN.INRegister, INTranSplit>.SameAsCurrent.And<BqlOperand<INTranSplit.lotSerialNbr, IBqlString>.IsNotNull>>>.ReadOnly readOnly = new PXViewOf<INTranSplit>.BasedOn<SelectFromBase<INTranSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTran>.On<INTranSplit.FK.Tran>>, FbqlJoins.Left<INRegisterItemLotSerialAttributesHeader>.On<INTranSplit.FK.RegisterItemLotSerialAttributesHeader>>, FbqlJoins.Left<INItemLotSerialAttributesHeader>.On<INRegisterItemLotSerialAttributesHeader.FK.INItemLotSerialAttributesHeader>>>.Where<KeysRelation<CompositeKey<Field<INTranSplit.docType>.IsRelatedTo<PX.Objects.IN.INRegister.docType>, Field<INTranSplit.refNbr>.IsRelatedTo<PX.Objects.IN.INRegister.refNbr>>.WithTablesOf<PX.Objects.IN.INRegister, INTranSplit>, PX.Objects.IN.INRegister, INTranSplit>.SameAsCurrent.And<BqlOperand<INTranSplit.lotSerialNbr, IBqlString>.IsNotNull>>>.ReadOnly((PXGraph) this.Base);
    using (new PXFieldScope(((PXSelectBase) readOnly).View, new Type[6]
    {
      typeof (INTran.tranDesc),
      typeof (INTranSplit.inventoryID),
      typeof (INTranSplit.lotSerialNbr),
      typeof (INTranSplit.tranType),
      typeof (INRegisterItemLotSerialAttributesHeader),
      typeof (INItemLotSerialAttributesHeader)
    }))
    {
      PXView view = ((PXSelectBase) readOnly).View;
      object[] objArray1 = new object[1]{ (object) doc };
      object[] objArray2 = Array.Empty<object>();
      foreach (PXResult<INTranSplit, INTran, INRegisterItemLotSerialAttributesHeader, INItemLotSerialAttributesHeader> pxResult in view.SelectMultiBound(objArray1, objArray2))
      {
        INTranSplit inTranSplit = PXResult<INTranSplit, INTran, INRegisterItemLotSerialAttributesHeader, INItemLotSerialAttributesHeader>.op_Implicit(pxResult);
        PXResult<INTranSplit, INTran, INRegisterItemLotSerialAttributesHeader, INItemLotSerialAttributesHeader>.op_Implicit(pxResult);
        if (!EnumerableExtensions.IsNotIn<string>(inTranSplit.TranType, "RCP", "ADJ") && valueTupleSet.Add((inTranSplit.InventoryID, inTranSplit.LotSerialNbr?.ToUpper())))
        {
          INRegisterItemLotSerialAttributesHeader source = PXResult<INTranSplit, INTran, INRegisterItemLotSerialAttributesHeader, INItemLotSerialAttributesHeader>.op_Implicit(pxResult);
          INItemLotSerialAttributesHeader attributesHeader1 = PXResult<INTranSplit, INTran, INRegisterItemLotSerialAttributesHeader, INItemLotSerialAttributesHeader>.op_Implicit(pxResult);
          int? inventoryId = source.InventoryID;
          if (inventoryId.HasValue)
          {
            inventoryId = attributesHeader1.InventoryID;
            if (inventoryId.HasValue)
            {
              attributesHeader1.MfgLotSerialNbr = source.MfgLotSerialNbr;
              PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, source.InventoryID);
              INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this.Base, inventoryItem?.LotSerClassID);
              int num;
              if (inLotSerClass == null)
              {
                num = 0;
              }
              else
              {
                nullable1 = inLotSerClass.UseLotSerSpecificDetails;
                num = nullable1.GetValueOrDefault() ? 1 : 0;
              }
              if (num != 0)
              {
                INItemLotSerialAttributesHeader attributesHeader2 = attributesHeader1;
                Decimal? nullable2;
                if (!attributesHeader2.SalesPrice.HasValue)
                  attributesHeader2.SalesPrice = nullable2 = (Decimal?) InventoryItemCurySettings.PK.Find((PXGraph) this.Base, inventoryItem.InventoryID, doc.BranchBaseCuryID)?.BasePrice;
                INItemLotSerialAttributesHeader attributesHeader3 = attributesHeader1;
                if (!attributesHeader3.RecPrice.HasValue)
                  attributesHeader3.RecPrice = nullable2 = (Decimal?) InventoryItemCurySettings.PK.Find((PXGraph) this.Base, inventoryItem.InventoryID, doc.BranchBaseCuryID)?.RecPrice;
                INItemLotSerialAttributesHeader attributesHeader4 = attributesHeader1;
                if (attributesHeader4.Descr == null)
                {
                  string descr;
                  attributesHeader4.Descr = descr = inventoryItem.Descr;
                }
              }
              INItemLotSerialAttributesHeader destination = ((PXSelectBase<INItemLotSerialAttributesHeader>) this.inItemLotSerialAttributesHeader).Update(attributesHeader1);
              this.CopyAttributes<INRegisterItemLotSerialAttributesHeader, INItemLotSerialAttributesHeader>(source, destination);
              continue;
            }
          }
          inventoryId = source.InventoryID;
          if (!inventoryId.HasValue)
          {
            IEnumerable<LotSerialGraphExtBase<INReleaseProcess>.AttributeInformation> requiredAttributes = this.GetRequiredAttributes(inTranSplit.InventoryID);
            if ((requiredAttributes != null ? requiredAttributes.FirstOrDefault<LotSerialGraphExtBase<INReleaseProcess>.AttributeInformation>() : (LotSerialGraphExtBase<INReleaseProcess>.AttributeInformation) null) != null)
            {
              PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inTranSplit.InventoryID);
              throw new PXException("At least one required attribute has not been specified for the {0} lot or serial number of the {1} item in the Line Details dialog box. Specify all required attributes to save the document.", new object[2]
              {
                (object) inTranSplit.LotSerialNbr,
                (object) inventoryItem?.InventoryCD?.Trim()
              });
            }
          }
          else
          {
            inventoryId = attributesHeader1.InventoryID;
            if (!inventoryId.HasValue)
              throw new PXInvalidOperationException();
          }
        }
      }
    }
    ((PXSelectBase) this.inItemLotSerialAttributesHeader).Cache.Persist((PXDBOperation) 1);
  }
}
