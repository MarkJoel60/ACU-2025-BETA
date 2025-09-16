// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DeprBookFilter
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
public class DeprBookFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BookID;

  [PXDBInt]
  [PXSelector(typeof (Search2<FABook.bookID, InnerJoin<FABookBalance, On<FABookBalance.bookID, Equal<FABook.bookID>>>, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  [PXDefault(typeof (Search2<FABookBalance.bookID, LeftJoin<FixedAsset, On<FABookBalance.assetID, Equal<FixedAsset.assetID>>>, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>, And<Current<FixedAsset.depreciable>, Equal<True>, And<Current<FixedAsset.underConstruction>, NotEqual<True>>>>>))]
  [PXUIField(DisplayName = "Book")]
  public int? BookID
  {
    get => this._BookID;
    set => this._BookID = value;
  }

  public abstract class bookID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  DeprBookFilter.bookID>
  {
  }
}
