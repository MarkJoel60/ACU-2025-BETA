// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_PostDoc_EntityType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_PostDoc_EntityType : IBqlField, IBqlOperand
{
  public const 
  #nullable disable
  string SERVICE_ORDER = "SO";
  public const string APPOINTMENT = "AP";

  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.PostDoc_EntityType().ID_LIST, new ID.PostDoc_EntityType().TX_LIST)
    {
    }
  }

  public class Appointment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_PostDoc_EntityType.Appointment>
  {
    public Appointment()
      : base("AP")
    {
    }
  }

  public class Service_Order : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_PostDoc_EntityType.Service_Order>
  {
    public Service_Order()
      : base("SO")
    {
    }
  }
}
