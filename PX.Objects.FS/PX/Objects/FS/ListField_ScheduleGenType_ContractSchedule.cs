// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_ScheduleGenType_ContractSchedule
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_ScheduleGenType_ContractSchedule : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.ScheduleGenType_ServiceContract().ID_LIST, new ID.ScheduleGenType_ServiceContract().TX_LIST)
    {
    }
  }

  public class ServiceOrder : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_ScheduleGenType_ContractSchedule.ServiceOrder>
  {
    public ServiceOrder()
      : base("SO")
    {
    }
  }

  public class Appointment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ScheduleGenType_ContractSchedule.Appointment>
  {
    public Appointment()
      : base("AP")
    {
    }
  }

  public class None : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ScheduleGenType_ContractSchedule.None>
  {
    public None()
      : base("NA")
    {
    }
  }
}
