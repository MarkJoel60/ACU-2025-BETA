// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLAccess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.GL;

public class GLAccess : BaseAccess
{
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXSelect<PX.Objects.GL.Account> Account;
  public PXSelect<PX.Objects.GL.Sub> Sub;
  public PXSelect<PX.Objects.GL.Branch> Branch;
  public PXFilter<PX.Objects.GL.SegmentFilter> SegmentFilter;
  public PXSelect<SegmentValue> SegmentAll;
  public PXSelect<SegmentValue> Segment;
  public PXSelectOrderBy<GLBudgetTree, OrderBy<Desc<GLBudgetTree.isGroup, Asc<GLBudgetTree.description>>>> BudgetTree;
  public PXSelect<SMPrinter> Printers;

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
  [PXDimensionSelector("BRANCH", typeof (Search<PX.Objects.GL.Branch.branchCD, Where<Match<Current<AccessInfo.userName>>>>), typeof (PX.Objects.GL.Branch.branchCD))]
  protected virtual void Branch_BranchCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [GLAccess.GLRelationGroupAccountSelector(typeof (RelationGroup.groupName), Filterable = true)]
  protected virtual void RelationGroup_GroupName_CacheAttached(PXCache sender)
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
  [PXDefault(typeof (PX.Objects.GL.SegmentFilter.segmentID))]
  protected virtual void SegmentValue_SegmentID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<SegmentValue.value, Where<SegmentValue.dimensionID, Equal<Current<PX.Objects.GL.SegmentFilter.dimensionID>>, And<SegmentValue.segmentID, Equal<Current<PX.Objects.GL.SegmentFilter.segmentID>>>>>), DescriptionField = typeof (SegmentValue.descr))]
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

  public GLAccess()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    ((PXSelectBase) this.Account).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Account).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Account.included>(((PXSelectBase) this.Account).Cache, (object) null);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Account.accountCD>(((PXSelectBase) this.Account).Cache, (object) null);
    ((PXSelectBase) this.Sub).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Sub).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Sub.included>(((PXSelectBase) this.Sub).Cache, (object) null);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Sub.subCD>(((PXSelectBase) this.Sub).Cache, (object) null);
    ((PXSelectBase) this.Branch).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Branch).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.Branch.included>(((PXSelectBase) this.Branch).Cache, (object) null);
    ((PXSelectBase) this.Segment).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Segment).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<SegmentValue.included>(((PXSelectBase) this.Segment).Cache, (object) null);
    PXUIFieldAttribute.SetEnabled<SegmentValue.value>(((PXSelectBase) this.Segment).Cache, (object) null);
    ((PXSelectBase) this.BudgetTree).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.BudgetTree).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<GLBudgetTree.included>(((PXSelectBase) this.BudgetTree).Cache, (object) null);
    PXUIFieldAttribute.SetEnabled<GLBudgetTree.groupID>(((PXSelectBase) this.BudgetTree).Cache, (object) null);
    ((PXSelectBase) this.Printers).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Printers).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<SMPrinter.included>(((PXSelectBase) this.Printers).Cache, (object) null);
    PXUIFieldAttribute.SetEnabled<SMPrinter.printerID>(((PXSelectBase) this.Printers).Cache, (object) null);
  }

  public virtual bool CanClipboardCopyPaste() => false;

  protected virtual void SegmentFilter_ValidCombos_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    Dimension dimension = PXResultset<Dimension>.op_Implicit(PXSelectBase<Dimension, PXSelect<Dimension, Where<Dimension.dimensionID, Equal<Current<PX.Objects.GL.SegmentFilter.dimensionID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (dimension == null)
      return;
    e.ReturnValue = (object) dimension.Validate;
  }

  protected virtual IEnumerable account()
  {
    GLAccess glAccess1 = this;
    if (((PXSelectBase<RelationGroup>) glAccess1.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) glAccess1.Group).Current.GroupName))
    {
      bool inserted = ((PXSelectBase) glAccess1.Group).Cache.GetStatus((object) ((PXSelectBase<RelationGroup>) glAccess1.Group).Current) == 2;
      GLAccess glAccess2 = glAccess1;
      object[] objArray = new object[1]
      {
        (object) new byte[0]
      };
      foreach (PXResult<PX.Objects.GL.Account> pxResult in PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where2<Match<Current<RelationGroup.groupName>>, Or2<Match<Required<PX.Objects.GL.Account.groupMask>>, Or<PX.Objects.GL.Account.groupMask, IsNull>>>>.Config>.Select((PXGraph) glAccess2, objArray))
      {
        PX.Objects.GL.Account item = PXResult<PX.Objects.GL.Account>.op_Implicit(pxResult);
        if (!inserted || item.Included.GetValueOrDefault())
        {
          ((PXSelectBase<PX.Objects.GL.Account>) glAccess1.Account).Current = item;
          yield return (object) item;
        }
        else if (item.GroupMask != null)
        {
          RelationGroup group = ((PXSelectBase<RelationGroup>) glAccess1.Group).Current;
          bool anyGroup = false;
          for (int i = 0; i < item.GroupMask.Length && i < group.GroupMask.Length; ++i)
          {
            if (group.GroupMask[i] != (byte) 0 && ((int) item.GroupMask[i] & (int) group.GroupMask[i]) == (int) group.GroupMask[i])
            {
              ((PXSelectBase<PX.Objects.GL.Account>) glAccess1.Account).Current = item;
              yield return (object) item;
            }
            anyGroup |= item.GroupMask[i] > (byte) 0;
          }
          if (!anyGroup)
          {
            ((PXSelectBase<PX.Objects.GL.Account>) glAccess1.Account).Current = item;
            yield return (object) item;
          }
          group = (RelationGroup) null;
        }
        item = (PX.Objects.GL.Account) null;
      }
    }
  }

  protected virtual IEnumerable sub()
  {
    GLAccess glAccess1 = this;
    if (((PXSelectBase<RelationGroup>) glAccess1.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) glAccess1.Group).Current.GroupName))
    {
      bool inserted = ((PXSelectBase) glAccess1.Group).Cache.GetStatus((object) ((PXSelectBase<RelationGroup>) glAccess1.Group).Current) == 2;
      GLAccess glAccess2 = glAccess1;
      object[] objArray = new object[1]
      {
        (object) new byte[0]
      };
      foreach (PXResult<PX.Objects.GL.Sub> pxResult in PXSelectBase<PX.Objects.GL.Sub, PXSelect<PX.Objects.GL.Sub, Where2<Match<Current<RelationGroup.groupName>>, Or2<Match<Required<PX.Objects.GL.Sub.groupMask>>, Or<PX.Objects.GL.Sub.groupMask, IsNull>>>>.Config>.Select((PXGraph) glAccess2, objArray))
      {
        PX.Objects.GL.Sub item = PXResult<PX.Objects.GL.Sub>.op_Implicit(pxResult);
        if (!inserted || item.Included.GetValueOrDefault())
        {
          ((PXSelectBase<PX.Objects.GL.Sub>) glAccess1.Sub).Current = item;
          yield return (object) item;
        }
        else if (item.GroupMask != null)
        {
          RelationGroup group = ((PXSelectBase<RelationGroup>) glAccess1.Group).Current;
          bool anyGroup = false;
          for (int i = 0; i < item.GroupMask.Length && i < group.GroupMask.Length; ++i)
          {
            if (group.GroupMask[i] != (byte) 0 && ((int) item.GroupMask[i] & (int) group.GroupMask[i]) == (int) group.GroupMask[i])
            {
              ((PXSelectBase<PX.Objects.GL.Sub>) glAccess1.Sub).Current = item;
              yield return (object) item;
            }
            anyGroup |= item.GroupMask[i] > (byte) 0;
          }
          if (!anyGroup)
          {
            ((PXSelectBase<PX.Objects.GL.Sub>) glAccess1.Sub).Current = item;
            yield return (object) item;
          }
          group = (RelationGroup) null;
        }
        item = (PX.Objects.GL.Sub) null;
      }
    }
  }

  protected virtual IEnumerable branch()
  {
    GLAccess glAccess1 = this;
    if (((PXSelectBase<RelationGroup>) glAccess1.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) glAccess1.Group).Current.GroupName))
    {
      bool inserted = ((PXSelectBase) glAccess1.Group).Cache.GetStatus((object) ((PXSelectBase<RelationGroup>) glAccess1.Group).Current) == 2;
      GLAccess glAccess2 = glAccess1;
      object[] objArray = new object[1]
      {
        (object) new byte[0]
      };
      foreach (PXResult<PX.Objects.GL.Branch> pxResult in PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where2<Match<Current<RelationGroup.groupName>>, Or<Match<Required<PX.Objects.GL.Branch.groupMask>>>>>.Config>.Select((PXGraph) glAccess2, objArray))
      {
        PX.Objects.GL.Branch item = PXResult<PX.Objects.GL.Branch>.op_Implicit(pxResult);
        if (!inserted)
        {
          ((PXSelectBase<PX.Objects.GL.Branch>) glAccess1.Branch).Current = item;
          yield return (object) item;
        }
        else if (item.GroupMask != null)
        {
          RelationGroup group = ((PXSelectBase<RelationGroup>) glAccess1.Group).Current;
          bool anyGroup = false;
          for (int i = 0; i < item.GroupMask.Length && i < group.GroupMask.Length; ++i)
          {
            if (group.GroupMask[i] != (byte) 0 && ((int) item.GroupMask[i] & (int) group.GroupMask[i]) == (int) group.GroupMask[i])
            {
              ((PXSelectBase<PX.Objects.GL.Branch>) glAccess1.Branch).Current = item;
              yield return (object) item;
            }
            anyGroup |= item.GroupMask[i] > (byte) 0;
          }
          if (!anyGroup)
          {
            ((PXSelectBase<PX.Objects.GL.Branch>) glAccess1.Branch).Current = item;
            yield return (object) item;
          }
          group = (RelationGroup) null;
        }
        item = (PX.Objects.GL.Branch) null;
      }
    }
  }

  protected virtual IEnumerable segmentAll()
  {
    GLAccess glAccess1 = this;
    if (((PXSelectBase<RelationGroup>) glAccess1.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) glAccess1.Group).Current.GroupName))
    {
      bool inserted = ((PXSelectBase) glAccess1.Group).Cache.GetStatus((object) ((PXSelectBase<RelationGroup>) glAccess1.Group).Current) == 2;
      GLAccess glAccess2 = glAccess1;
      object[] objArray = new object[1]
      {
        (object) new byte[0]
      };
      foreach (PXResult<SegmentValue> pxResult in PXSelectBase<SegmentValue, PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<Current<PX.Objects.GL.SegmentFilter.dimensionID>>, And<Where2<Match<Current<RelationGroup.groupName>>, Or<Match<Required<SegmentValue.groupMask>>>>>>>.Config>.Select((PXGraph) glAccess2, objArray))
      {
        SegmentValue item = PXResult<SegmentValue>.op_Implicit(pxResult);
        if (!inserted)
        {
          ((PXSelectBase<SegmentValue>) glAccess1.Segment).Current = item;
          yield return (object) item;
        }
        else if (item.GroupMask != null)
        {
          RelationGroup group = ((PXSelectBase<RelationGroup>) glAccess1.Group).Current;
          bool anyGroup = false;
          for (int i = 0; i < item.GroupMask.Length && i < group.GroupMask.Length; ++i)
          {
            if (group.GroupMask[i] != (byte) 0 && ((int) item.GroupMask[i] & (int) group.GroupMask[i]) == (int) group.GroupMask[i])
            {
              ((PXSelectBase<SegmentValue>) glAccess1.Segment).Current = item;
              yield return (object) item;
            }
            anyGroup |= item.GroupMask[i] > (byte) 0;
          }
          if (!anyGroup)
          {
            ((PXSelectBase<SegmentValue>) glAccess1.Segment).Current = item;
            yield return (object) item;
          }
          group = (RelationGroup) null;
        }
        item = (SegmentValue) null;
      }
    }
  }

  protected virtual IEnumerable segment()
  {
    GLAccess glAccess1 = this;
    if (((PXSelectBase<RelationGroup>) glAccess1.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) glAccess1.Group).Current.GroupName))
    {
      bool inserted = ((PXSelectBase) glAccess1.Group).Cache.GetStatus((object) ((PXSelectBase<RelationGroup>) glAccess1.Group).Current) == 2;
      GLAccess glAccess2 = glAccess1;
      object[] objArray = new object[1]
      {
        (object) new byte[0]
      };
      foreach (PXResult<SegmentValue> pxResult in PXSelectBase<SegmentValue, PXSelect<SegmentValue, Where<SegmentValue.dimensionID, Equal<Current<PX.Objects.GL.SegmentFilter.dimensionID>>, And<SegmentValue.segmentID, Equal<Current<PX.Objects.GL.SegmentFilter.segmentID>>, And<Where2<Match<Current<RelationGroup.groupName>>, Or<Match<Required<SegmentValue.groupMask>>>>>>>>.Config>.Select((PXGraph) glAccess2, objArray))
      {
        SegmentValue item = PXResult<SegmentValue>.op_Implicit(pxResult);
        if (!inserted)
        {
          ((PXSelectBase<SegmentValue>) glAccess1.Segment).Current = item;
          yield return (object) item;
        }
        else if (item.GroupMask != null)
        {
          RelationGroup group = ((PXSelectBase<RelationGroup>) glAccess1.Group).Current;
          bool anyGroup = false;
          for (int i = 0; i < item.GroupMask.Length && i < group.GroupMask.Length; ++i)
          {
            if (group.GroupMask[i] != (byte) 0 && ((int) item.GroupMask[i] & (int) group.GroupMask[i]) == (int) group.GroupMask[i])
            {
              ((PXSelectBase<SegmentValue>) glAccess1.Segment).Current = item;
              yield return (object) item;
            }
            anyGroup |= item.GroupMask[i] > (byte) 0;
          }
          if (!anyGroup)
          {
            ((PXSelectBase<SegmentValue>) glAccess1.Segment).Current = item;
            yield return (object) item;
          }
          group = (RelationGroup) null;
        }
        item = (SegmentValue) null;
      }
    }
  }

  protected virtual IEnumerable budgetTree()
  {
    GLAccess glAccess1 = this;
    if (((PXSelectBase<RelationGroup>) glAccess1.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) glAccess1.Group).Current.GroupName))
    {
      bool inserted = ((PXSelectBase) glAccess1.Group).Cache.GetStatus((object) ((PXSelectBase<RelationGroup>) glAccess1.Group).Current) == 2;
      GLAccess glAccess2 = glAccess1;
      object[] objArray = new object[1]
      {
        (object) new byte[0]
      };
      foreach (PXResult<GLBudgetTree> pxResult in PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where2<Match<Current<RelationGroup.groupName>>, Or2<Match<Required<GLBudgetTree.groupMask>>, Or<GLBudgetTree.groupMask, IsNull>>>>.Config>.Select((PXGraph) glAccess2, objArray))
      {
        GLBudgetTree item = PXResult<GLBudgetTree>.op_Implicit(pxResult);
        if (!inserted)
        {
          ((PXSelectBase<GLBudgetTree>) glAccess1.BudgetTree).Current = item;
          yield return (object) item;
        }
        else if (item.GroupMask != null)
        {
          RelationGroup group = ((PXSelectBase<RelationGroup>) glAccess1.Group).Current;
          bool anyGroup = false;
          for (int i = 0; i < item.GroupMask.Length && i < group.GroupMask.Length; ++i)
          {
            if (group.GroupMask[i] != (byte) 0 && ((int) item.GroupMask[i] & (int) group.GroupMask[i]) == (int) group.GroupMask[i])
            {
              ((PXSelectBase<GLBudgetTree>) glAccess1.BudgetTree).Current = item;
              yield return (object) item;
            }
            anyGroup |= item.GroupMask[i] > (byte) 0;
          }
          if (!anyGroup)
          {
            ((PXSelectBase<GLBudgetTree>) glAccess1.BudgetTree).Current = item;
            yield return (object) item;
          }
          group = (RelationGroup) null;
        }
        item = (GLBudgetTree) null;
      }
    }
  }

  protected virtual IEnumerable printers()
  {
    GLAccess glAccess1 = this;
    if (((PXSelectBase<RelationGroup>) glAccess1.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) glAccess1.Group).Current.GroupName))
    {
      bool inserted = ((PXSelectBase) glAccess1.Group).Cache.GetStatus((object) ((PXSelectBase<RelationGroup>) glAccess1.Group).Current) == 2;
      GLAccess glAccess2 = glAccess1;
      object[] objArray = new object[1]
      {
        (object) new byte[0]
      };
      foreach (PXResult<SMPrinter> pxResult in PXSelectBase<SMPrinter, PXSelect<SMPrinter, Where2<Match<Current<RelationGroup.groupName>>, Or2<Match<Required<SMPrinter.groupMask>>, Or<SMPrinter.groupMask, IsNull>>>>.Config>.Select((PXGraph) glAccess2, objArray))
      {
        SMPrinter item = PXResult<SMPrinter>.op_Implicit(pxResult);
        if (!inserted)
        {
          ((PXSelectBase<SMPrinter>) glAccess1.Printers).Current = item;
          yield return (object) item;
        }
        else if (item.GroupMask != null)
        {
          RelationGroup group = ((PXSelectBase<RelationGroup>) glAccess1.Group).Current;
          bool anyGroup = false;
          for (int i = 0; i < item.GroupMask.Length && i < group.GroupMask.Length; ++i)
          {
            if (group.GroupMask[i] != (byte) 0 && ((int) item.GroupMask[i] & (int) group.GroupMask[i]) == (int) group.GroupMask[i])
            {
              ((PXSelectBase<SMPrinter>) glAccess1.Printers).Current = item;
              yield return (object) item;
            }
            anyGroup |= item.GroupMask[i] > (byte) 0;
          }
          if (!anyGroup)
          {
            ((PXSelectBase<SMPrinter>) glAccess1.Printers).Current = item;
            yield return (object) item;
          }
          group = (RelationGroup) null;
        }
        item = (SMPrinter) null;
      }
    }
  }

  protected virtual void RelationGroup_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    base.RelationGroup_RowInserted(cache, e);
    ((RelationGroup) e.Row).SpecificModule = typeof (generalLedgerModule).Namespace;
  }

  protected virtual void RelationGroup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is RelationGroup row))
      return;
    if (string.IsNullOrEmpty(row.GroupName))
    {
      ((PXAction) this.Save).SetEnabled(false);
      ((PXSelectBase) this.Account).Cache.AllowInsert = false;
      ((PXSelectBase) this.Sub).Cache.AllowInsert = false;
    }
    else
    {
      ((PXAction) this.Save).SetEnabled(true);
      ((PXSelectBase) this.Account).Cache.AllowInsert = true;
      ((PXSelectBase) this.Sub).Cache.AllowInsert = true;
    }
  }

  protected virtual void Account_AccountCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (PXSelectorAttribute.Select<PX.Objects.GL.Account.accountCD>(sender, e.Row, e.NewValue) == null)
      throw new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) "[accountCD]"
      });
  }

  protected virtual void Account_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (PXSelectorAttribute.Select<PX.Objects.GL.Account.accountCD>(sender, e.Row) is PX.Objects.GL.Account account)
    {
      account.Included = new bool?(true);
      PXCache<PX.Objects.GL.Account>.RestoreCopy((PX.Objects.GL.Account) e.Row, account);
      sender.SetStatus(e.Row, (PXEntryStatus) 1);
    }
    else
      sender.Delete(e.Row);
  }

  protected virtual void Account_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.GL.Account row) || sender.GetStatus((object) row) != null)
      return;
    if (row.GroupMask != null)
    {
      foreach (byte num in row.GroupMask)
      {
        if (num != (byte) 0)
        {
          row.Included = new bool?(true);
          break;
        }
      }
    }
    else
      row.Included = new bool?(true);
  }

  protected virtual void Account_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Objects.GL.Account row = e.Row as PX.Objects.GL.Account;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (row.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = row.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      row.GroupMask = groupMask;
    }
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        bool? included = row.Included;
        row.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  protected virtual void Sub_SubCD_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (PXSelectorAttribute.Select<PX.Objects.GL.Sub.subCD>(sender, e.Row, e.NewValue) == null)
      throw new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) "[subCD]"
      });
  }

  protected virtual void Sub_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (PXSelectorAttribute.Select<PX.Objects.GL.Sub.subCD>(sender, e.Row) is PX.Objects.GL.Sub sub)
    {
      sub.Included = new bool?(true);
      PXCache<PX.Objects.GL.Sub>.RestoreCopy((PX.Objects.GL.Sub) e.Row, sub);
      sender.SetStatus(e.Row, (PXEntryStatus) 1);
    }
    else
      sender.Delete(e.Row);
  }

  protected virtual void Sub_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.GL.Sub row) || sender.GetStatus((object) row) != null)
      return;
    if (row.GroupMask != null)
    {
      foreach (byte num in row.GroupMask)
      {
        if (num != (byte) 0)
        {
          row.Included = new bool?(true);
          break;
        }
      }
    }
    else
      row.Included = new bool?(true);
  }

  protected virtual void Sub_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Objects.GL.Sub row = e.Row as PX.Objects.GL.Sub;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (row.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = row.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      row.GroupMask = groupMask;
    }
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        bool? included = row.Included;
        row.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  protected virtual void Branch_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (PXSelectorAttribute.Select<PX.Objects.GL.Branch.branchCD>(sender, e.Row) is PX.Objects.GL.Branch branch)
    {
      branch.Included = new bool?(true);
      PXCache<PX.Objects.GL.Branch>.RestoreCopy((PX.Objects.GL.Branch) e.Row, branch);
      sender.SetStatus(e.Row, (PXEntryStatus) 1);
    }
    else
      sender.Delete(e.Row);
  }

  protected virtual void Branch_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.GL.Branch row) || sender.GetStatus((object) row) != null)
      return;
    if (row.GroupMask != null)
    {
      foreach (byte num in row.GroupMask)
      {
        if (num != (byte) 0)
        {
          row.Included = new bool?(true);
          break;
        }
      }
    }
    else
      row.Included = new bool?(true);
  }

  protected virtual void Branch_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Objects.GL.Branch row = e.Row as PX.Objects.GL.Branch;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (row.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = row.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      row.GroupMask = groupMask;
    }
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        bool? included = row.Included;
        row.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  protected virtual void GLBudgetTree_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is GLBudgetTree row) || sender.GetStatus((object) row) != null)
      return;
    if (row.GroupMask != null)
    {
      foreach (byte num in row.GroupMask)
      {
        if (num != (byte) 0)
        {
          row.Included = new bool?(true);
          break;
        }
      }
    }
    else
      row.Included = new bool?(true);
  }

  protected virtual void GLBudgetTree_Description_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue != null)
      return;
    GLBudgetTree row = e.Row as GLBudgetTree;
    e.NewValue = (object) row.Description;
  }

  protected virtual void GLBudgetTree_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    GLBudgetTree newRow = e.NewRow as GLBudgetTree;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (newRow == null || newRow.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (newRow.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = newRow.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      newRow.GroupMask = groupMask;
    }
    bool? included;
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        included = newRow.Included;
        newRow.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) newRow.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) newRow.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
    Guid? groupId = newRow.GroupID;
    included = newRow.Included;
    int num = included.GetValueOrDefault() ? 1 : 0;
    this.UpdateChildGroupMask(groupId, num != 0);
  }

  protected virtual void GLBudgetTree_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (PXSelectorAttribute.Select<GLBudgetTree.groupID>(sender, e.Row) is GLBudgetTree glBudgetTree)
    {
      glBudgetTree.Included = new bool?(true);
      PXCache<GLBudgetTree>.RestoreCopy((GLBudgetTree) e.Row, glBudgetTree);
      sender.SetStatus(e.Row, (PXEntryStatus) 1);
    }
    else
      sender.Delete(e.Row);
  }

  protected virtual void GLBudgetTree_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    GLBudgetTree row = e.Row as GLBudgetTree;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (row.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = row.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      row.GroupMask = groupMask;
    }
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        bool? included = row.Included;
        row.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  private void UpdateChildGroupMask(Guid? GroupID, bool Included)
  {
    foreach (PXResult<GLBudgetTree> pxResult in PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) GroupID
    }))
    {
      GLBudgetTree glBudgetTree = PXResult<GLBudgetTree>.op_Implicit(pxResult);
      Guid? groupId = glBudgetTree.GroupID;
      Guid? parentGroupId = glBudgetTree.ParentGroupID;
      if ((groupId.HasValue == parentGroupId.HasValue ? (groupId.HasValue ? (groupId.GetValueOrDefault() != parentGroupId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        glBudgetTree.Included = new bool?(Included);
        ((PXSelectBase<GLBudgetTree>) this.BudgetTree).Update(glBudgetTree);
        this.UpdateChildGroupMask(glBudgetTree.GroupID, Included);
      }
    }
  }

  protected virtual void SMPrinter_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (PXSelectorAttribute.Select<SMPrinter.printerID>(sender, e.Row) is SMPrinter smPrinter)
    {
      smPrinter.Included = new bool?(true);
      PXCache<SMPrinter>.RestoreCopy((SMPrinter) e.Row, smPrinter);
      sender.SetStatus(e.Row, (PXEntryStatus) 1);
    }
    else
      sender.Delete(e.Row);
  }

  protected virtual void SMPrinter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SMPrinter row) || sender.GetStatus((object) row) != null)
      return;
    if (row.GroupMask != null)
    {
      foreach (byte num in row.GroupMask)
      {
        if (num != (byte) 0)
        {
          row.Included = new bool?(true);
          break;
        }
      }
    }
    else
      row.Included = new bool?(true);
  }

  protected virtual void SMPrinter_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    SMPrinter row = e.Row as SMPrinter;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (row.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = row.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      row.GroupMask = groupMask;
    }
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        bool? included = row.Included;
        row.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  protected virtual void SegmentValue_Value_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (PXSelectorAttribute.Select<SegmentValue.value>(sender, e.Row, e.NewValue) == null)
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system.", new object[1]
      {
        (object) "[value]"
      }));
  }

  protected virtual void SegmentValue_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (PXSelectorAttribute.Select<SegmentValue.value>(sender, e.Row) is SegmentValue segmentValue)
    {
      segmentValue.Included = new bool?(true);
      PXCache<SegmentValue>.RestoreCopy((SegmentValue) e.Row, segmentValue);
      sender.SetStatus(e.Row, (PXEntryStatus) 1);
    }
    else
      sender.Delete(e.Row);
  }

  protected virtual void SegmentValue_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SegmentValue row) || sender.GetStatus((object) row) != null)
      return;
    if (row.GroupMask != null)
    {
      foreach (byte num in row.GroupMask)
      {
        if (num != (byte) 0)
        {
          row.Included = new bool?(true);
          break;
        }
      }
    }
    else
      row.Included = new bool?(true);
  }

  protected virtual void SegmentValue_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    SegmentValue row = e.Row as SegmentValue;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (row.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = row.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      row.GroupMask = groupMask;
    }
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        bool? included = row.Included;
        row.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  public virtual void Persist()
  {
    this.populateNeighbours<Users>((PXSelectBase<Users>) this.Users);
    this.populateNeighbours<PX.Objects.GL.Account>((PXSelectBase<PX.Objects.GL.Account>) this.Account);
    this.populateNeighbours<PX.Objects.GL.Sub>((PXSelectBase<PX.Objects.GL.Sub>) this.Sub);
    this.populateNeighbours<PX.Objects.GL.Branch>((PXSelectBase<PX.Objects.GL.Branch>) this.Branch);
    this.populateNeighbours<SegmentValue>((PXSelectBase<SegmentValue>) this.SegmentAll);
    this.populateNeighbours<PX.Objects.GL.Sub>((PXSelectBase<PX.Objects.GL.Sub>) this.Sub);
    this.populateNeighbours<PX.Objects.GL.Account>((PXSelectBase<PX.Objects.GL.Account>) this.Account);
    this.populateNeighbours<Users>((PXSelectBase<Users>) this.Users);
    this.populateNeighbours<GLBudgetTree>((PXSelectBase<GLBudgetTree>) this.BudgetTree);
    this.populateNeighbours<SMPrinter>((PXSelectBase<SMPrinter>) this.Printers);
    base.Persist();
    PXSelectorAttribute.ClearGlobalCache<Users>();
    PXSelectorAttribute.ClearGlobalCache<PX.Objects.GL.Account>();
    PXSelectorAttribute.ClearGlobalCache<PX.Objects.GL.Sub>();
    PXSelectorAttribute.ClearGlobalCache<PX.Objects.GL.Branch>();
    PXSelectorAttribute.ClearGlobalCache<SegmentValue>();
    PXDimensionAttribute.Clear();
  }

  public static IEnumerable GroupDelegate(PXGraph graph, bool inclInserted)
  {
    PXResultset<Neighbour> set = PXSelectBase<Neighbour, PXSelectGroupBy<Neighbour, Where<Neighbour.leftEntityType, Equal<accountType>, Or<Neighbour.leftEntityType, Equal<subType>, Or<Neighbour.leftEntityType, Equal<segmentValueType>>>>, Aggregate<GroupBy<Neighbour.coverageMask, GroupBy<Neighbour.inverseMask, GroupBy<Neighbour.winCoverageMask, GroupBy<Neighbour.winInverseMask>>>>>>.Config>.Select(graph, Array.Empty<object>());
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select(graph, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(relationGroup.GroupName) | inclInserted && (relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (PX.Objects.GL.Account).Namespace) || UserAccess.InNeighbours(set, relationGroup))
        yield return (object) relationGroup;
    }
  }

  protected virtual IEnumerable group() => GLAccess.GroupDelegate((PXGraph) this, true);

  public class GLRelationGroupAccountSelectorAttribute(Type type) : PXCustomSelectorAttribute(type)
  {
    public virtual IEnumerable GetRecords() => GLAccess.GroupDelegate(this._Graph, false);
  }
}
