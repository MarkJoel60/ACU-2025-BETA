// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookHistory2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXHidden]
[Serializable]
public class FABookHistory2 : FABookHistory
{
  public new abstract class assetID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  FABookHistory2.assetID>
  {
  }

  public new abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookHistory2.bookID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABookHistory2.finPeriodID>
  {
  }

  public new abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookHistory2.closed>
  {
  }

  public new abstract class suspended : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABookHistory2.suspended>
  {
  }
}
