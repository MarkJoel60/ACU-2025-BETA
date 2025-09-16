// Decompiled with JetBrains decompiler
// Type: PX.SM.SMAccess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.SM;

public class SMAccess : BaseAccess
{
  public PXSelectOrderBy<EMailAccount, OrderBy<Desc<EMailAccount.included, Asc<EMailAccount.description>>>> Account;

  protected IEnumerable account()
  {
    SMAccess smAccess = this;
    if (smAccess.Group.Current != null && !string.IsNullOrEmpty(smAccess.Group.Current.GroupName))
    {
      SMAccess graph = smAccess;
      object[] objArray = new object[1]
      {
        (object) new byte[0]
      };
      foreach (PXResult<EMailAccount> pxResult in PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where2<Match<Current<RelationGroup.groupName>>, Or<Match<Required<EMailAccount.groupMask>>>>>.Config>.Select((PXGraph) graph, objArray))
      {
        EMailAccount emailAccount = (EMailAccount) pxResult;
        if (!emailAccount.UserID.HasValue)
        {
          smAccess.Account.Current = emailAccount;
          yield return (object) emailAccount;
        }
      }
    }
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Email Account")]
  protected virtual void _(
    Events.CacheAttached<EMailAccount.emailAccountID> e)
  {
  }

  protected void RelationGroup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is RelationGroup row))
      return;
    if (string.IsNullOrEmpty(row.GroupName))
    {
      this.Save.SetEnabled(false);
      this.Account.Cache.AllowInsert = false;
    }
    else
    {
      this.Save.SetEnabled(true);
      this.Account.Cache.AllowInsert = true;
    }
  }

  protected virtual void _(
    Events.FieldVerifying<EMailAccount.emailAccountID> e)
  {
    if (PXSelectorAttribute.Select<EMailAccount.emailAccountID>(e.Cache, e.Row, e.NewValue) == null)
      throw new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) "[emailAccountID]"
      });
  }

  protected void EMailAccount_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (PXSelectorAttribute.Select<EMailAccount.emailAccountID>(sender, e.Row) is EMailAccount copy)
    {
      copy.Included = new bool?(true);
      PXCache<EMailAccount>.RestoreCopy((EMailAccount) e.Row, copy);
      sender.SetStatus(e.Row, PXEntryStatus.Updated);
    }
    else
      sender.Delete(e.Row);
  }

  protected void EMailAccount_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EMailAccount row) || sender.GetStatus((object) row) != PXEntryStatus.Notchanged)
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

  protected void EMailAccount_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    EMailAccount row = e.Row as EMailAccount;
    RelationGroup current = this.Group.Current;
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
        bool flag = true;
        row.GroupMask[index] = !(included.GetValueOrDefault() == flag & included.HasValue) ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  public override void Persist()
  {
    this.populateNeighbours<PX.SM.Users>((PXSelectBase<PX.SM.Users>) this.Users);
    this.populateNeighbours<EMailAccount>((PXSelectBase<EMailAccount>) this.Account);
    base.Persist();
    PXSelectorAttribute.ClearGlobalCache<PX.SM.Users>();
  }
}
