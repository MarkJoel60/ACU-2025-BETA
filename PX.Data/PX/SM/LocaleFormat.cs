// Decompiled with JetBrains decompiler
// Type: PX.SM.LocaleFormat
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.SM;

/// <exclude />
[PXCacheName("Custom Locale Format")]
public class LocaleFormat : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [PXDBIdentity(IsKey = true)]
  public virtual int? FormatID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Format")]
  [PXCultureSelector]
  public virtual 
  #nullable disable
  string TemplateLocale { get; set; }

  [PXDBString(1, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Decimal Symbol")]
  [PXNumberSeparatorList]
  public virtual string NumberDecimalSeporator { get; set; }

  [PXDBString(1, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Digit Grouping Symbol")]
  [PXNumberSeparatorList]
  public virtual string NumberGroupSeparator { get; set; }

  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Date Time")]
  [PXStandartDateTimeFormatSelector('f')]
  public virtual string DateTimePattern { get; set; }

  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Short Time")]
  [PXStandartDateTimeFormatSelector('t')]
  public virtual string TimeShortPattern { get; set; }

  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Long Time")]
  [PXStandartDateTimeFormatSelector('T')]
  public virtual string TimeLongPattern { get; set; }

  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Short Date")]
  [PXStandartDateTimeFormatSelector('d')]
  public virtual string DateShortPattern { get; set; }

  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Long Date")]
  [PXStandartDateTimeFormatSelector('D')]
  public virtual string DateLongPattern { get; set; }

  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "AM Symbol")]
  public virtual string AMDesignator { get; set; }

  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "PM Symbol")]
  public virtual string PMDesignator { get; set; }

  public class PK : PrimaryKeyOf<LocaleFormat>.By<LocaleFormat.formatID>
  {
    public static LocaleFormat Find(PXGraph graph, int? formatID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<LocaleFormat>.By<LocaleFormat.formatID>.FindBy(graph, (object) formatID, options);
    }
  }

  public static class FK
  {
    public class Locale : 
      PrimaryKeyOf<Locale>.By<Locale.localeName>.ForeignKeyOf<LocaleFormat>.By<LocaleFormat.templateLocale>
    {
    }

    public class DateTimePattern : 
      PrimaryKeyOf<StandartDateTimeFormat>.By<StandartDateTimeFormat.pattern>.ForeignKeyOf<LocaleFormat>.By<LocaleFormat.dateTimePattern>
    {
    }

    public class TimeShortPattern : 
      PrimaryKeyOf<StandartDateTimeFormat>.By<StandartDateTimeFormat.pattern>.ForeignKeyOf<LocaleFormat>.By<LocaleFormat.timeShortPattern>
    {
    }

    public class TimeLongPattern : 
      PrimaryKeyOf<StandartDateTimeFormat>.By<StandartDateTimeFormat.pattern>.ForeignKeyOf<LocaleFormat>.By<LocaleFormat.timeLongPattern>
    {
    }

    public class DateShortPattern : 
      PrimaryKeyOf<StandartDateTimeFormat>.By<StandartDateTimeFormat.pattern>.ForeignKeyOf<LocaleFormat>.By<LocaleFormat.dateShortPattern>
    {
    }

    public class DateLongPattern : 
      PrimaryKeyOf<StandartDateTimeFormat>.By<StandartDateTimeFormat.pattern>.ForeignKeyOf<LocaleFormat>.By<LocaleFormat.dateLongPattern>
    {
    }
  }

  /// <exclude />
  public abstract class formatID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocaleFormat.formatID>
  {
  }

  /// <exclude />
  public abstract class templateLocale : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocaleFormat.templateLocale>
  {
  }

  /// <exclude />
  public abstract class numberDecimalSeporator : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocaleFormat.numberDecimalSeporator>
  {
  }

  /// <exclude />
  public abstract class numberGroupSeparator : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocaleFormat.numberGroupSeparator>
  {
  }

  /// <exclude />
  public abstract class dateTimePattern : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocaleFormat.dateTimePattern>
  {
  }

  /// <exclude />
  public abstract class timeShortPattern : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocaleFormat.timeShortPattern>
  {
  }

  /// <exclude />
  public abstract class timeLongPattern : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocaleFormat.timeLongPattern>
  {
  }

  /// <exclude />
  public abstract class dateShortPattern : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocaleFormat.dateShortPattern>
  {
  }

  /// <exclude />
  public abstract class dateLongPattern : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocaleFormat.dateLongPattern>
  {
  }

  /// <exclude />
  public abstract class aMDesignator : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocaleFormat.aMDesignator>
  {
  }

  /// <exclude />
  public abstract class pMDesignator : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocaleFormat.pMDesignator>
  {
  }
}
