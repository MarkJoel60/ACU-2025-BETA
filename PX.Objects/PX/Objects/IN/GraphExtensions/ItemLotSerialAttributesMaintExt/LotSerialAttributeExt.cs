// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.ItemLotSerialAttributesMaintExt.LotSerialAttributeExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN.DAC;
using System;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.ItemLotSerialAttributesMaintExt;

public class LotSerialAttributeExt : 
  LotSerialAttributesGridExt<
  #nullable disable
  INItemLotSerialAttributesMaint, INItemLotSerialAttributesHeader>
{
  public FbqlSelect<SelectFromBase<INItemLotSerialAttribute, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INItemLotSerialAttribute.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  INItemLotSerialAttributesHeader.inventoryID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INItemLotSerialAttribute.isActive, IBqlBool>.IsEqual<
  #nullable disable
  True>>>.Order<By<BqlField<
  #nullable enable
  INItemLotSerialAttribute.sortOrder, IBqlShort>.Asc>>, 
  #nullable disable
  INItemLotSerialAttribute>.View lotSerialAttributes;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  protected virtual void _(
    Events.RowPersisting<INItemLotSerialAttributesHeader> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1) || e.Row == null)
      return;
    this.VerifyRequiredAttributes(e.Row);
  }

  public override PXSelectBase<INItemLotSerialAttributesHeader> GetAttributesHeaderView()
  {
    return (PXSelectBase<INItemLotSerialAttributesHeader>) this.Base.CurrentItemLotSerial;
  }

  protected override INItemLotSerialAttributesHeader GetAttributesHeader(
    int? inventoryID,
    string lotSerialNbr,
    bool insert)
  {
    return PXResultset<INItemLotSerialAttributesHeader>.op_Implicit(this.GetAttributesHeaderView().Select(Array.Empty<object>()));
  }
}
