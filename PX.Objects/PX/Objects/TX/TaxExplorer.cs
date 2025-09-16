// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxExplorer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public class TaxExplorer : PXGraph<TaxExplorer>
{
  public PXFilter<TaxBuilderFilter> Filter;
  public PXSelect<TaxRecord> TaxRecords;
  public PXSelect<ZoneRecord> ZoneRecords;
  public PXSelectJoin<ZoneDetailRecord, InnerJoin<TaxRecord, On<ZoneDetailRecord.taxID, Equal<TaxRecord.taxID>>>> ZoneDetailRecords;
  private TaxBuilder.Result result;

  public virtual IEnumerable taxRecords()
  {
    TaxBuilderFilter current = ((PXSelectBase<TaxBuilderFilter>) this.Filter).Current;
    if (current != null && current.State != null)
    {
      bool found = false;
      foreach (TaxRecord taxRecord in ((PXSelectBase) this.TaxRecords).Cache.Inserted)
      {
        found = true;
        yield return (object) taxRecord;
      }
      if (!found)
      {
        foreach (TaxRecord tax in (IEnumerable<TaxRecord>) this.Taxes)
        {
          ((PXSelectBase) this.TaxRecords).Cache.SetStatus((object) tax, (PXEntryStatus) 2);
          yield return (object) tax;
        }
      }
    }
  }

  public virtual IEnumerable zoneRecords()
  {
    TaxBuilderFilter current = ((PXSelectBase<TaxBuilderFilter>) this.Filter).Current;
    if (current != null && current.State != null)
    {
      bool found = false;
      foreach (ZoneRecord zoneRecord in ((PXSelectBase) this.ZoneRecords).Cache.Inserted)
      {
        found = true;
        yield return (object) zoneRecord;
      }
      if (!found)
      {
        foreach (ZoneRecord zone in (IEnumerable<ZoneRecord>) this.Zones)
        {
          ((PXSelectBase) this.ZoneRecords).Cache.SetStatus((object) zone, (PXEntryStatus) 2);
          yield return (object) zone;
        }
      }
    }
  }

  public virtual IEnumerable zoneDetailRecords(string zoneID)
  {
    TaxBuilderFilter current = ((PXSelectBase<TaxBuilderFilter>) this.Filter).Current;
    if (current != null && current.State != null && !string.IsNullOrEmpty(zoneID))
    {
      bool found = false;
      foreach (ZoneDetailRecord zoneDetailRecord in ((PXSelectBase) this.ZoneDetailRecords).Cache.Inserted)
      {
        ZoneDetailRecord item = zoneDetailRecord;
        if (item.ZoneID == zoneID)
        {
          TaxRecord taxRecord = this.Taxes.Where<TaxRecord>((Func<TaxRecord, bool>) (t => t.TaxID == item.TaxID)).Single<TaxRecord>();
          PXResult<ZoneDetailRecord, TaxRecord> pxResult = new PXResult<ZoneDetailRecord, TaxRecord>(item, taxRecord);
          found = true;
          yield return (object) pxResult;
        }
      }
      if (!found)
      {
        foreach (ZoneDetailRecord zoneDetail in (IEnumerable<ZoneDetailRecord>) this.ZoneDetails)
        {
          ZoneDetailRecord record = zoneDetail;
          if (record.ZoneID == zoneID)
          {
            ((PXSelectBase) this.ZoneDetailRecords).Cache.SetStatus((object) record, (PXEntryStatus) 2);
            TaxRecord taxRecord = this.Taxes.Where<TaxRecord>((Func<TaxRecord, bool>) (t => t.TaxID == record.TaxID)).Single<TaxRecord>();
            yield return (object) new PXResult<ZoneDetailRecord, TaxRecord>(record, taxRecord);
          }
        }
      }
    }
  }

  public IList<TaxRecord> Taxes
  {
    get
    {
      if (this.result == null)
        this.result = TaxBuilderEngine.Execute((PXGraph) this, ((PXSelectBase<TaxBuilderFilter>) this.Filter).Current.State);
      return this.result.Taxes;
    }
  }

  public IList<ZoneRecord> Zones
  {
    get
    {
      if (this.result == null)
        this.result = TaxBuilderEngine.Execute((PXGraph) this, ((PXSelectBase<TaxBuilderFilter>) this.Filter).Current.State);
      return this.result.Zones;
    }
  }

  public IList<ZoneDetailRecord> ZoneDetails
  {
    get
    {
      if (this.result == null)
        this.result = TaxBuilderEngine.Execute((PXGraph) this, ((PXSelectBase<TaxBuilderFilter>) this.Filter).Current.State);
      return this.result.ZoneDetails;
    }
  }

  protected virtual void TaxBuilderFilter_State_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TaxBuilderFilter))
      return;
    this.result = (TaxBuilder.Result) null;
    ((PXSelectBase) this.TaxRecords).Cache.Clear();
    ((PXSelectBase) this.ZoneRecords).Cache.Clear();
    ((PXSelectBase) this.ZoneDetailRecords).Cache.Clear();
  }
}
