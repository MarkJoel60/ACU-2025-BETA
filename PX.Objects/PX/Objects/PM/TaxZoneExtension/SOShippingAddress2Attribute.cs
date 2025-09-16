// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.SOShippingAddress2Attribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.SO;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class SOShippingAddress2Attribute(System.Type SelectType) : SOShippingAddressAttribute(SelectType)
{
  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultAddress<SOShippingAddress, SOShippingAddress.addressID>(sender, DocumentRow, Row);
  }

  public override void DefaultAddress<TAddress, TAddressID>(
    PXCache sender,
    object DocumentRow,
    object AddressRow)
  {
    if ((string) sender.GetValue<SOOrder.behavior>(DocumentRow) == "TR")
    {
      base.DefaultAddress<TAddress, TAddressID>(sender, DocumentRow, AddressRow);
    }
    else
    {
      int startRow = -1;
      int totalRows = 0;
      bool addressFound = false;
      int? projectID = (int?) sender.GetValue<SOOrder.projectID>(DocumentRow);
      if (projectID.HasValue)
      {
        int? nullable1 = projectID;
        int num = 0;
        if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
        {
          PMProject pmProject = PMProject.PK.Find(sender.Graph, projectID);
          if (pmProject != null)
          {
            bool? nullable2 = pmProject.NonProject;
            if (!nullable2.GetValueOrDefault())
            {
              object[] objArray1 = new object[1]
              {
                (object) pmProject.BillAddressID
              };
              BqlCommand instance1 = BqlCommand.CreateInstance(new System.Type[1]
              {
                typeof (SelectFromBase<PMAddress, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMAddress.addressID, IBqlInt>.IsEqual<P.AsInt>>)
              });
              PMAddress pmAddress = (PMAddress) sender.Graph.TypedViews.GetView(instance1, false).Select((object[]) null, objArray1, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 1, ref totalRows).FirstOrDefault<object>();
              object[] objArray2 = new object[1]
              {
                (object) pmProject.SiteAddressID
              };
              BqlCommand instance2 = BqlCommand.CreateInstance(new System.Type[1]
              {
                typeof (SelectFromBase<PMAddress, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMAddress.addressID, IBqlInt>.IsEqual<P.AsInt>>)
              });
              PMAddress source = (PMAddress) sender.Graph.TypedViews.GetView(instance2, false).Select((object[]) null, objArray2, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 1, ref totalRows).FirstOrDefault<object>();
              if (source != null)
              {
                nullable2 = source.IsDefaultAddress;
                if (!nullable2.GetValueOrDefault())
                {
                  Address address = PropertyTransfer.Transfer<PMAddress, Address>(source, new Address());
                  address.AddressID = source.AddressID;
                  address.IsValidated = source.IsValidated;
                  address.BAccountID = (int?) pmAddress?.BAccountID;
                  addressFound = AddressAttribute.DefaultAddress<TAddress, TAddressID>(sender, this.FieldName, DocumentRow, AddressRow, (object) new PXResult<Address, SOShippingAddress>(address, new SOShippingAddress()));
                }
              }
              if (addressFound || this._Required)
                return;
              this.ClearRecord(sender, DocumentRow);
              return;
            }
          }
          this.DefaultAddress<TAddress, TAddressID>(sender, DocumentRow, AddressRow, ref startRow, ref totalRows, ref addressFound);
          return;
        }
      }
      this.DefaultAddress<TAddress, TAddressID>(sender, DocumentRow, AddressRow, ref startRow, ref totalRows, ref addressFound);
    }
  }

  private void DefaultAddress<TAddress, TAddressID>(
    PXCache sender,
    object DocumentRow,
    object AddressRow,
    ref int startRow,
    ref int totalRows,
    ref bool addressFound)
    where TAddress : class, IBqlTable, IAddress, new()
    where TAddressID : IBqlField
  {
    PXView view = sender.Graph.TypedViews.GetView(this._Select, false);
    if (!view.Select(new object[1]{ DocumentRow }, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 1, ref totalRows).Any<object>())
      return;
    using (List<object>.Enumerator enumerator = view.Select(new object[1]
    {
      DocumentRow
    }, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 1, ref totalRows).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      PXResult current = (PXResult) enumerator.Current;
      addressFound = AddressAttribute.DefaultAddress<TAddress, TAddressID>(sender, this.FieldName, DocumentRow, AddressRow, (object) current);
    }
  }
}
