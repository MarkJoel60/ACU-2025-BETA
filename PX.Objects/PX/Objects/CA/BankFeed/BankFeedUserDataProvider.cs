// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.BankFeedUserDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA.BankFeed;

internal class BankFeedUserDataProvider
{
  public virtual string GetUserForOrganization(int organizationId)
  {
    string userForOrganization;
    BankFeedUserDataProvider.Definition.GetOrganizationToUserMapFromSlot().TryGetValue(organizationId, out userForOrganization);
    return userForOrganization;
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    private const string key = "BankFeedUserCollection";

    public IReadOnlyDictionary<int, string> OrganizationToUserMap { get; private set; }

    public void Prefetch()
    {
      this.OrganizationToUserMap = (IReadOnlyDictionary<int, string>) GraphHelper.RowCast<CABankFeedUser>((IEnumerable) PXDatabase.Select<CABankFeedUser>()).ToDictionary<CABankFeedUser, int, string>((Func<CABankFeedUser, int>) (i => i.OrganizationID.Value), (Func<CABankFeedUser, string>) (i => i.ExternalUserID));
    }

    public static IReadOnlyDictionary<int, string> GetOrganizationToUserMapFromSlot()
    {
      return PXDatabase.GetSlot<BankFeedUserDataProvider.Definition>("BankFeedUserCollection", new Type[1]
      {
        typeof (CABankFeedUser)
      }).OrganizationToUserMap;
    }
  }
}
