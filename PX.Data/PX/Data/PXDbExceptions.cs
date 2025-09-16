// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDbExceptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Defines provider-independent exceptions that can occur (used by PXDatabaseException class)
/// </summary>
public enum PXDbExceptions
{
  DeleteForeignKeyConstraintViolation,
  DeleteReferenceConstraintViolation,
  InsertForeignKeyConstraintViolation,
  UpdateForeignKeyConstraintViolation,
  PrimaryKeyConstraintViolation,
  OperationSwitchRequired,
  Deadlock,
  Timeout,
  Unknown,
  DataWouldBeTruncated,
  FullTextSearchDisabled,
}
