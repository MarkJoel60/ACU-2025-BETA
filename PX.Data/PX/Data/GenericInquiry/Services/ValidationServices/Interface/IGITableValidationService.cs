// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.ValidationServices.Interface.IGITableValidationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Maintenance.GI;

#nullable disable
namespace PX.Data.GenericInquiry.Services.ValidationServices.Interface;

internal interface IGITableValidationService : 
  IRowValidationService<RowSelected, GITable>,
  IRowValidationService<RowInserted, GITable>,
  IRowValidationService<RowUpdated, GITable>,
  IRowValidationService<RowDeleted, GITable>,
  IRowValidationService<RowPersisting, GITable>
{
}
