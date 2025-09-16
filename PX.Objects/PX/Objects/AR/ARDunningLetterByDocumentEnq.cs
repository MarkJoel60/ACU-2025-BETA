// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningLetterByDocumentEnq
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

public class ARDunningLetterByDocumentEnq : PXGraph<
#nullable disable
ARDunningLetterByDocumentEnq>
{
  public PXFilter<ARDunningLetterByDocumentEnq.DLByDocumentFilter> Filter;
  public PXCancel<ARDunningLetterByDocumentEnq.DLByDocumentFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<ARDunningLetterDetail, InnerJoin<ARDunningLetter, On<ARDunningLetterDetail.dunningLetterID, Equal<ARDunningLetter.dunningLetterID>>, InnerJoin<ARInvoice, On<ARDunningLetterDetail.docType, Equal<ARInvoice.docType>, And<ARDunningLetterDetail.refNbr, Equal<ARInvoice.refNbr>>>>>, Where2<Where<ARDunningLetter.bAccountID, Equal<Current<ARDunningLetterByDocumentEnq.DLByDocumentFilter.bAccountID>>, Or<Current<ARDunningLetterByDocumentEnq.DLByDocumentFilter.bAccountID>, IsNull>>, And<ARDunningLetter.dunningLetterDate, GreaterEqual<Current<ARDunningLetterByDocumentEnq.DLByDocumentFilter.beginDate>>, And<ARDunningLetter.dunningLetterDate, LessEqual<Current<ARDunningLetterByDocumentEnq.DLByDocumentFilter.endDate>>, And2<Where<ARDunningLetter.dunningLetterLevel, GreaterEqual<Current<ARDunningLetterByDocumentEnq.DLByDocumentFilter.levelFrom>>, Or<Current<ARDunningLetterByDocumentEnq.DLByDocumentFilter.levelFrom>, IsNull>>, And<Where<ARDunningLetter.dunningLetterLevel, LessEqual<Current<ARDunningLetterByDocumentEnq.DLByDocumentFilter.levelTo>>, Or<Current<ARDunningLetterByDocumentEnq.DLByDocumentFilter.levelTo>, IsNull>>>>>>>, OrderBy<Asc<ARInvoice.dueDate>>> EnqResults;
  public PXSelect<ARDunningLetter> letter;
  public PXSelect<ARInvoice> inv;
  public PXAction<ARDunningLetterByDocumentEnq.DLByDocumentFilter> ViewDocument;
  public PXAction<ARDunningLetterByDocumentEnq.DLByDocumentFilter> ViewLetter;

  public ARDunningLetterByDocumentEnq()
  {
    ((PXSelectBase) this.EnqResults).AllowDelete = false;
    ((PXSelectBase) this.EnqResults).AllowInsert = false;
    ((PXSelectBase) this.EnqResults).AllowUpdate = false;
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Dunning Letter Status")]
  protected virtual void ARDunningLetter_Status_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDunningLetterDetail>) this.EnqResults).Current != null)
    {
      ARInvoice arInvoice = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.refNbr, Equal<Required<ARDunningLetterDetail.refNbr>>, And<ARInvoice.docType, Equal<Required<ARDunningLetterDetail.docType>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) ((PXSelectBase<ARDunningLetterDetail>) this.EnqResults).Current.RefNbr,
        (object) ((PXSelectBase<ARDunningLetterDetail>) this.EnqResults).Current.DocType
      }));
      if (arInvoice != null)
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<ARInvoice>) instance.Document).Current = arInvoice;
        PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewLetter(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDunningLetterDetail>) this.EnqResults).Current != null)
    {
      PXResultset<ARDunningLetter> pxResultset = PXSelectBase<ARDunningLetter, PXSelect<ARDunningLetter, Where<ARDunningLetter.dunningLetterID, Equal<Required<ARDunningLetter.dunningLetterID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<ARDunningLetterDetail>) this.EnqResults).Current.DunningLetterID
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

  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Balance")]
  protected virtual void ARInvoice_DocBal_CacheAttached(PXCache sender)
  {
  }

  [Serializable]
  public class DLByDocumentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _BeginDate;
    protected DateTime? _EndDate;
    protected int? _BAccountID;
    protected int? _LevelFrom;
    protected int? _LevelTo;

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

    [PXDBInt]
    [PXUIField(DisplayName = "From")]
    [PXDefault(1)]
    public virtual int? LevelFrom
    {
      get => this._LevelFrom;
      set => this._LevelFrom = value;
    }

    [PXDBInt]
    [PXUIField(DisplayName = "To")]
    [PXDefault(typeof (Search<ARDunningCustomerClass.dunningLetterLevel, Where<True, Equal<True>>, OrderBy<Desc<ARDunningCustomerClass.dunningLetterLevel>>>))]
    public virtual int? LevelTo
    {
      get => this._LevelTo;
      set => this._LevelTo = value;
    }

    public abstract class beginDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterByDocumentEnq.DLByDocumentFilter.beginDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterByDocumentEnq.DLByDocumentFilter.endDate>
    {
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterByDocumentEnq.DLByDocumentFilter.bAccountID>
    {
    }

    public abstract class levelFrom : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterByDocumentEnq.DLByDocumentFilter.levelFrom>
    {
    }

    public abstract class levelTo : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterByDocumentEnq.DLByDocumentFilter.levelTo>
    {
    }
  }
}
