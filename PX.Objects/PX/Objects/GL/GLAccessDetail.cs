// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLAccessDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GL;

public class GLAccessDetail : UserAccess
{
  public 
  #nullable disable
  PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXSelect<PX.Objects.GL.Account> Account;
  public PXSelect<PX.Objects.GL.Sub> Sub;
  public PXSelect<PX.Objects.GL.Branch> Branch;
  public PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<GLAccessDetail.subaccount>, And<SegmentValue.segmentID, Equal<Optional<SegmentValue.segmentID>>>>> Segment;
  public PXSelect<SegmentValue> AllSegments;
  public PXSelectOrderBy<GLBudgetTree, OrderBy<Asc<GLBudgetTree.description, Asc<GLBudgetTree.groupID, Asc<GLBudgetTree.subID>>>>> BudgetTree;
  public PXSelect<SMPrinter> Printers;
  public PXSave<SegmentValue> Save;
  public PXCancel<SegmentValue> Cancel;
  public PXFirst<SegmentValue> First;
  public PXPrevious<SegmentValue> Prev;
  public PXNext<SegmentValue> Next;
  public PXLast<SegmentValue> Last;
  public PXFilter<SegmentFilter> Filter;

  [PXDefault]
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimensionSelector("ACCOUNT", typeof (Search<PX.Objects.GL.Account.accountCD, Where<PX.Objects.GL.Account.accountingType, Equal<AccountEntityType.gLAccount>>>), typeof (PX.Objects.GL.Account.accountCD))]
  protected virtual void Account_AccountCD_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimensionSelector("SUBACCOUNT", typeof (PX.Objects.GL.Sub.subCD), typeof (PX.Objects.GL.Sub.subCD))]
  protected virtual void Sub_SubCD_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimensionSelector("BRANCH", typeof (Search<PX.Objects.GL.Branch.branchCD, Where<Match<Current<AccessInfo.userName>>>>), typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  protected virtual void Branch_BranchCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault("SUBACCOUNT")]
  [PXUIField(DisplayName = "Segmented Key ID", Visible = false)]
  protected virtual void SegmentValue_DimensionID_CacheAttached(PXCache sender)
  {
  }

  [PXDBShort(IsKey = true)]
  [PXUIField(DisplayName = "Segment ID", Required = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.Segment.segmentID, Where<PX.Objects.CS.Segment.dimensionID, Equal<Current<SegmentValue.dimensionID>>, And<PX.Objects.CS.Segment.validate, Equal<True>>>>), DescriptionField = typeof (PX.Objects.CS.Segment.descr))]
  [PXDefault(typeof (SegmentFilter.segmentID))]
  protected virtual void SegmentValue_SegmentID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<SegmentValue.value, Where<SegmentValue.dimensionID, Equal<Current<SegmentFilter.dimensionID>>, And<SegmentValue.segmentID, Equal<Current<SegmentFilter.segmentID>>>>>), DescriptionField = typeof (SegmentValue.descr))]
  protected virtual void SegmentValue_Value_CacheAttached(PXCache sender)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (Search3<GLBudgetTree.groupID, OrderBy<Desc<GLBudgetTree.isGroup, Asc<GLBudgetTree.description, Asc<GLBudgetTree.accountID, Asc<GLBudgetTree.groupID>>>>>>), new Type[] {typeof (GLBudgetTree.isGroup), typeof (GLBudgetTree.description), typeof (GLBudgetTree.accountID), typeof (GLBudgetTree.subID), typeof (GLBudgetTree.accountMask), typeof (GLBudgetTree.subMask)}, DescriptionField = typeof (GLBudgetTree.description))]
  protected virtual void GLBudgetTree_GroupID_CacheAttached(PXCache sender)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXSelector(typeof (Search<SMPrinter.printerID>), new Type[] {typeof (SMPrinter.printerName), typeof (SMPrinter.deviceHubID), typeof (SMPrinter.description), typeof (SMPrinter.isActive)}, DescriptionField = typeof (SMPrinter.printerName))]
  [PXUIField]
  protected virtual void SMPrinter_PrinterID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(30)]
  [PXDefault]
  [PXUIField]
  protected virtual void SMPrinter_DeviceHubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(20)]
  [PXDefault]
  [PXUIField]
  protected virtual void SMPrinter_PrinterName_CacheAttached(PXCache sender)
  {
  }

  public GLAccessDetail()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    ((PXSelectBase) this.Account).Cache.AllowDelete = false;
    ((PXSelectBase) this.Account).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Account).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Account.accountCD>(((PXSelectBase) this.Account).Cache, (object) null);
    ((PXSelectBase) this.Sub).Cache.AllowDelete = false;
    ((PXSelectBase) this.Sub).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Sub).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Sub.subCD>(((PXSelectBase) this.Sub).Cache, (object) null);
    ((PXSelectBase) this.Branch).Cache.AllowDelete = false;
    ((PXSelectBase) this.Branch).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Branch).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Branch.branchCD>(((PXSelectBase) this.Branch).Cache, (object) null);
    ((PXSelectBase) this.Segment).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Segment).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<SegmentValue.segmentID>(((PXSelectBase) this.Segment).Cache, (object) null);
    PXUIFieldAttribute.SetEnabled<SegmentValue.value>(((PXSelectBase) this.Segment).Cache, (object) null);
    ((PXGraph) this).Views.Caches.Remove(typeof (RelationGroup));
    ((PXGraph) this).Views.Caches.Add(typeof (RelationGroup));
  }

  protected virtual IEnumerable groups()
  {
    GLAccessDetail glAccessDetail = this;
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select((PXGraph) glAccessDetail, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (PX.Objects.GL.Account).Namespace || UserAccess.IsIncluded(((UserAccess) glAccessDetail).getMask(), relationGroup))
      {
        ((PXSelectBase<RelationGroup>) glAccessDetail.Groups).Current = relationGroup;
        yield return (object) relationGroup;
      }
    }
  }

  protected virtual byte[] getMask()
  {
    byte[] mask = (byte[]) null;
    if (((PXSelectBase<PX.Objects.GL.Account>) this.Account).Current != null)
      mask = ((PXSelectBase<PX.Objects.GL.Account>) this.Account).Current.GroupMask;
    else if (((PXSelectBase<PX.Objects.GL.Sub>) this.Sub).Current != null)
      mask = ((PXSelectBase<PX.Objects.GL.Sub>) this.Sub).Current.GroupMask;
    else if (((PXSelectBase<PX.Objects.GL.Branch>) this.Branch).Current != null)
      mask = ((PXSelectBase<PX.Objects.GL.Branch>) this.Branch).Current.GroupMask;
    else if (((PXSelectBase<GLBudgetTree>) this.BudgetTree).Current != null)
      mask = ((PXSelectBase<GLBudgetTree>) this.BudgetTree).Current.GroupMask;
    else if (((PXSelectBase<SMPrinter>) this.Printers).Current != null)
      mask = ((PXSelectBase<SMPrinter>) this.Printers).Current.GroupMask;
    else if (((PXSelectBase<SegmentValue>) this.Segment).Current != null)
      mask = ((PXSelectBase<SegmentValue>) this.Segment).Current.GroupMask;
    return mask;
  }

  public virtual void SegmentValue_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SegmentValue row))
      return;
    ((PXSelectBase<SegmentFilter>) this.Filter).Current.SegmentID = row.SegmentID;
  }

  public virtual void SegmentValue_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is SegmentValue row))
      return;
    sender.Clear();
    sender.Current = (object) row;
    SegmentValue segmentValue = PXResultset<SegmentValue>.op_Implicit(PXSelectBase<SegmentValue, PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<GLAccessDetail.subaccount>, And<SegmentValue.segmentID, Equal<Required<SegmentValue.segmentID>>, And<SegmentValue.value, Equal<Required<SegmentValue.value>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) row.SegmentID,
      (object) row.Value
    }));
    if (segmentValue == null)
      segmentValue = PXResultset<SegmentValue>.op_Implicit(PXSelectBase<SegmentValue, PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<GLAccessDetail.subaccount>, And<SegmentValue.segmentID, Equal<Required<SegmentValue.segmentID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) row.SegmentID
      }));
    if (segmentValue == null)
      segmentValue = PXResultset<SegmentValue>.op_Implicit(PXSelectBase<SegmentValue, PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<GLAccessDetail.subaccount>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (segmentValue != null)
    {
      row.SegmentID = segmentValue.SegmentID;
      row.Value = segmentValue.Value;
    }
    else
      row.Value = (string) null;
  }

  protected virtual void GLBudgetTree_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    foreach (PXResult<GLBudgetTree> pxResult in PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (e.NewRow as GLBudgetTree).GroupID
    }))
    {
      GLBudgetTree glBudgetTree = PXResult<GLBudgetTree>.op_Implicit(pxResult);
      UserAccess.PopulateNeighbours<GLBudgetTree>(sender, glBudgetTree, new List<byte[]>().ToArray(), (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
    }
  }

  public virtual void Persist()
  {
    ((PXSelectBase) this.Groups).View.Clear();
    if (((PXSelectBase<PX.Objects.GL.Account>) this.Account).Current != null)
    {
      UserAccess.PopulateNeighbours<PX.Objects.GL.Account>((PXSelectBase<PX.Objects.GL.Account>) this.Account, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      PXSelectorAttribute.ClearGlobalCache<PX.Objects.GL.Account>();
    }
    else if (((PXSelectBase<PX.Objects.GL.Sub>) this.Sub).Current != null)
    {
      UserAccess.PopulateNeighbours<PX.Objects.GL.Sub>((PXSelectBase<PX.Objects.GL.Sub>) this.Sub, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      PXSelectorAttribute.ClearGlobalCache<PX.Objects.GL.Sub>();
    }
    else if (((PXSelectBase<PX.Objects.GL.Branch>) this.Branch).Current != null)
    {
      UserAccess.PopulateNeighbours<PX.Objects.GL.Branch>((PXSelectBase<PX.Objects.GL.Branch>) this.Branch, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      PXSelectorAttribute.ClearGlobalCache<PX.Objects.GL.Branch>();
    }
    else if (((PXSelectBase<GLBudgetTree>) this.BudgetTree).Current != null)
      UserAccess.PopulateNeighbours<GLBudgetTree>((PXSelectBase<GLBudgetTree>) this.BudgetTree, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
    else if (((PXSelectBase<SMPrinter>) this.Printers).Current != null)
    {
      UserAccess.PopulateNeighbours<SMPrinter>((PXSelectBase<SMPrinter>) this.Printers, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
    }
    else
    {
      if (((PXSelectBase<SegmentValue>) this.Segment).Current == null)
        return;
      UserAccess.PopulateNeighbours<SegmentValue>((PXSelectBase<SegmentValue>) this.AllSegments, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      PXSelectorAttribute.ClearGlobalCache<SegmentValue>();
    }
    base.Persist();
  }

  public class subaccount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GLAccessDetail.subaccount>
  {
    public subaccount()
      : base("SUBACCOUNT")
    {
    }
  }
}
