// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CR;

public class CRCaseClassMaint : PXGraph<
#nullable disable
CRCaseClassMaint, CRCaseClass>
{
  [PXViewName("Case Class")]
  public PXSelect<CRCaseClass> CaseClasses;
  [PXHidden]
  public PXSelect<CRCaseClass, Where<CRCaseClass.caseClassID, Equal<Current<CRCaseClass.caseClassID>>>> CaseClassesCurrent;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<CRCaseClass, CRCase> Mapping;
  [PXHidden]
  public PXSelect<CRSetup> Setup;
  public PXSelect<CRCaseClassLaborMatrix, Where<CRCaseClassLaborMatrix.caseClassID, Equal<Current<CRCaseClass.caseClassID>>>> LaborMatrix;
  public FbqlSelect<SelectFromBase<CSCalendar, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CSCalendar.calendarID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CRCaseClass.calendarID, IBqlString>.FromCurrent>>, 
  #nullable disable
  CSCalendar>.View WorkCalendar;

  [PXDBInt(MinValue = 0, MaxValue = 1440)]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRCaseClass.reopenCaseTimeInDays> e)
  {
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CRCaseClass.caseClassID))]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRClassSeverityTime.caseClassID> e)
  {
  }

  [PXDefault(false)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<InventoryItem.stkItem> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CRCaseClass, CRCaseClass.perItemBilling> e)
  {
    CRCaseClass row = e.Row;
    if (row == null)
      return;
    if (PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelect<CRCase, Where<CRCase.caseClassID, Equal<Required<CRCaseClass.caseClassID>>, And<CRCase.isBillable, Equal<True>, And<CRCase.released, Equal<False>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.CaseClassID
    })) != null)
      throw new PXSetPropertyException("The billing mode could not be changed. There are unreleased cases associated with this class.", (PXErrorLevel) 4);
  }

  protected virtual void _(PX.Data.Events.RowInserted<CRCaseClass> e)
  {
    CRCaseClass row = e.Row;
    if (row == null || !row.IsBillable.GetValueOrDefault())
      return;
    row.RequireCustomer = new bool?(true);
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRCaseClass> e)
  {
    CRCaseClass row = e.Row;
    if (row == null)
      return;
    ((PXAction) this.Delete).SetEnabled(this.NoCaseExistsForClass(row));
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CRCaseClass> e)
  {
    CRCaseClass row = e.Row;
    if (row == null)
      return;
    CRSetup crSetup = PXResultset<CRSetup>.op_Implicit(((PXSelectBase<CRSetup>) this.Setup).Select(Array.Empty<object>()));
    if (crSetup == null || !(crSetup.DefaultCaseClassID == row.CaseClassID))
      return;
    crSetup.DefaultCaseClassID = (string) null;
    ((PXSelectBase<CRSetup>) this.Setup).Update(crSetup);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<CRCaseClass> e)
  {
    CRCaseClass row = e.Row;
    if (row != null && !this.NoCaseExistsForClass(row))
      throw new PXException("This record is referenced and cannot be deleted.");
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CRCaseClass> e)
  {
    CRCaseClass row = e.Row;
    if (row != null && e.Operation != 3 && !object.Equals(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRCaseClass>>) e).Cache.GetValue<CRCaseClass.allowEmployeeAsContact>((object) row), ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRCaseClass>>) e).Cache.GetValueOriginal<CRCaseClass.allowEmployeeAsContact>((object) row)) && !row.AllowEmployeeAsContact.GetValueOrDefault() && !this.NoEmployeeCaseExistsForClass(row))
      throw new PXException("The {0} case class cannot be changed because this case class is associated with at least one case.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CRCaseClass, CRCaseClass.calendarID> e)
  {
    if (e.Row == null)
      return;
    CSCalendar csCalendar = CSCalendar.PK.Find((PXGraph) this, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CRCaseClass, CRCaseClass.calendarID>, CRCaseClass, object>) e).NewValue as string);
    if (csCalendar != null && (OverlapsTheMidnight(csCalendar.MonWorkDay, csCalendar.MonStartTime, csCalendar.MonEndTime) || OverlapsTheMidnight(csCalendar.TueWorkDay, csCalendar.TueStartTime, csCalendar.TueEndTime) || OverlapsTheMidnight(csCalendar.WedWorkDay, csCalendar.WedStartTime, csCalendar.WedEndTime) || OverlapsTheMidnight(csCalendar.ThuWorkDay, csCalendar.ThuStartTime, csCalendar.ThuEndTime) || OverlapsTheMidnight(csCalendar.FriWorkDay, csCalendar.FriStartTime, csCalendar.FriEndTime) || OverlapsTheMidnight(csCalendar.SatWorkDay, csCalendar.SatStartTime, csCalendar.SatEndTime) || OverlapsTheMidnight(csCalendar.SunWorkDay, csCalendar.SunStartTime, csCalendar.SunEndTime)))
      throw new PXSetPropertyException<CRCaseClass.calendarID>("Calendars with time periods that pass over midnight are not supported for cases.", (PXErrorLevel) 4);

    static bool OverlapsTheMidnight(
      bool? dayIsActive,
      DateTime? startDateTime,
      DateTime? endDateTime)
    {
      return startDateTime.HasValue && endDateTime.HasValue && (!dayIsActive.HasValue || dayIsActive.GetValueOrDefault()) && startDateTime.Value > endDateTime.Value;
    }
  }

  protected virtual bool NoCaseExistsForClass(CRCaseClass row)
  {
    if (row == null)
      return true;
    return PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelect<CRCase, Where<CRCase.caseClassID, Equal<Required<CRCase.caseClassID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.CaseClassID
    })) == null;
  }

  protected virtual bool NoEmployeeCaseExistsForClass(CRCaseClass row)
  {
    if (row == null)
      return true;
    return PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelectJoin<CRCase, InnerJoin<Contact, On<Contact.contactID, Equal<CRCase.contactID>, And<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>, Where<CRCase.caseClassID, Equal<Required<CRCase.caseClassID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.CaseClassID
    })) == null;
  }
}
