// Decompiled with JetBrains decompiler
// Type: PX.Data.UserGroupLazyCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.QueryObjectModel;
using PX.TM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class UserGroupLazyCache : IPrefetchable, IPXCompanyDependent
{
  private static readonly System.Type[] _slotSubscriptionTables = new System.Type[2]
  {
    typeof (EPCompanyTree),
    typeof (EPCompanyTreeMember)
  };
  private readonly Dictionary<Guid, HashSet<int>> _groupIds = new Dictionary<Guid, HashSet<int>>();
  private readonly Dictionary<Guid, HashSet<string>> _groupDescriptions = new Dictionary<Guid, HashSet<string>>();
  private readonly Dictionary<Guid, HashSet<int>> _workTreeIds = new Dictionary<Guid, HashSet<int>>();
  private readonly Dictionary<Guid, HashSet<string>> _workTreeDescriptions = new Dictionary<Guid, HashSet<string>>();
  private const string SLOT_KEY = "UserGroupLazyCache";

  public static UserGroupLazyCache Current
  {
    get
    {
      return PXDatabase.GetSlot<UserGroupLazyCache>(nameof (UserGroupLazyCache), UserGroupLazyCache._slotSubscriptionTables);
    }
  }

  public void Prefetch()
  {
    this._groupIds.Clear();
    this._groupDescriptions.Clear();
    this._workTreeIds.Clear();
    this._workTreeDescriptions.Clear();
  }

  public HashSet<int> GetUserGroupIds(Guid userId)
  {
    this.LoadGroupInfoLazy(userId);
    return this._groupIds[userId];
  }

  public HashSet<string> GetUserGroupDescriptions(Guid userId)
  {
    this.LoadGroupInfoLazy(userId);
    return this._groupDescriptions[userId];
  }

  public HashSet<int> GetUserWorkTreeIds(Guid userId)
  {
    this.LoadWorkTreeInfoLazy(userId);
    return this._workTreeIds[userId];
  }

  public HashSet<string> GetUserWorkTreeDescriptions(Guid userId)
  {
    this.LoadWorkTreeInfoLazy(userId);
    return this._workTreeDescriptions[userId];
  }

  private void LoadWorkTreeInfoLazy(Guid userId)
  {
    if (this._workTreeIds.ContainsKey(userId))
      return;
    this.LoadGroupInfoLazy(userId);
    this._workTreeIds[userId] = new HashSet<int>((IEnumerable<int>) this._groupIds[userId]);
    this._workTreeDescriptions[userId] = new HashSet<string>((IEnumerable<string>) this._groupDescriptions[userId]);
    Queue<int> intQueue = new Queue<int>((IEnumerable<int>) this._groupIds[userId]);
    while (intQueue.Count > 0)
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<EPCompanyTree>((PXDataField) new PXDataField<EPCompanyTree.workGroupID>(), (PXDataField) new PXDataField<EPCompanyTree.description>(), (PXDataField) new PXDataFieldValue<EPCompanyTree.parentWGID>((object) intQueue.Dequeue())))
      {
        int num = pxDataRecord.GetInt32(0).Value;
        this._workTreeIds[userId].Add(num);
        this._workTreeDescriptions[userId].Add(pxDataRecord.GetString(1));
        intQueue.Enqueue(num);
      }
    }
  }

  private void LoadGroupInfoLazy(Guid userId)
  {
    if (this._groupIds.ContainsKey(userId))
      return;
    this._groupIds[userId] = new HashSet<int>();
    this._groupDescriptions[userId] = new HashSet<string>();
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<EPCompanyTreeMember>(Yaql.join<EPCompanyTree>(Yaql.eq<EPCompanyTree.workGroupID, EPCompanyTreeMember.workGroupID>("<declaring_type_name>", "<declaring_type_name>"), (YaqlJoinType) 0), (PXDataField) new PXDataFieldValue<EPCompanyTreeMember.contactID>((object) PXAccess.GetContactID()), (PXDataField) new PXDataField<EPCompanyTreeMember.workGroupID>(typeof (EPCompanyTreeMember).Name), (PXDataField) new PXDataField<EPCompanyTree.description>(typeof (EPCompanyTree).Name)))
    {
      this._groupIds[userId].Add(pxDataRecord.GetInt32(0).Value);
      this._groupDescriptions[userId].Add(pxDataRecord.GetString(1));
    }
  }
}
