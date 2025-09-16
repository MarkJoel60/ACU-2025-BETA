// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSWeekCodeDate
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Globalization;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Contracts/Routes calendar Week Code")]
[PXPrimaryGraph(typeof (CalendarWeekCodeMaint))]
[Serializable]
public class FSWeekCodeDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBDate(IsKey = true)]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? WeekCodeDate { get; set; }

  [PXDBString(4, IsUnicode = false, InputMask = ">CCCC")]
  [PXDefault]
  [PXUIField]
  public virtual 
  #nullable disable
  string WeekCode { get; set; }

  [PXDBString(1, IsUnicode = false, InputMask = ">C")]
  [PXUIField]
  public virtual string WeekCodeP1 { get; set; }

  [PXDBString(1, IsUnicode = false, InputMask = ">C")]
  [PXUIField]
  public virtual string WeekCodeP2 { get; set; }

  [PXDBString(1, IsUnicode = false, InputMask = ">C")]
  [PXUIField]
  public virtual string WeekCodeP3 { get; set; }

  [PXDBString(1, IsUnicode = false, InputMask = ">C")]
  [PXUIField]
  public virtual string WeekCodeP4 { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Start Date of Week")]
  public virtual DateTime? BeginDateOfWeek { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "End Date of Week")]
  public virtual DateTime? EndDateOfWeek { get; set; }

  [PXDBInt]
  [ListField_WeekDaysNumber.ListAtrribute]
  [PXUIField(DisplayName = "Day of Week")]
  public virtual int? DayOfWeek { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Day of Week")]
  public virtual string Mem_DayOfWeek
  {
    get
    {
      if (!this.WeekCodeDate.HasValue)
        return (string) null;
      DateTime dateTime = this.WeekCodeDate.Value;
      return SharedFunctions.getDayOfWeekByID((int) this.WeekCodeDate.Value.DayOfWeek).ToString();
    }
  }

  [PXInt]
  [PXUIField(DisplayName = "Week of Year")]
  public virtual int? Mem_WeekOfYear
  {
    get
    {
      if (!this.WeekCodeDate.HasValue)
        return new int?();
      DateTime dateTime = this.WeekCodeDate.Value;
      DateTime time = this.WeekCodeDate.Value;
      switch (CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time))
      {
        case System.DayOfWeek.Monday:
        case System.DayOfWeek.Tuesday:
        case System.DayOfWeek.Wednesday:
          time = time.AddDays(3.0);
          break;
      }
      return new int?(CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday));
    }
  }

  public class PK : PrimaryKeyOf<FSWeekCodeDate>.By<FSWeekCodeDate.weekCodeDate>
  {
    public static FSWeekCodeDate Find(PXGraph graph, DateTime? weekCodeDate, PKFindOptions options = 0)
    {
      return (FSWeekCodeDate) PrimaryKeyOf<FSWeekCodeDate>.By<FSWeekCodeDate.weekCodeDate>.FindBy(graph, (object) weekCodeDate, options);
    }
  }

  public abstract class weekCodeDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWeekCodeDate.weekCodeDate>
  {
  }

  public abstract class weekCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWeekCodeDate.weekCode>
  {
  }

  public abstract class weekCodeP1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWeekCodeDate.weekCodeP1>
  {
  }

  public abstract class weekCodeP2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWeekCodeDate.weekCodeP2>
  {
  }

  public abstract class weekCodeP3 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWeekCodeDate.weekCodeP3>
  {
  }

  public abstract class weekCodeP4 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWeekCodeDate.weekCodeP4>
  {
  }

  public abstract class beginDateOfWeek : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWeekCodeDate.beginDateOfWeek>
  {
  }

  public abstract class endDateOfWeek : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWeekCodeDate.endDateOfWeek>
  {
  }

  public abstract class dayOfWeek : ListField_WeekDaysNumber
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSWeekCodeDate.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWeekCodeDate.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWeekCodeDate.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSWeekCodeDate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWeekCodeDate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWeekCodeDate.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSWeekCodeDate.Tstamp>
  {
  }

  public abstract class mem_DayOfWeek : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWeekCodeDate.mem_DayOfWeek>
  {
  }

  public abstract class mem_WeekOfYear : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWeekCodeDate.mem_WeekOfYear>
  {
  }
}
