// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.Interface.IRowValidationService`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.GenericInquiry.Services.ValidationServices.Interface;

internal interface IRowValidationService<E, T>
  where E : RowEventType
  where T : IBqlTable
{
  IEnumerable<GIRowValidationError> ValidateRow(PXCache cache, T row);
}
