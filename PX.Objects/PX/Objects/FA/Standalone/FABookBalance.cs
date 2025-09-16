// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.Standalone.FABookBalance
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FA.Standalone;

public class FABookBalance : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public virtual int? AssetID { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? BookID { get; set; }

  [PXDBString(6, IsFixed = true, InputMask = "")]
  public virtual 
  #nullable disable
  string LastDeprPeriod { get; set; }

  [PXDBString(6, IsFixed = true)]
  public virtual string CurrDeprPeriod { get; set; }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalance.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalance.bookID>
  {
  }

  public abstract class lastDeprPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalance.lastDeprPeriod>
  {
  }

  public abstract class currDeprPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookBalance.currDeprPeriod>
  {
  }
}
