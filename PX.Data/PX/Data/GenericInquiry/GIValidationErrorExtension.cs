// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.GIValidationErrorExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.GenericInquiry;

internal static class GIValidationErrorExtension
{
  public static void OnError(
    this IEnumerable<GIValidationError> errors,
    System.Action<GIValidationError> action)
  {
    foreach (GIValidationError error in errors)
      action(error);
  }

  public static void OnLastError(
    this IEnumerable<GIRowValidationError> errors,
    System.Action<GIRowValidationError> action,
    bool executeOnEmptyError = false)
  {
    GIRowValidationError rowValidationError = errors.LastOrDefault<GIRowValidationError>();
    if (!(rowValidationError != null | executeOnEmptyError))
      return;
    action(rowValidationError);
  }

  public static PXSetPropertyException CreateSetPropertyException(this GIRowValidationError error)
  {
    return error.Arguments != null ? new PXSetPropertyException(error.Row, error.Message, error.ErrorLevel, (object[]) error.Arguments) : new PXSetPropertyException(error.Row, error.Message, error.ErrorLevel);
  }
}
