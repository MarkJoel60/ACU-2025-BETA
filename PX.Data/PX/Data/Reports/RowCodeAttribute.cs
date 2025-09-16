// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.RowCodeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Reports;

internal class RowCodeAttribute : PXAggregateAttribute, IPXFieldUpdatingSubscriber
{
  private const string MaskRowCode = ">aaaaaaaaaa";

  public RowCodeAttribute()
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDBStringAttribute(10)
    {
      IsUnicode = true,
      InputMask = ">aaaaaaaaaa"
    });
  }

  public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    string newValue = (string) e.NewValue;
    if (string.IsNullOrWhiteSpace(newValue))
      return;
    e.NewValue = (object) newValue.Trim();
  }
}
