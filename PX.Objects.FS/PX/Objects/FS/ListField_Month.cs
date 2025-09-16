// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Month
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Month : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Months().ID_LIST, new ID.Months().TX_LIST)
    {
    }
  }

  public class JANUARY : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.JANUARY>
  {
    public JANUARY()
      : base("JAN")
    {
    }
  }

  public class FEBRUARY : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.FEBRUARY>
  {
    public FEBRUARY()
      : base("FEB")
    {
    }
  }

  public class MARCH : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.MARCH>
  {
    public MARCH()
      : base("MAR")
    {
    }
  }

  public class APRIL : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.APRIL>
  {
    public APRIL()
      : base("APR")
    {
    }
  }

  public class MAY : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.MAY>
  {
    public MAY()
      : base(nameof (MAY))
    {
    }
  }

  public class JUNE : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.JUNE>
  {
    public JUNE()
      : base("JUN")
    {
    }
  }

  public class JULY : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.JULY>
  {
    public JULY()
      : base("JUL")
    {
    }
  }

  public class AUGUST : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.AUGUST>
  {
    public AUGUST()
      : base("AUG")
    {
    }
  }

  public class SEPTEMBER : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.SEPTEMBER>
  {
    public SEPTEMBER()
      : base("SEP")
    {
    }
  }

  public class OCTOBER : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.OCTOBER>
  {
    public OCTOBER()
      : base("OCT")
    {
    }
  }

  public class NOVEMBER : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.NOVEMBER>
  {
    public NOVEMBER()
      : base("NOV")
    {
    }
  }

  public class DECEMBER : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Month.DECEMBER>
  {
    public DECEMBER()
      : base("DEC")
    {
    }
  }
}
