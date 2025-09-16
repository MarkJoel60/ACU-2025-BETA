// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_RecordType_ContractSchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_RecordType_ContractSchedule : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.RecordType_ServiceContract().ID_LIST, new ID.RecordType_ServiceContract().TX_LIST)
    {
    }
  }

  public class ServiceContract : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_RecordType_ContractSchedule.ServiceContract>
  {
    public ServiceContract()
      : base("NRSC")
    {
    }
  }

  public class RouteServiceContract : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_RecordType_ContractSchedule.RouteServiceContract>
  {
    public RouteServiceContract()
      : base("IRSC")
    {
    }
  }

  public class EmployeeScheduleContract : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_RecordType_ContractSchedule.EmployeeScheduleContract>
  {
    public EmployeeScheduleContract()
      : base("EPSC")
    {
    }
  }
}
