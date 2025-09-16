// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookBalanceUpdateGL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXProjection(typeof (Select2<PX.Objects.FA.Standalone.FABookBalance, LeftJoin<FABook, On<FABook.bookID, Equal<PX.Objects.FA.Standalone.FABookBalance.bookID>>>, Where<FABook.updateGL, Equal<True>>>))]
[PXHidden]
[Serializable]
public class FABookBalanceUpdateGL : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.FA.Standalone.FABookBalance.assetID))]
  public virtual int? AssetID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.FA.Standalone.FABookBalance.bookID))]
  public virtual int? BookID { get; set; }

  public abstract class assetID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  FABookBalanceUpdateGL.assetID>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABookBalanceUpdateGL.bookID>
  {
  }
}
