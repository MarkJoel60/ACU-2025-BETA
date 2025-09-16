// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_ForecastDet_Type
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_ForecastDet_Type
{
  public const 
  #nullable disable
  string Schedule = "S";
  public const string ContractPeriod = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public static List<Tuple<string, string>> FullList = new List<Tuple<string, string>>()
    {
      new Tuple<string, string>("S", "Schedule"),
      new Tuple<string, string>("P", "Contract Period")
    };

    public ListAttribute()
      : base(ListField_ForecastDet_Type.ListAttribute.FullList.ToArray())
    {
    }
  }

  public class schedule : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_ForecastDet_Type.schedule>
  {
    public schedule()
      : base("S")
    {
    }
  }

  public class contractPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ForecastDet_Type.contractPeriod>
  {
    public contractPeriod()
      : base("P")
    {
    }
  }
}
