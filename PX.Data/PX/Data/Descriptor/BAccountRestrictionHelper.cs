// Decompiled with JetBrains decompiler
// Type: PX.Data.Descriptor.BAccountRestrictionHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.Data.PushNotifications;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data.Descriptor;

internal class BAccountRestrictionHelper : IBAccountRestrictionHelper
{
  private readonly 
  #nullable disable
  PXGraph _graph;

  public BAccountRestrictionHelper(PXGraph graph)
  {
    ExceptionExtensions.ThrowOnNull<PXGraph>(graph, nameof (graph), (string) null);
    this._graph = graph;
  }

  public void Persist()
  {
    if (!this._graph.Views.Caches.Contains(typeof (BAccountRestrictionHelper.BAccount)))
      this._graph.Views.Caches.Add(typeof (BAccountRestrictionHelper.BAccount));
    PXCache cach1 = this._graph.Caches["Customer"];
    if (cach1 != null && cach1.BqlTable.FullName.Equals("PX.Objects.AR.Customer", StringComparison.Ordinal))
      cach1.RowPersisted += new PXRowPersisted(this.SyncBAccountGroupMask);
    PXCache cach2 = this._graph.Caches["Vendor"];
    if (cach2 != null && cach2.BqlTable.FullName.Equals("PX.Objects.AP.Vendor", StringComparison.Ordinal))
      cach2.RowPersisted += new PXRowPersisted(this.SyncBAccountGroupMask);
    PXCache pxCache;
    if (!this._graph.Caches.TryGetValue(typeof (Neighbour), out pxCache))
      return;
    pxCache.RowPersisted += new PXRowPersisted(this.SyncBAccountNeighbours);
  }

  private void SyncBAccountNeighbours(PXCache sender, PXRowPersistedEventArgs e)
  {
    Neighbour row1 = e.Row as Neighbour;
    if (e.TranStatus != PXTranStatus.Open)
      return;
    bool isUsersLeftEntityType = false;
    if (typeof (Users).FullName.Equals(row1.LeftEntityType, StringComparison.Ordinal))
      isUsersLeftEntityType = true;
    else if (!typeof (Users).FullName.Equals(row1.RightEntityType, StringComparison.Ordinal))
      return;
    PXSelect<Neighbour, Where<Neighbour.leftEntityType, Equal<Required<Neighbour.leftEntityType>>, And<Neighbour.rightEntityType, Equal<Required<Neighbour.rightEntityType>>>>> pxSelect = new PXSelect<Neighbour, Where<Neighbour.leftEntityType, Equal<Required<Neighbour.leftEntityType>>, And<Neighbour.rightEntityType, Equal<Required<Neighbour.rightEntityType>>>>>(this._graph);
    pxSelect.View.Clear();
    Neighbour customerNeighbour;
    Neighbour vendorNeighbour;
    switch (isUsersLeftEntityType ? row1.RightEntityType : row1.LeftEntityType)
    {
      case "PX.Objects.AR.Customer":
        customerNeighbour = row1;
        string[] strArray1;
        if (!isUsersLeftEntityType)
          strArray1 = new string[2]
          {
            "PX.Objects.AP.Vendor",
            typeof (Users).FullName
          };
        else
          strArray1 = new string[2]
          {
            typeof (Users).FullName,
            "PX.Objects.AP.Vendor"
          };
        object[] objArray1 = (object[]) strArray1;
        vendorNeighbour = pxSelect.SelectSingle(objArray1);
        break;
      case "PX.Objects.AP.Vendor":
        vendorNeighbour = row1;
        string[] strArray2;
        if (!isUsersLeftEntityType)
          strArray2 = new string[2]
          {
            "PX.Objects.AR.Customer",
            typeof (Users).FullName
          };
        else
          strArray2 = new string[2]
          {
            typeof (Users).FullName,
            "PX.Objects.AR.Customer"
          };
        object[] objArray2 = (object[]) strArray2;
        customerNeighbour = pxSelect.SelectSingle(objArray2);
        break;
      default:
        return;
    }
    Neighbour row2 = this.CaclulateBAccountNeighbour(customerNeighbour, vendorNeighbour, isUsersLeftEntityType);
    bool flag = true;
    object obj = pxSelect.Cache.Locate((object) row2);
    if (obj == null)
    {
      string[] strArray3;
      if (!isUsersLeftEntityType)
        strArray3 = new string[2]
        {
          "PX.Objects.CR.BAccount",
          typeof (Users).FullName
        };
      else
        strArray3 = new string[2]
        {
          typeof (Users).FullName,
          "PX.Objects.CR.BAccount"
        };
      string[] strArray4 = strArray3;
      obj = (object) pxSelect.SelectSingle((object[]) strArray4);
      flag = false;
    }
    if (flag)
      pxSelect.Cache.Remove(obj);
    if (obj == null)
      pxSelect.Cache.PersistInserted((object) row2);
    else
      pxSelect.Cache.PersistUpdated((object) row2);
  }

