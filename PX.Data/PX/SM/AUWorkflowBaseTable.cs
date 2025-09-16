// Decompiled with JetBrains decompiler
// Type: PX.SM.AUWorkflowBaseTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public abstract class AUWorkflowBaseTable : PXBqlTable, IRemovable
{
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; } = new bool?(true);

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "System")]
  public bool? IsSystem { get; set; }

  public abstract class isActive : BqlType<IBqlBool, bool>.Field<
  #nullable disable
  AUWorkflowBaseTable.isActive>
  {
  }

  public abstract class isSystem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUWorkflowBaseTable.isSystem>
  {
  }
}
