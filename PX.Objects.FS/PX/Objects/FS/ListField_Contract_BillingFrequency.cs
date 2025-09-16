// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Contract_BillingFrequency
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Contract_BillingFrequency : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.ContractType_BillingFrequency().ID_LIST, new ID.ContractType_BillingFrequency().TX_LIST)
    {
    }
  }

  public class Every_4th_Month : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_BillingFrequency.Every_4th_Month>
  {
    public Every_4th_Month()
      : base("F")
    {
    }
  }

  public class Semi_Annual : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_BillingFrequency.Semi_Annual>
  {
    public Semi_Annual()
      : base("S")
    {
    }
  }

  public class Annual : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_BillingFrequency.Annual>
  {
    public Annual()
      : base("A")
    {
    }
  }

  public class Beg_Of_Contract : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_BillingFrequency.Beg_Of_Contract>
  {
    public Beg_Of_Contract()
      : base("B")
    {
    }
  }

  public class End_Of_Contract : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_BillingFrequency.End_Of_Contract>
  {
    public End_Of_Contract()
      : base("E")
    {
    }
  }

  public class Days_30_60_90 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_BillingFrequency.Days_30_60_90>
  {
    public Days_30_60_90()
      : base("D")
    {
    }
  }

  public class Time_Of_Service : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_BillingFrequency.Time_Of_Service>
  {
    public Time_Of_Service()
      : base("T")
    {
    }
  }

  public class None : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Contract_BillingFrequency.None>
  {
    public None()
      : base("N")
    {
    }
  }

  public class Monthly : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Contract_BillingFrequency.Monthly>
  {
    public Monthly()
      : base("M")
    {
    }
  }
}
