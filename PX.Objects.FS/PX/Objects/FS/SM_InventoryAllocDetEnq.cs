// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_InventoryAllocDetEnq
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

public class SM_InventoryAllocDetEnq : PXGraphExtension<
#nullable disable
InventoryAllocDetEnq>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXOverride]
  public virtual InventoryAllocDetEnq.ItemPlanWithExtraInfo[] UnwrapAndGroup(
    PXResultset<InventoryAllocDetEnq.INItemPlan> records,
    SM_InventoryAllocDetEnq.UnwrapAndGroupDelegate baseMethod)
  {
    InventoryAllocDetEnq.ItemPlanWithExtraInfo[] planWithExtraInfoArray = baseMethod(records);
    foreach (InventoryAllocDetEnq.ItemPlanWithExtraInfo planWithExtraInfo in planWithExtraInfoArray)
    {
      if (planWithExtraInfo.RefEntity == null && planWithExtraInfo.ItemPlan.RefEntityType == typeof (PX.Objects.FS.FSServiceOrder).FullName)
      {
        PX.Objects.FS.FSServiceOrder fsServiceOrder = PXResultset<PX.Objects.FS.FSServiceOrder>.op_Implicit(PXSelectBase<PX.Objects.FS.FSServiceOrder, PXSelect<PX.Objects.FS.FSServiceOrder, Where<PX.Objects.FS.FSServiceOrder.noteID, Equal<Required<PX.Objects.FS.FSServiceOrder.noteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) planWithExtraInfo.ItemPlan.RefNoteID
        }));
        if (fsServiceOrder != null)
          planWithExtraInfo.RefEntity = (object) new SM_InventoryAllocDetEnq.FSServiceOrder()
          {
            SrvOrdType = fsServiceOrder.SrvOrdType,
            RefNbr = fsServiceOrder.RefNbr,
            NoteID = fsServiceOrder.NoteID
          };
      }
    }
    return planWithExtraInfoArray;
  }

  [PXHidden]
  public class FSServiceOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(4, IsKey = true, IsFixed = true, InputMask = ">AAAA")]
    public virtual string SrvOrdType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    public virtual string RefNbr { get; set; }

    [PXDBGuid(false, IsImmutable = true)]
    public virtual Guid? NoteID { get; set; }

    public abstract class srvOrdType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SM_InventoryAllocDetEnq.FSServiceOrder.srvOrdType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SM_InventoryAllocDetEnq.FSServiceOrder.refNbr>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      SM_InventoryAllocDetEnq.FSServiceOrder.noteID>
    {
    }
  }

  public delegate InventoryAllocDetEnq.ItemPlanWithExtraInfo[] UnwrapAndGroupDelegate(
    PXResultset<InventoryAllocDetEnq.INItemPlan> records);
}
