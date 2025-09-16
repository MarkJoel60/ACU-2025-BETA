// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRActivityStatistics
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Activity Statistics")]
[Serializable]
public class CRActivityStatistics : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? NoteID { get; set; }

  [PXDBGuid(false)]
  [PXDBDefault(typeof (CRActivity.noteID))]
  public virtual Guid? LastIncomingActivityNoteID { get; set; }

  [PXDBGuid(false)]
  [PXDBDefault(typeof (CRActivity.noteID))]
  public virtual Guid? LastOutgoingActivityNoteID { get; set; }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, InputMask = "g")]
  [PXUIField(DisplayName = "Last Incoming Activity", Enabled = false)]
  public virtual DateTime? LastIncomingActivityDate { get; set; }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, InputMask = "g")]
  [PXUIField(DisplayName = "Last Outgoing Activity", Enabled = false)]
  public virtual DateTime? LastOutgoingActivityDate { get; set; }

  [PXDBGuid(false)]
  [PXDBDefault(typeof (CRActivity.noteID))]
  public virtual Guid? InitialOutgoingActivityCompletedAtNoteID { get; set; }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, InputMask = "g")]
  [PXUIField(DisplayName = "First Outgoing Activity", Enabled = false)]
  public virtual DateTime? InitialOutgoingActivityCompletedAtDate { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<CRActivityStatistics.lastIncomingActivityDate, IsNotNull, And<CRActivityStatistics.lastOutgoingActivityDate, IsNull>>, CRActivityStatistics.lastIncomingActivityDate, Case<Where<CRActivityStatistics.lastOutgoingActivityDate, IsNotNull, And<CRActivityStatistics.lastIncomingActivityDate, IsNull>>, CRActivityStatistics.lastOutgoingActivityDate, Case<Where<CRActivityStatistics.lastIncomingActivityDate, Greater<CRActivityStatistics.lastOutgoingActivityDate>>, CRActivityStatistics.lastIncomingActivityDate>>>, CRActivityStatistics.lastOutgoingActivityDate>), typeof (DateTime))]
  [PXUIField(DisplayName = "Last Activity", Enabled = false)]
  [PXDate]
  public virtual DateTime? LastActivityDate { get; set; }

  [PXUIField(DisplayName = "Last Activity Aging")]
  [PXInt]
  [PXIntList(typeof (CRActivityStatistics.LastActivityAgingEnum), new string[] {"None", "Last 30 Days", "30 - 60 Days", "60 - 90 Days", "Over 90 Days"})]
  [PXDBCalced(typeof (Switch<Case<Where<CRActivityStatistics.lastOutgoingActivityDate, IsNull>, CRActivityStatistics.LastActivityAgingConst.none, Case<Where<DateDiff<CRActivityStatistics.lastOutgoingActivityDate, CurrentValue<AccessInfo.businessDate>, DateDiff.day>, LessEqual<int30>>, CRActivityStatistics.LastActivityAgingConst.last30days, Case<Where<DateDiff<CRActivityStatistics.lastOutgoingActivityDate, CurrentValue<AccessInfo.businessDate>, DateDiff.day>, LessEqual<int60>>, CRActivityStatistics.LastActivityAgingConst.last3060days, Case<Where<DateDiff<CRActivityStatistics.lastOutgoingActivityDate, CurrentValue<AccessInfo.businessDate>, DateDiff.day>, LessEqual<int90>>, CRActivityStatistics.LastActivityAgingConst.last6090days>>>>, CRActivityStatistics.LastActivityAgingConst.over90days>), typeof (int?))]
  public int? LastActivityAging { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.CR.CRActivity.NoteID" /> value of the first incoming activity.
  /// </summary>
  [PXDBGuid(false)]
  [PXDBDefault(typeof (CRActivity.noteID))]
  public virtual Guid? InitialIncomingActivityNoteID { get; set; }

  /// <summary>The date and time of the first incoming activity.</summary>
  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, InputMask = "g")]
  public virtual DateTime? InitialIncomingActivityDate { get; set; }

  public class PK : PrimaryKeyOf<
  #nullable disable
  CRActivityStatistics>.By<CRActivityStatistics.noteID>
  {
    public static CRActivityStatistics Find(PXGraph graph, Guid? noteID, PKFindOptions options = 0)
    {
      return (CRActivityStatistics) PrimaryKeyOf<CRActivityStatistics>.By<CRActivityStatistics.noteID>.FindBy(graph, (object) noteID, options);
    }
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRActivityStatistics.noteID>
  {
  }

  public abstract class lastIncomingActivityNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRActivityStatistics.lastIncomingActivityNoteID>
  {
  }

  public abstract class lastOutgoingActivityNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRActivityStatistics.lastOutgoingActivityNoteID>
  {
  }

  public abstract class lastIncomingActivityDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRActivityStatistics.lastIncomingActivityDate>
  {
  }

  public abstract class lastOutgoingActivityDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRActivityStatistics.lastOutgoingActivityDate>
  {
  }

  public abstract class initialOutgoingActivityCompletedAtNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRActivityStatistics.initialOutgoingActivityCompletedAtNoteID>
  {
  }

  public abstract class initialOutgoingActivityCompletedAtDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRActivityStatistics.initialOutgoingActivityCompletedAtDate>
  {
  }

  public abstract class lastActivityDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRActivityStatistics.lastActivityDate>
  {
  }

  public enum LastActivityAgingEnum
  {
    None,
    Last30days,
    Last3060days,
    Last6090days,
    Over90days,
  }

  public class LastActivityAgingConst
  {
    public class none : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CRActivityStatistics.LastActivityAgingConst.none>
    {
      public none()
        : base(0)
      {
      }
    }

    public class last30days : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CRActivityStatistics.LastActivityAgingConst.last30days>
    {
      public last30days()
        : base(1)
      {
      }
    }

    public class last3060days : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CRActivityStatistics.LastActivityAgingConst.last3060days>
    {
      public last3060days()
        : base(2)
      {
      }
    }

    public class last6090days : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CRActivityStatistics.LastActivityAgingConst.last6090days>
    {
      public last6090days()
        : base(3)
      {
      }
    }

    public class over90days : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      CRActivityStatistics.LastActivityAgingConst.over90days>
    {
      public over90days()
        : base(4)
      {
      }
    }
  }

  public abstract class lastActivityAging : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRActivityStatistics.lastActivityAging>
  {
  }

  public abstract class initialIncomingActivityNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRActivityStatistics.initialIncomingActivityNoteID>
  {
  }

  public abstract class initialIncomingActivityDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRActivityStatistics.initialIncomingActivityDate>
  {
  }
}
