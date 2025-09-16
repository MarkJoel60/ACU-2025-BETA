// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_SourceType_Equipment
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_SourceType_Equipment : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.SourceType_Equipment().ID_LIST, new ID.SourceType_Equipment().TX_LIST)
    {
    }
  }

  public class SM_Equipment : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_SourceType_Equipment.SM_Equipment>
  {
    public SM_Equipment()
      : base("SME")
    {
    }
  }

  public class Vehicle : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_SourceType_Equipment.Vehicle>
  {
    public Vehicle()
      : base("VEH")
    {
    }
  }

  public class EP_Equipment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SourceType_Equipment.EP_Equipment>
  {
    public EP_Equipment()
      : base("EPE")
    {
    }
  }

  public class AR_INVOICE : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_SourceType_Equipment.AR_INVOICE>
  {
    public AR_INVOICE()
      : base("ARI")
    {
    }
  }
}
