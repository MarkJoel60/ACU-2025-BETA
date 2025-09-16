// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.ScorecardState
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
public class ScorecardState : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The unique identifier</summary>
  [PXString(IsKey = true)]
  public virtual 
  #nullable disable
  string Key { get; set; }

  /// <summary>The scoreCard title</summary>
  [PXString]
  public virtual string Name { get; set; }

  /// <summary>The scorecard value</summary>
  [PXString]
  public virtual string Value { get; set; }

  /// <summary>The scorecard level (normal, warning or error)</summary>
  [PXString]
  public virtual string Level { get; set; }

  /// <summary>
  /// A field indicating that the user does not have access to this indicator.
  /// </summary>
  [PXBool]
  public virtual bool? Disabled { get; set; }

  public abstract class key : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ScorecardState.key>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ScorecardState.name>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ScorecardState.value>
  {
  }

  public abstract class level : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ScorecardState.level>
  {
  }

  public abstract class disabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ScorecardState.disabled>
  {
  }
}
