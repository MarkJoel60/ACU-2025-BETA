// Decompiled with JetBrains decompiler
// Type: PX.Data.PXErrorHandling
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>This enumeration is used in the <see cref="T:PX.Data.PXUIFieldAttribute">PXUIField</see> attribute to specify when to handle the <tt>PXSetPropertyException</tt> exception related to the
/// field. If the exception is handled, the user gets a message box with the error description, and the field input control is marked as causing an error.</summary>
public enum PXErrorHandling
{
  /// <summary>The exception is reported only when the <see cref="T:PX.Data.PXUIFieldAttribute">PXUIField</see> attribute with the <tt>Visible</tt> property set to <see langword="true" /> is attached to a DAC field.</summary>
  WhenVisible,
  /// <summary>The exception is always reported by the <see cref="T:PX.Data.PXUIFieldAttribute">PXUIField</see> attribute attached to a DAC field.</summary>
  Always,
  /// <summary>The exception is never reported by the <see cref="T:PX.Data.PXUIFieldAttribute">PXUIField</see> attribute attached to a DAC field.</summary>
  Never,
}
