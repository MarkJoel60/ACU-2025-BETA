// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNoteTextAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXNoteTextAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    string returnValue = e.ReturnValue as string;
    if (string.IsNullOrWhiteSpace(returnValue))
      return;
    string[] strArray = returnValue.Split(sender.Graph.SqlDialect.WildcardFieldSeparatorChar);
    if (strArray.Length <= 1)
      return;
    e.ReturnValue = (object) strArray[0];
  }
}
