// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INTransferEntryExt.AddLotSerialPanelExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN.DAC.Projections;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INTransferEntryExt;

public class AddLotSerialPanelExt : INRegisterAddLotSerialPanelExtBase<INTransferEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  public override void Initialize()
  {
    base.Initialize();
    ((PXSelectBase<INItemLotSerialAttributesHeaderSelected>) this.LotSerialNbrResult).WhereAnd<Where<BqlOperand<INItemLotSerialAttributesHeaderSelected.siteID, IBqlInt>.IsEqual<BqlField<INRegister.siteID, IBqlInt>.FromCurrent.NoDefault>>>();
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(
    Events.CacheAttached<INItemLotSerialAttributesHeaderSelected.siteID> e)
  {
  }
}
