// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_BillingRule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_BillingRule : IBqlField, IBqlOperand
{
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new ID.BillingRule().ID_LIST, new ID.BillingRule().TX_LIST)
    {
    }
  }

  public class Time : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_BillingRule.Time>
  {
    public Time()
      : base("TIME")
    {
    }
  }

  public class FlatRate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_BillingRule.FlatRate>
  {
    public FlatRate()
      : base("FLRA")
    {
    }
  }

  public class None : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_BillingRule.None>
  {
    public None()
      : base("NONE")
    {
    }
  }
}
