// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.ValueWithInternal
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Diagnostics;

#nullable disable
namespace PX.PushNotifications;

/// <exclude />
[System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}", Name = "{ExternalValue}", Type = "{ExternalValue}")]
public sealed class ValueWithInternal
{
  [Obsolete("For deserialization only")]
  public ValueWithInternal()
  {
  }

  public ValueWithInternal(object externalValue, object internalValue)
  {
    this.InternalValue = internalValue;
    this.ExternalValue = externalValue;
  }

  public object InternalValue { get; set; }

  public object ExternalValue { get; set; }

  public override string ToString()
  {
    return this.ExternalValue?.ToString() ?? this.InternalValue?.ToString() ?? string.Empty;
  }

  public object GetValue() => this.InternalValue;

  public override bool Equals(object obj)
  {
    if (!(obj is ValueWithInternal valueWithInternal))
      return base.Equals(obj);
    object internalValue = this.InternalValue;
    if ((internalValue != null ? (!internalValue.Equals(valueWithInternal.InternalValue) ? 1 : 0) : (valueWithInternal.InternalValue != null ? 1 : 0)) != 0)
      return false;
    object externalValue = this.ExternalValue;
    return externalValue == null ? valueWithInternal.ExternalValue == null : externalValue.Equals(valueWithInternal.ExternalValue);
  }

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string DebuggerDisplay => this.ExternalValue?.ToString();

  public override int GetHashCode()
  {
    object internalValue = this.InternalValue;
    return internalValue == null ? 0 : internalValue.GetHashCode();
  }

  public static object UnwrapInternalValue(object value)
  {
    return !(value is ValueWithInternal valueWithInternal) ? value : valueWithInternal.InternalValue;
  }

  public static object UnwrapExternalValue(object value)
  {
    return !(value is ValueWithInternal valueWithInternal) ? value : valueWithInternal.ExternalValue;
  }
}
