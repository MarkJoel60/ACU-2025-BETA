// Decompiled with JetBrains decompiler
// Type: PX.SM.AvailableDataTypesListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert.Installer.DatabaseSetup;
using PX.Data;
using PX.Data.Update;
using System;
using System.Linq;

#nullable disable
namespace PX.SM;

public class AvailableDataTypesListAttribute : PXStringListAttribute
{
  public AvailableDataTypesListAttribute()
    : base(PXDataTypesHelper.AvailableDataTypes.Select<DataTypeInfo, string>((Func<DataTypeInfo, string>) (t => t.Name)).ToArray<string>(), PXDataTypesHelper.AvailableDataTypes.Select<DataTypeInfo, string>((Func<DataTypeInfo, string>) (t => t.Name)).ToArray<string>())
  {
  }
}
