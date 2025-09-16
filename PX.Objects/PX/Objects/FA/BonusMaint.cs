// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.BonusMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FA;

public class BonusMaint : PXGraph<BonusMaint, FABonus>
{
  public PXSelect<FABonus> Bonuses;
  public PXSelect<FABonusDetails, Where<FABonusDetails.bonusID, Equal<Current<FABonus.bonusID>>>> Details;

  protected virtual void FABonusDetails_StartDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FABonusDetails row = (FABonusDetails) e.Row;
    if (row == null)
      return;
    DateTime? endDate = row.EndDate;
    DateTime? nullable1;
    if (endDate.HasValue)
    {
      nullable1 = (DateTime?) e.NewValue;
      DateTime? nullable2 = endDate;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
        {
          (object) endDate
        });
    }
    FABonusDetails faBonusDetails1 = PXResultset<FABonusDetails>.op_Implicit(PXSelectBase<FABonusDetails, PXSelect<FABonusDetails, Where<FABonusDetails.lineNbr, Less<Current<FABonusDetails.lineNbr>>, And<FABonusDetails.bonusID, Equal<Current<FABonusDetails.bonusID>>>>, OrderBy<Desc<FABonusDetails.endDate>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    DateTime? nullable3 = faBonusDetails1 == null ? new DateTime?() : faBonusDetails1.EndDate;
    DateTime? nullable4;
    if (nullable3.HasValue)
    {
      nullable4 = (DateTime?) e.NewValue;
      nullable1 = nullable3;
      if ((nullable4.HasValue & nullable1.HasValue ? (nullable4.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
        {
          (object) nullable3
        });
    }
    FABonusDetails faBonusDetails2 = PXResultset<FABonusDetails>.op_Implicit(PXSelectBase<FABonusDetails, PXSelect<FABonusDetails, Where<FABonusDetails.lineNbr, Greater<Current<FABonusDetails.lineNbr>>, And<FABonusDetails.bonusID, Equal<Current<FABonusDetails.bonusID>>>>, OrderBy<Desc<FABonusDetails.endDate>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    DateTime? nullable5;
    if (faBonusDetails2 != null)
    {
      nullable5 = faBonusDetails2.StartDate;
    }
    else
    {
      nullable1 = new DateTime?();
      nullable5 = nullable1;
    }
    DateTime? nullable6 = nullable5;
    if (!nullable6.HasValue)
      return;
    nullable1 = (DateTime?) e.NewValue;
    nullable4 = nullable6;
    if ((nullable1.HasValue & nullable4.HasValue ? (nullable1.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than {0}.", new object[1]
      {
        (object) nullable6
      });
  }

  protected virtual void FABonusDetails_EndDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FABonusDetails row = (FABonusDetails) e.Row;
    if (row == null)
      return;
    DateTime? startDate = row.StartDate;
    DateTime? nullable1;
    if (startDate.HasValue)
    {
      nullable1 = (DateTime?) e.NewValue;
      DateTime? nullable2 = startDate;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
        {
          (object) startDate
        });
    }
    FABonusDetails faBonusDetails1 = PXResultset<FABonusDetails>.op_Implicit(PXSelectBase<FABonusDetails, PXSelect<FABonusDetails, Where<FABonusDetails.lineNbr, Greater<Current<FABonusDetails.lineNbr>>, And<FABonusDetails.bonusID, Equal<Current<FABonusDetails.bonusID>>>>, OrderBy<Desc<FABonusDetails.endDate>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    DateTime? nullable3 = faBonusDetails1 == null ? new DateTime?() : faBonusDetails1.StartDate;
    DateTime? nullable4;
    if (nullable3.HasValue)
    {
      nullable4 = (DateTime?) e.NewValue;
      nullable1 = nullable3;
      if ((nullable4.HasValue & nullable1.HasValue ? (nullable4.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than {0}.", new object[1]
        {
          (object) nullable3
        });
    }
    FABonusDetails faBonusDetails2 = PXResultset<FABonusDetails>.op_Implicit(PXSelectBase<FABonusDetails, PXSelect<FABonusDetails, Where<FABonusDetails.lineNbr, Less<Current<FABonusDetails.lineNbr>>, And<FABonusDetails.bonusID, Equal<Current<FABonusDetails.bonusID>>>>, OrderBy<Desc<FABonusDetails.endDate>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    DateTime? nullable5;
    if (faBonusDetails2 != null)
    {
      nullable5 = faBonusDetails2.EndDate;
    }
    else
    {
      nullable1 = new DateTime?();
      nullable5 = nullable1;
    }
    DateTime? nullable6 = nullable5;
    if (!nullable6.HasValue)
      return;
    nullable1 = (DateTime?) e.NewValue;
    nullable4 = nullable6;
    if ((nullable1.HasValue & nullable4.HasValue ? (nullable1.GetValueOrDefault() <= nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) nullable6
      });
  }
}
