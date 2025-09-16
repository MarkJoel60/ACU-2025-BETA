// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Status_ContractPeriod
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Status_ContractPeriod : IBqlField, IBqlOperand
{
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new ID.Status_ContractPeriod().ID_LIST, new ID.Status_ContractPeriod().TX_LIST)
    {
    }
  }

  public class Active : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_ContractPeriod.Active>
  {
    public Active()
      : base("A")
    {
    }
  }

  public class Inactive : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_ContractPeriod.Inactive>
  {
    public Inactive()
      : base("I")
    {
    }
  }

  public class Pending : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_ContractPeriod.Pending>
  {
    public Pending()
      : base("P")
    {
    }
  }

  public class Invoiced : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_ContractPeriod.Invoiced>
  {
    public Invoiced()
      : base("N")
    {
    }
  }
}
