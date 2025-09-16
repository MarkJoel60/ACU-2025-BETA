// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INABCCodeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.IN;

public class INABCCodeMaint : PXGraph<INABCCodeMaint>
{
  public PXFilter<INABCTotal> ABCTotals;
  public PXSelect<INABCCode> ABCCodes;
  public PXSave<INABCCode> Save;
  public PXCancel<INABCCode> Cancel;

  private Decimal CalcTotalABCPct()
  {
    Decimal num = 0.0M;
    foreach (PXResult<INABCCode> pxResult in ((PXSelectBase<INABCCode>) this.ABCCodes).Select(Array.Empty<object>()))
    {
      INABCCode inabcCode = PXResult<INABCCode>.op_Implicit(pxResult);
      num += inabcCode.ABCPct.GetValueOrDefault();
    }
    return num;
  }

  protected virtual IEnumerable aBCTotals()
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    INABCCodeMaint inabcCodeMaint = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    PXCache cach = ((PXGraph) inabcCodeMaint).Caches[typeof (INABCTotal)];
    INABCTotal current = (INABCTotal) cach.Current;
    current.TotalABCPct = new Decimal?(inabcCodeMaint.CalcTotalABCPct());
    cach.IsDirty = false;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) current;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  protected virtual void INABCCode_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    Decimal num = this.CalcTotalABCPct();
    if (num != 0M && num != 100M)
      throw new PXRowPersistingException(typeof (INABCTotal.totalABCPct).Name, (object) num, "Total % should be 100%");
  }

  public virtual void INABCCode_CountsPerYear_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue != null && ((short) e.NewValue < (short) 0 || (short) e.NewValue > (short) 365))
      throw new PXSetPropertyException("This value should be between {0} and {1}", (PXErrorLevel) 4, new object[2]
      {
        (object) 0,
        (object) 365
      });
  }

  public virtual void INABCCode_MaxCountInaccuracyPct_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if ((Decimal) e.NewValue < 0M || (Decimal) e.NewValue > 100M)
      throw new PXSetPropertyException("Percentage value should be between 0 and 100");
  }

  public virtual void INABCCode_ABCPct_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    if ((Decimal) e.NewValue < 0M || (Decimal) e.NewValue > 100M)
      throw new PXSetPropertyException("Percentage value should be between 0 and 100");
  }
}
