// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRContactClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
public class CRContactClassMaint : PXGraph<CRContactClassMaint, CRContactClass>
{
  [PXViewName("Contact Class")]
  public PXSelect<CRContactClass> Class;
  [PXHidden]
  public PXSelect<CRContactClass, Where<CRContactClass.classID, Equal<Current<CRContactClass.classID>>>> ClassCurrent;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<CRContactClass, Contact> Mapping;
  [PXHidden]
  public PXSelect<CRSetup> Setup;

  protected virtual void _(Events.RowDeleted<CRContactClass> e)
  {
    CRContactClass row = e.Row;
    if (row == null)
      return;
    CRSetup crSetup = PXResultset<CRSetup>.op_Implicit(((PXSelectBase<CRSetup>) this.Setup).Select(Array.Empty<object>()));
    if (crSetup == null || !(crSetup.DefaultContactClassID == row.ClassID))
      return;
    crSetup.DefaultContactClassID = (string) null;
    ((PXSelectBase<CRSetup>) this.Setup).Update(crSetup);
  }

  protected virtual void _(
    Events.FieldVerifying<CRContactClass, CRContactClass.defaultOwner> e)
  {
    CRContactClass row = e.Row;
    if (row == null || ((Events.FieldVerifyingBase<Events.FieldVerifying<CRContactClass, CRContactClass.defaultOwner>, CRContactClass, object>) e).NewValue != null)
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<CRContactClass, CRContactClass.defaultOwner>, CRContactClass, object>) e).NewValue = (object) (row.DefaultOwner ?? "N");
  }

  protected virtual void _(
    Events.FieldUpdated<CRContactClass, CRContactClass.defaultOwner> e)
  {
    CRContactClass row = e.Row;
    if (row == null || e.NewValue == ((Events.FieldUpdatedBase<Events.FieldUpdated<CRContactClass, CRContactClass.defaultOwner>, CRContactClass, object>) e).OldValue)
      return;
    row.DefaultAssignmentMapID = new int?();
  }

  protected virtual void _(Events.RowSelected<CRContactClass> e)
  {
    CRContactClass row = e.Row;
    if (row == null)
      return;
    ((PXAction) this.Delete).SetEnabled(this.CanDelete(row));
  }

  protected virtual void _(Events.RowDeleting<CRContactClass> e)
  {
    CRContactClass row = e.Row;
    if (row != null && !this.CanDelete(row))
      throw new PXException("This record is referenced and cannot be deleted.");
  }

  private bool CanDelete(CRContactClass row)
  {
    if (row != null)
    {
      if (PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.classID, Equal<Required<CRContactClass.classID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) row.ClassID
      })) != null)
        return false;
    }
    return true;
  }
}
