// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_Frequency_Type
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_Frequency_Type : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.Frequency_Type().ID_LIST, new ID.Frequency_Type().TX_LIST)
    {
    }
  }

  public class Weekly : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_Frequency_Type.Weekly>
  {
    public Weekly()
      : base("WK")
    {
    }
  }

  public class Monthly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Frequency_Type.Monthly>
  {
    public Monthly()
      : base("MT")
    {
    }
  }

  public class None : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_Frequency_Type.None>
  {
    public None()
      : base("NO")
    {
    }
  }
}
