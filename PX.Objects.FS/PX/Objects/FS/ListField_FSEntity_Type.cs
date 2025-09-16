// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_FSEntity_Type
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_FSEntity_Type
{
  public const 
  #nullable disable
  string ServiceOrder = "PX.Objects.FS.FSServiceOrder";
  public const string Appointment = "PX.Objects.FS.FSAppointment";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("PX.Objects.FS.FSServiceOrder", "Service Order"),
        PXStringListAttribute.Pair("PX.Objects.FS.FSAppointment", "Appointment")
      })
    {
    }
  }

  public class serviceOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_FSEntity_Type.serviceOrder>
  {
    public serviceOrder()
      : base("PX.Objects.FS.FSServiceOrder")
    {
    }
  }

  public class appointment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_FSEntity_Type.appointment>
  {
    public appointment()
      : base("PX.Objects.FS.FSAppointment")
    {
    }
  }
}
