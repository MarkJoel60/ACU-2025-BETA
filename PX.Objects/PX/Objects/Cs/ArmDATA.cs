// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ArmDATA
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Reports;
using PX.DbServices.QueryObjectModel;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS;

public class ArmDATA : SoapNavigator.DATA
{
  public object FormatPeriod(object period)
  {
    if (period is string)
      period = (object) ((string) period).Replace("-", "");
    return this.ExtToUI((object) "RowBatch.FinPeriodID", period);
  }

  public object FormatPeriod(object period, object shift)
  {
    try
    {
      string str = ((string) period).Replace("-", "");
      return this.FormatPeriod((object) this.ShiftPeriod(0, $"{str.Substring(2):0000}{str.Substring(0, 2):00}", shift));
    }
    catch
    {
      return this.FormatPeriod(period);
    }
  }

  public object FormatPeriod(object period, object shiftYear, object shiftPeriod)
  {
    try
    {
      string str = ((string) period).Replace("-", "");
      int num = int.Parse(str.Substring(2));
      if (shiftYear != null && (int) shiftYear != 0)
        num += (int) shiftYear;
      return (object) this.ShiftPeriod(0, $"{num.ToString():0000}{str.Substring(0, 2):00}", shiftPeriod);
    }
    catch
    {
      return this.FormatPeriod(period);
    }
  }

  private string ShiftPeriod(int organizationId, string periodString, object shift)
  {
    int int32 = Convert.ToInt32(shift);
    PXSelectBase<FinPeriod> pxSelectBase = (PXSelectBase<FinPeriod>) new PXSelect<FinPeriod, Where<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>(this._Graph);
    if (int32 < 0)
      pxSelectBase.WhereAnd<Where<FinPeriod.finPeriodID, Less<Required<FinPeriod.finPeriodID>>>>();
    else
      pxSelectBase.WhereAnd<Where<FinPeriod.finPeriodID, GreaterEqual<Required<FinPeriod.finPeriodID>>>>();
    pxSelectBase.OrderByNew<OrderBy<Asc<FinPeriod.finPeriodID>>>();
    FinPeriod finPeriod = PXResultset<FinPeriod>.op_Implicit(pxSelectBase.SelectWindowed(int32, 1, new object[2]
    {
      (object) organizationId,
      (object) periodString
    }));
    return $"{finPeriod.FinPeriodID.Substring(4, 2):00}{finPeriod.FinPeriodID.Substring(0, 4):0000}";
  }

  public object FormatYear(object period)
  {
    string str = this.FormatPeriod(period) as string;
    if (!string.IsNullOrEmpty(str))
    {
      int num = str.IndexOf("-");
      if (num >= 0 && num < str.Length - 1)
        return (object) str.Substring(num + 1);
    }
    return (object) str;
  }

  public object FormatYear(object period, object shift)
  {
    try
    {
      int int32 = Convert.ToInt32(shift);
      return (object) $"{int.Parse((string) this.FormatYear(period)) + int32:0000}";
    }
    catch
    {
      return this.FormatYear(period);
    }
  }

  public object GetNumberOfPeriods(object startPeriod, object endPeriod)
  {
    return this.GetNumberOfPeriods((object) 0, startPeriod, endPeriod);
  }

  public object GetNumberOfPeriods(object organizationId, object startPeriod, object endPeriod)
  {
    try
    {
      organizationId = this.CorrectOrganizationId(organizationId);
      IEnumerable<PXDataRecord> source = PXDatabase.SelectMulti<FinPeriod>(new PXDataField[5]
      {
        (PXDataField) new PXDataField<FinPeriod.finPeriodID>(),
        (PXDataField) new PXDataFieldValue<FinPeriod.finPeriodID>((PXDbType) 3, new int?(6), (object) ArmDATA.FormatForStoring(startPeriod as string), (PXComp) 3),
        (PXDataField) new PXDataFieldValue<FinPeriod.finPeriodID>((PXDbType) 3, new int?(6), (object) ArmDATA.FormatForStoring(endPeriod as string), (PXComp) 5),
        (PXDataField) new PXDataFieldValue<FinPeriod.organizationID>((PXDbType) 8, new int?(4), organizationId),
        (PXDataField) new PXDataFieldOrder<FinPeriod.finPeriodID>()
      });
      if (source != null)
        return (object) (source.Count<PXDataRecord>() - 1);
    }
    catch
    {
    }
    return (object) null;
  }

  public object GetPeriodStartDate(object period) => this.GetPeriodStartDate((object) 0, period);

