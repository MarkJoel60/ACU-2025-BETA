// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXCultureValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Translation;

/// <exclude />
public class PXCultureValue
{
  public string CultureName;
  public string Value;
  public bool IsNotLocalizable;

  public PXCultureValue()
  {
  }

  public PXCultureValue(string cultureName, string value, bool isNotLocalizable)
  {
    this.CultureName = cultureName;
    this.Value = value;
    this.IsNotLocalizable = isNotLocalizable;
  }
}
