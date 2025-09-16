// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickingJob
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Attributes;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO;

[PXTable]
[PXCacheName]
public class SOPickingJob : WMSJob
{
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXDBDefault(typeof (SOPickingWorksheet.worksheetNbr))]
  [PXParent(typeof (SOPickingJob.FK.Worksheet))]
  public virtual 
  #nullable disable
  string WorksheetNbr { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (SOPicker.pickerNbr))]
  [PXUIField(DisplayName = "Picker Nbr.", Enabled = false)]
  [PXParent(typeof (SOPickingJob.FK.Picker))]
  public virtual int? PickerNbr { get; set; }

  [PXDBString(4, IsFixed = true, IsUnicode = false)]
  [PXDefault("PICK")]
  public override string JobType { get; set; }

  [PXDBString(3, IsFixed = true, IsUnicode = false)]
  [SOPickingJob.status.List]
  [PXDefault("HLD")]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public override string Status { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Assigned Picker")]
  [PXSelector(typeof (Search<Users.pKID, Where<BqlOperand<Users.isHidden, IBqlBool>.IsEqual<False>>>), SubstituteKey = typeof (Users.username))]
  [PXUIEnabled(typeof (BqlOperand<SOPickingJob.actualAssigneeID, IBqlGuid>.IsNull))]
  [PXForeignReference(typeof (SOPickingJob.FK.PreferredAssignee))]
  public override Guid? PreferredAssigneeID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Actual Picker", Enabled = false)]
  [PXSelector(typeof (Search<Users.pKID, Where<BqlOperand<Users.isHidden, IBqlBool>.IsEqual<False>>>), SubstituteKey = typeof (Users.username))]
  [PXForeignReference(typeof (SOPickingJob.FK.ActualAssignee))]
  public override Guid? ActualAssigneeID { get; set; }

  [DBConditionalModifiedDateTime(typeof (SOPickingJob.status), "PNG", KeepValue = true)]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<SOPickingJob.status, IBqlString>.IsIn<SOPickingJob.status.reenqueued, SOPickingJob.status.assigned>>.Else<SOPickingJob.pickingStartedAt>))]
  [PXUIField(DisplayName = "Picking Started", Enabled = false)]
  public virtual DateTime? PickingStartedAt { get; set; }

  [DBConditionalModifiedDateTime(typeof (SOPickingJob.status), "PED", KeepValue = true)]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<SOPickingJob.status, IBqlString>.IsEqual<SOPickingJob.status.picking>>.Else<SOPickingJob.pickedAt>))]
  [PXUIField(DisplayName = "Picking Finished", Enabled = false)]
  public virtual DateTime? PickedAt { get; set; }

  [PXString]
  [PXUIField]
  [PXFormula(typeof (IsNull<Parent<SOPickingWorksheet.singleShipmentNbr>, Parent<SOPicker.pickListNbr>>))]
  public virtual string PickListNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatic Shipment Confirmation")]
  [PXUIEnabled(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingJob.status, NotIn3<SOPickingJob.status.picked, SOPickingJob.status.completed, SOPickingJob.status.cancelled>>>>>.And<BqlOperand<Parent<SOPickingWorksheet.worksheetType>, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>>))]
  public virtual bool? AutomaticShipmentConfirmation { get; set; }

  public new class PK : PrimaryKeyOf<SOPickingJob>.By<SOPickingJob.jobID>
  {
    public static SOPickingJob Find(PXGraph graph, int? jobID, PKFindOptions options = 0)
    {
      return (SOPickingJob) PrimaryKeyOf<SOPickingJob>.By<SOPickingJob.jobID>.FindBy(graph, (object) jobID, options);
    }
  }

  public new static class FK
  {
    public class Job : 
      PrimaryKeyOf<WMSJob>.By<WMSJob.jobID>.ForeignKeyOf<SOPickingJob>.By<SOPickingJob.jobID>
    {
    }

    public class PreferredAssignee : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<SOPickingJob>.By<SOPickingJob.preferredAssigneeID>
    {
    }

    public class ActualAssignee : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<SOPickingJob>.By<SOPickingJob.actualAssigneeID>
    {
    }

    public class Worksheet : 
      PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.ForeignKeyOf<SOPickingJob>.By<SOPickingJob.worksheetNbr>
    {
    }

    public class Picker : 
      PrimaryKeyOf<SOPicker>.By<SOPicker.worksheetNbr, SOPicker.pickerNbr>.ForeignKeyOf<SOPickingJob>.By<SOPickingJob.worksheetNbr, SOPickingJob.pickerNbr>
    {
    }
  }

  public new abstract class jobID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingJob.jobID>
  {
  }

  public abstract class worksheetNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPickingJob.worksheetNbr>
  {
  }

  public abstract class pickerNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingJob.pickerNbr>
  {
  }

  public new abstract class jobType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPickingJob.jobType>
  {
    public const string Picking = "PICK";
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPickingJob.status>
  {
    public const string OnHold = "HLD";
    public const string Enqueued = "ENQ";
    public const string Assigned = "ASG";
    public const string Picking = "PNG";
    public const string Reenqueued = "RNQ";
    public const string Picked = "PED";
    public const string Completed = "CMP";
    public const string Cancelled = "CNL";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string OnHold = "On Hold";
      public const string Enqueued = "Added to Queue";
      public const string Assigned = "Assigned";
      public const string Picking = "Being Picked";
      public const string Reenqueued = "Returned to Queue";
      public const string Picked = "Picked";
      public const string Completed = "Completed";
      public const string Cancelled = "Cancelled";
    }

    public class onHold : WMSJob.status.onHold
    {
    }

    public class enqueued : WMSJob.status.enqueued
    {
    }

    public class assigned : WMSJob.status.assigned
    {
    }

    public class picking : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOPickingJob.status.picking>
    {
      public picking()
        : base("PNG")
      {
      }
    }

    public class reenqueued : WMSJob.status.reenqueued
    {
    }

    public class picked : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOPickingJob.status.picked>
    {
      public picked()
        : base("PED")
      {
      }
    }

    public class completed : WMSJob.status.completed
    {
    }

    public class cancelled : WMSJob.status.cancelled
    {
    }

    public class ListAttribute : WMSJob.status.ListAttribute
    {
      public ListAttribute()
        : base(SOPickingJob.status.ListAttribute.GetPairs().ToArray<Tuple<string, string>>())
      {
      }

      protected ListAttribute(params Tuple<string, string>[] valuesToLabels)
        : base(valuesToLabels)
      {
      }

      protected new static IEnumerable<Tuple<string, string>> GetPairs()
      {
        foreach (Tuple<string, string> pair in WMSJob.status.ListAttribute.GetPairs())
        {
          if (pair.Item1 == "RNQ")
          {
            yield return PXStringListAttribute.Pair("PNG", "Being Picked");
            yield return pair;
            yield return PXStringListAttribute.Pair("PED", "Picked");
          }
          else
            yield return pair;
        }
      }
    }
  }

  public new abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickingJob.priority>
  {
  }

  public new abstract class preferredAssigneeID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickingJob.preferredAssigneeID>
  {
  }

  public new abstract class actualAssigneeID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickingJob.actualAssigneeID>
  {
  }

  public new abstract class enqueuedAt : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingJob.enqueuedAt>
  {
  }

  public abstract class pickingStartedAt : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingJob.pickingStartedAt>
  {
  }

  public new abstract class reenqueuedAt : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingJob.reenqueuedAt>
  {
  }

  public abstract class pickedAt : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOPickingJob.pickedAt>
  {
  }

  public new abstract class completedAt : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickingJob.completedAt>
  {
  }

  public abstract class pickListNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOPickingJob.pickListNbr>
  {
  }

  public abstract class automaticShipmentConfirmation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickingJob.automaticShipmentConfirmation>
  {
  }
}