  private Neighbour CaclulateBAccountNeighbour(
    Neighbour customerNeighbour,
    Neighbour vendorNeighbour,
    bool isUsersLeftEntityType)
  {
    string str1 = isUsersLeftEntityType ? typeof (Users).FullName : "PX.Objects.CR.BAccount";
    string str2 = isUsersLeftEntityType ? "PX.Objects.CR.BAccount" : typeof (Users).FullName;
    Neighbour destination = new Neighbour()
    {
      LeftEntityType = str1,
      RightEntityType = str2
    };
    if (customerNeighbour == null)
    {
      this.CopyAllMasks(vendorNeighbour, destination);
      return destination;
    }
    if (vendorNeighbour == null)
    {
      this.CopyAllMasks(customerNeighbour, destination);
      return destination;
    }
    destination.CoverageMask = this.GetBitwiseOr(customerNeighbour.CoverageMask, vendorNeighbour.CoverageMask);
    destination.InverseMask = this.GetBitwiseOr(customerNeighbour.InverseMask, vendorNeighbour.InverseMask);
    destination.WinCoverageMask = this.GetBitwiseOr(customerNeighbour.WinCoverageMask, vendorNeighbour.WinCoverageMask);
    destination.WinInverseMask = this.GetBitwiseOr(customerNeighbour.WinInverseMask, vendorNeighbour.WinInverseMask);
    return destination;
  }

  private byte[] GetBitwiseOr(byte[] a, byte[] b)
  {
    byte[] destinationArray = new byte[System.Math.Max(a.Length, b.Length)];
    Array.Copy((Array) a, (Array) destinationArray, a.Length);
    for (int index = 0; index < b.Length; ++index)
      destinationArray[index] |= b[index];
    return destinationArray;
  }

  private void CopyAllMasks(Neighbour source, Neighbour destination)
  {
    destination.CoverageMask = source.CoverageMask;
    destination.InverseMask = source.InverseMask;
    destination.WinCoverageMask = source.WinCoverageMask;
    destination.WinInverseMask = source.WinInverseMask;
  }

  private void SyncBAccountGroupMask(PXCache sender, PXRowPersistedEventArgs e)
  {
    string fullName = sender.BqlTable.FullName;
    object row1 = e.Row;
    if (e.TranStatus != PXTranStatus.Open || !(sender.GetValue(row1, "GroupMask") is byte[] mask) || !(sender.GetValue(row1, typeof (BAccountRestrictionHelper.BAccount.bAccountID).Name) is int bAccountID))
      return;
    (byte[] numArray1, byte[] numArray2) = this.GetChildMasks(fullName, new int?(bAccountID), mask);
    BAccountRestrictionHelper.BAccount row2 = BAccountRestrictionHelper.BAccount.PK.Find(this._graph, bAccountID);
    byte[] bitwiseOr = this.GetBitwiseOr(numArray1, numArray2);
    if (row2 == null || ((IEnumerable<byte>) row2.GroupMask).SequenceEqual<byte>((IEnumerable<byte>) bitwiseOr))
      return;
    row2.GroupMask = bitwiseOr;
    using (new SuppressPushNotificationsScope())
      this._graph.Caches[typeof (BAccountRestrictionHelper.BAccount)].PersistUpdated((object) row2);
  }

