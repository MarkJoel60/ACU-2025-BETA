// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.TenantShapshotDeletion.DAC.DeletionAction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Maintenance.TenantShapshotDeletion.DAC;

[PXCacheName("Deletion Action")]
public class DeletionAction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Action")]
  [DeletionActionList]
  [PXDBString(1, IsFixed = true)]
  [PXDefault("S")]
  public virtual 
  #nullable disable
  string Name { get; set; }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeletionAction.name>
  {
  }
}
