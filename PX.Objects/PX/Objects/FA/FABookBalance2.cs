// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABookBalance2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXProjection(typeof (Select2<FABookBalance, InnerJoin<FixedAsset, On<FABookBalance.assetID, Equal<FixedAsset.assetID>>, InnerJoin<PX.Objects.GL.Branch, On<FixedAsset.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<FABook, On<FABookBalance.bookID, Equal<FABook.bookID>>, LeftJoin<FABookPeriod, On<FABookPeriod.bookID, Equal<FABookBalance.bookID>, And<FABookPeriod.organizationID, Equal<IIf<Where<FABook.updateGL, Equal<True>>, PX.Objects.GL.Branch.organizationID, FinPeriod.organizationID.masterValue>>, And<FABookPeriod.finPeriodID, Equal<FABookBalance.deprFromPeriod>>>>, LeftJoin<FABookBalance2.FABookPeriod2, On<FABookBalance2.FABookPeriod2.bookID, Equal<FABookBalance.bookID>, And<FABookBalance2.FABookPeriod2.organizationID, Equal<IIf<Where<FABook.updateGL, Equal<True>>, PX.Objects.GL.Branch.organizationID, FinPeriod.organizationID.masterValue>>, And<FABookBalance2.FABookPeriod2.finPeriodID, Equal<FABookBalance.deprToPeriod>>>>>>>>>, Where<FixedAsset.depreciable, Equal<True>, And<FixedAsset.underConstruction, NotEqual<True>>>>))]
[PXHidden]
[Serializable]
public class FABookBalance2 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBCalced(typeof (Sub<Add<int1, FABookBalance2.FABookPeriod2.finYear>, FABookPeriod.finYear>), typeof (int))]
  public virtual int? ExactLife { get; set; }

  public abstract class exactLife : BqlType<IBqlInt, int>.Field<
  #nullable disable
  FABookBalance2.exactLife>
  {
  }

  [PXHidden]
  [Serializable]
  public class FABookPeriod2 : FABookPeriod
  {
    public new abstract class bookID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FABookBalance2.FABookPeriod2.bookID>
    {
    }

    public new abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FABookBalance2.FABookPeriod2.organizationID>
    {
    }

    public new abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FABookBalance2.FABookPeriod2.finPeriodID>
    {
    }

    public new abstract class finYear : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FABookBalance2.FABookPeriod2.finYear>
    {
    }
  }
}
