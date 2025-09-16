// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.MappingFieldNameTree.MappingFieldTreeNode
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Api.Export.MappingFieldNameTree;

public class MappingFieldTreeNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  public 
  #nullable disable
  string Key { get; set; }

  [PXString]
  public string Value { get; set; }

  [PXString]
  public string Text { get; set; }

  [PXInt]
  public int? OrderNumber { get; set; }

  [PXString]
  public string Icon { get; set; }

  public abstract class key : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MappingFieldTreeNode.key>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MappingFieldTreeNode.value>
  {
  }

  public abstract class text : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MappingFieldTreeNode.text>
  {
  }

  public abstract class orderNumber : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MappingFieldTreeNode.orderNumber>
  {
  }

  public abstract class icon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MappingFieldTreeNode.icon>
  {
  }
}
