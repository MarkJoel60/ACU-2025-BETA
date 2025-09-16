// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_SourceType_ServiceOrder
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_SourceType_ServiceOrder : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.SourceType_ServiceOrder().ID_LIST, new ID.SourceType_ServiceOrder().TX_LIST)
    {
    }
  }

  public class Case : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_SourceType_ServiceOrder.Case>
  {
    public Case()
      : base("CR")
    {
    }
  }

  public class Opportunity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SourceType_ServiceOrder.Opportunity>
  {
    public Opportunity()
      : base("OP")
    {
    }
  }

  public class SalesOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SourceType_ServiceOrder.SalesOrder>
  {
    public SalesOrder()
      : base("SO")
    {
    }
  }

  public class ServiceDispatch : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SourceType_ServiceOrder.ServiceDispatch>
  {
    public ServiceDispatch()
      : base("SD")
    {
    }
  }
}