  private (byte[] CustomerMask, byte[] VendorMask) GetChildMasks(
    string entityTypeFullName,
    int? key,
    byte[] mask)
  {
    byte[] numArray1 = new byte[0];
    byte[] numArray2 = new byte[0];
    switch (entityTypeFullName)
    {
      case "PX.Objects.AR.Customer":
        BAccountRestrictionHelper.Vendor data1 = BAccountRestrictionHelper.Vendor.PK.Find(this._graph, new int?(key.Value));
        if (data1 != null)
          numArray2 = this._graph.Caches[typeof (BAccountRestrictionHelper.Vendor)].GetValue((object) data1, "GroupMask") as byte[];
        numArray1 = mask;
        break;
      case "PX.Objects.AP.Vendor":
        BAccountRestrictionHelper.Customer data2 = BAccountRestrictionHelper.Customer.PK.Find(this._graph, new int?(key.Value));
        if (data2 != null)
          numArray1 = this._graph.Caches[typeof (BAccountRestrictionHelper.Customer)].GetValue((object) data2, "GroupMask") as byte[];
        numArray2 = mask;
        break;
    }
    return (numArray1, numArray2);
  }

  [PXHidden]
  public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public const string TypeFullName = "PX.Objects.CR.BAccount";

    [PXDBIdentity]
    public virtual int? BAccountID { get; set; }

    [PXDBGroupMask]
    public virtual byte[] GroupMask { get; set; }

    public class PK : 
      PrimaryKeyOf<BAccountRestrictionHelper.BAccount>.By<BAccountRestrictionHelper.BAccount.bAccountID>
    {
      public static BAccountRestrictionHelper.BAccount Find(
        PXGraph graph,
        int bAccountID,
        PKFindOptions options = PKFindOptions.None)
      {
        return PrimaryKeyOf<BAccountRestrictionHelper.BAccount>.By<BAccountRestrictionHelper.BAccount.bAccountID>.FindBy(graph, (object) bAccountID, options);
      }
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BAccountRestrictionHelper.BAccount.bAccountID>
    {
    }

    public abstract class groupMask : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      BAccountRestrictionHelper.BAccount.groupMask>
    {
    }
  }

  [PXHidden]
  public class Customer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public const string TypeFullName = "PX.Objects.AR.Customer";

    [PXDBIdentity]
    public virtual int? BAccountID { get; set; }

    [PXDBGroupMask]
    public virtual byte[] GroupMask { get; set; }

    public class PK : 
      PrimaryKeyOf<BAccountRestrictionHelper.Customer>.By<BAccountRestrictionHelper.Customer.bAccountID>.Dirty
    {
      public static BAccountRestrictionHelper.Customer Find(
        PXGraph graph,
        int? bAccountID,
        PKFindOptions options = PKFindOptions.None)
      {
        return PrimaryKeyOf<BAccountRestrictionHelper.Customer>.By<BAccountRestrictionHelper.Customer.bAccountID>.Dirty.FindBy(graph, (object) bAccountID, options);
      }
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BAccountRestrictionHelper.Customer.bAccountID>
    {
    }

    public abstract class groupMask : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      BAccountRestrictionHelper.Customer.groupMask>
    {
    }
  }

  [PXHidden]
  public class Vendor : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public const string TypeFullName = "PX.Objects.AP.Vendor";

    [PXDBIdentity]
    public virtual int? BAccountID { get; set; }

    [PXDBGroupMask]
    public virtual byte[] GroupMask { get; set; }

    public class PK : 
      PrimaryKeyOf<BAccountRestrictionHelper.Vendor>.By<BAccountRestrictionHelper.Vendor.bAccountID>.Dirty
    {
      public static BAccountRestrictionHelper.Vendor Find(
        PXGraph graph,
        int? bAccountID,
        PKFindOptions options = PKFindOptions.None)
      {
        return PrimaryKeyOf<BAccountRestrictionHelper.Vendor>.By<BAccountRestrictionHelper.Vendor.bAccountID>.Dirty.FindBy(graph, (object) bAccountID, options);
      }
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BAccountRestrictionHelper.Vendor.bAccountID>
    {
    }

    public abstract class groupMask : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      BAccountRestrictionHelper.Vendor.groupMask>
    {
    }
  }
}
