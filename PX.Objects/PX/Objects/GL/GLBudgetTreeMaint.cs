// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLBudgetTreeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

public class GLBudgetTreeMaint : PXGraph<GLBudgetTreeMaint>
{
  public PXSelectOrderBy<GLBudgetTree, OrderBy<Asc<GLBudgetTree.sortOrder>>> Details;
  public PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Argument<Guid?>>>, OrderBy<Asc<GLBudgetTree.sortOrder>>> Tree;
  public PXFilter<AccountsPreloadFilter> PreloadFilter;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  private bool SubEnabled = PXAccess.FeatureInstalled<FeaturesSet.subAccount>();
  public PXSave<GLBudgetTree> Save;
  public PXCancel<GLBudgetTree> Cancel;
  public PXAction<GLBudgetTree> ShowPreload;
  public PXAction<GLBudgetTree> Preload;
  public PXAction<GLBudgetTree> ConfigureSecurity;
  public PXDelete<GLBudgetTree> DeleteNode;
  public PXAction<GLBudgetTree> left;
  public PXAction<GLBudgetTree> right;
  public PXAction<GLBudgetTree> up;
  public PXAction<GLBudgetTree> down;
  public PXAction<GLBudgetTree> deleteGroup;

  public GLBudgetTreeMaint()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
  }

  protected virtual IEnumerable tree([PXGuid] Guid? groupID)
  {
    GLBudgetTreeMaint glBudgetTreeMaint1 = this;
    if (!groupID.HasValue)
      yield return (object) new GLBudgetTree()
      {
        GroupID = new Guid?(Guid.Empty),
        Description = PXSiteMap.RootNode.Title
      };
    GLBudgetTreeMaint glBudgetTreeMaint2 = glBudgetTreeMaint1;
    object[] objArray = new object[1]{ (object) groupID };
    foreach (PXResult<GLBudgetTree> pxResult in PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.groupID>>>>.Config>.Select((PXGraph) glBudgetTreeMaint2, objArray))
    {
      GLBudgetTree glBudgetTree = PXResult<GLBudgetTree>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(glBudgetTree.Description) && glBudgetTree.IsGroup.Value && glBudgetTreeMaint1.SearchForMatchingChild(glBudgetTree.GroupID))
        yield return (object) glBudgetTree;
    }
  }

  protected virtual IEnumerable details([PXGuid] Guid? groupID)
  {
    if (!groupID.HasValue)
      groupID = ((PXSelectBase<GLBudgetTree>) this.Tree).Current != null ? ((PXSelectBase<GLBudgetTree>) this.Tree).Current.GroupID : new Guid?(Guid.Empty);
    this.CurrentSelected.Group = groupID;
    GLBudgetTree glBudgetTree1 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.groupID, Equal<Required<GLBudgetTree.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.CurrentSelected.Group
    }));
    if (glBudgetTree1 != null)
    {
      this.CurrentSelected.AccountID = glBudgetTree1.AccountID.HasValue ? glBudgetTree1.AccountID : new int?(int.MinValue);
      this.CurrentSelected.SubID = glBudgetTree1.SubID.HasValue ? glBudgetTree1.SubID : new int?(int.MinValue);
      this.CurrentSelected.GroupMask = glBudgetTree1.GroupMask;
    }
    else
    {
      this.CurrentSelected.AccountID = new int?(int.MinValue);
      this.CurrentSelected.SubID = new int?(int.MinValue);
      this.CurrentSelected.GroupMask = (byte[]) null;
    }
    this.CurrentSelected.AccountMaskWildcard = SubCDUtils.CreateSubCDWildcard(glBudgetTree1 != null ? glBudgetTree1.AccountMask : string.Empty, "ACCOUNT");
    this.CurrentSelected.SubMaskWildcard = glBudgetTree1 != null ? glBudgetTree1.SubMask : string.Empty;
    List<GLBudgetTree> glBudgetTreeList = new List<GLBudgetTree>();
    foreach (PXResult<GLBudgetTree> pxResult in PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>, And<GLBudgetTree.parentGroupID, NotEqual<GLBudgetTree.groupID>, And<Match<Current<AccessInfo.userName>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) groupID
    }))
    {
      GLBudgetTree glBudgetTree2 = PXResult<GLBudgetTree>.op_Implicit(pxResult);
      if (glBudgetTree2.GroupMask != null)
      {
        foreach (byte num in glBudgetTree2.GroupMask)
        {
          if (num != (byte) 0)
            glBudgetTree2.Secured = new bool?(true);
        }
      }
      glBudgetTreeList.Add(glBudgetTree2);
    }
    if (PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.groupID, Equal<Required<GLBudgetTree.groupID>>, And<Match<Current<AccessInfo.userName>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) groupID
    }).Count == 0)
    {
      Guid? nullable = groupID;
      Guid empty = Guid.Empty;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() != empty ? 1 : 0) : 1) != 0)
      {
        ((PXSelectBase) this.Details).Cache.AllowInsert = false;
        ((PXSelectBase) this.Details).Cache.AllowDelete = false;
        ((PXSelectBase) this.Details).Cache.AllowUpdate = false;
        goto label_22;
      }
    }
    ((PXSelectBase) this.Details).Cache.AllowInsert = true;
    ((PXSelectBase) this.Details).Cache.AllowDelete = true;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = true;
