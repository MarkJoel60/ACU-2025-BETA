// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.Restriction
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
public class Restriction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  public virtual 
  #nullable disable
  string Key { get; set; }

  [PXBool]
  public virtual bool? Disabled { get; set; }

  public abstract class key : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Restriction.key>
  {
  }

  public abstract class disabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Restriction.disabled>
  {
  }
}
