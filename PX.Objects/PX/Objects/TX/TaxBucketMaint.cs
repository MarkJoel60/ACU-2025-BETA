// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxBucketMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Scopes;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public class TaxBucketMaint : PXGraph<TaxBucketMaint>
{
  public PXSave<TaxBucketMaster> Save;
  public PXCancel<TaxBucketMaster> Cancel;
  public PXFilter<TaxBucketMaster> Bucket;
  public PXSelectJoin<TaxBucketLine, InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<TaxBucketLine.vendorID>, And<TaxReportLine.taxReportRevisionID, Equal<TaxBucketLine.taxReportRevisionID>, And<TaxReportLine.lineNbr, Equal<TaxBucketLine.lineNbr>>>>>, Where<TaxBucketLine.vendorID, Equal<Current<TaxBucketMaster.vendorID>>, And<TaxBucketLine.bucketID, Equal<Current<TaxBucketMaster.bucketID>>, And<TaxBucketLine.taxReportRevisionID, Equal<Current<TaxBucketMaster.taxReportRevisionID>>, And<TaxReportLine.tempLineNbr, IsNull>>>>, OrderBy<Asc<TaxReportLine.sortOrder>>> BucketLine;
  public PXSelect<TaxReportLine> ReportLineView;
  public PXSetup<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<TaxBucketMaster.vendorID>>>> Vendor;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;

  public TaxBucketMaint()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(TaxBucketMaint.\u003C\u003Ec.\u003C\u003E9__6_0 ?? (TaxBucketMaint.\u003C\u003Ec.\u003C\u003E9__6_0 = new PXFieldDefaulting((object) TaxBucketMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__6_0))));
  }

  private void ValidateBucket()
  {
    TaxBucketMaster current = ((PXSelectBase<TaxBucketMaster>) this.Bucket).Current;
    int? nullable1 = current.VendorID;
    int BAccountID1 = nullable1.Value;
    nullable1 = current.TaxReportRevisionID;
    int TaxreportRevisionID1 = nullable1.Value;
    new TaxReportMaint.TaxBucketAnalizer((PXGraph) this, BAccountID1, "P", TaxreportRevisionID1).DoChecks(current.BucketID.Value);
    int? nullable2 = current.VendorID;
    int BAccountID2 = nullable2.Value;
    nullable2 = current.TaxReportRevisionID;
    int TaxreportRevisionID2 = nullable2.Value;
    new TaxReportMaint.TaxBucketAnalizer((PXGraph) this, BAccountID2, "A", TaxreportRevisionID2).DoChecks(current.BucketID.Value);
  }

  public virtual void Persist()
  {
    if (!this.Bucket.VerifyFullyValid())
      return;
    this.ValidateBucket();
    TaxReportMaint.CheckReportSettingsEditable((PXGraph) this, ((PXSelectBase<TaxBucketMaster>) this.Bucket).Current.VendorID, ((PXSelectBase<TaxBucketMaster>) this.Bucket).Current.TaxReportRevisionID);
    ((PXGraph) this).Persist();
  }

  protected virtual void TaxBucketMaster_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TaxBucketMaster row) || !(cache.GetStateExt<TaxBucketMaster.taxReportRevisionID>(e.Row) is PXFieldState stateExt) || stateExt.ErrorLevel == 4)
      return;
    cache.RaiseExceptionHandling<TaxBucketMaster.taxReportRevisionID>(e.Row, stateExt.Value, (Exception) null);
    TaxReportMaint.CheckReportSettingsEditableAndSetWarningTo<TaxBucketMaster.taxReportRevisionID>((PXGraph) this, cache, (object) row, row.VendorID, row.TaxReportRevisionID);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<TaxBucketMaster.vendorID> e)
  {
    if (e.Row == null)
      return;
    object row = e.Row;
    if (((PXGraph) this).IsImport)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<TaxBucketMaster.vendorID>>) e).Cache.SetValue<TaxBucketMaster.taxReportRevisionID>(e.Row, (object) null);
  }

  protected virtual void TaxBucketLine_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (FlaggedModeScopeBase<TaxBucketMaint.SuppressCascadeScope>.IsActive)
      return;
    using (new TaxBucketMaint.SuppressCascadeScope())
    {
      foreach (PXResult<TaxReportLine, TaxBucketLine> pxResult in PXSelectBase<TaxReportLine, PXSelectJoin<TaxReportLine, LeftJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>, And<TaxBucketLine.taxReportRevisionID, Equal<TaxReportLine.taxReportRevisionID>, And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>>>, Where<TaxReportLine.vendorID, Equal<Required<TaxReportLine.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Required<TaxReportLine.taxReportRevisionID>>, And<TaxReportLine.tempLineNbr, Equal<Required<TaxReportLine.tempLineNbr>>, And<TaxBucketLine.bucketID, Equal<Required<TaxBucketLine.bucketID>>>>>>>.Config>.Select((PXGraph) this, new object[4]
      {
        (object) ((TaxBucketLine) e.Row).VendorID,
        (object) ((TaxBucketLine) e.Row).TaxReportRevisionID,
        (object) ((TaxBucketLine) e.Row).LineNbr,
        (object) ((TaxBucketLine) e.Row).BucketID
      }))
      {
        if (PXResult<TaxReportLine, TaxBucketLine>.op_Implicit(pxResult).BucketID.HasValue)
          ((PXSelectBase) this.BucketLine).Cache.Delete((object) PXResult<TaxReportLine, TaxBucketLine>.op_Implicit(pxResult));
      }
    }
  }

  protected virtual void TaxBucketLine_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (FlaggedModeScopeBase<TaxBucketMaint.SuppressCascadeScope>.IsActive)
      return;
    using (new TaxBucketMaint.SuppressCascadeScope())
    {
      foreach (PXResult<TaxReportLine> pxResult in PXSelectBase<TaxReportLine, PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Required<TaxReportLine.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Required<TaxReportLine.taxReportRevisionID>>, And<TaxReportLine.tempLineNbr, Equal<Required<TaxReportLine.tempLineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) ((TaxBucketLine) e.Row).VendorID,
        (object) ((TaxBucketLine) e.Row).TaxReportRevisionID,
        (object) ((TaxBucketLine) e.Row).LineNbr
      }))
      {
        TaxBucketLine copy = PXCache<TaxBucketLine>.CreateCopy((TaxBucketLine) e.Row);
        copy.LineNbr = PXResult<TaxReportLine>.op_Implicit(pxResult).LineNbr;
        copy.TaxReportRevisionID = PXResult<TaxReportLine>.op_Implicit(pxResult).TaxReportRevisionID;
        ((PXSelectBase) this.BucketLine).Cache.Insert((object) copy);
      }
      ((PXSelectBase) this.BucketLine).Cache.Current = e.Row;
    }
  }

  protected virtual void TaxBucketLine_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    TaxBucketLine row = (TaxBucketLine) e.Row;
    TaxReportLine taxReportLine = (TaxReportLine) ((PXSelectBase) this.ReportLineView).Cache.Locate((object) new TaxReportLine()
    {
      VendorID = row.VendorID,
      TaxReportRevisionID = row.TaxReportRevisionID,
      LineNbr = row.LineNbr
    });
    if ((taxReportLine != null ? (!taxReportLine.TempLineNbr.HasValue ? 1 : 0) : 1) == 0)
      return;
    this.CheckUnique(cache, row);
  }

  protected virtual void TaxBucketLine_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TaxBucketLine row))
      return;
    PXUIFieldAttribute.SetReadOnly<TaxBucketLine.lineNbr>(cache, (object) row, row.LineNbr.HasValue);
  }

  protected void CheckUnique(PXCache cache, TaxBucketLine bucketLine)
  {
    PXResult<TaxBucketLine, TaxReportLine>[] array = ((IEnumerable<PXResult<TaxBucketLine>>) PXSelectBase<TaxBucketLine, PXSelectJoin<TaxBucketLine, InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<TaxBucketLine.vendorID>, And<TaxReportLine.taxReportRevisionID, Equal<TaxBucketLine.taxReportRevisionID>, And<TaxReportLine.lineNbr, Equal<TaxBucketLine.lineNbr>>>>>, Where<TaxBucketLine.vendorID, Equal<Current<TaxBucketMaster.vendorID>>, And<TaxBucketLine.bucketID, Equal<Current<TaxBucketMaster.bucketID>>, And<TaxReportLine.tempLineNbr, IsNull, And<TaxBucketLine.taxReportRevisionID, Equal<Required<TaxBucketLine.taxReportRevisionID>>, And<TaxBucketLine.lineNbr, Equal<Required<TaxBucketLine.lineNbr>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) bucketLine.TaxReportRevisionID,
      (object) bucketLine.LineNbr
    })).AsEnumerable<PXResult<TaxBucketLine>>().Cast<PXResult<TaxBucketLine, TaxReportLine>>().ToArray<PXResult<TaxBucketLine, TaxReportLine>>();
    if (((IEnumerable<PXResult<TaxBucketLine, TaxReportLine>>) array).Any<PXResult<TaxBucketLine, TaxReportLine>>())
    {
      TaxReportLine taxReportLine = PXResult<TaxBucketLine, TaxReportLine>.op_Implicit(((IEnumerable<PXResult<TaxBucketLine, TaxReportLine>>) array).First<PXResult<TaxBucketLine, TaxReportLine>>());
      TaxBucket taxBucket = PXResultset<TaxBucket>.op_Implicit(PXSelectBase<TaxBucket, PXSelect<TaxBucket, Where<TaxBucket.vendorID, Equal<Current<TaxBucketMaster.vendorID>>, And<TaxBucket.bucketID, Equal<Current<TaxBucketMaster.bucketID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      cache.RaiseExceptionHandling<TaxBucketLine.lineNbr>((object) bucketLine, (object) bucketLine.LineNbr, (Exception) new PXSetPropertyException("The reporting group '{0}' already contains the report line '{1}'.", new object[2]
      {
        (object) taxBucket.Name,
        (object) taxReportLine.Descr
      }));
      throw new PXException("The reporting group '{0}' already contains the report line '{1}'.", new object[2]
      {
        (object) taxBucket.Name,
        (object) taxReportLine.Descr
      });
    }
  }

  protected virtual void TaxBucketMaster_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    TaxBucket taxBucket = PXResultset<TaxBucket>.op_Implicit(PXSelectBase<TaxBucket, PXSelect<TaxBucket, Where<TaxBucket.vendorID, Equal<Required<TaxBucket.vendorID>>, And<TaxBucket.bucketID, Equal<Required<TaxBucket.bucketID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) ((TaxBucketMaster) e.Row).VendorID,
      (object) ((TaxBucketMaster) e.Row).BucketID
    }));
    if (taxBucket == null)
      return;
    ((TaxBucketMaster) e.Row).BucketType = taxBucket.BucketType;
  }

  public class SuppressCascadeScope : FlaggedModeScopeBase<TaxBucketMaint.SuppressCascadeScope>
  {
  }
}