label_22:
    return (IEnumerable) glBudgetTreeList;
  }

  private SelectedNode CurrentSelected
  {
    get
    {
      PXCache cach = ((PXGraph) this).Caches[typeof (SelectedNode)];
      if (cach.Current == null)
      {
        cach.Insert();
        cach.IsDirty = false;
      }
      return (SelectedNode) cach.Current;
    }
  }

  private bool SearchForMatchingChild(Guid? GroupID)
  {
    PXResultset<GLBudgetTree> pxResultset = PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>, And<GLBudgetTree.isGroup, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) GroupID
    });
    if (PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.groupID, Equal<Required<GLBudgetTree.groupID>>, And<Match<Current<AccessInfo.userName>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) GroupID
    }).Count != 0)
      return true;
    if (PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>, And<Match<Current<AccessInfo.userName>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) GroupID
    }).Count != 0)
      return true;
    foreach (PXResult<GLBudgetTree> pxResult in pxResultset)
    {
      if (this.SearchForMatchingChild(PXResult<GLBudgetTree>.op_Implicit(pxResult).GroupID))
        return true;
    }
    return false;
  }

  [PXUIField]
  [PXButton(ImageSet = "main", ImageKey = "RecordDel", Tooltip = "Delete node", DisplayOnMainToolbar = false)]
  public virtual IEnumerable deleteNode(PXAdapter adapter)
  {
    GLBudgetTree copy = PXCache<GLBudgetTree>.CreateCopy(((PXSelectBase<GLBudgetTree>) this.Details).Current);
    if (((PXSelectBase<GLBudgetTree>) this.Details).Current.IsGroup.GetValueOrDefault())
    {
      Guid? groupId = ((PXSelectBase<GLBudgetTree>) this.Details).Current.GroupID;
      Guid? parentGroupId = ((PXSelectBase<GLBudgetTree>) this.Details).Current.ParentGroupID;
      if ((groupId.HasValue == parentGroupId.HasValue ? (groupId.HasValue ? (groupId.GetValueOrDefault() != parentGroupId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        if (((PXSelectBase<GLBudgetTree>) this.Details).Ask("Delete Group", "The child records will be deleted. Are you sure you want to delete the group?", (MessageButtons) 4) == 6)
        {
          this.deleteRecords(((PXSelectBase<GLBudgetTree>) this.Details).Current.GroupID);
          ((PXSelectBase) this.Details).Cache.Delete((object) copy);
          goto label_5;
        }
        goto label_5;
      }
    }
    ((PXSelectBase) this.Details).Cache.Delete((object) copy);
label_5:
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable showPreload(PXAdapter adapter)
  {
    GLBudgetTree glBudgetTree = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.groupID, Equal<Required<GLBudgetTree.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.CurrentSelected.Group
    }));
    string str = new string('?', ((PXStringState) ((PXSelectBase) this.Details).Cache.GetStateExt((object) null, typeof (GLBudgetTree.subID).Name)).InputMask.Length - 1);
    if (glBudgetTree != null)
    {
      ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.AccountCDWildcard = SubCDUtils.CreateSubCDWildcard(glBudgetTree != null ? glBudgetTree.AccountMask : string.Empty, "ACCOUNT");
      if (glBudgetTree.AccountMask != null)
      {
        Account account1 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.active, Equal<True>, And<Account.accountCD, Like<Required<SelectedNode.accountMaskWildcard>>>>, OrderBy<Asc<Account.accountCD>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
        {
          (object) ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.AccountCDWildcard
        }));
        Account account2 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.active, Equal<True>, And<Account.accountCD, Like<Required<AccountsPreloadFilter.accountCDWildcard>>>>, OrderBy<Desc<Account.accountCD>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
        {
          (object) ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.AccountCDWildcard
        }));
        ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.FromAccount = (int?) account1?.AccountID;
        ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.ToAccount = (int?) account2?.AccountID;
      }
      else
      {
        ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.FromAccount = new int?();
        ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.ToAccount = new int?();
      }
      ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.SubIDFilter = glBudgetTree.SubMask ?? str;
    }
    else
    {
      ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.FromAccount = new int?();
      ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.ToAccount = new int?();
      ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.SubIDFilter = str;
    }
    if (((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current != null && glBudgetTree != null && glBudgetTree.AccountMask != null && !((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.FromAccount.HasValue && ((PXSelectBase<GLBudgetTree>) this.Details).Ask("Can not preload articles", string.Format(PXMessages.LocalizeNoPrefix("No lines can be preloaded using Account mask provided: {0}"), (object) glBudgetTree.AccountMask), (MessageButtons) 0) == 1)
      return adapter.Get();
    ((PXSelectBase<GLBudgetTree>) this.Details).AskExt();
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable preload(PXAdapter adapter)
  {
    if (!((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.FromAccount.HasValue)
    {
      ((PXSelectBase) this.PreloadFilter).Cache.RaiseExceptionHandling<AccountsPreloadFilter.fromAccount>((object) ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current, (object) ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.FromAccount, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 5));
      ((PXSelectBase) this.PreloadFilter).Cache.RaiseExceptionHandling<AccountsPreloadFilter.toAccount>((object) ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current, (object) ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.ToAccount, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 5));
      return adapter.Get();
    }
    GLBudgetTree glBudgetTree1 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.groupID, Equal<Required<GLBudgetTree.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.CurrentSelected.Group
    }));
    ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.AccountCDWildcard = SubCDUtils.CreateSubCDWildcard(glBudgetTree1 != null ? glBudgetTree1.AccountMask : string.Empty, "ACCOUNT");
    GLBudgetTree glBudgetTree2 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>>, OrderBy<Desc<GLBudgetTree.sortOrder>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) this.CurrentSelected.Group
    }));
    int num = glBudgetTree2 == null ? 1 : glBudgetTree2.SortOrder.Value + 1;
    bool flag = true;
    List<GLBudgetTree> nodes = new List<GLBudgetTree>();
    object[] objArray = new object[2];
    int? nullable1 = ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.FromAccount;
    objArray[0] = (object) (nullable1.HasValue ? PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Current<AccountsPreloadFilter.fromAccount>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).AccountCD : PXResult<Account>.op_Implicit(((IQueryable<PXResult<Account>>) PXSelectBase<Account, PXSelect<Account>.Config>.Select((PXGraph) this, Array.Empty<object>())).First<PXResult<Account>>()).AccountCD);
    nullable1 = ((PXSelectBase<AccountsPreloadFilter>) this.PreloadFilter).Current.ToAccount;
    objArray[1] = (object) (nullable1.HasValue ? PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Current<AccountsPreloadFilter.toAccount>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).AccountCD : PXResult<Account>.op_Implicit(((IQueryable<PXResult<Account>>) PXSelectBase<Account, PXSelect<Account>.Config>.Select((PXGraph) this, Array.Empty<object>())).Last<PXResult<Account>>()).AccountCD);
    foreach (PXResult<Account> pxResult1 in PXSelectBase<Account, PXSelect<Account, Where<Account.accountCD, GreaterEqual<Required<Account.accountCD>>, And<Account.active, Equal<True>, And<Account.accountCD, LessEqual<Required<Account.accountCD>>, And<Account.accountID, NotEqual<Current<PX.Objects.GL.GLSetup.ytdNetIncAccountID>>>>>>>.Config>.Select((PXGraph) this, objArray))
    {
      foreach (PXResult<Sub> pxResult2 in PXSelectBase<Sub, PXSelect<Sub, Where<Sub.active, Equal<True>, And<Sub.subCD, Like<Current<AccountsPreloadFilter.subCDWildcard>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      {
        Account account = PXResult<Account>.op_Implicit(pxResult1);
        Sub sub = PXResult<Sub>.op_Implicit(pxResult2);
        GLBudgetTree glBudgetTree3 = new GLBudgetTree();
        if (glBudgetTree1 != null)
        {
          nullable1 = account.AccountID;
          int? nullable2 = glBudgetTree1.AccountID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = sub.SubID;
            nullable1 = glBudgetTree1.SubID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              continue;
          }
        }
        glBudgetTree3.AccountID = account.AccountID;
        glBudgetTree3.SubID = sub.SubID;
        glBudgetTree3.SortOrder = new int?(num++);
        glBudgetTree3.GroupMask = this.CurrentSelected.GroupMask;
        nodes.Add(glBudgetTree3);
        flag = false;
      }
    }
    if (flag && ((PXSelectBase<GLBudgetTree>) this.Details).Ask("Can not preload articles", "No lines can be preloaded using Account/Subaccount mask provided.", (MessageButtons) 0) == 1)
      return adapter.Get();
    if (nodes.Count > 500)
    {
      if (((PXSelectBase<GLBudgetTree>) this.Details).Ask("Confirmation", string.Format(PXMessages.LocalizeNoPrefix("{0} subarticles will be created. Are you sure you want to continue?"), (object) nodes.Count), (MessageButtons) 1) == 1)
        this.InsertPreloadedNodes((IEnumerable<GLBudgetTree>) nodes);
    }
    else
      this.InsertPreloadedNodes((IEnumerable<GLBudgetTree>) nodes);
    return adapter.Get();
  }

  protected virtual void InsertPreloadedNodes(IEnumerable<GLBudgetTree> nodes)
  {
    bool flag = false;
    foreach (GLBudgetTree node in nodes)
    {
      try
      {
        ((PXSelectBase<GLBudgetTree>) this.Details).Insert(node);
      }
      catch (PXSetPropertyException ex)
      {
        flag = true;
      }
    }
    if (flag)
      throw new PXException("Because some segment values of subaccounts have been deactivated or deleted on the Segment Values (CS203000) form, the subaccounts with these values will not be loaded.");
  }

  [PXLookupButton]
  [PXUIField]
  public virtual IEnumerable configureSecurity(PXAdapter adapter)
  {
    GLBudgetTree glBudgetTree = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.groupID, Equal<Current<GLBudgetTree.groupID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<GLBudgetTree>) this.Details).Current
    }, Array.Empty<object>()));
    if (glBudgetTree != null)
    {
      GLAccessByBudgetNode instance = PXGraph.CreateInstance<GLAccessByBudgetNode>();
      ((PXSelectBase<GLBudgetTree>) instance.BudgetTree).Current = glBudgetTree;
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Restricted Groups");
    }
    throw new PXException("Budget Tree node cannot be found in the system.");
  }

  [PXUIField]
  [PXButton(ImageSet = "main", ImageKey = "ArrowLeft", Tooltip = "Move left")]
  public virtual IEnumerable Left(PXAdapter adapter)
  {
    GLBudgetTree glBudgetTree1 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.groupID, Equal<Required<GLBudgetTree.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.CurrentSelected.Group
    }));
    if (glBudgetTree1 != null)
    {
      GLBudgetTree glBudgetTree2 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.isGroup, Equal<True>, And<GLBudgetTree.groupID, Equal<Required<GLBudgetTree.groupID>>>>, OrderBy<Desc<GLBudgetTree.sortOrder>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) glBudgetTree1.ParentGroupID
      }));
      if (glBudgetTree2 != null)
      {
        GLBudgetTree glBudgetTree3 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>>, OrderBy<Desc<GLBudgetTree.sortOrder>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
        {
          (object) glBudgetTree2.ParentGroupID
        }));
        if (glBudgetTree3 != null)
        {
          glBudgetTree1.ParentGroupID = glBudgetTree3.ParentGroupID;
          GLBudgetTree glBudgetTree4 = glBudgetTree1;
          int? sortOrder = glBudgetTree3.SortOrder;
          int? nullable = sortOrder.HasValue ? new int?(sortOrder.GetValueOrDefault() + 1) : new int?();
          glBudgetTree4.SortOrder = nullable;
          ((PXSelectBase<GLBudgetTree>) this.Tree).Update(glBudgetTree1);
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageSet = "main", ImageKey = "ArrowRight", Tooltip = "Move right")]
  public virtual IEnumerable Right(PXAdapter adapter)
  {
    GLBudgetTree glBudgetTree1 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.groupID, Equal<Required<GLBudgetTree.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.CurrentSelected.Group
    }));
    if (glBudgetTree1 != null)
    {
      GLBudgetTree glBudgetTree2 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.isGroup, Equal<True>, And<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>, And<GLBudgetTree.sortOrder, Less<Required<GLBudgetTree.sortOrder>>>>>, OrderBy<Desc<GLBudgetTree.sortOrder>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
      {
        (object) glBudgetTree1.ParentGroupID,
        (object) glBudgetTree1.SortOrder
      }));
      if (glBudgetTree2 != null && (!glBudgetTree2.IsGroup.Value || glBudgetTree2.AccountMask == null || glBudgetTree2.SubMask == null || ((PXSelectBase<GLBudgetTree>) this.Details).Ask("Cannot move group", "Group cannot be moved into the aggregating article.", (MessageButtons) 0) != 1))
      {
        GLBudgetTree glBudgetTree3 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>>, OrderBy<Desc<GLBudgetTree.sortOrder>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
        {
          (object) glBudgetTree2.GroupID
        }));
        if (glBudgetTree3 != null)
        {
          glBudgetTree1.ParentGroupID = glBudgetTree3.ParentGroupID;
          GLBudgetTree glBudgetTree4 = glBudgetTree1;
          int? sortOrder = glBudgetTree3.SortOrder;
          int? nullable = sortOrder.HasValue ? new int?(sortOrder.GetValueOrDefault() + 1) : new int?();
          glBudgetTree4.SortOrder = nullable;
          ((PXSelectBase<GLBudgetTree>) this.Tree).Update(glBudgetTree1);
        }
        else
        {
          glBudgetTree1.ParentGroupID = glBudgetTree2.GroupID;
          glBudgetTree1.SortOrder = new int?(1);
          ((PXSelectBase<GLBudgetTree>) this.Tree).Update(glBudgetTree1);
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageSet = "main", ImageKey = "ArrowUp", Tooltip = "Move node up")]
  public virtual IEnumerable Up(PXAdapter adapter)
  {
    GLBudgetTree glBudgetTree1 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.groupID, Equal<Required<GLBudgetTree.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.CurrentSelected.Group
    }));
    if (glBudgetTree1 != null)
    {
      GLBudgetTree glBudgetTree2 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.isGroup, Equal<True>, And<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>, And<GLBudgetTree.sortOrder, Less<Required<GLBudgetTree.sortOrder>>>>>, OrderBy<Desc<GLBudgetTree.sortOrder>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
      {
        (object) glBudgetTree1.ParentGroupID,
        (object) glBudgetTree1.SortOrder
      }));
      if (glBudgetTree2 != null)
      {
        int num = glBudgetTree1.SortOrder.Value;
        glBudgetTree1.SortOrder = glBudgetTree2.SortOrder;
        ((PXSelectBase<GLBudgetTree>) this.Tree).Update(glBudgetTree1);
        glBudgetTree2.SortOrder = new int?(num);
        ((PXSelectBase<GLBudgetTree>) this.Tree).Update(glBudgetTree2);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageSet = "main", ImageKey = "ArrowDown", Tooltip = "Move node down")]
  public virtual IEnumerable Down(PXAdapter adapter)
  {
    GLBudgetTree glBudgetTree1 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.groupID, Equal<Required<GLBudgetTree.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.CurrentSelected.Group
    }));
    if (glBudgetTree1 != null)
    {
      GLBudgetTree glBudgetTree2 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.isGroup, Equal<True>, And<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>, And<GLBudgetTree.sortOrder, Greater<Required<GLBudgetTree.sortOrder>>>>>, OrderBy<Asc<GLBudgetTree.sortOrder>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
      {
        (object) glBudgetTree1.ParentGroupID,
        (object) glBudgetTree1.SortOrder
      }));
      if (glBudgetTree2 != null)
      {
        int num = glBudgetTree1.SortOrder.Value;
        glBudgetTree1.SortOrder = glBudgetTree2.SortOrder;
        ((PXSelectBase<GLBudgetTree>) this.Tree).Update(glBudgetTree1);
        glBudgetTree2.SortOrder = new int?(num);
        ((PXSelectBase<GLBudgetTree>) this.Tree).Update(glBudgetTree2);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageSet = "main", ImageKey = "RecordDel", Tooltip = "Delete node")]
  public virtual IEnumerable DeleteGroup(PXAdapter adapter)
  {
    if (((PXSelectBase<GLBudgetTree>) this.Details).Ask("Delete Group", "The child records will be deleted. Are you sure you want to delete the group?", (MessageButtons) 4) == 6)
    {
      Guid? groupId = ((PXSelectBase<GLBudgetTree>) this.Details).Current.GroupID;
      Guid? parentGroupId = ((PXSelectBase<GLBudgetTree>) this.Details).Current.ParentGroupID;
      if ((groupId.HasValue == parentGroupId.HasValue ? (groupId.HasValue ? (groupId.GetValueOrDefault() != parentGroupId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        this.deleteRecords(this.CurrentSelected.Group);
      ((PXSelectBase) this.Tree).Cache.Delete((object) PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.groupID, Equal<Required<GLBudgetTree.groupID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) this.CurrentSelected.Group
      })));
    }
    return adapter.Get();
  }

  private void deleteRecords(Guid? groupID)
  {
    foreach (PXResult<GLBudgetTree> pxResult in PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) groupID
    }))
    {
      GLBudgetTree glBudgetTree = PXResult<GLBudgetTree>.op_Implicit(pxResult);
      Guid? groupId = glBudgetTree.GroupID;
      Guid? parentGroupId = glBudgetTree.ParentGroupID;
      if ((groupId.HasValue == parentGroupId.HasValue ? (groupId.HasValue ? (groupId.GetValueOrDefault() != parentGroupId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && glBudgetTree.IsGroup.GetValueOrDefault())
        this.deleteRecords(glBudgetTree.GroupID);
      ((PXSelectBase) this.Tree).Cache.Delete((object) glBudgetTree);
    }
  }

  protected virtual void GLBudgetTree_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    GLBudgetTree row = (GLBudgetTree) e.Row;
    if (row == null)
      return;
    bool? nullable1 = row.IsGroup;
    int? nullable2;
    if (nullable1.HasValue)
    {
      PXCache pxCache1 = cache;
      GLBudgetTree glBudgetTree1 = row;
      nullable1 = row.IsGroup;
      int num1 = !nullable1.Value ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<GLBudgetTree.accountID>(pxCache1, (object) glBudgetTree1, num1 != 0);
      PXCache pxCache2 = cache;
      GLBudgetTree glBudgetTree2 = row;
      nullable1 = row.IsGroup;
      int num2 = !nullable1.Value ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<GLBudgetTree.subID>(pxCache2, (object) glBudgetTree2, num2 != 0);
      nullable1 = row.Rollup;
      if (nullable1.HasValue)
      {
        nullable1 = row.Rollup;
        if (nullable1.Value)
        {
          nullable1 = row.IsGroup;
          if (!nullable1.Value)
          {
            nullable2 = row.AccountID;
            if (nullable2.HasValue)
            {
              nullable2 = row.SubID;
              if (nullable2.HasValue)
              {
                PXUIFieldAttribute.SetEnabled<GLBudgetTree.isGroup>(cache, (object) row, false);
                PXUIFieldAttribute.SetEnabled<GLBudgetTree.accountMask>(cache, (object) row, false);
                PXUIFieldAttribute.SetEnabled<GLBudgetTree.subMask>(cache, (object) row, false);
              }
            }
          }
        }
      }
      nullable1 = row.IsGroup;
      if (nullable1.Value)
      {
        if (PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.GroupID
        }).Count > 0)
        {
          PXUIFieldAttribute.SetEnabled<GLBudgetTree.accountMask>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<GLBudgetTree.subMask>(cache, (object) row, false);
        }
        else
        {
          PXUIFieldAttribute.SetEnabled<GLBudgetTree.accountMask>(cache, (object) row, true);
          PXUIFieldAttribute.SetEnabled<GLBudgetTree.subMask>(cache, (object) row, true);
        }
      }
    }
    PXCache pxCache = cache;
    GLBudgetTree glBudgetTree = row;
    nullable2 = row.AccountID;
    if (!nullable2.HasValue)
    {
      nullable2 = row.SubID;
      if (!nullable2.HasValue)
        goto label_16;
    }
    int num;
    if (this.SubEnabled)
    {
      nullable2 = row.AccountID;
      num = !nullable2.HasValue ? 0 : (!this.SubEnabled ? 1 : 0);
      goto label_17;
    }
