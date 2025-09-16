// Decompiled with JetBrains decompiler
// Type: PX.CS.RenumberingFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.CS;

[Serializable]
public class RenumberingFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField(DisplayName = "Numbering Step")]
  public virtual int? Step { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Length")]
  public virtual int? MaskLength { get; set; }

  public abstract class step : BqlType<IBqlInt, int>.Field<
  #nullable disable
  RenumberingFilter.step>
  {
  }

  public abstract class maskLength : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RenumberingFilter.maskLength>
  {
  }
}
