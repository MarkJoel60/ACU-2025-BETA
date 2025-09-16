// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.CRShippingAddress2Attribute
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
using System.Linq;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class CRShippingAddress2Attribute(System.Type SelectType) : CRShippingAddressAttribute(SelectType)
{
  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultAddress<CRShippingAddress, CRShippingAddress.addressID>(sender, DocumentRow, Row);
  }

  public override void DefaultAddress<TAddress, TAddressID>(
    PXCache sender,
    object DocumentRow,
    object AddressRow)
  {
    int num1 = -1;
    int num2 = 0;
    bool flag = false;
    int? projectID = (int?) sender.GetValue(DocumentRow, "ProjectID");
    if (projectID.HasValue)
    {
      int? nullable1 = projectID;
      int num3 = 0;
      if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
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
            PMAddress pmAddress = (PMAddress) sender.Graph.TypedViews.GetView(instance1, false).Select((object[]) null, objArray1, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2).FirstOrDefault<object>();
            object[] objArray2 = new object[1]
            {
              (object) pmProject.SiteAddressID
            };
            BqlCommand instance2 = BqlCommand.CreateInstance(new System.Type[1]
            {
              typeof (SelectFromBase<PMAddress, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMAddress.addressID, IBqlInt>.IsEqual<P.AsInt>>)
            });
            PMAddress source = (PMAddress) sender.Graph.TypedViews.GetView(instance2, false).Select((object[]) null, objArray2, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2).FirstOrDefault<object>();
            if (source != null)
            {
              nullable2 = source.IsDefaultAddress;
              if (!nullable2.GetValueOrDefault())
              {
                Address address = PropertyTransfer.Transfer<PMAddress, Address>(source, new Address());
                address.AddressID = source.AddressID;
                address.IsValidated = source.IsValidated;
                address.BAccountID = pmAddress.BAccountID;
                flag = AddressAttribute.DefaultAddress<TAddress, TAddressID>(sender, this.FieldName, DocumentRow, AddressRow, (object) new PXResult<Address, CRShippingAddress>(address, new CRShippingAddress()));
              }
            }
            if (flag || this._Required)
              return;
            this.ClearRecord(sender, DocumentRow);
            return;
          }
        }
        base.DefaultAddress<TAddress, TAddressID>(sender, DocumentRow, AddressRow);
        return;
      }
    }
    base.DefaultAddress<TAddress, TAddressID>(sender, DocumentRow, AddressRow);
  }
}