  public object GetPeriodStartDate(object organizationId, object period)
  {
    try
    {
      string str = ArmDATA.FormatForStoring(period as string);
      organizationId = this.CorrectOrganizationId(organizationId);
      if (str != null)
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<FinPeriod>(new PXDataField[4]
        {
          (PXDataField) new PXDataField<FinPeriod.startDate>(),
          (PXDataField) new PXDataField<FinPeriod.endDate>(),
          (PXDataField) new PXDataFieldValue<FinPeriod.finPeriodID>((PXDbType) 3, new int?(6), (object) str),
          (PXDataField) new PXDataFieldValue<FinPeriod.organizationID>((PXDbType) 8, new int?(4), organizationId)
        }))
        {
          if (pxDataRecord != null)
          {
            DateTime? dateTime1 = pxDataRecord.GetDateTime(0);
            DateTime? dateTime2 = pxDataRecord.GetDateTime(1);
            DateTime? nullable1 = dateTime1;
            DateTime? nullable2 = dateTime2;
            return (nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 ? (object) (dateTime1.HasValue ? new DateTime?(dateTime1.Value.AddDays(-1.0)) : dateTime1) : (object) dateTime1;
          }
        }
      }
    }
    catch
    {
    }
    return (object) null;
  }

  private object CorrectOrganizationId(object organizationId)
  {
    if (organizationId is string)
      organizationId = (object) PXAccess.GetOrganizationID(((string) organizationId)?.Trim());
    return organizationId;
  }

  public object GetPeriodEndDate(object period) => this.GetPeriodEndDate((object) 0, period);

  public object GetPeriodEndDate(object organizationId, object period)
  {
    try
    {
      string str = ArmDATA.FormatForStoring(period as string);
      organizationId = this.CorrectOrganizationId(organizationId);
      if (str != null)
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<FinPeriod>(new PXDataField[3]
        {
          (PXDataField) new PXDataField<FinPeriod.endDate>(),
          (PXDataField) new PXDataFieldValue<FinPeriod.finPeriodID>((PXDbType) 3, new int?(6), (object) str),
          (PXDataField) new PXDataFieldValue<FinPeriod.organizationID>((PXDbType) 8, new int?(4), organizationId)
        }))
        {
          if (pxDataRecord != null)
          {
            DateTime? dateTime = pxDataRecord.GetDateTime(0);
            return (object) (dateTime.HasValue ? new DateTime?(dateTime.Value.AddDays(-1.0)) : dateTime);
          }
        }
      }
    }
    catch
    {
    }
    return (object) null;
  }

  public object GetPeriodDescription(object period)
  {
    return this.GetPeriodDescription((object) 0, period);
  }

  public object GetPeriodDescription(object organizationId, object period)
  {
    try
    {
      string str = ArmDATA.FormatForStoring(period as string);
      organizationId = this.CorrectOrganizationId(organizationId);
      if (str != null)
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<FinPeriod>(new PXDataField[3]
        {
          (PXDataField) new PXDataField<FinPeriod.descr>(),
          (PXDataField) new PXDataFieldValue<FinPeriod.finPeriodID>((PXDbType) 3, new int?(6), (object) str),
          (PXDataField) new PXDataFieldValue<FinPeriod.organizationID>((PXDbType) 8, new int?(4), organizationId)
        }))
        {
          if (pxDataRecord != null)
            return (object) pxDataRecord.GetString(0);
        }
      }
    }
    catch
    {
    }
    return (object) null;
  }

  private static string FormatForStoring(string period)
  {
    if (string.IsNullOrEmpty(period))
      return (string) null;
    period = period.Replace("-", "");
    return period.Trim().Length != 6 ? period : period.Substring(2, 4) + period.Substring(0, 2);
  }

  public object GetBranchText(object branchCode)
  {
    try
    {
      string str = branchCode as string;
      if (!string.IsNullOrEmpty(str))
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PX.Objects.GL.Branch>(Yaql.join<PX.Objects.CR.BAccount>(Yaql.eq<PX.Objects.CR.BAccount.bAccountID, PX.Objects.GL.Branch.bAccountID>("<declaring_type_name>", "<declaring_type_name>"), (YaqlJoinType) 0), new PXDataField[2]
        {
          (PXDataField) new PXDataField<PX.Objects.CR.BAccount.acctName>(),
          (PXDataField) new PXDataFieldValue<PX.Objects.GL.Branch.branchCD>((object) str)
        }))
          return pxDataRecord == null ? (object) (string) null : (object) pxDataRecord.GetString(0);
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
    }
    return (object) null;
  }
}