label_16:
    num = 1;
label_17:
    PXUIFieldAttribute.SetEnabled<GLBudgetTree.isGroup>(pxCache, (object) glBudgetTree, num != 0);
  }

  protected virtual void GLBudgetTree_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is GLBudgetTree row))
      return;
    Guid? groupId = row.GroupID;
    Guid empty = Guid.Empty;
    if ((groupId.HasValue ? (groupId.GetValueOrDefault() == empty ? 1 : 0) : 0) != 0)
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      int num;
      if (!row.IsGroup.Value)
      {
        groupId = row.GroupID;
        Guid? parentGroupId = row.ParentGroupID;
        num = groupId.HasValue == parentGroupId.HasValue ? (groupId.HasValue ? (groupId.GetValueOrDefault() == parentGroupId.GetValueOrDefault() ? 1 : 0) : 1) : 0;
      }
      else
        num = 1;
      bool flag = num != 0;
      if ((!row.AccountID.HasValue || !row.SubID.HasValue) && !flag)
        ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<GLBudgetTree.accountID>(e.Row, (object) row.AccountID, (Exception) new PXSetPropertyException(PXMessages.LocalizeNoPrefix("Account/Subaccount may not be empty for non-node lines."), (PXErrorLevel) 5));
      else if ((row.AccountMask == null || row.SubMask == null) && !flag && this.SubEnabled || row.AccountMask == null && !flag && !this.SubEnabled)
        ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<GLBudgetTree.accountID>(e.Row, (object) row.AccountMask, (Exception) new PXSetPropertyException(PXMessages.LocalizeNoPrefix("Account/Subaccount Mask may not be empty for non-node lines."), (PXErrorLevel) 5));
      else if (this.SubEnabled & flag && row.AccountMask == null && row.SubMask != null)
      {
        ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<GLBudgetTree.accountID>(e.Row, (object) row.AccountMask, (Exception) new PXSetPropertyException(PXMessages.LocalizeNoPrefix("Account Mask may not be empty if Subaccount Mask is entered."), (PXErrorLevel) 5));
      }
      else
      {
        if (!(this.SubEnabled & flag) || row.AccountMask == null || row.SubMask != null)
          return;
        ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<GLBudgetTree.accountID>(e.Row, (object) row.AccountMask, (Exception) new PXSetPropertyException(PXMessages.LocalizeNoPrefix("Subaccount Mask may not be empty if Account Mask is entered."), (PXErrorLevel) 5));
      }
    }
  }

  private void VerifyParentChildMask(GLBudgetTree child, GLBudgetTree parent)
  {
    Guid? parentGroupId = child.ParentGroupID;
    Guid? groupId = parent.GroupID;
    if ((parentGroupId.HasValue == groupId.HasValue ? (parentGroupId.HasValue ? (parentGroupId.GetValueOrDefault() == groupId.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0 || parent.AccountMask == null || parent.SubMask == null)
      return;
    if (!this.MatchMask(child.AccountMask, parent.AccountMask ?? string.Empty, false))
    {
      ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<GLBudgetTree.accountMask>((object) child, (object) child.AccountMask, (Exception) new PXSetPropertyException(string.Format(PXMessages.LocalizeNoPrefix("Account Mask should not extend beyond the boundaries of the parent node's Account Mask ({0})"), (object) parent.AccountMask), (PXErrorLevel) 4));
    }
    else
    {
      if (this.MatchMask(child.SubMask, parent.SubMask ?? string.Empty, false))
        return;
      ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<GLBudgetTree.subMask>((object) child, (object) child.SubMask, (Exception) new PXSetPropertyException(string.Format(PXMessages.LocalizeNoPrefix("Subaccount Mask should not extend beyond the boundaries of the parent node's Subaccount Mask ({0})"), (object) parent.SubMask), (PXErrorLevel) 4));
    }
  }

  public virtual void Persist()
  {
    PXResultset<GLBudgetTree> pxResultset = new PXResultset<GLBudgetTree>();
    List<GLBudgetTree> list = GraphHelper.RowCast<GLBudgetTree>(!this.SubEnabled ? (IEnumerable) PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.accountMask, IsNotNull>>.Config>.Select((PXGraph) this, Array.Empty<object>()) : (IEnumerable) PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.accountMask, IsNotNull, And<GLBudgetTree.subMask, IsNotNull>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<GLBudgetTree>();
    Dictionary<GLBudgetTree, PXEntryStatus> dictionary = new Dictionary<GLBudgetTree, PXEntryStatus>();
    foreach (GLBudgetTree key in list)
      dictionary.Add(key, ((PXSelectBase) this.Details).Cache.GetStatus((object) key));
    for (int index1 = 0; index1 < list.Count; ++index1)
    {
      GLBudgetTree glBudgetTree1 = list[index1];
      for (int index2 = index1 + 1; index2 < list.Count; ++index2)
      {
        GLBudgetTree glBudgetTree2 = list[index2];
        Guid? groupId1 = glBudgetTree1.GroupID;
        Guid? nullable1 = glBudgetTree2.GroupID;
        if ((groupId1.HasValue == nullable1.HasValue ? (groupId1.HasValue ? (groupId1.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && dictionary[glBudgetTree2] != null)
        {
          int? nullable2;
          int? nullable3;
          if (this.SubEnabled)
          {
            nullable2 = glBudgetTree1.AccountID;
            if (nullable2.HasValue)
            {
              nullable2 = glBudgetTree1.SubID;
              if (nullable2.HasValue)
              {
                nullable2 = glBudgetTree2.AccountID;
                if (nullable2.HasValue)
                {
                  nullable2 = glBudgetTree2.SubID;
                  if (nullable2.HasValue)
                  {
                    nullable2 = glBudgetTree2.AccountID;
                    nullable3 = glBudgetTree1.AccountID;
                    if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
                    {
                      nullable3 = glBudgetTree2.SubID;
                      nullable2 = glBudgetTree1.SubID;
                      if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
                        goto label_19;
                    }
                  }
                }
              }
            }
          }
          if (!this.SubEnabled)
          {
            nullable2 = glBudgetTree1.AccountID;
            if (nullable2.HasValue)
            {
              nullable2 = glBudgetTree2.AccountID;
              if (nullable2.HasValue)
              {
                nullable2 = glBudgetTree2.AccountID;
                nullable3 = glBudgetTree1.AccountID;
                if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
                  goto label_20;
              }
              else
                goto label_20;
            }
            else
              goto label_20;
          }
          else
            goto label_20;
label_19:
          ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<GLBudgetTree.accountID>((object) glBudgetTree2, (object) glBudgetTree2.AccountID, (Exception) new PXSetPropertyException(PXMessages.LocalizeNoPrefix("Duplicate GL Account/Sub Entry"), (PXErrorLevel) 5));
label_20:
          this.VerifyParentChildMask(glBudgetTree2, glBudgetTree1);
          this.VerifyParentChildMask(glBudgetTree1, glBudgetTree2);
          nullable1 = glBudgetTree2.ParentGroupID;
          Guid? groupId2 = glBudgetTree1.GroupID;
          if ((nullable1.HasValue == groupId2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != groupId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          {
            Guid? parentGroupId = glBudgetTree1.ParentGroupID;
            nullable1 = glBudgetTree2.GroupID;
            if ((parentGroupId.HasValue == nullable1.HasValue ? (parentGroupId.HasValue ? (parentGroupId.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && this.SubEnabled && (this.MatchMask(glBudgetTree1.AccountMask, glBudgetTree2.AccountMask) || this.MatchMask(glBudgetTree2.AccountMask, glBudgetTree1.AccountMask)) && (this.MatchMask(glBudgetTree1.SubMask, glBudgetTree2.SubMask) || this.MatchMask(glBudgetTree2.SubMask, glBudgetTree1.SubMask)))
              goto label_23;
          }
          if (this.SubEnabled || !this.MatchMask(glBudgetTree1.AccountMask, glBudgetTree2.AccountMask) && !this.MatchMask(glBudgetTree2.AccountMask, glBudgetTree1.AccountMask))
            continue;
label_23:
          bool flag = true;
          if (glBudgetTree1.IsGroup.Value)
          {
            foreach (PXResult<GLBudgetTree> collectChildNode in this.collectChildNodes(glBudgetTree1.GroupID))
            {
              GLBudgetTree glBudgetTree3 = PXResult<GLBudgetTree>.op_Implicit(collectChildNode);
              if (glBudgetTree2 == glBudgetTree3)
                flag = false;
            }
          }
          if (flag)
          {
            ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<GLBudgetTree.accountMask>((object) glBudgetTree2, (object) glBudgetTree2.AccountMask, (Exception) new PXSetPropertyException(string.Format(PXMessages.LocalizeNoPrefix("Account-Subaccount mask pair overlaps with another Account-Subaccount mask pair: {0} - {1}"), (object) glBudgetTree1.AccountMask, (object) glBudgetTree1.SubMask), (PXErrorLevel) 4));
            ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<GLBudgetTree.subMask>((object) glBudgetTree2, (object) glBudgetTree2.SubMask, (Exception) new PXSetPropertyException(string.Format(PXMessages.LocalizeNoPrefix("Account-Subaccount mask pair overlaps with another Account-Subaccount mask pair: {0} - {1}"), (object) glBudgetTree1.AccountMask, (object) glBudgetTree1.SubMask), (PXErrorLevel) 4));
          }
        }
      }
    }
    ((PXGraph) this).Persist();
  }

  protected virtual void GLBudgetTree_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is GLBudgetTree row))
      return;
    int? nullable1;
    if (this.CurrentSelected.AccountID.HasValue && this.CurrentSelected.SubID.HasValue)
    {
      nullable1 = this.CurrentSelected.AccountID;
      int num1 = 0;
      if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
      {
        nullable1 = this.CurrentSelected.SubID;
        int num2 = 0;
        if (nullable1.GetValueOrDefault() > num2 & nullable1.HasValue)
          row.Rollup = new bool?(true);
      }
    }
    row.GroupMask = this.CurrentSelected.GroupMask;
    nullable1 = row.SortOrder;
    int num3 = 0;
    if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue)
    {
      GLBudgetTree glBudgetTree1 = PXResultset<GLBudgetTree>.op_Implicit(PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>>, OrderBy<Desc<GLBudgetTree.sortOrder>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) this.CurrentSelected.Group
      }));
      GLBudgetTree glBudgetTree2 = row;
      int num4;
      if (glBudgetTree1 != null)
      {
        nullable1 = glBudgetTree1.SortOrder;
        num4 = nullable1.Value + 1;
      }
      else
        num4 = 1;
      int? nullable2 = new int?(num4);
      glBudgetTree2.SortOrder = nullable2;
    }
    bool? nullable3 = row.Rollup;
    if (!nullable3.HasValue)
      return;
    nullable3 = row.Rollup;
    if (!nullable3.Value)
      return;
    nullable3 = row.IsGroup;
    if (nullable3.Value)
      return;
    PXUIFieldAttribute.SetEnabled<GLBudgetTree.isGroup>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<GLBudgetTree.accountMask>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<GLBudgetTree.subMask>(cache, (object) row, false);
  }

  protected virtual void GLBudgetTree_SubMask_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void AccountsPreloadFilter_SubIDFilter_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLBudgetTree_GroupID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is GLBudgetTree row))
      return;
    row.GroupID = new Guid?(Guid.NewGuid());
    row.ParentGroupID = !this.CurrentSelected.Group.HasValue ? new Guid?(Guid.Empty) : this.CurrentSelected.Group;
  }

  protected virtual void GLBudgetTree_SubID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    if (!this.MatchMask(PXResultset<Sub>.op_Implicit(PXSelectBase<Sub, PXSelect<Sub, Where<Sub.subID, Equal<Required<Sub.subID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    })).SubCD, this.CurrentSelected.SubMaskWildcard ?? string.Empty))
      throw new PXSetPropertyException(string.Format(PXMessages.LocalizeNoPrefix("Selected subaccount is not allowed in this group or does not exist (Subaccount mask: {0})"), (object) this.CurrentSelected.SubMaskWildcard));
  }

  protected virtual bool MatchMask(string accountCD, string mask)
  {
    return this.MatchMask(accountCD, mask, true);
  }

  protected virtual bool MatchMask(string accountCD, string mask, bool replaceChars)
  {
    if (mask.Length > 0 && accountCD.Length > 0 && mask.Length > accountCD.Length)
      mask = mask.Substring(0, accountCD.Length);
    for (int index = 0; index < mask.Length; ++index)
    {
      if (replaceChars)
      {
        if (mask[index] == '?' && accountCD[index] != '?')
        {
          char[] charArray = mask.ToCharArray();
          charArray[index] = accountCD[index];
          mask = new string(charArray);
        }
        if (accountCD[index] == '?' && mask[index] != '?')
        {
          char[] charArray = accountCD.ToCharArray();
          charArray[index] = mask[index];
          accountCD = new string(charArray);
        }
      }
      if (index >= accountCD.Length || mask[index] != '?' && (int) mask[index] != (int) accountCD[index])
        return false;
    }
    return true;
  }

  private PXResultset<GLBudgetTree> collectChildNodes(Guid? GroupID)
  {
    PXResultset<GLBudgetTree> pxResultset = new PXResultset<GLBudgetTree>();
    foreach (PXResult<GLBudgetTree> pxResult in PXSelectBase<GLBudgetTree, PXSelect<GLBudgetTree, Where<GLBudgetTree.parentGroupID, Equal<Required<GLBudgetTree.parentGroupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) GroupID
    }))
    {
      pxResultset.Add(pxResult);
      pxResultset.AddRange((IEnumerable<PXResult<GLBudgetTree>>) this.collectChildNodes(PXResult<GLBudgetTree>.op_Implicit(pxResult).GroupID));
    }
    return pxResultset;
  }

  protected virtual void GLBudgetTree_SubID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is GLBudgetTree row))
      return;
    if (row.SubMask == null)
      row.SubMask = PXResultset<Sub>.op_Implicit(PXSelectBase<Sub, PXSelect<Sub, Where<Sub.subID, Equal<Required<Sub.subID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.SubID
      })).SubCD;
    if (((PXGraph) this).IsImport)
      return;
    row.IsGroup = new bool?(false);
  }

  protected virtual void GLBudgetTree_IsGroup_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    PXStringState stateExt1 = (PXStringState) sender.GetStateExt((object) null, typeof (GLBudgetTree.accountID).Name);
    PXDBStringAttribute.SetInputMask(sender, typeof (GLBudgetTree.accountMask).Name, stateExt1.InputMask.Replace('#', 'C'));
    PXStringState stateExt2 = (PXStringState) sender.GetStateExt((object) null, typeof (GLBudgetTree.subID).Name);
    PXDBStringAttribute.SetInputMask(sender, typeof (GLBudgetTree.subMask).Name, stateExt2.InputMask.Replace('A', 'C'));
  }

  protected virtual void GLBudgetTree_IsGroup_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    GLBudgetTree row = e.Row as GLBudgetTree;
    if (e.NewValue == null)
      return;
    bool? isGroup = row.IsGroup;
    if (!isGroup.HasValue)
      return;
    isGroup = row.IsGroup;
    if (!isGroup.Value || (bool) e.NewValue)
      return;
    if (((PXSelectBase<GLBudgetTree>) this.Details).Ask("Delete Group", "The child records will be deleted. Are you sure you want to delete the group?", (MessageButtons) 4) == 6)
    {
      this.deleteRecords(row.GroupID);
    }
    else
    {
      e.NewValue = (object) true;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void GLBudgetTree_AccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    GLBudgetTree row = e.Row as GLBudgetTree;
    object obj;
    sender.RaiseFieldDefaulting<GLBudgetTree.description>(e.Row, ref obj);
    if (obj != null)
      row.Description = obj.ToString();
    if (((PXGraph) this).IsImport)
      return;
    row.IsGroup = new bool?(false);
  }

  protected virtual void GLBudgetTree_AccountMask_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || e.Row == null)
      return;
    PXStringState stateExt = (PXStringState) sender.GetStateExt((object) null, typeof (GLBudgetTree.accountID).Name);
    e.NewValue = (object) ((string) e.NewValue).PadRight(stateExt.InputMask.Length - 1, '?').Replace(' ', '?');
  }

  protected virtual void GLBudgetTree_SubMask_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || e.Row == null)
      return;
    PXSegmentedState stateExt = (PXSegmentedState) sender.GetStateExt((object) null, typeof (GLBudgetTree.subID).Name);
    e.NewValue = (object) ((string) e.NewValue).PadRight(((IEnumerable<PXSegment>) stateExt.Segments).Sum<PXSegment>((Func<PXSegment, int>) (s => (int) s.Length)), '?').Replace(' ', '?');
  }
}
