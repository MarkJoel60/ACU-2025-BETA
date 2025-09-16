// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_SrvOrdType_GenerateInvoiceBy
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_SrvOrdType_GenerateInvoiceBy : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.SrvOrdType_GenerateInvoiceBy().ID_LIST, new ID.SrvOrdType_GenerateInvoiceBy().TX_LIST)
    {
    }
  }

  public class CrmAr : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_SrvOrdType_GenerateInvoiceBy.CrmAr>
  {
    public CrmAr()
      : base("CRAR")
    {
    }
  }

  public class SalesOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SrvOrdType_GenerateInvoiceBy.SalesOrder>
  {
    public SalesOrder()
      : base("SORD")
    {
    }
  }

  public class Project : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SrvOrdType_GenerateInvoiceBy.Project>
  {
    public Project()
      : base("PROJ")
    {
    }
  }

  public class NotBillable : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SrvOrdType_GenerateInvoiceBy.NotBillable>
  {
    public NotBillable()
      : base("NBIL")
    {
    }
  }
}
