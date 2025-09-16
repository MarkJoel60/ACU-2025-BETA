// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.ShipmentAuditSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO.WMS;

public class ShipmentAuditSupport : 
  BarcodeDrivenStateMachine<
  #nullable disable
  PickPackShip, PickPackShip.Host>.ScanExtension
{
  public PXSelect<ShipmentAuditSupport.SOShipmentUpdate> updShipment;

  public virtual Type[] GetChildTypes()
  {
    return new Type[3]
    {
      typeof (SOShipLineSplit),
      typeof (SOShipLine),
      typeof (SOPackageDetailEx)
    };
  }

  /// Overrides <see cref="!:SOShipmentEntry.Persist" />
  [PXOverride]
  public void Persist(Action base_Persist)
  {
    ShipmentAuditSupport.SOShipmentUpdate soShipmentUpdate1 = (ShipmentAuditSupport.SOShipmentUpdate) null;
    if (!((PXSelectBase) this.Graph.Document).Cache.IsInsertedUpdatedDeleted)
    {
      SOShipment modifiedShipment = this.FindModifiedShipment();
      if (modifiedShipment != null)
      {
        PXSelect<ShipmentAuditSupport.SOShipmentUpdate> updShipment = this.updShipment;
        ShipmentAuditSupport.SOShipmentUpdate soShipmentUpdate2 = new ShipmentAuditSupport.SOShipmentUpdate();
        soShipmentUpdate2.ShipmentType = modifiedShipment.ShipmentType;
        soShipmentUpdate2.ShipmentNbr = modifiedShipment.ShipmentNbr;
        soShipmentUpdate1 = soShipmentUpdate2;
        ((PXSelectBase<ShipmentAuditSupport.SOShipmentUpdate>) updShipment).Insert(soShipmentUpdate2);
      }
    }
    try
    {
      base_Persist();
    }
    catch
    {
      ((PXSelectBase) this.updShipment).Cache.Clear();
      throw;
    }
    if (soShipmentUpdate1 == null)
      return;
    ((PXSelectBase) this.Graph.Document).Cache.Clear();
    ((PXSelectBase) this.Graph.Document).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase<SOShipment>) this.Graph.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) this.Graph.Document).Search<SOShipment.shipmentNbr>((object) soShipmentUpdate1.ShipmentNbr, Array.Empty<object>()));
  }

  public virtual SOShipment FindModifiedShipment()
  {
    Type[] childTypes = this.GetChildTypes();
    // ISSUE: method pointer
    return childTypes == null || childTypes.Length == 0 ? (SOShipment) null : ((IEnumerable<Type>) childTypes).Select<Type, SOShipment>(new Func<Type, SOShipment>((object) this, __methodptr(\u003CFindModifiedShipment\u003Eg__findModifiedParent\u007C3_1))).FirstOrDefault<SOShipment>((Func<SOShipment, bool>) (s => s != null));
  }

  [PXHidden]
  [ShipmentAuditSupport.SOShipmentUpdate.Accumulator(BqlTable = typeof (SOShipment), SingleRecord = true)]
  public class SOShipmentUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(1, IsKey = true)]
    public virtual string ShipmentType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    public virtual string ShipmentNbr { get; set; }

    [PXDBLastModifiedByID]
    public virtual Guid? LastModifiedByID { get; set; }

    [PXDBLastModifiedByScreenID]
    public virtual string LastModifiedByScreenID { get; set; }

    [PXDBLastModifiedDateTime]
    public virtual DateTime? LastModifiedDateTime { get; set; }

    public abstract class shipmentType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ShipmentAuditSupport.SOShipmentUpdate.shipmentType>
    {
    }

    public abstract class shipmentNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ShipmentAuditSupport.SOShipmentUpdate.shipmentNbr>
    {
    }

    public abstract class lastModifiedByID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ShipmentAuditSupport.SOShipmentUpdate.lastModifiedByID>
    {
    }

    public abstract class lastModifiedByScreenID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ShipmentAuditSupport.SOShipmentUpdate.lastModifiedByScreenID>
    {
    }

    public abstract class lastModifiedDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ShipmentAuditSupport.SOShipmentUpdate.lastModifiedDateTime>
    {
    }

    public class AccumulatorAttribute : PXAccumulatorAttribute
    {
      protected virtual bool PrepareInsert(
        PXCache sender,
        object row,
        PXAccumulatorCollection columns)
      {
        if (!base.PrepareInsert(sender, row, columns))
          return false;
        ShipmentAuditSupport.SOShipmentUpdate soShipmentUpdate = (ShipmentAuditSupport.SOShipmentUpdate) row;
        columns.UpdateOnly = true;
        columns.Update<ShipmentAuditSupport.SOShipmentUpdate.lastModifiedByID>((object) soShipmentUpdate.LastModifiedByID, (PXDataFieldAssign.AssignBehavior) 0);
        columns.Update<ShipmentAuditSupport.SOShipmentUpdate.lastModifiedByScreenID>((object) soShipmentUpdate.LastModifiedByScreenID, (PXDataFieldAssign.AssignBehavior) 0);
        columns.Update<ShipmentAuditSupport.SOShipmentUpdate.lastModifiedDateTime>((object) soShipmentUpdate.LastModifiedDateTime, (PXDataFieldAssign.AssignBehavior) 0);
        return true;
      }
    }
  }
}
