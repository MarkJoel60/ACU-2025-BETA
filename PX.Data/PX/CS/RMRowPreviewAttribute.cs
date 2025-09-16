// Decompiled with JetBrains decompiler
// Type: PX.CS.RMRowPreviewAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Reports.ARm;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.CS;

public class RMRowPreviewAttribute : RMPreviewAttribute
{
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    PXCache cach = sender.Graph is RMRowSetMaint graph ? graph.Caches[typeof (RMDataSource)] : (PXCache) null;
    if (cach == null || e.Row == null)
      return;
    string type = graph.RowSet.Current.Type;
    RMRow row = (RMRow) e.Row;
    switch ((int) (ARmRowType) (int) row.RowType.Value)
    {
      case 0:
      case 3:
        if (row.InCycle.GetValueOrDefault())
        {
          e.ReturnValue = (object) "! REF !";
          break;
        }
        if (!string.IsNullOrEmpty(row.Formula))
        {
          e.ReturnValue = (object) row.Formula;
          break;
        }
        IEnumerable<RMPreviewDescriptorProviderExt<RMRowSetMaint>> descriptorProviderExts = sender.Graph.Extensions.OfType<RMPreviewDescriptorProviderExt<RMRowSetMaint>>();
        RMPreviewAttribute.PreviewItemDescriptor[] previewItemDescriptorArray = (RMPreviewAttribute.PreviewItemDescriptor[]) null;
        foreach (RMPreviewDescriptorProviderExt<RMRowSetMaint> descriptorProviderExt in descriptorProviderExts)
          previewItemDescriptorArray = descriptorProviderExt.GetPreviewDescription(type, previewItemDescriptorArray);
        RMDataSource rmDataSource = new RMDataSource()
        {
          DataSourceID = row.DataSourceID
        };
        RMDataSource dataSource = cach.Locate((object) rmDataSource) as RMDataSource;
        e.ReturnValue = (object) this.GetPreviewText(cach, dataSource, previewItemDescriptorArray);
        break;
      case 2:
        e.ReturnValue = (object) "";
        break;
      default:
        e.ReturnValue = (object) "";
        break;
    }
  }
}
