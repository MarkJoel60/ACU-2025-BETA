// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_FSPOSource
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

public abstract class ListField_FSPOSource
{
  public const 
  #nullable disable
  string PurchaseToServiceOrder = "O";
  public const string PurchaseToAppointment = "A";

  public class ListAttribute : PXStringListAttribute
  {
    public static List<Tuple<string, string>> FullList = new List<Tuple<string, string>>()
    {
      new Tuple<string, string>("O", "Purchase to Service Order"),
      new Tuple<string, string>("A", "Purchase to Appointment")
    };

    public ListAttribute()
      : base(ListField_FSPOSource.ListAttribute.FullList.ToArray())
    {
    }
  }

  public class purchaseToServiceOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_FSPOSource.purchaseToServiceOrder>
  {
    public purchaseToServiceOrder()
      : base("O")
    {
    }
  }

  public class purchaseToAppointment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_FSPOSource.purchaseToAppointment>
  {
    public purchaseToAppointment()
      : base("A")
    {
    }
  }
}
