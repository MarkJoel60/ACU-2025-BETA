// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethodMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.IN;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public class DepreciationMethodMaint : PXGraph<DepreciationMethodMaint, FADepreciationMethod>
{
  public PXSelect<FADepreciationMethod, Where<FADepreciationMethod.isTableMethod, Equal<False>>> Method;
  public PXSetupOptional<PX.Objects.FA.FASetup> FASetup;

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search<FADepreciationMethod.methodCD, Where<FADepreciationMethod.isTableMethod, NotEqual<True>>>), DescriptionField = typeof (FADepreciationMethod.description))]
  [PXUIField]
  [PXDefault]
  [PXFieldDescription]
  public void FADepreciationMethod_MethodCD_CacheAttached(PXCache cache)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("B")]
  [FARecordType.MethodList]
  public void FADepreciationMethod_RecordType_CacheAttached(PXCache cache)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public void FADepreciationMethod_IsTableMethod_CacheAttached(PXCache cache)
  {
  }

  protected virtual void FADepreciationMethod_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FADepreciationMethod row = (FADepreciationMethod) e.Row;
    if (row == null)
      return;
    bool? nullable = row.IsPredefined;
    int num;
    if (nullable.GetValueOrDefault())
    {
      nullable = ((PXSelectBase<PX.Objects.FA.FASetup>) this.FASetup).Current.AllowEditPredefinedDeprMethod;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 1;
    bool flag = num != 0;
    sender.AllowUpdate = flag;
    sender.AllowDelete = flag;
    ((PXCache) GraphHelper.Caches<FADepreciationMethodLines>((PXGraph) this)).AllowInsert = flag;
    ((PXCache) GraphHelper.Caches<FADepreciationMethodLines>((PXGraph) this)).AllowUpdate = flag;
    ((PXCache) GraphHelper.Caches<FADepreciationMethodLines>((PXGraph) this)).AllowDelete = flag;
    PXUIFieldAttribute.SetVisible<FADepreciationMethod.yearlyAccountancy>(sender, (object) null, !row.IsNewZealandMethod);
    FAAveragingConvention.SetAveragingConventionsList<FADepreciationMethod.averagingConvention>(sender, (object) row, new KeyValuePair<object, Dictionary<object, string[]>>((object) row.DepreciationMethod, FAAveragingConvention.DeprMethodDisabledValues));
  }

  protected virtual void FADepreciationMethod_UsefulLife_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FADepreciationMethod row = (FADepreciationMethod) e.Row;
    if (row == null)
      return;
    row.RecoveryPeriod = new int?((int) (row.UsefulLife.GetValueOrDefault() * 12M));
  }

  protected virtual void FADepreciationMethod_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    FADepreciationMethod row = (FADepreciationMethod) e.Row;
    if (row == null)
      return;
    FABookSettings faBookSettings = PXResultset<FABookSettings>.op_Implicit(PXSelectBase<FABookSettings, PXSelect<FABookSettings, Where<FABookSettings.depreciationMethodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.MethodID
    }));
    FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.depreciationMethodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.MethodID
    }));
    if (faBookSettings != null || faBookBalance != null)
      throw new PXSetPropertyException("You cannot delete Depreciation Method because this Method used in Fixed Assets or Classes.");
  }

  public virtual void Persist()
  {
    foreach (FADepreciationMethod depreciationMethod in ((PXSelectBase) this.Method).Cache.Updated)
    {
      depreciationMethod.IsPredefined = new bool?(false);
      ((PXSelectBase<FADepreciationMethod>) this.Method).Update(depreciationMethod);
    }
    ((PXGraph) this).Persist();
  }
}
