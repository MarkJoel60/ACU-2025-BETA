// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSEntityType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public class FSEntityType
{
  public const 
  #nullable disable
  string ServiceOrder = "FSSO";
  public const string Appointment = "FSAP";
  public const string ServiceContract = "FSSC";
  public const string SalesOrder = "PXSO";
  public const string SOInvoice = "PXSI";
  public const string ARInvoice = "PXAR";
  public const string APInvoice = "PXAP";
  public const string PMRegister = "PXPM";
  public const string INReceipt = "PXIR";
  public const string INIssue = "PXIS";
  public const string SOCreditMemo = "PXSM";
  public const string ARCreditMemo = "PXAM";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[12]
      {
        PXStringListAttribute.Pair("FSSO", "Service Order"),
        PXStringListAttribute.Pair("FSAP", "Appointment"),
        PXStringListAttribute.Pair("FSSC", "Service Contract"),
        PXStringListAttribute.Pair("PXSO", "Sales Order"),
        PXStringListAttribute.Pair("PXSI", "SO Invoice"),
        PXStringListAttribute.Pair("PXAR", "AR Invoice"),
        PXStringListAttribute.Pair("PXAP", "AP Invoice"),
        PXStringListAttribute.Pair("PXPM", "Project Transaction"),
        PXStringListAttribute.Pair("PXIR", "Receipt"),
        PXStringListAttribute.Pair("PXIS", "Issue"),
        PXStringListAttribute.Pair("PXSM", "SO Credit Memo"),
        PXStringListAttribute.Pair("PXAM", "AR Credit Memo")
      })
    {
    }
  }

  public class serviceOrder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.serviceOrder>
  {
    public serviceOrder()
      : base("FSSO")
    {
    }
  }

  public class appointment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.appointment>
  {
    public appointment()
      : base("FSAP")
    {
    }
  }

  public class serviceContract : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.serviceContract>
  {
    public serviceContract()
      : base("FSSC")
    {
    }
  }

  public class salesOrder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.salesOrder>
  {
    public salesOrder()
      : base("PXSO")
    {
    }
  }

  public class soInvoice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.soInvoice>
  {
    public soInvoice()
      : base("PXSI")
    {
    }
  }

  public class arInvoice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.arInvoice>
  {
    public arInvoice()
      : base("PXAR")
    {
    }
  }

  public class apInvoice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.apInvoice>
  {
    public apInvoice()
      : base("PXAP")
    {
    }
  }

  public class pmRegister : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.pmRegister>
  {
    public pmRegister()
      : base("PXPM")
    {
    }
  }

  public class inIssue : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.inIssue>
  {
    public inIssue()
      : base("PXIS")
    {
    }
  }

  public class inReceipt : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.inReceipt>
  {
    public inReceipt()
      : base("PXIR")
    {
    }
  }

  public class soCreditMemo : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.soCreditMemo>
  {
    public soCreditMemo()
      : base("PXSM")
    {
    }
  }

  public class arCreditMemo : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSEntityType.arCreditMemo>
  {
    public arCreditMemo()
      : base("PXAM")
    {
    }
  }
}
