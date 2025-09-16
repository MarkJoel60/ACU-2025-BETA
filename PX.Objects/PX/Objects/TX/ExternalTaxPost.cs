// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ExternalTaxPost
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.TaxProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

#nullable enable
namespace PX.Objects.TX;

[Serializable]
public class ExternalTaxPost : PXGraph<
#nullable disable
ExternalTaxPost>
{
  [PXFilterable(new Type[] {})]
  public PXProcessing<ExternalTaxPost.Document> Items;
  public PXAction<ExternalTaxPost.Document> viewDocument;

  public IEnumerable items()
  {
    ExternalTaxPost externalTaxPost = this;
    bool found = false;
    foreach (ExternalTaxPost.Document document in ((PXSelectBase) externalTaxPost.Items).Cache.Inserted)
    {
      found = true;
      yield return (object) document;
    }
    if (!found)
    {
      PXSelectBase<PX.Objects.AR.ARInvoice> pxSelectBase = (PXSelectBase<PX.Objects.AR.ARInvoice>) new PXSelectJoin<PX.Objects.AR.ARInvoice, InnerJoin<TaxZone, On<TaxZone.taxZoneID, Equal<PX.Objects.AR.ARInvoice.taxZoneID>>>, Where<TaxZone.isExternal, Equal<True>, And<PX.Objects.AR.ARInvoice.isTaxValid, Equal<True>, And<PX.Objects.AR.ARInvoice.released, Equal<True>, And<PX.Objects.AR.ARInvoice.isTaxPosted, Equal<False>, And<PX.Objects.AR.ARInvoice.nonTaxable, Equal<False>>>>>>>((PXGraph) externalTaxPost);
      PXSelectBase<PX.Objects.AP.APInvoice> selectAP = (PXSelectBase<PX.Objects.AP.APInvoice>) new PXSelectJoin<PX.Objects.AP.APInvoice, InnerJoin<TaxZone, On<TaxZone.taxZoneID, Equal<PX.Objects.AP.APInvoice.taxZoneID>>>, Where<TaxZone.isExternal, Equal<True>, And<PX.Objects.AP.APInvoice.isTaxValid, Equal<True>, And<PX.Objects.AP.APInvoice.released, Equal<True>, And<PX.Objects.AP.APInvoice.isTaxPosted, Equal<False>, And<PX.Objects.AP.APInvoice.nonTaxable, Equal<False>>>>>>>((PXGraph) externalTaxPost);
      PXSelectBase<CAAdj> selectCA = (PXSelectBase<CAAdj>) new PXSelectJoin<CAAdj, InnerJoin<TaxZone, On<TaxZone.taxZoneID, Equal<CAAdj.taxZoneID>>>, Where<TaxZone.isExternal, Equal<True>, And<CAAdj.isTaxValid, Equal<True>, And<CAAdj.released, Equal<True>, And<CAAdj.isTaxPosted, Equal<False>, And<CAAdj.nonTaxable, Equal<False>>>>>>>((PXGraph) externalTaxPost);
      foreach (PXResult<PX.Objects.AR.ARInvoice> pxResult in pxSelectBase.Select(Array.Empty<object>()))
      {
        PX.Objects.AR.ARInvoice arInvoice = PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(pxResult);
        yield return (object) ((PXSelectBase<ExternalTaxPost.Document>) externalTaxPost.Items).Insert(new ExternalTaxPost.Document()
        {
          Module = "AR",
          TaxZoneID = arInvoice.TaxZoneID,
          DocType = arInvoice.DocType,
          RefNbr = arInvoice.RefNbr,
          BranchID = arInvoice.BranchID,
          DocDate = arInvoice.DocDate,
          FinPeriodID = arInvoice.FinPeriodID,
          Amount = arInvoice.CuryDocBal,
          CuryID = arInvoice.CuryID,
          DocDesc = arInvoice.DocDesc,
          DrCr = arInvoice.DrCr
        });
      }
      foreach (PXResult<PX.Objects.AP.APInvoice> pxResult in selectAP.Select(Array.Empty<object>()))
      {
        PX.Objects.AP.APInvoice apInvoice = PXResult<PX.Objects.AP.APInvoice>.op_Implicit(pxResult);
        yield return (object) ((PXSelectBase<ExternalTaxPost.Document>) externalTaxPost.Items).Insert(new ExternalTaxPost.Document()
        {
          Module = "AP",
          DocType = apInvoice.DocType,
          TaxZoneID = apInvoice.TaxZoneID,
          RefNbr = apInvoice.RefNbr,
          BranchID = apInvoice.BranchID,
          DocDate = apInvoice.DocDate,
          FinPeriodID = apInvoice.FinPeriodID,
          Amount = apInvoice.CuryDocBal,
          CuryID = apInvoice.CuryID,
          DocDesc = apInvoice.DocDesc,
          DrCr = apInvoice.DrCr
        });
      }
      foreach (PXResult<CAAdj> pxResult in selectCA.Select(Array.Empty<object>()))
      {
        CAAdj caAdj = PXResult<CAAdj>.op_Implicit(pxResult);
        yield return (object) ((PXSelectBase<ExternalTaxPost.Document>) externalTaxPost.Items).Insert(new ExternalTaxPost.Document()
        {
          Module = "CA",
          DocType = caAdj.DocType,
          TaxZoneID = caAdj.TaxZoneID,
          RefNbr = caAdj.RefNbr,
          BranchID = caAdj.BranchID,
          DocDate = caAdj.TranDate,
          FinPeriodID = caAdj.FinPeriodID,
          Amount = caAdj.CuryTranAmt,
          CuryID = caAdj.CuryID,
          DocDesc = caAdj.TranDesc,
          DrCr = caAdj.DrCr
        });
      }
      ((PXSelectBase) externalTaxPost.Items).Cache.IsDirty = false;
    }
  }

  public ExternalTaxPost()
  {
    ((PXProcessingBase<ExternalTaxPost.Document>) this.Items).SetSelected<ExternalTaxPost.Document.selected>();
    // ISSUE: method pointer
    ((PXProcessingBase<ExternalTaxPost.Document>) this.Items).SetProcessDelegate(new PXProcessingBase<ExternalTaxPost.Document>.ProcessListDelegate((object) null, __methodptr(Release)));
    this.Items.SetProcessCaption("Post");
    this.Items.SetProcessAllCaption("Post All");
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    ExternalTaxPost.Document current = ((PXSelectBase<ExternalTaxPost.Document>) this.Items).Current;
    if (current != null && !string.IsNullOrEmpty(current.Module) && !string.IsNullOrEmpty(current.DocType) && !string.IsNullOrEmpty(current.RefNbr))
    {
      switch (current.Module)
      {
        case "AR":
          ARInvoiceEntry instance1 = PXGraph.CreateInstance<ARInvoiceEntry>();
          ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance1.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance1.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) current.RefNbr, new object[1]
          {
            (object) current.DocType
          }));
          if (((PXSelectBase<PX.Objects.AR.ARInvoice>) instance1.Document).Current != null)
          {
            PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance1, true, "View Document");
            ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException;
          }
          break;
        case "AP":
          APInvoiceEntry instance2 = PXGraph.CreateInstance<APInvoiceEntry>();
          ((PXSelectBase<PX.Objects.AP.APInvoice>) instance2.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) instance2.Document).Search<PX.Objects.AP.APInvoice.refNbr>((object) current.RefNbr, new object[1]
          {
            (object) current.DocType
          }));
          if (((PXSelectBase<PX.Objects.AP.APInvoice>) instance2.Document).Current != null)
          {
            PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance2, true, "View Document");
            ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException;
          }
          break;
        case "CA":
          CATranEntry instance3 = PXGraph.CreateInstance<CATranEntry>();
          ((PXSelectBase<CAAdj>) instance3.CAAdjRecords).Current = PXResultset<CAAdj>.op_Implicit(((PXSelectBase<CAAdj>) instance3.CAAdjRecords).Search<CAAdj.adjRefNbr>((object) current.RefNbr, new object[1]
          {
            (object) current.DocType
          }));
          if (((PXSelectBase<CAAdj>) instance3.CAAdjRecords).Current != null)
          {
            PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance3, true, "View Document");
            ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException;
          }
          break;
      }
    }
    return adapter.Get();
  }

  public static void Release(List<ExternalTaxPost.Document> list)
  {
    ExternalTaxPost.ExternalTaxPostProcess instance = PXGraph.CreateInstance<ExternalTaxPost.ExternalTaxPostProcess>();
    for (int index = 0; index < list.Count; ++index)
    {
      ExternalTaxPost.Document doc = list[index];
      try
      {
        ((PXGraph) instance).Clear();
        instance.Post(doc);
        PXProcessing<ExternalTaxPost.Document>.SetInfo(index, "The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        PXProcessing<ExternalTaxPost.Document>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
      }
    }
  }

  protected virtual void Document_RowPersisting(PXCache sedner, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  [Serializable]
  public class Document : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Module;
    protected string _DocType;
    protected string _TaxZoneID;
    protected string _RefNbr;
    protected bool? _Selected = new bool?(false);
    protected int? _BranchID;
    protected DateTime? _DocDate;
    protected string _FinPeriodID;
    protected Decimal? _Amount;
    protected string _CuryID;
    protected string _DocDesc;
    protected string _DrCr;

    [PXString(2, IsFixed = true, IsKey = true)]
    [PXUIField(DisplayName = "Module")]
    [BatchModule.FullList]
    public virtual string Module
    {
      get => this._Module;
      set => this._Module = value;
    }

    [PXString(3, IsKey = true, IsFixed = true)]
    [PXUIField(DisplayName = "Doc. Type")]
    public virtual string DocType
    {
      get => this._DocType;
      set => this._DocType = value;
    }

    [PXString(10, IsUnicode = true)]
    [PXUIField]
    public virtual string TaxZoneID
    {
      get => this._TaxZoneID;
      set => this._TaxZoneID = value;
    }

    [PXString(15, IsUnicode = true, IsKey = true, InputMask = "")]
    [PXUIField(DisplayName = "Reference Nbr.")]
    public virtual string RefNbr
    {
      get => this._RefNbr;
      set => this._RefNbr = value;
    }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [Branch(null, null, true, true, true)]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [PXDate]
    [PXUIField(DisplayName = "Date")]
    public virtual DateTime? DocDate
    {
      get => this._DocDate;
      set => this._DocDate = value;
    }

    [PeriodID(null, null, null, true)]
    [PXUIField(DisplayName = "Fin. Period")]
    public virtual string FinPeriodID
    {
      get => this._FinPeriodID;
      set => this._FinPeriodID = value;
    }

    [PXBaseCury]
    [PXUIField]
    public virtual Decimal? Amount
    {
      get => this._Amount;
      set => this._Amount = value;
    }

    [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXUIField(DisplayName = "Currency")]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXString(256 /*0x0100*/, IsUnicode = true)]
    [PXUIField]
    public virtual string DocDesc
    {
      get => this._DocDesc;
      set => this._DocDesc = value;
    }

    [PXDBString(1, IsFixed = true)]
    public virtual string DrCr
    {
      get => this._DrCr;
      set => this._DrCr = value;
    }

    public abstract class module : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTaxPost.Document.module>
    {
    }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExternalTaxPost.Document.docType>
    {
    }

    public abstract class taxZoneID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExternalTaxPost.Document.taxZoneID>
    {
    }

    public abstract class refNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTaxPost.Document.refNbr>
    {
    }

    public abstract class selected : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ExternalTaxPost.Document.selected>
    {
    }

    public abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ExternalTaxPost.Document.branchID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ExternalTaxPost.Document.docDate>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExternalTaxPost.Document.finPeriodID>
    {
    }

    public abstract class amount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ExternalTaxPost.Document.amount>
    {
    }

    public abstract class curyID : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTaxPost.Document.curyID>
    {
    }

    public abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExternalTaxPost.Document.docDesc>
    {
    }

    public abstract class drCr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTaxPost.Document.drCr>
    {
    }
  }

  public class ExternalTaxPostProcess : PXGraph<ExternalTaxPost.ExternalTaxPostProcess>
  {
    protected readonly Func<PXGraph, string, ITaxProvider> TaxProviderFactory;

    public ExternalTaxPostProcess()
    {
      this.TaxProviderFactory = ExternalTaxBase<PXGraph>.TaxProviderFactory;
    }

    public void Post(ExternalTaxPost.Document doc)
    {
      if (!TaxPluginMaint.IsActive((PXGraph) this, doc.TaxZoneID))
        throw new PXException("External tax provider is not configured.");
      ITaxProvider itaxProvider = this.TaxProviderFactory((PXGraph) this, doc.TaxZoneID);
      CommitTaxRequest commitTaxRequest = this.BuildCommitTaxRequest(doc);
      if (commitTaxRequest == null)
        return;
      CommitTaxResult commitTaxResult = itaxProvider.CommitTax(commitTaxRequest);
      if (((ResultBase) commitTaxResult).IsSuccess)
      {
        if (doc.Module == "AP")
          PXDatabase.Update<PX.Objects.AP.APRegister>(new PXDataFieldParam[3]
          {
            (PXDataFieldParam) new PXDataFieldAssign("IsTaxPosted", (object) true),
            (PXDataFieldParam) new PXDataFieldRestrict("DocType", (PXDbType) 3, new int?(3), (object) doc.DocType, (PXComp) 0),
            (PXDataFieldParam) new PXDataFieldRestrict("RefNbr", (PXDbType) 12, new int?(15), (object) doc.RefNbr, (PXComp) 0)
          });
        else if (doc.Module == "AR")
        {
          PXDatabase.Update<ARRegister>(new PXDataFieldParam[3]
          {
            (PXDataFieldParam) new PXDataFieldAssign("IsTaxPosted", (object) true),
            (PXDataFieldParam) new PXDataFieldRestrict("DocType", (PXDbType) 3, new int?(3), (object) doc.DocType, (PXComp) 0),
            (PXDataFieldParam) new PXDataFieldRestrict("RefNbr", (PXDbType) 12, new int?(15), (object) doc.RefNbr, (PXComp) 0)
          });
        }
        else
        {
          if (!(doc.Module == "CA"))
            return;
          PXDatabase.Update<CAAdj>(new PXDataFieldParam[2]
          {
            (PXDataFieldParam) new PXDataFieldAssign("IsTaxPosted", (object) true),
            (PXDataFieldParam) new PXDataFieldRestrict("AdjRefNbr", (PXDbType) 12, new int?(15), (object) doc.RefNbr, (PXComp) 0)
          });
        }
      }
      else
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (string message in ((ResultBase) commitTaxResult).Messages)
          stringBuilder.AppendLine(message);
        throw new PXException(stringBuilder.ToString());
      }
    }

    public virtual CommitTaxRequest BuildCommitTaxRequest(ExternalTaxPost.Document doc)
    {
      CommitTaxRequest commitTaxRequest = (CommitTaxRequest) null;
      if (doc.Module == "AP")
      {
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Search<PX.Objects.AP.APInvoice.refNbr>((object) doc.RefNbr, new object[1]
        {
          (object) doc.DocType
        }));
        PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current;
        int num;
        if (current == null)
        {
          num = 0;
        }
        else
        {
          bool? isTaxPosted = current.IsTaxPosted;
          bool flag = false;
          num = isTaxPosted.GetValueOrDefault() == flag & isTaxPosted.HasValue ? 1 : 0;
        }
        if (num != 0)
          commitTaxRequest = ((PXGraph) instance).GetExtension<APInvoiceEntryExternalTax>().BuildCommitTaxRequest(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current);
      }
      else if (doc.Module == "AR")
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) doc.RefNbr, new object[1]
        {
          (object) doc.DocType
        }));
        PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current;
        int num;
        if (current == null)
        {
          num = 0;
        }
        else
        {
          bool? isTaxPosted = current.IsTaxPosted;
          bool flag = false;
          num = isTaxPosted.GetValueOrDefault() == flag & isTaxPosted.HasValue ? 1 : 0;
        }
        if (num != 0)
          commitTaxRequest = ((PXGraph) instance).GetExtension<ARInvoiceEntryExternalTax>().BuildCommitTaxRequest(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current);
      }
      else if (doc.Module == "CA")
      {
        CATranEntry instance = PXGraph.CreateInstance<CATranEntry>();
        ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current = PXResultset<CAAdj>.op_Implicit(((PXSelectBase<CAAdj>) instance.CAAdjRecords).Search<CAAdj.adjRefNbr>((object) doc.RefNbr, new object[1]
        {
          (object) doc.DocType
        }));
        CAAdj current = ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current;
        int num;
        if (current == null)
        {
          num = 0;
        }
        else
        {
          bool? isTaxPosted = current.IsTaxPosted;
          bool flag = false;
          num = isTaxPosted.GetValueOrDefault() == flag & isTaxPosted.HasValue ? 1 : 0;
        }
        if (num != 0)
          commitTaxRequest = ((PXGraph) instance).GetExtension<CATranEntryExternalTax>().BuildCommitTaxRequest(((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current);
      }
      return commitTaxRequest;
    }

    public virtual TaxDocumentType GetTaxDocumentType(ExternalTaxPost.Document doc)
    {
      switch (doc.Module)
      {
        case "AP":
          return !(doc.DrCr == "C") ? (TaxDocumentType) 4 : (TaxDocumentType) 6;
        case "AR":
          return !(doc.DrCr == "D") ? (TaxDocumentType) 2 : (TaxDocumentType) 6;
        case "CA":
          return !(doc.DrCr == "D") ? (TaxDocumentType) 4 : (TaxDocumentType) 2;
        default:
          throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Invalid module {0}: Only AP, AR, or CA is expected.", new object[1]
          {
            (object) doc.Module
          }));
      }
    }
  }
}
