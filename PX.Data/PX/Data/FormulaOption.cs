// Decompiled with JetBrains decompiler
// Type: PX.Data.FormulaOption
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

[PXHidden]
[PXVirtual]
public class FormulaOption : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true, IsUnicode = true)]
  public virtual 
  #nullable disable
  string Category { get; set; }

  [PXString(IsKey = true, IsUnicode = true)]
  public virtual string Value { get; set; }

  public abstract class category : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FormulaOption.category>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FormulaOption.value>
  {
  }
}
