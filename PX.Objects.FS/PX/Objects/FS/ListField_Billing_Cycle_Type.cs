// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Billing_Cycle_Type
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Billing_Cycle_Type : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Billing_Cycle_Type().ID_LIST, new ID.Billing_Cycle_Type().TX_LIST)
    {
    }
  }

  public class Appointment : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_Billing_Cycle_Type.Appointment>
  {
    public Appointment()
      : base("AP")
    {
    }
  }

  public class ServiceOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Billing_Cycle_Type.ServiceOrder>
  {
    public ServiceOrder()
      : base("SO")
    {
    }
  }

  public class TimeFrame : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Billing_Cycle_Type.TimeFrame>
  {
    public TimeFrame()
      : base("TC")
    {
    }
  }

  public class PurchaseOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Billing_Cycle_Type.PurchaseOrder>
  {
    public PurchaseOrder()
      : base("PO")
    {
    }
  }

  public class WorkOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Billing_Cycle_Type.WorkOrder>
  {
    public WorkOrder()
      : base("WO")
    {
    }
  }
}
