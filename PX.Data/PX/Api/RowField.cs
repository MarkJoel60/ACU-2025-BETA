// Decompiled with JetBrains decompiler
// Type: PX.Api.RowField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.Api;

internal class RowField
{
  public readonly object Value;
  public readonly PXFieldState State;
  public readonly PXDBLocalizableStringAttribute.Translations Translations;

  public RowField(
    object value,
    PXFieldState state,
    PXDBLocalizableStringAttribute.Translations translations)
  {
    this.Value = value;
    this.State = state;
    this.Translations = translations;
  }
}
