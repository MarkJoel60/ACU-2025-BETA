// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.TranBookFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class TranBookFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BookID;

  [PXDBInt]
  [PXSelector(typeof (Search2<FABook.bookID, InnerJoin<FABookBalance, On<FABookBalance.bookID, Equal<FABook.bookID>>>, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXUIField(DisplayName = "Book")]
  public virtual int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  public abstract class bookID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  TranBookFilter.bookID>
  {
  }
}
