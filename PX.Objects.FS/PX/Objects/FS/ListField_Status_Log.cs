// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Status_Log
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Status_Log : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Status_Log().ID_LIST, new ID.Status_Log().TX_LIST)
    {
    }
  }

  public class Completed : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_Log.Completed>
  {
    public Completed()
      : base("C")
    {
    }
  }

  public class InProcess : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_Log.InProcess>
  {
    public InProcess()
      : base("P")
    {
    }
  }

  public class Paused : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Status_Log.Paused>
  {
    public Paused()
      : base("S")
    {
    }
  }
}
