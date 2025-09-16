// Decompiled with JetBrains decompiler
// Type: PX.Data.MultiFactorAuth.StartTwoFactorPipelineResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.MultiFactorAuth;

public class StartTwoFactorPipelineResult
{
  public StartTwoFactorPipelineResult()
  {
  }

  public StartTwoFactorPipelineResult(
    string text,
    bool isError,
    int resendTimer,
    bool hasNoDevice)
  {
    this.Text = text;
    this.HasNoDevice = hasNoDevice;
    this.IsError = isError;
    this.ResendTimer = resendTimer;
  }

  public string Text { get; set; }

  public int IsMultiFactor { get; set; }

  public bool IsError { get; set; }

  public int ResendTimer { get; set; } = -1;

  public bool HasNoDevice { get; set; }

  public string[] Providers { get; set; }
}
