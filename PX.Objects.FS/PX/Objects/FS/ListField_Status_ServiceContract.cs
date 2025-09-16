// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Status_ServiceContract
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Status_ServiceContract : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Status_ServiceContract().ID_LIST, new ID.Status_ServiceContract().TX_LIST)
    {
    }
  }

  public class Draft : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_ServiceContract.Draft>
  {
    public Draft()
      : base("D")
    {
    }
  }

  public class Active : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_ServiceContract.Active>
  {
    public Active()
      : base("A")
    {
    }
  }

  public class Suspended : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_ServiceContract.Suspended>
  {
    public Suspended()
      : base("S")
    {
    }
  }

  public class Canceled : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_ServiceContract.Canceled>
  {
    public Canceled()
      : base("X")
    {
    }
  }

  public class Expired : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_Status_ServiceContract.Expired>
  {
    public Expired()
      : base("E")
    {
    }
  }
}
