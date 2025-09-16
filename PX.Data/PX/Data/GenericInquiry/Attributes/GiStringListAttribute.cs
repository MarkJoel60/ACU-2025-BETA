// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Attributes.GiStringListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.GenericInquiry.Attributes;

public class GiStringListAttribute(string[] values, string[] labels) : PXStringListAttribute(values, labels)
{
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Cancel)
      return;
    base.FieldSelecting(sender, e);
  }
}
