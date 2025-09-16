// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.FSSrvOrdAddress2Attribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.FS;
using System.Linq;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class FSSrvOrdAddress2Attribute(System.Type SelectType) : FSSrvOrdAddressAttribute(SelectType)
{
  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultAddress<FSAddress, FSAddress.addressID>(sender, DocumentRow, Row);
  }

  public override void DefaultAddress<TAddress, TAddressID>(
    PXCache sender,
    object DocumentRow,
    object AddressRow)
  {
    int num1 = -1;
    int num2 = 0;
    bool flag = false;
    int? projectID = (int?) sender.GetValue<FSServiceOrder.projectID>(DocumentRow);
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
                PX.Objects.CR.Address address = PropertyTransfer.Transfer<PMAddress, PX.Objects.CR.Address>(source, new PX.Objects.CR.Address());
                address.AddressID = source.AddressID;
                address.IsValidated = source.IsValidated;
                address.BAccountID = pmAddress.BAccountID;
                flag = AddressAttribute.DefaultAddress<TAddress, TAddressID>(sender, this.FieldName, DocumentRow, AddressRow, (object) new PXResult<PX.Objects.CR.Address, FSAddress>(address, new FSAddress()));
              }
            }
            if (flag || this._Required)
              return;
            this.ClearRecord(sender, DocumentRow);
            return;
          }
        }
        base.DefaultAddress<FSAddress, FSAddress.addressID>(sender, DocumentRow, AddressRow);
        return;
      }
    }
    base.DefaultAddress<FSAddress, FSAddress.addressID>(sender, DocumentRow, AddressRow);
  }
}
