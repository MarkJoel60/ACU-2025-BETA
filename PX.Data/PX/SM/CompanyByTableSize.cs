// Decompiled with JetBrains decompiler
// Type: PX.SM.CompanyByTableSize
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class CompanyByTableSize : TableSize
{
  [PXUIField(DisplayName = "Type")]
  [PXString(IsUnicode = true)]
  public virtual 
  #nullable disable
  string Type { get; set; }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Name")]
  public virtual string Name { get; set; }

  [PXUIField(DisplayName = "Size in DB", Enabled = false)]
  [PXDecimal]
  public virtual Decimal? Size { get; set; }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyByTableSize.type>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CompanyByTableSize.name>
  {
  }

  public abstract class size : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CompanyByTableSize.size>
  {
  }
}
