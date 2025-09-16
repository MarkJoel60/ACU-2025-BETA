// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMSJob
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Extensions;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
public class WMSJob : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPXSelectable
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? JobID { get; set; }

  [PXDBString(4, IsFixed = true, IsUnicode = false)]
  [PXDefault]
  public virtual 
  #nullable disable
  string JobType { get; set; }

  [PXDBString(3, IsFixed = true, IsUnicode = false)]
  [WMSJob.status.List]
  [PXDefault("HLD")]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string Status { get; set; }

  [PXDBInt]
  [WMSJob.priority.List]
  [PXDefault(2)]
  [PXUIField(DisplayName = "Priority")]
  public virtual int? Priority { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Preferred Assignee")]
  [PXSelector(typeof (Search<Users.pKID, Where<BqlOperand<Users.isHidden, IBqlBool>.IsEqual<False>>>), SubstituteKey = typeof (Users.username))]
  [PXUIEnabled(typeof (BqlOperand<WMSJob.actualAssigneeID, IBqlGuid>.IsNull))]
  [PXForeignReference(typeof (WMSJob.FK.PreferredAssignee))]
  public virtual Guid? PreferredAssigneeID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Actual Assignee", Enabled = false)]
  [PXSelector(typeof (Search<Users.pKID, Where<BqlOperand<Users.isHidden, IBqlBool>.IsEqual<False>>>), SubstituteKey = typeof (Users.username))]
  [PXForeignReference(typeof (WMSJob.FK.ActualAssignee))]
  public virtual Guid? ActualAssigneeID { get; set; }

  [DBConditionalModifiedDateTime(typeof (WMSJob.status), "HLD", InvertLogic = true)]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<WMSJob.status, IBqlString>.IsEqual<WMSJob.status.onHold>>.Else<WMSJob.enqueuedAt>))]
  [PXUIField(DisplayName = "Added to Queue at", Enabled = false)]
  public virtual DateTime? EnqueuedAt { get; set; }

  [DBConditionalModifiedDateTime(typeof (WMSJob.status), "RNQ", KeepValue = true)]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<WMSJob.status, IBqlString>.IsIn<WMSJob.status.onHold, WMSJob.status.assigned>>.Else<WMSJob.reenqueuedAt>))]
  [PXUIField(DisplayName = "Returned to Queue at", Enabled = false)]
  public virtual DateTime? ReenqueuedAt { get; set; }

  [DBConditionalModifiedDateTime(typeof (WMSJob.status), "CMP")]
  [PXUIField(DisplayName = "Completed at", Enabled = false)]
  public virtual DateTime? CompletedAt { get; set; }

  [PXInt]
  [PXDBCalced(typeof (BqlOperand<WMSJob.lastModifiedDateTime, IBqlDateTime>.Diff<Now>.Minutes), typeof (int), Persistent = false)]
  public virtual int? MinutesSinceLastModification { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  public class PK : PrimaryKeyOf<WMSJob>.By<WMSJob.jobID>
  {
    public static WMSJob Find(PXGraph graph, int? jobID, PKFindOptions options = 0)
    {
      return (WMSJob) PrimaryKeyOf<WMSJob>.By<WMSJob.jobID>.FindBy(graph, (object) jobID, options);
    }
  }

  public static class FK
  {
    public class PreferredAssignee : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<WMSJob>.By<WMSJob.preferredAssigneeID>
    {
    }

    public class ActualAssignee : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<WMSJob>.By<WMSJob.actualAssigneeID>
    {
    }
  }

  public abstract class jobID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WMSJob.jobID>
  {
  }

  public abstract class jobType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WMSJob.jobType>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WMSJob.status>
  {
    public const string OnHold = "HLD";
    public const string Enqueued = "ENQ";
    public const string Assigned = "ASG";
    public const string Reenqueued = "RNQ";
    public const string Completed = "CMP";
    public const string Cancelled = "CNL";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string OnHold = "On Hold";
      public const string Enqueued = "Added to Queue";
      public const string Assigned = "Assigned";
      public const string Reenqueued = "Returned to Queue";
      public const string Completed = "Completed";
      public const string Cancelled = "Cancelled";
    }

    public class onHold : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WMSJob.status.onHold>
    {
      public onHold()
        : base("HLD")
      {
      }
    }

    public class enqueued : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WMSJob.status.enqueued>
    {
      public enqueued()
        : base("ENQ")
      {
      }
    }

    public class assigned : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WMSJob.status.assigned>
    {
      public assigned()
        : base("ASG")
      {
      }
    }

    public class reenqueued : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WMSJob.status.reenqueued>
    {
      public reenqueued()
        : base("RNQ")
      {
      }
    }

    public class completed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WMSJob.status.completed>
    {
      public completed()
        : base("CMP")
      {
      }
    }

    public class cancelled : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WMSJob.status.cancelled>
    {
      public cancelled()
        : base("CNL")
      {
      }
    }

    public class ListAttribute : PXStringListAttribute, IPXRowUpdatedSubscriber
    {
      public ListAttribute()
        : base(WMSJob.status.ListAttribute.GetPairs().ToArray<Tuple<string, string>>())
      {
      }

      protected ListAttribute(params Tuple<string, string>[] valuesToLabels)
        : base(valuesToLabels)
      {
      }

      protected static IEnumerable<Tuple<string, string>> GetPairs()
      {
        yield return PXStringListAttribute.Pair("HLD", "On Hold");
        yield return PXStringListAttribute.Pair("ENQ", "Added to Queue");
        yield return PXStringListAttribute.Pair("ASG", "Assigned");
        yield return PXStringListAttribute.Pair("RNQ", "Returned to Queue");
        yield return PXStringListAttribute.Pair("CMP", "Completed");
        yield return PXStringListAttribute.Pair("CNL", "Cancelled");
      }

      void IPXRowUpdatedSubscriber.RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
      {
        if (cache.ObjectsEqual<WMSJob.status, WMSJob.preferredAssigneeID>(e.OldRow, e.Row) || !(e.Row is WMSJob row))
          return;
        if (row.PreferredAssigneeID.HasValue && EnumerableExtensions.IsIn<string>(row.Status, "ENQ", "RNQ"))
        {
          cache.LiteUpdate<WMSJob>(row, (Action<ValueSetter<WMSJob>>) (set => set.Set<string>((Expression<Func<WMSJob, string>>) (j => j.Status), "ASG")));
        }
        else
        {
          if (row.PreferredAssigneeID.HasValue || !(row.Status == "ASG"))
            return;
          cache.LiteUpdate<WMSJob>(row, (Action<ValueSetter<WMSJob>>) (set => set.Set<string>((Expression<Func<WMSJob, string>>) (j => j.Status), "RNQ")));
        }
      }
    }
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WMSJob.priority>
  {
    public const int Low = 1;
    public const int Medium = 2;
    public const int High = 3;
    public const int Urgent = 4;

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string Low = "Low";
      public const string Medium = "Medium";
      public const string High = "High";
      public const string Urgent = "Urgent";
    }

    public class ListAttribute : PXIntListAttribute
    {
      public ListAttribute()
        : base(new Tuple<int, string>[4]
        {
          PXIntListAttribute.Pair(4, "Urgent"),
          PXIntListAttribute.Pair(3, "High"),
          PXIntListAttribute.Pair(2, "Medium"),
          PXIntListAttribute.Pair(1, "Low")
        })
      {
      }
    }

    public class low : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    WMSJob.priority.low>
    {
      public low()
        : base(1)
      {
      }
    }

    public class medium : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    WMSJob.priority.medium>
    {
      public medium()
        : base(2)
      {
      }
    }

    public class high : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    WMSJob.priority.high>
    {
      public high()
        : base(3)
      {
      }
    }

    public class urgent : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    WMSJob.priority.urgent>
    {
      public urgent()
        : base(4)
      {
      }
    }
  }

  public abstract class preferredAssigneeID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WMSJob.preferredAssigneeID>
  {
  }

  public abstract class actualAssigneeID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WMSJob.actualAssigneeID>
  {
  }

  public abstract class enqueuedAt : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  WMSJob.enqueuedAt>
  {
  }

  public abstract class reenqueuedAt : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  WMSJob.reenqueuedAt>
  {
  }

  public abstract class completedAt : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  WMSJob.completedAt>
  {
  }

  public abstract class minutesSinceLastModification : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WMSJob.minutesSinceLastModification>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WMSJob.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WMSJob.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WMSJob.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WMSJob.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WMSJob.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WMSJob.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WMSJob.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  WMSJob.Tstamp>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WMSJob.selected>
  {
  }
}
