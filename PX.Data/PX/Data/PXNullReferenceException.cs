// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNullReferenceException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// Derived from PXException. The exception that is thrown when a configuration provider error has occurred.
/// </summary>
[Serializable]
public class PXNullReferenceException : PXException
{
  public PXNullReferenceException(Exception inner)
    : base(PXNullReferenceException.GetExceptionText(inner), inner)
  {
  }

  public PXNullReferenceException(Exception inner, string function)
    : base(inner, PXNullReferenceException.GetExceptionText(inner, function))
  {
  }

  public PXNullReferenceException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXNullReferenceException>(this, info);
  }

  /// <exclude />
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXNullReferenceException>(this, info);
    base.GetObjectData(info, context);
  }

  private static string GetExceptionText(Exception ex, string function = null)
  {
    if (ex != null)
    {
      System.Diagnostics.StackFrame frame = PXStackTrace.GetStackTrace(ex).GetFrame(0);
      if (frame != null)
        return $"An unhandled exception has occurred in the function '{frame.GetMethod().Name}'. Please see the trace log for more details.";
    }
    return function != null ? $"An unhandled exception has occurred in the function '{function}'. Please see the trace log for more details." : "An unhandled exception has occurred. Please see the trace log for more details.";
  }

  internal static Exception CheckException(Exception ex)
  {
    return !(ex is NullReferenceException) ? ex : (Exception) new PXNullReferenceException(ex);
  }

  internal static void ThrowException(Exception ex)
  {
    throw PXException.PreserveStack(PXNullReferenceException.CheckException(ex));
  }
}
