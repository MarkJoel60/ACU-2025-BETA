// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.NavigationTreeViewNode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM.Project.Overview;

[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class NavigationTreeViewNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The unique identifier of the navigation section</summary>
  [PXString(IsKey = true)]
  public virtual 
  #nullable disable
  string Key { get; set; }

  /// <summary>The parent section key</summary>
  [PXString]
  public virtual string ParentKey { get; set; }

  /// <summary>The name (Title) of the section</summary>
  [PXString]
  public virtual string Name { get; set; }

  /// <summary>The section ordinal index</summary>
  [PXInt]
  public virtual int? Index { get; set; }

  /// <summary>
  /// The section navigation name (needed for group sections).
  /// </summary>
  [PXString]
  public virtual string Navigation { get; set; }

  public abstract class key : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NavigationTreeViewNode.key>
  {
  }

  public abstract class parentKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NavigationTreeViewNode.parentKey>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NavigationTreeViewNode.name>
  {
  }

  public abstract class index : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  NavigationTreeViewNode.index>
  {
  }

  public abstract class navigation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NavigationTreeViewNode.navigation>
  {
  }
}
