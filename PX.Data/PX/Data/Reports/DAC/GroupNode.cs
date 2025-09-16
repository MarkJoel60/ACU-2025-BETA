// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.DAC.GroupNode
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Reports.DAC;

[PXHidden]
public class GroupNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  public 
  #nullable disable
  string GroupId { get; set; }

  [PXString]
  public string ParentGroupId { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  public abstract class groupId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GroupNode.groupId>
  {
  }

  public abstract class parentGroupId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GroupNode.parentGroupId>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GroupNode.description>
  {
  }
}
