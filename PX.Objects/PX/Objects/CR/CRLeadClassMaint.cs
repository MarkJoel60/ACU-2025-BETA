// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRLeadClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
public class CRLeadClassMaint : PXGraph<CRLeadClassMaint, CRLeadClass>
{
  [PXViewName("Lead Class")]
  public PXSelect<CRLeadClass> Class;
  [PXHidden]
  public PXSelect<CRLeadClass, Where<CRLeadClass.classID, Equal<Current<CRLeadClass.classID>>>> ClassCurrent;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<CRLeadClass, CRLead> Mapping;
  [PXHidden]
  public PXSelect<CRSetup> Setup;

  protected virtual void _(Events.RowDeleted<CRLeadClass> e)
  {
    CRLeadClass row = e.Row;
    if (row == null)
      return;
    CRSetup crSetup = PXResultset<CRSetup>.op_Implicit(((PXSelectBase<CRSetup>) this.Setup).Select(Array.Empty<object>()));
    if (crSetup == null || !(crSetup.DefaultLeadClassID == row.ClassID))
      return;
    crSetup.DefaultLeadClassID = (string) null;
    ((PXSelectBase<CRSetup>) this.Setup).Update(crSetup);
  }

  protected virtual void _(
    Events.FieldVerifying<CRLeadClass, CRLeadClass.defaultOwner> e)
  {
    CRLeadClass row = e.Row;
    if (row == null || ((Events.FieldVerifyingBase<Events.FieldVerifying<CRLeadClass, CRLeadClass.defaultOwner>, CRLeadClass, object>) e).NewValue != null)
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<CRLeadClass, CRLeadClass.defaultOwner>, CRLeadClass, object>) e).NewValue = (object) (row.DefaultOwner ?? "N");
  }

  protected virtual void _(
    Events.FieldUpdated<CRLeadClass, CRLeadClass.defaultOwner> e)
  {
    CRLeadClass row = e.Row;
    if (row == null || e.NewValue == ((Events.FieldUpdatedBase<Events.FieldUpdated<CRLeadClass, CRLeadClass.defaultOwner>, CRLeadClass, object>) e).OldValue)
      return;
    row.DefaultAssignmentMapID = new int?();
  }

  protected virtual void _(Events.RowSelected<CRLeadClass> e)
  {
    CRLeadClass row = e.Row;
    if (row == null)
      return;
    ((PXAction) this.Delete).SetEnabled(this.CanDelete(row));
  }

  protected virtual void _(Events.RowDeleting<CRLeadClass> e)
  {
    CRLeadClass row = e.Row;
    if (row != null && !this.CanDelete(row))
      throw new PXException("This record is referenced and cannot be deleted.");
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Attribute")]
  protected virtual void _(
    Events.CacheAttached<CSAttributeGroup.attributeID> e)
  {
  }

  private bool CanDelete(CRLeadClass row)
  {
    if (row != null)
    {
      if (PXResultset<CRLead>.op_Implicit(PXSelectBase<CRLead, PXSelect<CRLead, Where<CRLead.classID, Equal<Required<CRLeadClass.classID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) row.ClassID
      })) != null)
        return false;
    }
    return true;
  }
}
