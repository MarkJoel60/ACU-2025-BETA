// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_AppResizePrecision
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_AppResizePrecision : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXIntListAttribute
  {
    public ListAtrribute()
      : base(new ID.AppResizePrecision_Setup().ID_LIST, new ID.AppResizePrecision_Setup().TX_LIST)
    {
    }
  }

  public class MINUTES_10 : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  ListField_AppResizePrecision.MINUTES_10>
  {
    public MINUTES_10()
      : base(10)
    {
    }
  }

  public class MINUTES_15 : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ListField_AppResizePrecision.MINUTES_15>
  {
    public MINUTES_15()
      : base(15)
    {
    }
  }

  public class MINUTES_30 : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ListField_AppResizePrecision.MINUTES_30>
  {
    public MINUTES_30()
      : base(30)
    {
    }
  }

  public class MINUTES_60 : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ListField_AppResizePrecision.MINUTES_60>
  {
    public MINUTES_60()
      : base(60)
    {
    }
  }
}
