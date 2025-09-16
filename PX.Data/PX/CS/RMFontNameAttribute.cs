// Decompiled with JetBrains decompiler
// Type: PX.CS.RMFontNameAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.CS;

public class RMFontNameAttribute : PXDBStringAttribute
{
  public RMFontNameAttribute()
    : base(30)
  {
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnValue is string))
      return;
    string returnValue = (string) e.ReturnValue;
    PXLocalizerRepository.SpecialLocalizer.LocalizeFontFamilyName(ref returnValue);
    e.ReturnValue = (object) returnValue;
  }
}
