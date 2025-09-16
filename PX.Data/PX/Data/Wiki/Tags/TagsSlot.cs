// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Tags.TagsSlot
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;

#nullable enable
namespace PX.Data.Wiki.Tags;

public class TagsSlot : IPrefetchable, IPXCompanyDependent
{
  protected ReaderWriterLock _lock = new ReaderWriterLock();
  protected Dictionary<Guid, string> _tagsByID = new Dictionary<Guid, string>();
  protected Dictionary<string, Guid> _tagsByCD = new Dictionary<string, Guid>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  protected Dictionary<Guid, Guid?> _tagParents = new Dictionary<Guid, Guid?>();
  protected Dictionary<Guid, HashSet<Guid>> _tagChildren = new Dictionary<Guid, HashSet<Guid>>();
  protected HashSet<string> _roles = new HashSet<string>();
  protected Dictionary<string, Dictionary<Guid, short>> _tagAccessForRoleMap = new Dictionary<string, Dictionary<Guid, short>>();
  protected static readonly ConcurrentDictionary<string, Dictionary<Guid, short>> _tagAccessForUserMap = new ConcurrentDictionary<string, Dictionary<Guid, short>>();

  public void Prefetch()
  {
    TagsSlot._tagAccessForUserMap.Clear();
    this._tagsByID = new Dictionary<Guid, string>();
    this._tagsByCD = new Dictionary<string, Guid>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._tagParents = new Dictionary<Guid, Guid?>();
    this._tagChildren = new Dictionary<Guid, HashSet<Guid>>();
    this._tagAccessForRoleMap = new Dictionary<string, Dictionary<Guid, short>>();
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Tag>((PXDataField) new PXDataField<Tag.tagID>(), (PXDataField) new PXDataField<Tag.tagCD>(), (PXDataField) new PXDataField<Tag.parentTagID>()))
    {
      Guid valueOrDefault = pxDataRecord.GetGuid(0).GetValueOrDefault();
      string key = pxDataRecord.GetString(1);
      Guid? guid = pxDataRecord.GetGuid(2);
      this._tagsByID[valueOrDefault] = key;
      this._tagsByCD[key] = valueOrDefault;
      this._tagParents[valueOrDefault] = guid;
    }
    this._tagParents[CommonTags.AllTagsID] = new Guid?();
    foreach (KeyValuePair<Guid, Guid?> tagParent in this._tagParents)
    {
      for (Guid? nullable = new Guid?(tagParent.Key); nullable.HasValue; nullable = this._tagParents[nullable.Value])
      {
        HashSet<Guid> guidSet;
        if (!this._tagChildren.TryGetValue(nullable.Value, out guidSet))
        {
          this._tagChildren[nullable.Value] = guidSet = new HashSet<Guid>();
          guidSet.Add(nullable.Value);
        }
        guidSet.Add(tagParent.Key);
      }
    }
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<RoleInTag>((PXDataField) new PXDataField<RoleInTag.tagID>(), (PXDataField) new PXDataField<RoleInTag.rolename>(), (PXDataField) new PXDataField<RoleInTag.accessRights>()))
    {
      Guid valueOrDefault1 = pxDataRecord.GetGuid(0).GetValueOrDefault();
      string key = pxDataRecord.GetString(1);
      short valueOrDefault2 = pxDataRecord.GetInt16(2).GetValueOrDefault();
      this._roles.Add(key);
      Dictionary<Guid, short> dictionary;
      if (!this._tagAccessForRoleMap.TryGetValue(key, out dictionary))
        this._tagAccessForRoleMap[key] = dictionary = new Dictionary<Guid, short>();
      dictionary[valueOrDefault1] = valueOrDefault2;
    }
  }

  public static void Clear()
  {
    PXDatabase.ResetSlot<TagsSlot>(nameof (TagsSlot), typeof (PX.SM.Roles), typeof (RoleInTag), typeof (Tag));
    TagsSlot._tagAccessForUserMap.Clear();
  }

  protected static TagsSlot GetTagsSlot()
  {
    return PXDatabase.GetSlot<TagsSlot>(nameof (TagsSlot), typeof (PX.SM.Roles), typeof (RoleInTag), typeof (Tag));
  }

  public static Dictionary<Guid, short> GetTagsAccessForUser(IPrincipal user)
  {
    return TagsSlot._tagAccessForUserMap.GetOrAdd(user.Identity.Name, (Func<string, Dictionary<Guid, short>>) (_ => TagsSlot.ReadTagsAccessForUser(user)));
  }

  public static bool HasAccessToTag(
    Guid? tagID,
    short requiredAccessLevel,
    IDictionary<Guid, short>? tagAccessMap = null)
  {
    if (tagAccessMap == null)
      tagAccessMap = (IDictionary<Guid, short>) TagsSlot.GetTagsAccessForUser(PXContext.PXIdentity.AuthUser);
    short accessLevel;
    return tagID.HasValue && tagAccessMap.TryGetValue(tagID.Value, out accessLevel) && TagAccessLevel.HasAccess(accessLevel, requiredAccessLevel);
  }

  public static bool CanViewTag(Guid? tagID, IDictionary<Guid, short>? tagAccessMap = null)
  {
    return TagsSlot.HasAccessToTag(tagID, (short) 1, tagAccessMap);
  }

  protected static Dictionary<Guid, short> ReadTagsAccessForUser(IPrincipal user)
  {
    TagsSlot tagsSlot = TagsSlot.GetTagsSlot();
    Dictionary<Guid, short> dictionary1 = new Dictionary<Guid, short>();
    foreach (string role in tagsSlot._roles)
    {
      Dictionary<Guid, short> dictionary2;
      if (user.IsInRole(role) && tagsSlot._tagAccessForRoleMap.TryGetValue(role, out dictionary2))
      {
        foreach (KeyValuePair<Guid, short> keyValuePair in dictionary2)
        {
          Guid guid;
          short num;
          EnumerableExtensions.Deconstruct<Guid, short>(keyValuePair, ref guid, ref num);
          Guid key = guid;
          short accessLevel1 = num;
          short accessLevel2;
          if (!dictionary1.TryGetValue(key, out accessLevel2))
            accessLevel2 = (short) 0;
          dictionary1[key] = TagAccessLevel.Combine(accessLevel1, accessLevel2);
        }
      }
    }
    return dictionary1;
  }

  public static HashSet<Guid> GetChildren(Guid tagID)
  {
    HashSet<Guid> guidSet;
    return !TagsSlot.GetTagsSlot()._tagChildren.TryGetValue(tagID, out guidSet) ? new HashSet<Guid>() : guidSet;
  }

  public static Dictionary<Guid, string> TagByID() => TagsSlot.GetTagsSlot()._tagsByID;

  public static Dictionary<string, Guid> TagByCD() => TagsSlot.GetTagsSlot()._tagsByCD;
}
