// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.EPNowDefaultAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.EP;

/// <summary>Sets the DateTime.Now property as the default value.</summary>
public class EPNowDefaultAttribute : PXDefaultAttribute
{
  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    base.FieldDefaulting(sender, e);
    e.NewValue = (object) System.DateTime.Now;
  }
}
