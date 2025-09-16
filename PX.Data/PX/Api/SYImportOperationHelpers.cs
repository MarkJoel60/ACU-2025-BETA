// Decompiled with JetBrains decompiler
// Type: PX.Api.SYImportOperationHelpers
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Api;

internal static class SYImportOperationHelpers
{
  public static SYValidation GetValidationSettings(this SYImportOperation operation)
  {
    bool? validate = operation.Validate;
    if (!validate.HasValue || !validate.GetValueOrDefault())
      return SYValidation.None;
    bool? validateAndSave = operation.ValidateAndSave;
    bool flag = true;
    return validateAndSave.GetValueOrDefault() == flag & validateAndSave.HasValue ? SYValidation.SaveIfValid : SYValidation.Validate;
  }
}
