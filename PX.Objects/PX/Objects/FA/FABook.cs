// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FABook
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXPrimaryGraph(typeof (BookMaint))]
[PXCacheName("FA Book")]
[Serializable]
public class FABook : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// An unbound service field, which indicates that the book is marked for processing.
  /// </summary>
  /// <value>
  /// If the value of the field is <c>true</c>, the book will be processed; otherwise, the book will not be processed.
  /// </value>
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  /// <summary>
  /// The identifier of the book.
  /// The identifier is used for foreign references; it can be negative for newly inserted records.
  /// </summary>
  /// <value>A unique integer number.</value>
  [PXDBIdentity]
  public virtual int? BookID { get; set; }

  /// <summary>
  /// A string identifier, which contains a key value. This field is also a selector for navigation.
  /// </summary>
  /// <value>The value can be entered only manually.</value>
  [PXDefault]
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC")]
  [PXUIField]
  public virtual 
  #nullable disable
  string BookCode { get; set; }

  /// <summary>The description of the book.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <summary>
  /// A flag that determines whether the book posts FA transaction data to the General Ledger module.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Posting Book")]
  public virtual bool? UpdateGL { get; set; }

  /// <summary>The type of the middle point of the period.</summary>
  /// <value>
  /// The field can have the values, which are described in <see cref="T:PX.Objects.FA.FABook.midMonthType.ListAttribute" />
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("F")]
  [PXUIField(DisplayName = "Mid-Period Type")]
  [FABook.midMonthType.List]
  public virtual string MidMonthType { get; set; }

  /// <summary>
  /// Value for <see cref="P:PX.Objects.FA.FABook.MidMonthType" />.
  /// </summary>
  [PXDBShort]
  [PXUIField(DisplayName = "Mid-Period Day")]
  public virtual short? MidMonthDay { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  public class PK : PrimaryKeyOf<FABook>.By<FABook.bookID>
  {
    public static FABook Find(PXGraph graph, int? bookID, PKFindOptions options = 0)
    {
      return (FABook) PrimaryKeyOf<FABook>.By<FABook.bookID>.FindBy(graph, (object) bookID, options);
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABook.selected>
  {
  }

  public abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FABook.bookID>
  {
    public class Marker(int value) : Constant<int>(value)
    {
    }

    public class Markers
    {
      public const int GLBook = 1;
      public const int GLOrAnyBook = 2;

      public class glBook : FABook.bookID.Marker
      {
        public glBook()
          : base(1)
        {
        }
      }

      public class glOrAnyBook : FABook.bookID.Marker
      {
        public glOrAnyBook()
          : base(2)
        {
        }
      }
    }
  }

  public abstract class bookCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABook.bookCode>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABook.description>
  {
  }

  public abstract class updateGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FABook.updateGL>
  {
  }

  public abstract class midMonthType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FABook.midMonthType>
  {
    public const string FixedDay = "F";
    public const string NumberOfDays = "N";
    public const string PeriodDaysHalve = "H";

    /// <summary>The type of month middle point.</summary>
    /// <value>
    /// Allowed values are:
    /// <list type="bullet">
    /// <item> <term><c>F</c></term> <description>Fixed Day. Mid-period is determined by the number of day in the month.</description> </item>
    /// <item> <term><c>N</c></term> <description>Number of Days. Mid-period is determined by the number of days that have passed since the beginning of the period.</description> </item>
    /// </list>
    /// </value>
    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "F", "N" }, new string[2]
        {
          "Fixed Day",
          "Number of Days"
        })
      {
      }
    }

    public class fixedDay : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FABook.midMonthType.fixedDay>
    {
      public fixedDay()
        : base("F")
      {
      }
    }

    public class numberOfDays : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FABook.midMonthType.numberOfDays>
    {
      public numberOfDays()
        : base("N")
      {
      }
    }

    public class periodDaysHalve : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FABook.midMonthType.periodDaysHalve>
    {
      public periodDaysHalve()
        : base("H")
      {
      }
    }
  }

  public abstract class midMonthDay : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FABook.midMonthDay>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FABook.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABook.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABook.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABook.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FABook.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FABook.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FABook.lastModifiedDateTime>
  {
  }
}
