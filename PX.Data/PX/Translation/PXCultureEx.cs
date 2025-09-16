// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXCultureEx
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Translation;

/// <exclude />
public class PXCultureEx
{
  public string ResourceKey;
  public string Language;
  public string Value;
  public bool IsNotLocalizable;

  public PXCultureEx()
  {
  }

  public PXCultureEx(string resourceKey, string language, string value, bool isNotLocalizable)
  {
    this.ResourceKey = resourceKey;
    this.Language = language;
    this.Value = value;
    this.IsNotLocalizable = isNotLocalizable;
  }
}
