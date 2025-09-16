// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Appointment_Service_Action_Type
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Appointment_Service_Action_Type : ListField_Service_Action_Type
{
  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new ID.Service_Action_Type().ID_LIST, new ID.Appointment_Service_Action_Type().TX_LIST)
    {
    }
  }

  public new class No_Items_Related : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_Appointment_Service_Action_Type.No_Items_Related>
  {
    public No_Items_Related()
      : base("N")
    {
    }
  }

  public new class Picked_Up_Items : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Appointment_Service_Action_Type.Picked_Up_Items>
  {
    public Picked_Up_Items()
      : base("P")
    {
    }
  }

  public new class Delivered_Items : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Appointment_Service_Action_Type.Delivered_Items>
  {
    public Delivered_Items()
      : base("D")
    {
    }
  }
}
