// Decompiled with JetBrains decompiler
// Type: ScreenActionExtraData
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
public class ScreenActionExtraData
{
  public ScreenActionExtraData(
    string actionDiscriminator,
    string beforeRunForm,
    string afterRunForm,
    string settings)
  {
    this.ActionDiscriminator = actionDiscriminator;
    this.BeforeRunForm = beforeRunForm;
    this.AfterRunForm = afterRunForm;
    this.Settings = settings;
  }

  public string ActionDiscriminator { get; }

  public string BeforeRunForm { get; }

  public string AfterRunForm { get; }

  public virtual string Settings { get; }
}
