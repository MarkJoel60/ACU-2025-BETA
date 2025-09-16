// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxReportMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public class TaxReportMaint : PXGraph<TaxReportMaint>
{
  public const string TAG_TAXZONE = "<TAXZONE>";
  public static DateTime MAX_VALIDTO = new DateTime(9999, 6, 6);
  private readonly TaxReportLinesByTaxZonesReloader taxDetailsReloader;
  public PXSave<TaxReport> Save;
  public PXCancel<TaxReport> Cancel;
  public PXAction<TaxReport> Up;
  public PXAction<TaxReport> Down;
  public PXSelect<TaxReport> Report;
  public TaxReportLinesOrderedSelect ReportLine;
  public PXSelect<TaxBucket, Where<TaxBucket.vendorID, Equal<Current<TaxReport.vendorID>>>> Bucket;
  public PXSelect<TaxBucketLine, Where<TaxBucketLine.vendorID, Equal<Required<TaxReport.vendorID>>, And<TaxBucketLine.taxReportRevisionID, Equal<Required<TaxReport.revisionID>>>>> TaxBucketLines;
  public PXSelect<TaxBucketLine, Where<TaxBucketLine.vendorID, Equal<Required<TaxReportLine.vendorID>>, And<TaxBucketLine.taxReportRevisionID, Equal<Required<TaxReportLine.taxReportRevisionID>>, And<TaxBucketLine.lineNbr, Equal<Required<TaxBucketLine.lineNbr>>>>>> TaxBucketLine_Vendor_LineNbr;
  public PXAction<TaxReport> viewGroupDetails;
  public PXAction<TaxReport> updateTaxZoneLines;
  public PXAction<TaxReport> createReportVersion;
  public PXAction<TaxReport> copyReportVersion;
  public PXAction<TaxReport> deleteReportVersion;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (TaxReport.vendorID))]
  protected virtual void TaxBucket_VendorID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (TaxReport))]
  [PXParent(typeof (Select<TaxReport, Where<TaxReport.vendorID, Equal<Current<TaxBucket.vendorID>>>>), LeaveChildren = true)]
  [PXUIField]
  protected virtual void TaxBucket_BucketID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (TaxReport.vendorID))]
  protected virtual void TaxReportLine_VendorID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable cancel(PXAdapter a)
  {
    TaxReportMaint graph = this;
    TaxReport taxReport1 = (TaxReport) null;
    int? vendorId = (int?) ((PXSelectBase<TaxReport>) graph.Report).Current?.VendorID;
    string str = (string) null;
    int? revisionId1 = (int?) ((PXSelectBase<TaxReport>) graph.Report).Current?.RevisionID;
    int? nullable1 = new int?();
    if (a.Searches != null)
    {
      if (a.Searches.Length != 0 && a.Searches[0] != null)
        str = a.Searches[0].ToString();
      if (a.Searches.Length > 1 && a.Searches[1] != null)
        nullable1 = new int?(int.Parse(a.Searches[1].ToString()));
    }
    if (!string.IsNullOrEmpty(str))
    {
      PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor>.Config>.Search<PX.Objects.AP.Vendor.acctCD>((PXGraph) graph, (object) str, Array.Empty<object>()));
      str = vendor == null ? (string) null : vendor.AcctCD;
    }
    if (a.Searches != null && str != null)
    {
      foreach (TaxReport taxReport2 in ((PXAction) new PXCancel<TaxReport>((PXGraph) graph, "Cancel")).Press(a))
      {
        ((PXSelectBase) graph.Report).Cache.Clear();
        taxReport1 = taxReport2;
        int? nullable2 = taxReport1.RevisionID;
        int? nullable3 = revisionId1;
        if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
        {
          nullable3 = vendorId;
          nullable2 = taxReport1.VendorID;
          if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue && nullable1.HasValue)
          {
            if (PXSelectorAttribute.Select<TaxReport.revisionID>(((PXSelectBase) graph.Report).Cache, (object) taxReport1, (object) taxReport1.RevisionID) == null)
            {
              PXCache cache = ((PXSelectBase) graph.Report).Cache;
              TaxReport taxReport3 = taxReport1;
              // ISSUE: variable of a boxed type
              __Boxed<int?> revisionId2 = (ValueType) taxReport1.RevisionID;
              object[] objArray = new object[2]
              {
                (object) PXUIFieldAttribute.GetDisplayName<TaxReport.revisionID>(((PXSelectBase) graph.Report).Cache),
                null
              };
              nullable2 = taxReport1.RevisionID;
              objArray[1] = (object) nullable2.ToString();
              PXSetPropertyException propertyException = new PXSetPropertyException("{0} '{1}' cannot be found in the system.", (PXErrorLevel) 4, objArray);
              cache.RaiseExceptionHandling<TaxReport.revisionID>((object) taxReport3, (object) revisionId2, (Exception) propertyException);
              continue;
            }
            continue;
          }
        }
        taxReport1 = TaxReportMaint.GetLastReportVersion((PXGraph) graph, taxReport1.VendorID);
        if (taxReport1 == null)
        {
          taxReport1 = taxReport2;
          taxReport1.RevisionID = new int?(1);
          taxReport1.ValidFrom = ((PXGraph) graph).Accessinfo.BusinessDate;
          taxReport1.ValidTo = new DateTime?(TaxReportMaint.MAX_VALIDTO);
          taxReport1 = (TaxReport) ((PXSelectBase) graph.Report).Cache.Insert((object) taxReport1);
          ((PXSelectBase) graph.Report).Cache.IsDirty = ((PXSelectBase) graph.Report).Cache.Inserted.Count() > 0L;
        }
      }
    }
    yield return (object) taxReport1;
  }

  [PXUIField]
  [PXButton(ImageSet = "main", ImageKey = "ArrowUp", Tooltip = "Move Row Up")]
  public virtual IEnumerable up(PXAdapter adapter)
  {
    this.ReportLine.ArrowUpForCurrentRow();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageSet = "main", ImageKey = "ArrowDown", Tooltip = "Move Row Down")]
  public virtual IEnumerable down(PXAdapter adapter)
  {
    this.ReportLine.ArrowDownForCurrentRow();
    return adapter.Get();
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  protected IEnumerable reportLine()
  {
    TaxReportMaint taxReportMaint = this;
    TaxReport current = ((PXSelectBase<TaxReport>) taxReportMaint.Report).Current;
    int? nullable;
    int num;
    if (current == null)
    {
      num = 1;
    }
    else
    {
      nullable = current.VendorID;
      num = !nullable.HasValue ? 1 : 0;
    }
    if (num == 0)
    {
      bool showTaxZones = ((PXSelectBase<TaxReport>) taxReportMaint.Report).Current.ShowNoTemp.GetValueOrDefault();
      TaxReportMaint graph1 = taxReportMaint;
      nullable = ((PXSelectBase<TaxReport>) taxReportMaint.Report).Current.VendorID;
      int BAccountID1 = nullable.Value;
      nullable = ((PXSelectBase<TaxReport>) taxReportMaint.Report).Current.RevisionID;
      int TaxreportRevisionID1 = nullable.Value;
      Dictionary<int, List<int>> dictionary1 = new TaxReportMaint.TaxBucketAnalizer((PXGraph) graph1, BAccountID1, "P", TaxreportRevisionID1).AnalyzeBuckets(showTaxZones);
      TaxReportMaint graph2 = taxReportMaint;
      nullable = ((PXSelectBase<TaxReport>) taxReportMaint.Report).Current.VendorID;
      int BAccountID2 = nullable.Value;
      nullable = ((PXSelectBase<TaxReport>) taxReportMaint.Report).Current.RevisionID;
      int TaxreportRevisionID2 = nullable.Value;
      Dictionary<int, List<int>> dictionary2 = new TaxReportMaint.TaxBucketAnalizer((PXGraph) graph2, BAccountID2, "A", TaxreportRevisionID2).AnalyzeBuckets(showTaxZones);
      Dictionary<int, List<int>>[] bucketsArr = new Dictionary<int, List<int>>[2]
      {
        dictionary1,
        dictionary2
      };
      Dictionary<int, TaxReportLine> taxReporLinesByLineNumber = GraphHelper.RowCast<TaxReportLine>((IEnumerable) PXSelectBase<TaxReportLine, PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Current<TaxReport.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Current<TaxReport.revisionID>>, And<Where2<Where<Current<TaxReport.showNoTemp>, Equal<False>, And<TaxReportLine.tempLineNbr, IsNull>>, Or<Where<Current<TaxReport.showNoTemp>, Equal<True>, And<Where<TaxReportLine.tempLineNbr, IsNull, And<TaxReportLine.tempLine, Equal<False>, Or<TaxReportLine.tempLineNbr, IsNotNull>>>>>>>>>>, OrderBy<Asc<TaxReportLine.sortOrder, Asc<TaxReportLine.taxZoneID>>>>.Config>.Select((PXGraph) taxReportMaint, Array.Empty<object>())).ToDictionary<TaxReportLine, int>((Func<TaxReportLine, int>) (taxLine => taxLine.LineNbr.Value));
      foreach (TaxReportLine taxReportLine in taxReporLinesByLineNumber.Values)
      {
        TaxReportLine taxline = taxReportLine;
        if (!showTaxZones)
        {
          // ISSUE: explicit non-virtual call
          foreach (Dictionary<int, List<int>> dictionary3 in ((IEnumerable<Dictionary<int, List<int>>>) bucketsArr).Where<Dictionary<int, List<int>>>((Func<Dictionary<int, List<int>>, bool>) (dict => dict != null && __nonvirtual (dict.ContainsKey(taxline.LineNbr.Value)))))
          {
            nullable = taxline.LineNbr;
            int key = nullable.Value;
            IOrderedEnumerable<int> values = dictionary3[key].Where<int>((Func<int, bool>) (lineNbr => taxReporLinesByLineNumber.ContainsKey(lineNbr))).Select<int, int>((Func<int, int>) (lineNbr => taxReporLinesByLineNumber[lineNbr].SortOrder.Value)).OrderBy<int, int>((Func<int, int>) (lineNbr => lineNbr));
            taxline.BucketSum = string.Join<int>("+", (IEnumerable<int>) values);
          }
        }
        yield return (object) taxline;
      }
    }
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewGroupDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<TaxReport>) this.Report).Current != null && ((PXSelectBase<TaxBucket>) this.Bucket).Current != null && (((PXSelectBase) this.Report).Cache.Updated.Count() <= 0L && ((PXSelectBase) this.Report).Cache.Inserted.Count() <= 0L || ((PXSelectBase<TaxReport>) this.Report).Ask("You will be redirected to another page, all unsaved data will be lost. Continue?", (MessageButtons) 4) != 7))
    {
      TaxBucketMaint instance = PXGraph.CreateInstance<TaxBucketMaint>();
      ((PXSelectBase<TaxBucketMaster>) instance.Bucket).Current.VendorID = ((PXSelectBase<TaxReport>) this.Report).Current.VendorID;
      ((PXSelectBase<TaxBucketMaster>) instance.Bucket).Current.TaxReportRevisionID = ((PXSelectBase<TaxReport>) this.Report).Current.RevisionID;
      ((PXSelectBase<TaxBucketMaster>) instance.Bucket).Current.BucketID = ((PXSelectBase<TaxBucket>) this.Bucket).Current.BucketID;
      ((PXSelectBase<TaxBucketMaster>) instance.Bucket).Current.BucketType = ((PXSelectBase<TaxBucket>) this.Bucket).Current.BucketType;
      throw new PXRedirectRequiredException((PXGraph) instance, "Group Details");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable UpdateTaxZoneLines(PXAdapter adapter)
  {
    this.taxDetailsReloader.ReloadTaxReportLinesForTaxZones();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Report Management")]
  protected virtual IEnumerable CreateReportVersion(PXAdapter adapter)
  {
    TaxReportMaint graph = this;
    if (((PXSelectBase<TaxReport>) graph.Report).Current != null)
    {
      int? nullable1 = ((PXSelectBase<TaxReport>) graph.Report).Current.VendorID;
      if (nullable1.HasValue)
      {
        TaxReport taxReport1 = new TaxReport();
        taxReport1.VendorID = ((PXSelectBase<TaxReport>) graph.Report).Current.VendorID;
        TaxReport taxReport2 = taxReport1;
        nullable1 = TaxReportMaint.GetLastReportVersion((PXGraph) graph, taxReport1.VendorID).RevisionID;
        int? nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
        taxReport2.RevisionID = nullable2;
        TaxReport taxReport3 = GraphHelper.Caches<TaxReport>((PXGraph) graph).Insert(taxReport1);
        ((PXSelectBase) graph.Report).Cache.SetValueExt<TaxReport.validFrom>((object) taxReport3, (object) ((PXGraph) graph).Accessinfo.BusinessDate);
        taxReport3.ValidTo = new DateTime?(TaxReportMaint.MAX_VALIDTO);
        GraphHelper.Caches<TaxReport>((PXGraph) graph).Update(taxReport3);
      }
    }
    yield return (object) ((PXSelectBase<TaxReport>) graph.Report).Current;
  }

  [PXUIField]
  [PXButton(Category = "Report Management")]
  protected virtual IEnumerable CopyReportVersion(PXAdapter adapter)
  {
    TaxReportMaint graph = this;
    if (((PXSelectBase<TaxReport>) graph.Report).Current != null)
    {
      int? nullable1 = ((PXSelectBase<TaxReport>) graph.Report).Current.VendorID;
      if (nullable1.HasValue)
      {
        ((PXSelectBase<TaxReport>) graph.Report).Current.ShowNoTemp = new bool?(false);
        PXResultset<TaxReportLine> pxResultset1 = ((PXSelectBase<TaxReportLine>) graph.ReportLine).Select(Array.Empty<object>());
        PXResultset<TaxBucketLine> pxResultset2 = ((PXSelectBase<TaxBucketLine>) graph.TaxBucketLines).Select(new object[2]
        {
          (object) ((PXSelectBase<TaxReport>) graph.Report).Current.VendorID,
          (object) ((PXSelectBase<TaxReport>) graph.Report).Current.RevisionID
        });
        TaxReport taxReport1 = new TaxReport();
        taxReport1.VendorID = ((PXSelectBase<TaxReport>) graph.Report).Current.VendorID;
        TaxReport taxReport2 = taxReport1;
        nullable1 = TaxReportMaint.GetLastReportVersion((PXGraph) graph, taxReport1.VendorID).RevisionID;
        int? nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
        taxReport2.RevisionID = nullable2;
        TaxReport taxReport3 = GraphHelper.Caches<TaxReport>((PXGraph) graph).Insert(taxReport1);
        ((PXSelectBase) graph.Report).Cache.SetValueExt<TaxReport.validFrom>((object) taxReport3, (object) ((PXGraph) graph).Accessinfo.BusinessDate);
        taxReport3.ValidTo = new DateTime?(TaxReportMaint.MAX_VALIDTO);
        TaxReport taxReport4 = GraphHelper.Caches<TaxReport>((PXGraph) graph).Update(taxReport3);
        Dictionary<int, int> dictionary1 = new Dictionary<int, int>();
        foreach (PXResult<TaxReportLine> pxResult in pxResultset1)
        {
          TaxReportLine taxReportLine1 = PXResult<TaxReportLine>.op_Implicit(pxResult);
          TaxReportLine copy = PXCache<TaxReportLine>.CreateCopy(taxReportLine1);
          copy.TempLine = new bool?(false);
          copy.TaxReportRevisionID = taxReport4.RevisionID;
          TaxReportLine taxReportLine2 = copy;
          nullable1 = new int?();
          int? nullable3 = nullable1;
          taxReportLine2.LineNbr = nullable3;
          TaxReportLine taxReportLine3 = GraphHelper.Caches<TaxReportLine>((PXGraph) graph).Insert(copy);
          Dictionary<int, int> dictionary2 = dictionary1;
          nullable1 = taxReportLine1.LineNbr;
          int key = nullable1.Value;
          nullable1 = taxReportLine3.LineNbr;
          int num = nullable1.Value;
          dictionary2.Add(key, num);
          if (taxReportLine1.TempLine.GetValueOrDefault())
          {
            taxReportLine3.TempLine = new bool?(true);
            GraphHelper.Caches<TaxReportLine>((PXGraph) graph).Update(taxReportLine3);
          }
        }
        foreach (PXResult<TaxBucketLine> pxResult1 in pxResultset2)
        {
          TaxBucketLine taxBucketLine1 = PXResult<TaxBucketLine>.op_Implicit(pxResult1);
          if (dictionary1.ContainsKey(taxBucketLine1.LineNbr.Value))
          {
            TaxBucketLine copy1 = PXCache<TaxBucketLine>.CreateCopy(taxBucketLine1);
            copy1.TaxReportRevisionID = taxReport4.RevisionID;
            copy1.LineNbr = new int?(dictionary1[taxBucketLine1.LineNbr.Value]);
            TaxBucketLine taxBucketLine2 = GraphHelper.Caches<TaxBucketLine>((PXGraph) graph).Insert(copy1);
            foreach (PXResult<TaxReportLine, TaxBucketLine> pxResult2 in PXSelectBase<TaxReportLine, PXSelectJoin<TaxReportLine, LeftJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>, And<TaxBucketLine.taxReportRevisionID, Equal<TaxReportLine.taxReportRevisionID>, And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>>>, Where<TaxReportLine.vendorID, Equal<Required<TaxReportLine.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Required<TaxReportLine.taxReportRevisionID>>, And<TaxReportLine.tempLineNbr, Equal<Required<TaxReportLine.tempLineNbr>>>>>>.Config>.Select((PXGraph) graph, new object[3]
            {
              (object) taxBucketLine2.VendorID,
              (object) taxBucketLine2.TaxReportRevisionID,
              (object) taxBucketLine2.LineNbr
            }))
            {
              TaxBucketLine copy2 = PXCache<TaxBucketLine>.CreateCopy(taxBucketLine2);
              copy2.LineNbr = PXResult<TaxReportLine, TaxBucketLine>.op_Implicit(pxResult2).LineNbr;
              copy2.TaxReportRevisionID = PXResult<TaxReportLine, TaxBucketLine>.op_Implicit(pxResult2).TaxReportRevisionID;
              GraphHelper.Caches<TaxBucketLine>((PXGraph) graph).Insert(copy2);
            }
          }
        }
      }
    }
    yield return (object) ((PXSelectBase<TaxReport>) graph.Report).Current;
  }

  [PXUIField]
  [PXButton(Category = "Report Management")]
  protected virtual IEnumerable DeleteReportVersion(PXAdapter adapter)
  {
    TaxReportMaint graph = this;
    if (graph.IsReportVersionHasHistory((PXGraph) graph, ((PXSelectBase<TaxReport>) graph.Report).Current.RevisionID, ((PXSelectBase<TaxReport>) graph.Report).Current.VendorID))
      throw new PXException("The report version has history and cannot be deleted.");
    if (((PXSelectBase<TaxReport>) graph.Report).Ask("Delete Notification", "The current report version will be deleted.", (MessageButtons) 4) == 6)
    {
      TaxReport taxReport1 = new TaxReport();
      ((PXSelectBase<TaxReport>) graph.Report).Current.ShowNoTemp = new bool?(false);
      TaxReport copy = PXCache<TaxReport>.CreateCopy(((PXSelectBase<TaxReport>) graph.Report).Current);
      PXResultset<TaxReportLine> pxResultset1 = ((PXSelectBase<TaxReportLine>) graph.ReportLine).Select(Array.Empty<object>());
      PXResultset<TaxBucketLine> pxResultset2 = ((PXSelectBase<TaxBucketLine>) graph.TaxBucketLines).Select(new object[2]
      {
        (object) copy.VendorID,
        (object) copy.RevisionID
      });
      foreach (PXResult<TaxReportLine> pxResult in pxResultset1)
      {
        TaxReportLine taxReportLine = PXResult<TaxReportLine>.op_Implicit(pxResult);
        GraphHelper.Caches<TaxReportLine>((PXGraph) graph).Delete(taxReportLine);
      }
      foreach (PXResult<TaxBucketLine> pxResult in pxResultset2)
      {
        TaxBucketLine taxBucketLine = PXResult<TaxBucketLine>.op_Implicit(pxResult);
        GraphHelper.Caches<TaxBucketLine>((PXGraph) graph).Delete(taxBucketLine);
      }
      GraphHelper.Caches<TaxReport>((PXGraph) graph).Delete(copy);
      TaxReport previuosReportVersion = graph.GetPreviuosReportVersion((PXGraph) graph, copy);
      if (previuosReportVersion != null)
      {
        TaxReport taxReport2 = previuosReportVersion;
        DateTime? validTo = copy.ValidTo;
        DateTime maxValidto = TaxReportMaint.MAX_VALIDTO;
        DateTime? nullable = (validTo.HasValue ? (validTo.GetValueOrDefault() == maxValidto ? 1 : 0) : 0) != 0 ? new DateTime?(TaxReportMaint.MAX_VALIDTO) : copy.ValidTo;
        taxReport2.ValidTo = nullable;
        GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<TaxReport>((PXGraph) graph), (object) previuosReportVersion);
      }
      ((PXGraph) graph).Actions.PressSave();
      ((PXSelectBase<TaxReport>) graph.Report).Current = previuosReportVersion;
    }
    yield return (object) ((PXSelectBase<TaxReport>) graph.Report).Current;
  }

  public static Dictionary<int, List<int>> AnalyseBuckets(
    PXGraph graph,
    int BAccountID,
    int TaxReportRevisionID,
    string TaxLineType,
    bool CalcWithZones,
    Func<PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>, bool> ShowTaxReportLine = null)
  {
    TaxReportMaint.TaxBucketAnalizer taxBucketAnalizer = new TaxReportMaint.TaxBucketAnalizer(graph, BAccountID, TaxLineType, TaxReportRevisionID);
    if (ShowTaxReportLine != null)
      taxBucketAnalizer.showTaxReportLine = ShowTaxReportLine;
    return taxBucketAnalizer.AnalyzeBuckets(CalcWithZones);
  }

  private void UpdateNet(object row)
  {
    bool flag = false;
    TaxReportLine taxReportLine1 = row as TaxReportLine;
    taxReportLine1.TaxReportRevisionID = ((PXSelectBase<TaxReport>) this.Report).Current.RevisionID;
    if (taxReportLine1.NetTax.Value && !taxReportLine1.TempLineNbr.HasValue)
    {
      foreach (PXResult<TaxReportLine> pxResult in PXSelectBase<TaxReportLine, PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Required<TaxReport.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Required<TaxReport.revisionID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) taxReportLine1.VendorID,
        (object) taxReportLine1.TaxReportRevisionID
      }))
      {
        TaxReportLine taxReportLine2 = PXResult<TaxReportLine>.op_Implicit(pxResult);
        if (taxReportLine2.NetTax.Value)
        {
          int? lineNbr = taxReportLine2.LineNbr;
          int? nullable = taxReportLine1.LineNbr;
          if (!(lineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & lineNbr.HasValue == nullable.HasValue))
          {
            nullable = taxReportLine2.TempLineNbr;
            lineNbr = taxReportLine1.LineNbr;
            if (!(nullable.GetValueOrDefault() == lineNbr.GetValueOrDefault() & nullable.HasValue == lineNbr.HasValue))
            {
              taxReportLine2.NetTax = new bool?(false);
              ((PXSelectBase) this.ReportLine).Cache.Update((object) taxReportLine2);
              flag = true;
            }
          }
        }
      }
    }
    if (!flag)
      return;
    ((PXSelectBase) this.ReportLine).View.RequestRefresh();
  }

  public TaxReportLine CreateChildLine(TaxReportLine template, TaxZone zone)
  {
    TaxReportLine copy = PXCache<TaxReportLine>.CreateCopy(template);
    copy.TempLineNbr = copy.LineNbr;
    copy.TaxZoneID = zone.TaxZoneID;
    copy.LineNbr = new int?();
    copy.TempLine = new bool?(false);
    copy.ReportLineNbr = (string) null;
    copy.SortOrder = template.SortOrder;
    if (!string.IsNullOrEmpty(copy.Descr))
    {
      int startIndex = copy.Descr.IndexOf("<TAXZONE>", StringComparison.OrdinalIgnoreCase);
      if (startIndex >= 0)
        copy.Descr = copy.Descr.Remove(startIndex, "<TAXZONE>".Length).Insert(startIndex, copy.TaxZoneID);
    }
    return copy;
  }

  private void UpdateZones(PXCache sender, TaxReportLine oldRow, TaxReportLine newRow)
  {
    bool? tempLine1;
    if (oldRow != null)
    {
      if (newRow != null)
      {
        tempLine1 = newRow.TempLine;
        bool flag = false;
        if (!(tempLine1.GetValueOrDefault() == flag & tempLine1.HasValue))
          goto label_7;
      }
      if (!string.IsNullOrEmpty(newRow?.Descr))
      {
        int startIndex = newRow.Descr.IndexOf("<TAXZONE>", StringComparison.OrdinalIgnoreCase);
        if (startIndex >= 0)
          newRow.Descr = newRow.Descr.Remove(startIndex, "<TAXZONE>".Length).TrimEnd(' ');
      }
      this.DeleteChildTaxLinesForMainTaxLine(oldRow);
    }
label_7:
    bool? tempLine2;
    if (newRow != null)
    {
      tempLine1 = newRow.TempLine;
      if (tempLine1.GetValueOrDefault())
      {
        tempLine1 = newRow.TempLine;
        tempLine2 = (bool?) oldRow?.TempLine;
        if (!(tempLine1.GetValueOrDefault() == tempLine2.GetValueOrDefault() & tempLine1.HasValue == tempLine2.HasValue))
        {
          newRow.TaxZoneID = (string) null;
          if (string.IsNullOrEmpty(newRow.Descr) || newRow.Descr.IndexOf("<TAXZONE>", StringComparison.OrdinalIgnoreCase) < 0)
            newRow.Descr += " <TAXZONE>";
          foreach (PXResult<TaxZone> pxResult in PXSelectBase<TaxZone, PXSelect<TaxZone>.Config>.Select((PXGraph) this, Array.Empty<object>()))
          {
            TaxZone zone = PXResult<TaxZone>.op_Implicit(pxResult);
            TaxReportLine childLine = this.CreateChildLine(newRow, zone);
            sender.Insert((object) childLine);
          }
        }
      }
    }
    if (newRow == null)
      return;
    tempLine2 = newRow.TempLine;
    if (!tempLine2.GetValueOrDefault() || oldRow == null)
      return;
    tempLine2 = oldRow.TempLine;
    if (!tempLine2.GetValueOrDefault())
      return;
    this.UpdateTaxLineOnFieldUpdatedWhenDetailByTaxZoneNotChanged(sender, oldRow, newRow);
  }

  private void DeleteChildTaxLinesForMainTaxLine(TaxReportLine mainLine)
  {
    foreach (PXResult<TaxReportLine> pxResult in PXSelectBase<TaxReportLine, PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Required<TaxReportLine.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Required<TaxReportLine.taxReportRevisionID>>, And<TaxReportLine.tempLineNbr, Equal<Required<TaxReportLine.tempLineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) mainLine.VendorID,
      (object) mainLine.TaxReportRevisionID,
      (object) mainLine.LineNbr
    }))
      ((PXSelectBase) this.ReportLine).Cache.Delete((object) PXResult<TaxReportLine>.op_Implicit(pxResult));
  }

  private void UpdateTaxLineOnFieldUpdatedWhenDetailByTaxZoneNotChanged(
    PXCache sender,
    TaxReportLine oldRow,
    TaxReportLine newRow)
  {
    foreach (PXResult<TaxReportLine> pxResult in PXSelectBase<TaxReportLine, PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Required<TaxReportLine.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Required<TaxReportLine.taxReportRevisionID>>, And<TaxReportLine.tempLineNbr, Equal<Required<TaxReportLine.tempLineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) oldRow.VendorID,
      (object) oldRow.TaxReportRevisionID,
      (object) oldRow.LineNbr
    }))
    {
      TaxReportLine taxReportLine = PXResult<TaxReportLine>.op_Implicit(pxResult);
      taxReportLine.Descr = newRow.Descr;
      if (!string.IsNullOrEmpty(taxReportLine.Descr))
      {
        int startIndex = taxReportLine.Descr.IndexOf("<TAXZONE>", StringComparison.OrdinalIgnoreCase);
        if (startIndex >= 0)
          taxReportLine.Descr = taxReportLine.Descr.Remove(startIndex, "<TAXZONE>".Length).Insert(startIndex, taxReportLine.TaxZoneID);
      }
      taxReportLine.NetTax = newRow.NetTax;
      taxReportLine.LineType = newRow.LineType;
      taxReportLine.LineMult = newRow.LineMult;
      taxReportLine.SortOrder = newRow.SortOrder;
      sender.Update((object) taxReportLine);
    }
  }

  protected virtual void TaxReport_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ((PXAction) this.deleteReportVersion).SetEnabled(false);
    ((PXAction) this.createReportVersion).SetEnabled(false);
    ((PXAction) this.copyReportVersion).SetEnabled(false);
    if (e.Row == null)
      return;
    TaxReport row = (TaxReport) e.Row;
    bool flag1 = !row.ShowNoTemp.Value;
    PXUIFieldAttribute.SetVisible<TaxReportLine.tempLine>(((PXSelectBase) this.ReportLine).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<TaxReportLine.bucketSum>(((PXSelectBase) this.ReportLine).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<TaxReportLine.tempLine>(((PXSelectBase) this.ReportLine).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<TaxReportLine.netTax>(((PXSelectBase) this.ReportLine).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<TaxReportLine.taxZoneID>(((PXSelectBase) this.ReportLine).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<TaxReportLine.lineType>(((PXSelectBase) this.ReportLine).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<TaxReportLine.lineMult>(((PXSelectBase) this.ReportLine).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<TaxReportLine.sortOrder>(((PXSelectBase) this.ReportLine).Cache, (object) null, flag1);
    this.ReportLine.AllowDragDrop = flag1;
    ((PXSelectBase) this.ReportLine).AllowInsert = flag1;
    ((PXAction) this.Up).SetEnabled(flag1);
    ((PXAction) this.Down).SetEnabled(flag1);
    TaxReportMaint.CheckReportSettingsEditableAndSetWarningTo<TaxReport.revisionID>((PXGraph) this, sender, (object) row, row.VendorID, row.RevisionID);
    if (!row.VendorID.HasValue)
      return;
    bool flag2 = this.IsReportVersionHasHistory((PXGraph) this, row.RevisionID, row.VendorID);
    ((PXAction) this.deleteReportVersion).SetEnabled(!flag2 && ((PXSelectBase) this.Report).Cache.Inserted.Count() == 0L);
    ((PXAction) this.createReportVersion).SetEnabled(((PXSelectBase) this.Report).Cache.Inserted.Count() == 0L && !((PXSelectBase) this.Report).Cache.IsDirty);
    ((PXAction) this.copyReportVersion).SetEnabled(((PXSelectBase) this.Report).Cache.Inserted.Count() == 0L && !((PXSelectBase) this.Report).Cache.IsDirty);
    PXCache cache = ((PXSelectBase) this.Report).Cache;
    TaxReport taxReport = row;
    int num;
    if (!flag2)
    {
      DateTime? validTo = row.ValidTo;
      DateTime maxValidto = TaxReportMaint.MAX_VALIDTO;
      num = validTo.HasValue ? (validTo.GetValueOrDefault() == maxValidto ? 1 : 0) : 0;
    }
    else
      num = 0;
    PXUIFieldAttribute.SetEnabled<TaxReport.validFrom>(cache, (object) taxReport, num != 0);
  }

  protected virtual void TaxReport_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is TaxReport row))
      return;
    if (!row.ShowNoTemp.HasValue)
      row.ShowNoTemp = new bool?(false);
    if (row.LineCntr.HasValue)
      return;
    row.LineCntr = new int?(0);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<TaxReport> e)
  {
    if (e.Row == null || !e.Row.ValidFrom.HasValue)
      return;
    TaxReport row = e.Row;
    PXEntryStatus status = ((PXSelectBase) this.Report).Cache.GetStatus((object) row);
    if (status != 2)
    {
      if (status != 1)
        return;
      DateTime? validTo = row.ValidTo;
      DateTime maxValidto = TaxReportMaint.MAX_VALIDTO;
      if ((validTo.HasValue ? (validTo.GetValueOrDefault() == maxValidto ? 1 : 0) : 0) == 0)
        return;
    }
    TaxReport taxReport = status == 2 ? this.GetLastReportVersion((PXGraph) this, row.VendorID, row.RevisionID) : this.GetPreviuosReportVersion((PXGraph) this, row);
    if (taxReport == null)
      return;
    taxReport.ValidTo = new DateTime?(row.ValidFrom.Value.AddDays(-1.0));
    GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<TaxReport>((PXGraph) this), (object) taxReport);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<TaxReport.validFrom> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxReport.validFrom>, object, object>) e).NewValue == null)
      return;
    TaxReport row = e.Row as TaxReport;
    TaxReport copy = (TaxReport) ((PXSelectBase) this.Report).Cache.CreateCopy((object) row);
    copy.ValidFrom = (DateTime?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxReport.validFrom>, object, object>) e).NewValue;
    string reportPeriodInHistory = this.GetLastReportPeriodInHistory((PXGraph) this, row.VendorID);
    PX.Objects.AP.Vendor byId = VendorMaint.GetByID((PXGraph) this, row.VendorID);
    if (reportPeriodInHistory != null)
    {
      DateTime dateTime = TaxYearMaint.GetTaxPeriodByKey((PXGraph) this, ((PXAccess.Organization) PXAccess.GetBranch(((PXGraph) this).Accessinfo.BranchID).Organization).OrganizationID, row.VendorID, reportPeriodInHistory).EndDate.Value;
      dateTime = dateTime.AddDays(-1.0);
      DateTime? validFrom = copy.ValidFrom;
      if ((validFrom.HasValue ? (dateTime >= validFrom.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXSetPropertyException("A report version with the specified date cannot be created. There is a prepared report for the {0} tax period for the {1} tax agency.", (PXErrorLevel) 4, new object[2]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForError(reportPeriodInHistory),
          (object) byId.AcctCD
        });
    }
    TaxReport taxReport = ((PXSelectBase) this.Report).Cache.GetStatus((object) row) == 2 ? this.GetLastReportVersion((PXGraph) this, row.VendorID, row.RevisionID) : this.GetPreviuosReportVersion((PXGraph) this, row);
    if (taxReport == null)
      return;
    DateTime? validFrom1 = copy.ValidFrom;
    DateTime? validFrom2 = taxReport.ValidFrom;
    if ((validFrom1.HasValue & validFrom2.HasValue ? (validFrom1.GetValueOrDefault() <= validFrom2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      TaxReport reportVersionByDate = TaxReportMaint.GetTaxReportVersionByDate((PXGraph) this, copy.VendorID, copy.ValidFrom, row.RevisionID);
      PXSetPropertyException propertyException;
      if (reportVersionByDate == null)
        propertyException = new PXSetPropertyException("The {0} report version is already valid from {1}. You cannot add a new report version with a date earlier than the date of the {0} version.", (PXErrorLevel) 4, new object[2]
        {
          (object) taxReport.RevisionID,
          (object) taxReport.ValidFrom
        });
      else
        propertyException = new PXSetPropertyException("The {0} report version already exists for the specified date for the {1} tax agency.", (PXErrorLevel) 4, new object[2]
        {
          (object) reportVersionByDate.RevisionID,
          (object) VendorMaint.GetByID((PXGraph) this, row.VendorID)?.AcctCD
        });
      throw propertyException;
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<TaxReport> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<TaxReport>>) e).Cache.GetStatus((object) e.Row) == 2 || ((PXGraph) this).IsImport)
      return;
    TaxReport taxReport = TaxReport.PK.Find((PXGraph) this, e.Row.VendorID, e.Row.RevisionID);
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<TaxReport>>) e).Cache.ObjectsEqual<TaxReport.vendorID, TaxReport.revisionID, TaxReport.validFrom>((object) e.Row, (object) taxReport))
      return;
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<TaxReport>>) e).Cache.IsDirty = false;
    ((PXSelectBase) this.Report).Cache.SetStatus((object) e.Row, (PXEntryStatus) 0);
  }

  protected virtual void TaxReportLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TaxReportLine row))
      return;
    PXCache pxCache1 = sender;
    TaxReportLine taxReportLine1 = row;
    bool? nullable = row.HideReportLine;
    int num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<TaxReportLine.reportLineNbr>(pxCache1, (object) taxReportLine1, num1 != 0);
    PXCache pxCache2 = sender;
    TaxReportLine taxReportLine2 = row;
    nullable = row.TempLine;
    int num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<TaxReportLine.taxZoneID>(pxCache2, (object) taxReportLine2, num2 != 0);
  }

  protected virtual void TaxReportLine_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.UpdateNet(e.Row);
    this.UpdateZones(sender, (TaxReportLine) null, e.Row as TaxReportLine);
  }

  protected virtual void TaxReportLine_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.UpdateNet(e.Row);
    this.UpdateZones(sender, e.OldRow as TaxReportLine, e.Row as TaxReportLine);
  }

  protected virtual void TaxReportLine_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    this.UpdateZones(sender, e.Row as TaxReportLine, (TaxReportLine) null);
  }

  protected virtual void TaxReportLine_HideReportLine_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TaxReportLine row) || !row.HideReportLine.GetValueOrDefault())
      return;
    sender.SetValueExt<TaxReportLine.reportLineNbr>((object) row, (object) null);
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<TaxReport>) this.Report).Current != null)
    {
      this.CheckAndWarnTaxBoxNumbers();
      TaxReportMaint.CheckReportSettingsEditable((PXGraph) this, ((PXSelectBase<TaxReport>) this.Report).Current.VendorID, ((PXSelectBase<TaxReport>) this.Report).Current.RevisionID);
      this.SyncReportLinesAndBucketLines();
    }
    ((PXGraph) this).Persist();
  }

  private void SyncReportLinesAndBucketLines()
  {
    if (((PXCache) GraphHelper.Caches<TaxReportLine>((PXGraph) this)).Inserted.Count() > 0L && ((PXCache) GraphHelper.Caches<TaxReport>((PXGraph) this)).Inserted.Count() > 0L)
    {
      List<TaxReportLine> list1 = GraphHelper.RowCast<TaxReportLine>((IEnumerable) PXSelectBase<TaxReportLine, PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<BqlField<TaxReport.vendorID, IBqlInt>.FromCurrent>, And<TaxReportLine.taxReportRevisionID, Equal<BqlField<TaxReport.revisionID, IBqlInt>.FromCurrent>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<TaxReportLine>();
      List<TaxBucketLine> list2 = GraphHelper.RowCast<TaxBucketLine>((IEnumerable) ((PXSelectBase<TaxBucketLine>) this.TaxBucketLines).Select(new object[2]
      {
        (object) ((PXSelectBase<TaxReport>) this.Report).Current.VendorID,
        (object) ((PXSelectBase<TaxReport>) this.Report).Current.RevisionID
      })).ToList<TaxBucketLine>();
      foreach (TaxReportLine taxReportLine in list1)
      {
        if (taxReportLine.TempLineNbr.HasValue)
        {
          foreach (TaxBucketLine taxBucketLine in GraphHelper.RowCast<TaxBucketLine>((IEnumerable) ((PXSelectBase<TaxBucketLine>) this.TaxBucketLine_Vendor_LineNbr).Select(new object[3]
          {
            (object) taxReportLine.VendorID,
            (object) taxReportLine.TaxReportRevisionID,
            (object) taxReportLine.TempLineNbr
          })))
          {
            TaxBucketLine copy = PXCache<TaxBucketLine>.CreateCopy(taxBucketLine);
            copy.LineNbr = taxReportLine.LineNbr;
            copy.TaxReportRevisionID = taxReportLine.TaxReportRevisionID;
            if (((PXSelectBase) this.TaxBucketLine_Vendor_LineNbr).Cache.Locate((object) copy) == null)
              ((PXSelectBase) this.TaxBucketLine_Vendor_LineNbr).Cache.Insert((object) copy);
          }
        }
      }
      foreach (TaxBucketLine taxBucketLine in list2)
      {
        TaxBucketLine bucketLine = taxBucketLine;
        if (list1.Find((Predicate<TaxReportLine>) (i =>
        {
          int? reportRevisionId1 = i.TaxReportRevisionID;
          int? reportRevisionId2 = bucketLine.TaxReportRevisionID;
          if (reportRevisionId1.GetValueOrDefault() == reportRevisionId2.GetValueOrDefault() & reportRevisionId1.HasValue == reportRevisionId2.HasValue)
          {
            int? vendorId1 = i.VendorID;
            int? vendorId2 = bucketLine.VendorID;
            if (vendorId1.GetValueOrDefault() == vendorId2.GetValueOrDefault() & vendorId1.HasValue == vendorId2.HasValue)
            {
              int? lineNbr1 = i.LineNbr;
              int? lineNbr2 = bucketLine.LineNbr;
              return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
            }
          }
          return false;
        })) == null)
          ((PXSelectBase<TaxBucketLine>) this.TaxBucketLines).Delete(bucketLine);
      }
    }
    else
    {
      ((PXSelectBase) this.TaxBucketLine_Vendor_LineNbr).Cache.Clear();
      foreach (TaxReportLine taxReportLine in GraphHelper.RowCast<TaxReportLine>(((PXSelectBase) this.ReportLine).Cache.Inserted))
      {
        foreach (TaxBucketLine taxBucketLine in GraphHelper.RowCast<TaxBucketLine>((IEnumerable) ((PXSelectBase<TaxBucketLine>) this.TaxBucketLine_Vendor_LineNbr).Select(new object[3]
        {
          (object) taxReportLine.VendorID,
          (object) taxReportLine.TaxReportRevisionID,
          (object) taxReportLine.TempLineNbr
        })))
        {
          TaxBucketLine copy = PXCache<TaxBucketLine>.CreateCopy(taxBucketLine);
          copy.LineNbr = taxReportLine.LineNbr;
          copy.TaxReportRevisionID = taxReportLine.TaxReportRevisionID;
          ((PXSelectBase) this.TaxBucketLine_Vendor_LineNbr).Cache.Insert((object) copy);
        }
      }
      foreach (TaxReportLine taxReportLine in GraphHelper.RowCast<TaxReportLine>(((PXSelectBase) this.ReportLine).Cache.Deleted))
      {
        foreach (object obj in ((PXSelectBase<TaxBucketLine>) this.TaxBucketLine_Vendor_LineNbr).Select(new object[3]
        {
          (object) taxReportLine.VendorID,
          (object) taxReportLine.TaxReportRevisionID,
          (object) taxReportLine.LineNbr
        }))
          ((PXSelectBase) this.TaxBucketLine_Vendor_LineNbr).Cache.Delete(obj);
      }
    }
  }

  public static void CheckReportSettingsEditableAndSetWarningTo<TRevisionIDField>(
    PXGraph graph,
    PXCache cache,
    object row,
    int? vendorID,
    int? taxReportRevisionID)
    where TRevisionIDField : IBqlField
  {
    if (!vendorID.HasValue || !taxReportRevisionID.HasValue || !TaxReportMaint.PrepearedTaxPeriodForReportVersionExists(graph, vendorID, taxReportRevisionID))
      return;
    PXFieldState stateExt = (PXFieldState) cache.GetStateExt<TRevisionIDField>(row);
    cache.RaiseExceptionHandling<TRevisionIDField>(row, stateExt.Value, (Exception) new PXSetPropertyException("The report version cannot be modified because there is a prepared tax report in the system for the selected tax agency. You need to release or void the prepared tax report before modifying the settings.", (PXErrorLevel) 2));
  }

  public static void CheckReportSettingsEditable(
    PXGraph graph,
    int? vendorID,
    int? taxReportRevisionID)
  {
    if (vendorID.HasValue && taxReportRevisionID.HasValue && TaxReportMaint.PrepearedTaxPeriodForReportVersionExists(graph, vendorID, taxReportRevisionID))
      throw new PXException("The report version cannot be modified because there is a prepared tax report in the system for the selected tax agency. You need to release or void the prepared tax report before modifying the settings.");
  }

  protected virtual bool IsReportVersionHasHistory(PXGraph graph, int? revisionId, int? vendorID)
  {
    int? rowCount = PXSelectBase<TaxHistory, PXSelectGroupBy<TaxHistory, Where<TaxHistory.taxReportRevisionID, Equal<Required<TaxReport.revisionID>>, And<TaxHistory.vendorID, Equal<Required<TaxReport.vendorID>>>>, Aggregate<Count>>.Config>.Select(graph, new object[2]
    {
      (object) revisionId,
      (object) vendorID
    }).RowCount;
    int num = 0;
    return rowCount.GetValueOrDefault() > num & rowCount.HasValue;
  }

  public static TaxReport GetLastReportVersion(PXGraph graph, int? vendorID)
  {
    return PXResultset<TaxReport>.op_Implicit(PXSelectBase<TaxReport, PXSelect<TaxReport, Where<TaxReport.vendorID, Equal<Required<TaxReport.vendorID>>, And<TaxReport.validTo, Equal<Required<TaxReport.validTo>>>>, OrderBy<Desc<TaxReport.validFrom>>>.Config>.Select(graph, new object[2]
    {
      (object) vendorID,
      (object) TaxReportMaint.MAX_VALIDTO
    }));
  }

  /// <summary>Get last Tax Report Version for VendorID</summary>
  /// <param name="exceptRevisionID">RevisionID to be excluded from search. Set null to get max version</param>
  /// <returns></returns>
  protected virtual TaxReport GetLastReportVersion(
    PXGraph graph,
    int? vendorID,
    int? exceptRevisionID)
  {
    if (!exceptRevisionID.HasValue)
      return TaxReportMaint.GetLastReportVersion(graph, vendorID);
    return PXResultset<TaxReport>.op_Implicit(PXSelectBase<TaxReport, PXSelect<TaxReport, Where<TaxReport.vendorID, Equal<Required<TaxReport.vendorID>>, And<TaxReport.revisionID, NotEqual<Required<TaxReport.revisionID>>, And<TaxReport.validTo, Equal<Required<TaxReport.validTo>>>>>, OrderBy<Desc<TaxReport.validTo>>>.Config>.SelectWindowed(graph, 0, 1, new object[3]
    {
      (object) vendorID,
      (object) exceptRevisionID,
      (object) TaxReportMaint.MAX_VALIDTO
    }));
  }

  protected virtual TaxReport GetPreviuosReportVersion(PXGraph graph, TaxReport taxReport)
  {
    return PXResultset<TaxReport>.op_Implicit(PXSelectBase<TaxReport, PXSelect<TaxReport, Where<TaxReport.vendorID, Equal<Required<TaxReport.vendorID>>, And<TaxReport.validFrom, LessEqual<Required<TaxReport.validTo>>, And<TaxReport.validTo, NotEqual<Required<TaxReport.validTo>>>>>, OrderBy<Desc<TaxReport.validTo>>>.Config>.SelectWindowed(graph, 0, 1, new object[3]
    {
      (object) taxReport.VendorID,
      (object) taxReport.ValidFrom,
      (object) TaxReportMaint.MAX_VALIDTO
    }));
  }

  protected virtual string GetLastReportPeriodInHistory(PXGraph graph, int? vendorID)
  {
    return PXResultset<TaxHistory>.op_Implicit(PXSelectBase<TaxHistory, PXSelect<TaxHistory, Where<TaxHistory.vendorID, Equal<Required<TaxReport.vendorID>>>, OrderBy<Desc<TaxHistory.taxPeriodID>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
    {
      (object) vendorID
    }))?.TaxPeriodID;
  }

  public static bool PrepearedTaxPeriodForReportVersionExists(
    PXGraph graph,
    int? vendorID,
    int? taxReportRevisionID)
  {
    return PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelectJoin<TaxPeriod, LeftJoin<TaxReport, On<TaxReport.vendorID, Equal<TaxPeriod.vendorID>>>, Where<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxReport.revisionID, Equal<Required<TaxReport.revisionID>>, And<TaxPeriod.status, Equal<TaxPeriodStatus.prepared>, And<Add<TaxPeriod.endDate, int_1>, Between<TaxReport.validFrom, TaxReport.validTo>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) vendorID,
      (object) taxReportRevisionID
    })) != null;
  }

  public static TaxReport GetTaxReportVersionByRevisionID(
    PXGraph graph,
    int? vendorID,
    int? revisionID)
  {
    return PXResultset<TaxReport>.op_Implicit(PXSelectBase<TaxReport, PXSelect<TaxReport, Where<TaxReport.vendorID, Equal<Required<TaxReport.vendorID>>, And<TaxReport.revisionID, Equal<Required<TaxReport.revisionID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) vendorID,
      (object) revisionID
    }));
  }

  public static TaxReport GetTaxReportVersionByDate(
    PXGraph graph,
    int? vendorID,
    DateTime? searchDate)
  {
    if (!vendorID.HasValue || !searchDate.HasValue)
      return (TaxReport) null;
    return GraphHelper.RowCast<TaxReport>((IEnumerable) PXSelectBase<TaxReport, PXSelect<TaxReport, Where<TaxReport.vendorID, Equal<Required<TaxReport.vendorID>>, And<TaxReport.validFrom, LessEqual<Required<TaxReport.validFrom>>>>, OrderBy<Desc<TaxReport.validFrom>>>.Config>.Select(graph, new object[2]
    {
      (object) vendorID,
      (object) searchDate
    })).ToList<TaxReport>().FirstOrDefault<TaxReport>();
  }

  /// <summary>Get Tax Report Version by date</summary>
  /// <param name="exceptRevisionID">RevisionID to be excluded from search. Set null to get all versions</param>
  /// <returns></returns>
  public static TaxReport GetTaxReportVersionByDate(
    PXGraph graph,
    int? vendorID,
    DateTime? searchDate,
    int? exceptRevisionID)
  {
    if (!vendorID.HasValue || !searchDate.HasValue)
      return (TaxReport) null;
    if (!exceptRevisionID.HasValue)
      return TaxReportMaint.GetTaxReportVersionByDate(graph, vendorID, searchDate);
    return GraphHelper.RowCast<TaxReport>((IEnumerable) PXSelectBase<TaxReport, PXSelect<TaxReport, Where<TaxReport.vendorID, Equal<Required<TaxReport.vendorID>>, And<TaxReport.validFrom, LessEqual<Required<TaxReport.validFrom>>, And<TaxReport.revisionID, NotEqual<Required<TaxReport.revisionID>>>>>, OrderBy<Desc<TaxReport.validFrom>>>.Config>.Select(graph, new object[3]
    {
      (object) vendorID,
      (object) searchDate,
      (object) exceptRevisionID
    })).ToList<TaxReport>().FirstOrDefault<TaxReport>();
  }

  private void CheckAndWarnTaxBoxNumbers()
  {
    HashSet<string> taxboxNumbers = new HashSet<string>();
    foreach (TaxReportLine taxReportLine in GraphHelper.RowCast<TaxReportLine>((IEnumerable) ((PXSelectBase<TaxReportLine>) this.ReportLine).Select(Array.Empty<object>())).Where<TaxReportLine>((Func<TaxReportLine, bool>) (line => line.ReportLineNbr != null)))
    {
      if (((PXSelectBase) this.ReportLine).Cache.GetStatus((object) taxReportLine) == null)
        taxboxNumbers.Add(taxReportLine.ReportLineNbr);
    }
    this.CheckTaxBoxNumberUniqueness(((PXSelectBase) this.ReportLine).Cache.Inserted, taxboxNumbers);
    this.CheckTaxBoxNumberUniqueness(((PXSelectBase) this.ReportLine).Cache.Updated, taxboxNumbers);
  }

  public virtual void CheckTaxBoxNumberUniqueness(
    IEnumerable toBeChecked,
    HashSet<string> taxboxNumbers)
  {
    foreach (TaxReportLine taxReportLine in toBeChecked.OfType<TaxReportLine>().Where<TaxReportLine>((Func<TaxReportLine, bool>) (line => line.ReportLineNbr != null && !taxboxNumbers.Add(line.ReportLineNbr))))
      ((PXSelectBase) this.ReportLine).Cache.RaiseExceptionHandling<TaxReportLine.reportLineNbr>((object) taxReportLine, (object) taxReportLine.ReportLineNbr, (Exception) new PXSetPropertyException("Tax box numbers must be unique."));
  }

  protected virtual void TaxReportLine_SortOrder_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    TaxReport current = ((PXSelectBase<TaxReport>) this.Report).Current;
    TaxReportLine row = (TaxReportLine) e.Row;
    int? newSortOrder = e.NewValue as int?;
    if (row == null || row.TempLineNbr.HasValue || current == null || current.ShowNoTemp.GetValueOrDefault())
      return;
    int? sortOrder = row.SortOrder;
    int? nullable1 = newSortOrder;
    if (!(sortOrder.GetValueOrDefault() == nullable1.GetValueOrDefault() & sortOrder.HasValue == nullable1.HasValue))
    {
      if (newSortOrder.HasValue)
      {
        int? nullable2 = newSortOrder;
        int num1 = 0;
        if (!(nullable2.GetValueOrDefault() <= num1 & nullable2.HasValue))
        {
          if (!GraphHelper.RowCast<TaxReportLine>((IEnumerable) ((PXSelectBase<TaxReportLine>) this.ReportLine).Select(Array.Empty<object>())).Any<TaxReportLine>((Func<TaxReportLine, bool>) (line =>
          {
            int num2 = line.SortOrder.Value;
            int? nullable3 = newSortOrder;
            int valueOrDefault = nullable3.GetValueOrDefault();
            return num2 == valueOrDefault & nullable3.HasValue;
          })))
            return;
          throw new PXSetPropertyException("The report line number is in use.");
        }
      }
      throw new PXSetPropertyException(!newSortOrder.HasValue ? "'{0}' must have a value." : "'{0}' should be positive.", new object[1]
      {
        (object) "[sortOrder]"
      });
    }
  }

  protected virtual void TaxReportLine_SortOrder_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    TaxReportLine row = (TaxReportLine) e.Row;
    if (row == null || row.TempLineNbr.HasValue || object.Equals((object) row.SortOrder, e.OldValue))
      return;
    if (row.TempLine.GetValueOrDefault())
    {
      foreach (PXResult<TaxReportLine> pxResult in PXSelectBase<TaxReportLine, PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Required<TaxReportLine.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Required<TaxReportLine.taxReportRevisionID>>, And<TaxReportLine.tempLineNbr, Equal<Required<TaxReportLine.tempLineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) row.VendorID,
        (object) row.LineNbr,
        (object) row.TaxReportRevisionID
      }))
      {
        TaxReportLine taxReportLine = PXResult<TaxReportLine>.op_Implicit(pxResult);
        taxReportLine.SortOrder = row.SortOrder;
        GraphHelper.SmartSetStatus(((PXSelectBase) this.ReportLine).Cache, (object) taxReportLine, (PXEntryStatus) 1, (PXEntryStatus) 0);
      }
    }
    ((PXSelectBase) this.ReportLine).View.RequestRefresh();
  }

  protected virtual void TaxReportLine_LineType_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    TaxReportLine row = (TaxReportLine) e.Row;
    if (e.NewValue == null)
      return;
    bool? netTax = row.NetTax;
    if (!netTax.HasValue)
      return;
    netTax = row.NetTax;
    if (netTax.Value && (string) e.NewValue == "A")
      throw new PXSetPropertyException("Net Tax line must have 'Tax' type.", (PXErrorLevel) 5);
  }

  protected virtual void TaxReportLine_NetTax_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    TaxReportLine row = (TaxReportLine) e.Row;
    if (e.NewValue != null && (bool) e.NewValue && row.LineType == "A")
      throw new PXSetPropertyException("Net Tax line must have 'Tax' type.", (PXErrorLevel) 5);
  }

  public TaxReportMaint()
  {
    PX.Objects.AP.APSetup current = ((PXSelectBase<PX.Objects.AP.APSetup>) this.APSetup).Current;
    PXUIFieldAttribute.SetVisible<TaxReportLine.lineNbr>(((PXSelectBase) this.ReportLine).Cache, (object) null, false);
    this.taxDetailsReloader = new TaxReportLinesByTaxZonesReloader(this);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(TaxReportMaint.\u003C\u003Ec.\u003C\u003E9__69_0 ?? (TaxReportMaint.\u003C\u003Ec.\u003C\u003E9__69_0 = new PXFieldDefaulting((object) TaxReportMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__69_0))));
  }

  public class TaxBucketAnalizer
  {
    private Dictionary<int, List<int>> _bucketsLinesAggregates;
    private Dictionary<int, List<int>> _bucketsLinesAggregatesSorted;
    private Dictionary<int, List<int>> _bucketsDict;
    private Dictionary<int, int> _bucketLinesOccurence;
    private Dictionary<int, Dictionary<int, int>> _bucketsLinesPairs;
    private int _bAccountID;
    private int _taxreportVersionID;
    private string _taxLineType;
    private PXGraph _graph;
    private PXSelectJoin<TaxBucketLine, LeftJoin<TaxReportLine, On<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>, And<TaxBucketLine.taxReportRevisionID, Equal<TaxReportLine.taxReportRevisionID>, And<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>>>>, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<TaxBucketLine.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>, LeftJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<PX.Objects.AP.Vendor.curyID>>>>>, Where<TaxBucketLine.vendorID, Equal<Required<TaxBucketLine.vendorID>>, And<TaxBucketLine.taxReportRevisionID, Equal<Required<TaxBucketLine.taxReportRevisionID>>, And<TaxReportLine.lineType, Equal<Required<TaxReportLine.lineType>>>>>> _vendorBucketLines;

    public Func<PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>, bool> showTaxReportLine { get; set; }

    private IEnumerable<PXResult<TaxBucketLine>> selectBucketLines(
      int VendorId,
      string LineType,
      int TaxReportVersion)
    {
      return (IEnumerable<PXResult<TaxBucketLine>>) ((IEnumerable<PXResult<TaxBucketLine>>) ((PXSelectBase<TaxBucketLine>) this._vendorBucketLines).Select(new object[3]
      {
        (object) VendorId,
        (object) TaxReportVersion,
        (object) LineType
      })).AsEnumerable<PXResult<TaxBucketLine>>().Cast<PXResult<TaxBucketLine, TaxReportLine, PX.Objects.AP.Vendor, PX.Objects.CM.Extensions.Currency>>().Where<PXResult<TaxBucketLine, TaxReportLine, PX.Objects.AP.Vendor, PX.Objects.CM.Extensions.Currency>>((Func<PXResult<TaxBucketLine, TaxReportLine, PX.Objects.AP.Vendor, PX.Objects.CM.Extensions.Currency>, bool>) (set => this.showTaxReportLine(new PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>(PXResult<TaxBucketLine, TaxReportLine, PX.Objects.AP.Vendor, PX.Objects.CM.Extensions.Currency>.op_Implicit(set), PXResult<TaxBucketLine, TaxReportLine, PX.Objects.AP.Vendor, PX.Objects.CM.Extensions.Currency>.op_Implicit(set), PXResult<TaxBucketLine, TaxReportLine, PX.Objects.AP.Vendor, PX.Objects.CM.Extensions.Currency>.op_Implicit(set), new TaxTranRevReport()))));
    }

    public TaxBucketAnalizer(
      PXGraph graph,
      int BAccountID,
      string TaxLineType,
      int TaxreportRevisionID)
    {
      this._bAccountID = BAccountID;
      this._taxreportVersionID = TaxreportRevisionID;
      this._taxLineType = TaxLineType;
      this._graph = graph;
      this._vendorBucketLines = new PXSelectJoin<TaxBucketLine, LeftJoin<TaxReportLine, On<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>, And<TaxBucketLine.taxReportRevisionID, Equal<TaxReportLine.taxReportRevisionID>, And<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>>>>, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<TaxBucketLine.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>, LeftJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<PX.Objects.AP.Vendor.curyID>>>>>, Where<TaxBucketLine.vendorID, Equal<Required<TaxBucketLine.vendorID>>, And<TaxBucketLine.taxReportRevisionID, Equal<Required<TaxBucketLine.taxReportRevisionID>>, And<TaxReportLine.lineType, Equal<Required<TaxReportLine.lineType>>>>>>(this._graph);
      this.showTaxReportLine = (Func<PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>, bool>) (line => true);
    }

    public Dictionary<int, List<int>> AnalyzeBuckets(bool CalcWithZones)
    {
      this.calcOccurances(CalcWithZones);
      this.fillAgregates();
      return this._bucketsLinesAggregatesSorted;
    }

    public void DoChecks(int BucketID)
    {
      if (this._bucketsDict == null)
      {
        this.calcOccurances(true);
        this.fillAgregates();
      }
      this.doChecks(BucketID);
    }

    public static void CheckTaxAgencySettings(
      PXGraph graph,
      int BAccountID,
      int TaxReportRevisionID)
    {
      PXResultset<TaxBucket> pxResultset = PXSelectBase<TaxBucket, PXSelect<TaxBucket, Where<TaxBucket.vendorID, Equal<Required<TaxBucket.vendorID>>>>.Config>.Select(graph, new object[1]
      {
        (object) BAccountID
      });
      if (pxResultset == null)
        return;
      TaxReportMaint.TaxBucketAnalizer taxBucketAnalizer1 = new TaxReportMaint.TaxBucketAnalizer(graph, BAccountID, "P", TaxReportRevisionID);
      TaxReportMaint.TaxBucketAnalizer taxBucketAnalizer2 = new TaxReportMaint.TaxBucketAnalizer(graph, BAccountID, "A", TaxReportRevisionID);
      foreach (PXResult<TaxBucket> pxResult in pxResultset)
      {
        int BucketID = PXResult<TaxBucket>.op_Implicit(pxResult).BucketID.Value;
        taxBucketAnalizer1.DoChecks(BucketID);
        taxBucketAnalizer2.DoChecks(BucketID);
      }
    }

    [Obsolete("Will be removed in future versions of Acumatica")]
    public static Dictionary<int, int> TransposeDictionary(Dictionary<int, List<int>> oldDict)
    {
      if (oldDict == null)
        return (Dictionary<int, int>) null;
      Dictionary<int, int> dictionary = new Dictionary<int, int>(oldDict.Count);
      foreach (KeyValuePair<int, List<int>> keyValuePair in oldDict)
      {
        foreach (int key in keyValuePair.Value)
          dictionary[key] = keyValuePair.Key;
      }
      return dictionary;
    }

    public static bool IsSubList(List<int> searchList, List<int> subList)
    {
      if (subList.Count > searchList.Count)
        return false;
      for (int index = 0; index < subList.Count; ++index)
      {
        if (!searchList.Contains(subList[index]))
          return false;
      }
      return true;
    }

    public static List<int> SubstList(List<int> searchList, List<int> substList, int substVal)
    {
      if (!TaxReportMaint.TaxBucketAnalizer.IsSubList(searchList, substList))
        return searchList;
      List<int> resList = searchList.ToList<int>();
      substList.ForEach((Action<int>) (val => resList.Remove(val)));
      resList.Add(substVal);
      return resList;
    }

    private void calcOccurances(bool CalcWithZones)
    {
      if (!CalcWithZones)
        ((PXSelectBase<TaxBucketLine>) this._vendorBucketLines).WhereAnd<Where<TaxReportLine.tempLineNbr, IsNull>>();
      IEnumerable<PXResult<TaxBucketLine>> pxResults = this.selectBucketLines(this._bAccountID, this._taxLineType, this._taxreportVersionID);
      if (pxResults == null)
      {
        this._bucketsDict = (Dictionary<int, List<int>>) null;
      }
      else
      {
        this._bucketsDict = new Dictionary<int, List<int>>();
        foreach (PXResult<TaxBucketLine> pxResult in pxResults)
        {
          TaxBucketLine taxBucketLine = (TaxBucketLine) ((PXResult) pxResult)[typeof (TaxBucketLine)];
          TaxReportLine taxReportLine = (TaxReportLine) ((PXResult) pxResult)[typeof (TaxReportLine)];
          int? nullable = taxBucketLine.BucketID;
          if (nullable.HasValue)
          {
            nullable = taxReportLine.LineNbr;
            if (nullable.HasValue)
            {
              Dictionary<int, List<int>> bucketsDict1 = this._bucketsDict;
              nullable = taxBucketLine.BucketID;
              int key1 = nullable.Value;
              if (!bucketsDict1.ContainsKey(key1))
              {
                Dictionary<int, List<int>> bucketsDict2 = this._bucketsDict;
                nullable = taxBucketLine.BucketID;
                int key2 = nullable.Value;
                List<int> intList = new List<int>();
                bucketsDict2[key2] = intList;
              }
              Dictionary<int, List<int>> bucketsDict3 = this._bucketsDict;
              nullable = taxBucketLine.BucketID;
              int key3 = nullable.Value;
              List<int> intList1 = bucketsDict3[key3];
              nullable = taxBucketLine.LineNbr;
              int num = nullable.Value;
              intList1.Add(num);
            }
          }
        }
        List<int> list = this._bucketsDict.Keys.ToList<int>();
        for (int index1 = 0; index1 < list.Count; ++index1)
        {
          for (int index2 = index1 + 1; index2 < list.Count; ++index2)
          {
            if (this._bucketsDict[list[index1]].Count == this._bucketsDict[list[index2]].Count && TaxReportMaint.TaxBucketAnalizer.IsSubList(this._bucketsDict[list[index1]], this._bucketsDict[list[index2]]))
            {
              this._bucketsDict.Remove(list[index1]);
              break;
            }
          }
        }
        this._bucketLinesOccurence = new Dictionary<int, int>();
        this._bucketsLinesPairs = new Dictionary<int, Dictionary<int, int>>();
        foreach (KeyValuePair<int, List<int>> keyValuePair in this._bucketsDict)
        {
          foreach (int key in keyValuePair.Value)
          {
            if (!this._bucketLinesOccurence.ContainsKey(key))
              this._bucketLinesOccurence[key] = 0;
            this._bucketLinesOccurence[key]++;
          }
          for (int index3 = 0; index3 < keyValuePair.Value.Count - 1; ++index3)
          {
            for (int index4 = index3 + 1; index4 < keyValuePair.Value.Count; ++index4)
            {
              int key4;
              int key5;
              if (keyValuePair.Value[index3] < keyValuePair.Value[index4])
              {
                key4 = keyValuePair.Value[index3];
                key5 = keyValuePair.Value[index4];
              }
              else
              {
                key4 = keyValuePair.Value[index4];
                key5 = keyValuePair.Value[index3];
              }
              if (!this._bucketsLinesPairs.ContainsKey(key4))
                this._bucketsLinesPairs[key4] = new Dictionary<int, int>();
              if (!this._bucketsLinesPairs[key4].ContainsKey(key5))
                this._bucketsLinesPairs[key4][key5] = 0;
              this._bucketsLinesPairs[key4][key5]++;
            }
          }
        }
      }
    }

    private void fillAgregates()
    {
      if (this._bucketsDict == null || this._bucketLinesOccurence == null || this._bucketsLinesPairs == null)
        return;
      this._bucketsLinesAggregates = new Dictionary<int, List<int>>();
      foreach (KeyValuePair<int, Dictionary<int, int>> bucketsLinesPair in this._bucketsLinesPairs)
      {
        foreach (KeyValuePair<int, int> keyValuePair in bucketsLinesPair.Value)
        {
          if (keyValuePair.Value == 1)
          {
            int num1 = this._bucketLinesOccurence[bucketsLinesPair.Key];
            int num2 = this._bucketLinesOccurence[keyValuePair.Key];
            int key = 0;
            int num3 = 0;
            if (num1 != num2)
            {
              if (num1 > num2)
              {
                key = bucketsLinesPair.Key;
                num3 = keyValuePair.Key;
              }
              else
              {
                key = keyValuePair.Key;
                num3 = bucketsLinesPair.Key;
              }
            }
            if (key != 0)
            {
              if (!this._bucketsLinesAggregates.ContainsKey(key))
                this._bucketsLinesAggregates[key] = new List<int>();
              this._bucketsLinesAggregates[key].Add(num3);
            }
          }
        }
      }
      List<KeyValuePair<int, List<int>>> list = this._bucketsLinesAggregates.ToList<KeyValuePair<int, List<int>>>();
      list.Sort((Comparison<KeyValuePair<int, List<int>>>) ((firstPair, nextPair) => firstPair.Value.Count - nextPair.Value.Count != 0 ? firstPair.Value.Count - nextPair.Value.Count : firstPair.Key - nextPair.Key));
      for (int index1 = 0; index1 < list.Count; ++index1)
      {
        for (int index2 = index1 + 1; index2 < list.Count; ++index2)
        {
          KeyValuePair<int, List<int>> keyValuePair = list[index2];
          List<int> searchList = keyValuePair.Value;
          keyValuePair = list[index1];
          List<int> substList = keyValuePair.Value;
          keyValuePair = list[index1];
          int key = keyValuePair.Key;
          List<int> collection = TaxReportMaint.TaxBucketAnalizer.SubstList(searchList, substList, key);
          List<int> intList1 = collection;
          keyValuePair = list[index2];
          List<int> intList2 = keyValuePair.Value;
          if (intList1 != intList2)
          {
            keyValuePair = list[index2];
            keyValuePair.Value.Clear();
            keyValuePair = list[index2];
            keyValuePair.Value.AddRange((IEnumerable<int>) collection);
          }
        }
      }
      this._bucketsLinesAggregatesSorted = new Dictionary<int, List<int>>();
      foreach (KeyValuePair<int, List<int>> keyValuePair in list)
      {
        keyValuePair.Value.Sort();
        this._bucketsLinesAggregatesSorted[keyValuePair.Key] = keyValuePair.Value;
      }
    }

    private void doChecks(int BucketID)
    {
      if (this._bucketsLinesAggregatesSorted == null)
        throw new PXException("Unexpected call");
      int num1 = 0;
      int num2 = 0;
      if (!this._bucketsDict.ContainsKey(BucketID))
        return;
      foreach (int key in this._bucketsDict[BucketID])
      {
        if (this._bucketsLinesAggregatesSorted.ContainsKey(key))
          ++num2;
        else
          ++num1;
      }
      if (num2 > 0 && num1 == 0)
        throw new PXSetPropertyException("Tax reporting group {0} contains only aggregate amount lines! Please review your tax reporting setup.", (PXErrorLevel) 4, new object[1]
        {
          (object) BucketID.ToString()
        });
    }
  }
}
