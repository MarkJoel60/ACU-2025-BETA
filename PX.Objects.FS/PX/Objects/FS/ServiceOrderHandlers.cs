// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceOrderHandlers
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public static class ServiceOrderHandlers
{
  public static void FSSODet_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (e.Row == null)
      return;
    FSSODet row = (FSSODet) e.Row;
    if (row.LineRef != null)
      return;
    row.LineRef = row.LineNbr.Value.ToString("0000");
  }
}
