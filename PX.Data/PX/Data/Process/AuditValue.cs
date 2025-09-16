// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.AuditValue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Process;

[Serializable]
public class AuditValue
{
  public object OldValue { get; set; }

  public object NewValue { get; set; }

  public string DisplayName { get; set; }

  public string Format { get; set; }

  public AuditValue(string displayName, object value)
    : this(displayName, value, (object) null)
  {
  }

  public AuditValue(string displayName, object newValue, object oldValue)
  {
    this.DisplayName = displayName;
    this.NewValue = newValue;
    this.OldValue = oldValue;
  }
}
