// Decompiled with JetBrains decompiler
// Type: PX.Data.CustomizationOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public class CustomizationOptions : ConfiguredValueBase
{
  public string CustomizerRole { get; private set; } = "Customizer";

  protected override void SetConfiguredValue(string value) => this.CustomizerRole = value;

  protected override string ConfigurationKey { get; } = "CustomizationRole";
}
