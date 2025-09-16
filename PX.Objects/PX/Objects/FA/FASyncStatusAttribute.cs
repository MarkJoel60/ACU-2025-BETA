// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FASyncStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FA;

public class FASyncStatusAttribute : PXEventSubscriberAttribute, IPXRowUpdatedSubscriber
{
  public void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<FixedAsset.status>(e.Row, e.OldRow))
      return;
    FixedAsset row = (FixedAsset) e.Row;
    FADetails faDetails = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) (int?) row?.AssetID
    }));
    sender.Graph.Caches[typeof (FADetails)].SetValue<FADetails.status>((object) faDetails, (object) row.Status);
    GraphHelper.MarkUpdated(sender.Graph.Caches[typeof (FADetails)], (object) faDetails);
  }
}
