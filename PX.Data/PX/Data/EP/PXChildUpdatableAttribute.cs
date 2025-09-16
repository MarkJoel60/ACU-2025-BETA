// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.PXChildUpdatableAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.EP;

/// <summary>Allows specific fields update, related to activity entities.</summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public sealed class PXChildUpdatableAttribute : PXEventSubscriberAttribute
{
  public bool AutoRefresh;
  public bool UpdateRequest;
  private bool _showHint = true;

  public string TextField { get; set; }

  public bool ShowHint
  {
    get => this._showHint;
    set => this._showHint = value;
  }
}
