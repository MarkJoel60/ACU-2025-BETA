// Decompiled with JetBrains decompiler
// Type: PX.Data.PXErrorInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Web.UI;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public class PXErrorInfo
{
  public string Message { get; private set; }

  public string StackTrace { get; private set; }

  public string Icon { get; private set; }

  public PXErrorInfo(string message, string stackTrace)
  {
    this.Message = message;
    this.StackTrace = stackTrace;
    this.Icon = Sprite.Main.GetFullUrl("Fail");
  }
}
