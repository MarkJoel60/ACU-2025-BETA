// Decompiled with JetBrains decompiler
// Type: PX.CS.RMColumnPreviewAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.CS;

public class RMColumnPreviewAttribute : RMPreviewAttribute
{
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    PXCache cach = sender.Graph is RMColumnSetMaint graph ? graph.Caches[typeof (RMDataSource)] : (PXCache) null;
    if (cach == null || e.Row == null)
      return;
    string type = graph.ColumnSet.Current.Type;
    RMColumn row = (RMColumn) e.Row;
    IEnumerable<RMPreviewDescriptorProviderExt<RMColumnSetMaint>> descriptorProviderExts = sender.Graph.Extensions.OfType<RMPreviewDescriptorProviderExt<RMColumnSetMaint>>();
    RMPreviewAttribute.PreviewItemDescriptor[] previewItemDescriptorArray = (RMPreviewAttribute.PreviewItemDescriptor[]) null;
    foreach (RMPreviewDescriptorProviderExt<RMColumnSetMaint> descriptorProviderExt in descriptorProviderExts)
      previewItemDescriptorArray = descriptorProviderExt.GetPreviewDescription(type, previewItemDescriptorArray);
    RMDataSource rmDataSource = new RMDataSource()
    {
      DataSourceID = row.DataSourceID
    };
    RMDataSource dataSource = cach.Locate((object) rmDataSource) as RMDataSource;
    e.ReturnValue = (object) this.GetPreviewText(cach, dataSource, previewItemDescriptorArray);
  }
}
