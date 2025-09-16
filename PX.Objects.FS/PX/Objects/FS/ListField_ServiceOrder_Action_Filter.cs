// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_ServiceOrder_Action_Filter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_ServiceOrder_Action_Filter : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.ServiceOrder_Action_Filter().ID_LIST, new ID.ServiceOrder_Action_Filter().TX_LIST)
    {
    }
  }

  public class Undefined : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_ServiceOrder_Action_Filter.Undefined>
  {
    public Undefined()
      : base("UD")
    {
    }
  }

  public class Complete : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ServiceOrder_Action_Filter.Complete>
  {
    public Complete()
      : base("CO")
    {
    }
  }

  public class Cancel : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ServiceOrder_Action_Filter.Cancel>
  {
    public Cancel()
      : base("CA")
    {
    }
  }

  public class Reopen : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ServiceOrder_Action_Filter.Reopen>
  {
    public Reopen()
      : base("RE")
    {
    }
  }

  public class Close : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ServiceOrder_Action_Filter.Close>
  {
    public Close()
      : base("CL")
    {
    }
  }

  public class Unclose : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ServiceOrder_Action_Filter.Unclose>
  {
    public Unclose()
      : base("UN")
    {
    }
  }

  public class AllowInvoice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ServiceOrder_Action_Filter.AllowInvoice>
  {
    public AllowInvoice()
      : base("AL")
    {
    }
  }
}
