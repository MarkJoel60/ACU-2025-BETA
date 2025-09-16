// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntryApiRetainage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AP;

public sealed class APInvoiceEntryApiRetainage : PXGraphExtension<
#nullable disable
APInvoiceEntry>
{
  public PXFilter<APInvoiceEntryApiRetainage.RetainageOptionsFilter> ReleaseRetainageFilter;
  public PXAction<APInvoice> releaseRetainageAmount;

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>();

  [PXButton]
  [PXUIField(DisplayName = "Release Retainage Amount", Visible = false)]
  public IEnumerable ReleaseRetainageAmount(PXAdapter adapter)
  {
    APInvoiceEntryApiRetainage.RetainageOptionsFilter current1 = this.ReleaseRetainageFilter.Current;
    APInvoice current2 = this.Base.Document.Current;
    this.ValidateOptionsAndInvoice(current1, current2);
    APRetainageRelease retainageRelease = this.InitRetainageGraph(current2);
    APSetup current3 = retainageRelease.APSetup.Current;
    bool isAutoRelease = (current3 != null ? (current3.RetainageBillsAutoRelease.GetValueOrDefault() ? 1 : 0) : 0) != 0;
    PXFilteredProcessing<APInvoiceExt, APRetainageFilter> documentList = retainageRelease.DocumentList;
    List<APInvoiceExt> invoicesToRelease = documentList.Select().RowCast<APInvoiceExt>().ToList<APInvoiceExt>();
    if (current1.AmountToRelease.HasValue)
    {
      foreach (APInvoiceExt data in invoicesToRelease)
        documentList.Cache.SetValueExt<APInvoiceExt.curyRetainageReleasedAmt>((object) data, (object) current1.AmountToRelease);
    }
    PXFilter<APRetainageFilter> filter = retainageRelease.Filter;
    try
    {
      filter.Cache.SetValueExt<APRetainageFilter.docDate>((object) filter.Current, (object) current1.Date);
    }
    catch (PXSetPropertyException ex)
    {
      throw new PXException("{0}: {1}", new object[2]
      {
        (object) "Date",
        (object) ex.Message
      });
    }
    filter.Cache.SetValueExt<APRetainageFilter.finPeriodID>((object) filter.Current, (object) current1.PostPeriod);
    PXFieldState stateExt = (PXFieldState) filter.Cache.GetStateExt<APRetainageFilter.finPeriodID>((object) filter.Current);
    if (!string.IsNullOrEmpty(stateExt?.Error))
    {
      if (stateExt.ErrorLevel == PXErrorLevel.Error)
        throw new PXException("{0}", new object[1]
        {
          (object) stateExt.Error
        });
      throw new PXException("{0}: {1}", new object[2]
      {
        (object) "PostPeriod",
        (object) stateExt.Error
      });
    }
    RetainageOptions updatedOptions = new RetainageOptions()
    {
      DocDate = filter.Current.DocDate,
      MasterFinPeriodID = FinPeriodIDAttribute.CalcMasterPeriodID<APRetainageFilter.finPeriodID>(retainageRelease.Caches[typeof (APRetainageFilter)], (object) filter.Current)
    };
    PXLongOperation.StartOperation<APInvoiceEntry>((PXGraphExtension<APInvoiceEntry>) this, (PXToggleAsyncDelegate) (() => PXGraph.CreateInstance<APInvoiceEntry>().GetExtension<APInvoiceEntryRetainage>()?.ReleaseRetainageProc(invoicesToRelease, updatedOptions, isAutoRelease)));
    return adapter.Get();
  }

  protected void ValidateOptionsAndInvoice(
    APInvoiceEntryApiRetainage.RetainageOptionsFilter options,
    APInvoice invoice)
  {
    if ((options != null ? (!options.Date.HasValue ? 1 : 0) : 1) != 0)
      throw new PXException("The {0} parameter is mandatory for using the Release Retainage action.", new object[1]
      {
        (object) "Date"
      });
    if (string.IsNullOrEmpty(options.PostPeriod))
      throw new PXException("The {0} parameter is mandatory for using the Release Retainage action.", new object[1]
      {
        (object) "PostPeriod"
      });
    bool? nullable1 = !string.IsNullOrEmpty(invoice?.RefNbr) ? invoice.RetainageApply : throw new PXException("The {0} parameter is mandatory for using the Release Retainage action.", new object[1]
    {
      (object) "ReferenceNbr"
    });
    if (!nullable1.GetValueOrDefault())
      throw new PXException("The Release Retainage action is not available for the document.");
    APInvoiceEntryRetainage extension = this.Base.GetExtension<APInvoiceEntryRetainage>();
    int num1;
    if (extension == null)
    {
      num1 = 1;
    }
    else
    {
      PXAction<APInvoice> releaseRetainage = extension.releaseRetainage;
      bool? nullable2;
      if (releaseRetainage == null)
      {
        nullable1 = new bool?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new bool?(releaseRetainage.GetEnabled());
      nullable1 = nullable2;
      num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0)
      throw new PXException("The Release Retainage action is not available for the document.");
    Decimal? amountToRelease1 = options.AmountToRelease;
    Decimal num2 = 0M;
    if (amountToRelease1.GetValueOrDefault() <= num2 & amountToRelease1.HasValue)
      throw new PXException("The amount to release must be greater than zero.");
    Decimal? amountToRelease2 = options.AmountToRelease;
    Decimal num3 = 0M;
    if (amountToRelease2.GetValueOrDefault() > num3 & amountToRelease2.HasValue)
    {
      nullable1 = invoice.PaymentsByLinesAllowed;
      if (nullable1.GetValueOrDefault())
        throw new PXException("The Release Retainage action with the specified amount to release cannot be used because the Pay by Line check box is selected for the document.");
    }
    Decimal? amountToRelease3 = options.AmountToRelease;
    Decimal? retainageUnreleasedAmt = invoice.CuryRetainageUnreleasedAmt;
    if (amountToRelease3.GetValueOrDefault() > retainageUnreleasedAmt.GetValueOrDefault() & amountToRelease3.HasValue & retainageUnreleasedAmt.HasValue)
      throw new PXException("The amount to release is greater than the unreleased retainage amount for the document.");
    APRegister reversingDoc;
    if (this.Base.CheckReversingRetainageDocumentAlreadyExists(invoice, out reversingDoc))
      throw new PXException("The retainage {0} cannot be released because the reversing document {1} {2} exists in the system.", new object[3]
      {
        (object) APInvoiceEntryApiRetainage.GetDocumentType(invoice.DocType),
        (object) APInvoiceEntryApiRetainage.GetDocumentType(reversingDoc.DocType),
        (object) reversingDoc.RefNbr
      });
  }

  protected APRetainageRelease InitRetainageGraph(APInvoice invoice)
  {
    APRetainageRelease instance = PXGraph.CreateInstance<APRetainageRelease>();
    APRetainageFilter current = instance.Filter.Current;
    current.BranchID = invoice.BranchID;
    current.OrgBAccountID = new int?(PXAccess.GetBranch(invoice.BranchID).BAccountID);
    current.VendorID = invoice.VendorID;
    current.RefNbr = invoice.RefNbr;
    current.ShowBillsWithOpenBalance = new bool?(invoice.OpenDoc.GetValueOrDefault());
    if (instance.DocumentList.SelectSingle() != null)
      return instance;
    APRetainageInvoice retainageInvoice = PXSelectBase<APRetainageInvoice, PXSelectJoin<APRetainageInvoice, InnerJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRetainageInvoice.docType>, And<APInvoice.refNbr, Equal<APRetainageInvoice.refNbr>>>>, Where<APRetainageInvoice.isRetainageDocument, Equal<True>, And<APRetainageInvoice.origDocType, Equal<Required<APInvoice.docType>>, And<APRetainageInvoice.origRefNbr, Equal<Required<APInvoice.refNbr>>>>>>.Config>.Select((PXGraph) this.Base, (object) invoice.DocType, (object) invoice.RefNbr).RowCast<APRetainageInvoice>().FirstOrDefault<APRetainageInvoice>((Func<APRetainageInvoice, bool>) (i => !i.Released.GetValueOrDefault()));
    throw new PXException("The retainage cannot be released because {0} {1} associated with this {2} has not been released yet. To proceed, delete or release the retainage document.", new object[3]
    {
      (object) APInvoiceEntryApiRetainage.GetDocumentType(retainageInvoice.DocType),
      (object) retainageInvoice.RefNbr,
      (object) APInvoiceEntryApiRetainage.GetDocumentType(invoice.DocType)
    });
  }

  private static string GetDocumentType(string documentTypeCode)
  {
    return !string.IsNullOrEmpty(documentTypeCode) ? PXMessages.LocalizeNoPrefix(APInvoiceEntry.APDocTypeDict[documentTypeCode]) : (string) null;
  }

  /// <summary>
  /// DAC for filter which is used to release retainage via API
  /// </summary>
  [PXHidden]
  public class RetainageOptionsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <summary>Date of generated retainage documents</summary>
    [PXDate]
    [PXUIField(DisplayName = "Date")]
    public virtual System.DateTime? Date { get; set; }

    /// <summary>Financial period for generated retainage documents</summary>
    [PXString]
    [PXUIField(DisplayName = "Post Period")]
    public virtual string PostPeriod { get; set; }

    /// <summary>Which retainage amount should be released</summary>
    [PXDecimal]
    [PXUIField(DisplayName = "Amount to Release")]
    public virtual Decimal? AmountToRelease { get; set; }

    public abstract class date : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APInvoiceEntryApiRetainage.RetainageOptionsFilter.date>
    {
    }

    public abstract class postPeriod : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntryApiRetainage.RetainageOptionsFilter.postPeriod>
    {
    }

    public abstract class amountToRelease : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntryApiRetainage.RetainageOptionsFilter.amountToRelease>
    {
    }
  }
}
