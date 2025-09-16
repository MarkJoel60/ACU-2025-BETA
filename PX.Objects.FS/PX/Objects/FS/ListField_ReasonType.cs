// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_ReasonType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_ReasonType : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.ReasonType().ID_LIST, new ID.ReasonType().TX_LIST)
    {
    }
  }

  public class Cancel_Service_Order : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_ReasonType.Cancel_Service_Order>
  {
    public Cancel_Service_Order()
      : base("CSOR")
    {
    }
  }

  public class Cancel_Appointment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ReasonType.Cancel_Appointment>
  {
    public Cancel_Appointment()
      : base("CAPP")
    {
    }
  }

  public class Workflow_Stage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ReasonType.Workflow_Stage>
  {
    public Workflow_Stage()
      : base("WSTG")
    {
    }
  }

  public class Appointment_Detail : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ReasonType.Appointment_Detail>
  {
    public Appointment_Detail()
      : base("APPD")
    {
    }
  }

  public class General : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_ReasonType.General>
  {
    public General()
      : base("GNRL")
    {
    }
  }
}
