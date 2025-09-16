// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.Interface.IFieldValidationService`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.GenericInquiry.Services.ValidationServices.Interface;

internal interface IFieldValidationService<E, T, F>
  where E : FieldEventType
  where T : IBqlTable
  where F : IBqlField
{
  IEnumerable<GIFieldValidationError> ValidateField(PXCache cache, T row, object newValue);
}
