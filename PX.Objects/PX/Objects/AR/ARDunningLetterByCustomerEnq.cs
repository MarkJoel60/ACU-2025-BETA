// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningLetterByCustomerEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.AR;

public class ARDunningLetterByCustomerEnq : PXGraph<
#nullable disable
ARDunningLetterByCustomerEnq>
{
  public PXAction<ARDunningLetterByCustomerEnq.DLByCustomerFilter> ViewDocument;
  public PXFilter<ARDunningLetterByCustomerEnq.DLByCustomerFilter> Filter;
  public PXCancel<ARDunningLetterByCustomerEnq.DLByCustomerFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXSelectJoinGroupBy<ARDunningLetter, LeftJoin<Customer, On<Customer.bAccountID, Equal<ARDunningLetter.bAccountID>>, LeftJoin<ARDunningLetterDetail, On<ARDunningLetterDetail.dunningLetterID, Equal<ARDunningLetter.dunningLetterID>>>>, Where2<Where<ARDunningLetter.bAccountID, Equal<Current<ARDunningLetterByCustomerEnq.DLByCustomerFilter.bAccountID>>, Or<Current<ARDunningLetterByCustomerEnq.DLByCustomerFilter.bAccountID>, IsNull>>, And<ARDunningLetter.dunningLetterDate, GreaterEqual<Current<ARDunningLetterByCustomerEnq.DLByCustomerFilter.beginDate>>, And<ARDunningLetter.dunningLetterDate, LessEqual<Current<ARDunningLetterByCustomerEnq.DLByCustomerFilter.endDate>>>>>, Aggregate<GroupBy<ARDunningLetter.dunningLetterID, Sum<ARDunningLetterDetail.overdueBal, Count<ARDunningLetterDetail.refNbr>>>>, OrderBy<Asc<ARDunningLetter.dunningLetterDate>>> EnqResults;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDunningLetter>) this.EnqResults).Current != null)
    {
      PXResultset<ARDunningLetter> pxResultset = PXSelectBase<ARDunningLetter, PXSelect<ARDunningLetter, Where<ARDunningLetter.dunningLetterID, Equal<Required<ARDunningLetter.dunningLetterID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<ARDunningLetter>) this.EnqResults).Current.DunningLetterID
      });
      if (pxResultset != null)
      {
        ARDunningLetterMaint instance = PXGraph.CreateInstance<ARDunningLetterMaint>();
        ((PXSelectBase<ARDunningLetter>) instance.Document).Current = PXResultset<ARDunningLetter>.op_Implicit(pxResultset);
        PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 1);
      }
    }
    return adapter.Get();
  }

  [PXCustomizeBaseAttribute]
  protected virtual void _(Events.CacheAttached<ARDunningLetter.branchID> e)
  {
  }

  public ARDunningLetterByCustomerEnq()
  {
    ((PXSelectBase) this.EnqResults).AllowDelete = false;
    ((PXSelectBase) this.EnqResults).AllowInsert = false;
    ((PXSelectBase) this.EnqResults).AllowUpdate = false;
  }

  public virtual IEnumerable enqResults()
  {
    foreach (PXResult<ARDunningLetter> pxResult in PXSelectBase<ARDunningLetter, PXSelectJoinGroupBy<ARDunningLetter, LeftJoin<Customer, On<Customer.bAccountID, Equal<ARDunningLetter.bAccountID>>, LeftJoin<ARDunningLetterDetail, On<ARDunningLetterDetail.dunningLetterID, Equal<ARDunningLetter.dunningLetterID>>>>, Where2<Where<ARDunningLetter.bAccountID, Equal<Current<ARDunningLetterByCustomerEnq.DLByCustomerFilter.bAccountID>>, Or<Current<ARDunningLetterByCustomerEnq.DLByCustomerFilter.bAccountID>, IsNull>>, And<ARDunningLetter.dunningLetterDate, GreaterEqual<Current<ARDunningLetterByCustomerEnq.DLByCustomerFilter.beginDate>>, And<ARDunningLetter.dunningLetterDate, LessEqual<Current<ARDunningLetterByCustomerEnq.DLByCustomerFilter.endDate>>>>>, Aggregate<GroupBy<ARDunningLetter.dunningLetterID, GroupBy<ARDunningLetter.released, GroupBy<ARDunningLetter.voided, Sum<ARDunningLetterDetail.overdueBal, Count<ARDunningLetterDetail.refNbr>>>>>>, OrderBy<Asc<ARDunningLetter.dunningLetterDate>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      PXResult<ARDunningLetter>.op_Implicit(pxResult).DetailsCount = ((PXResult) pxResult).RowCount;
      yield return (object) pxResult;
    }
  }

  [Serializable]
  public class DLByCustomerFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _BeginDate;
    protected DateTime? _EndDate;
    protected int? _BAccountID;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Start Date")]
    public virtual DateTime? BeginDate
    {
      get => this._BeginDate;
      set => this._BeginDate = value;
    }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "End Date")]
    public virtual DateTime? EndDate
    {
      get => this._EndDate;
      set => this._EndDate = value;
    }

    [Customer]
    public virtual int? BAccountID
    {
      get => this._BAccountID;
      set => this._BAccountID = value;
    }

    public abstract class beginDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterByCustomerEnq.DLByCustomerFilter.beginDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterByCustomerEnq.DLByCustomerFilter.endDate>
    {
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterByCustomerEnq.DLByCustomerFilter.bAccountID>
    {
    }
  }
}
