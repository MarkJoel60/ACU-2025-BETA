// Decompiled with JetBrains decompiler
// Type: PX.Data.IPXExceptionHandlingSubscriber
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Indicates that an attribute that implemets this interface subscribes
/// to the <tt>ExceptionHandling</tt> event.
/// </summary>
public interface IPXExceptionHandlingSubscriber
{
  void ExceptionHandling(PXCache sender, PXExceptionHandlingEventArgs e);
}
