// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorSMEquipmentRefNbrAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorSMEquipmentRefNbrAttribute : PXSelectorAttribute
{
  public FSSelectorSMEquipmentRefNbrAttribute()
    : base(typeof (Search2<FSEquipment.refNbr, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSEquipment.customerID>>>, Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, OrderBy<Asc<FSEquipment.refNbr>>>), SelectorBase_Equipment.selectorColumns)
  {
  }
}
