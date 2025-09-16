// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_WarrantyDurationType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_WarrantyDurationType : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.WarrantyDurationType().ID_LIST, new ID.WarrantyDurationType().TX_LIST)
    {
    }
  }

  public class Day : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_WarrantyDurationType.Day>
  {
    public Day()
      : base("D")
    {
    }
  }

  public class Month : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_WarrantyDurationType.Month>
  {
    public Month()
      : base("M")
    {
    }
  }

  public class Year : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_WarrantyDurationType.Year>
  {
    public Year()
      : base("Y")
    {
    }
  }
}
