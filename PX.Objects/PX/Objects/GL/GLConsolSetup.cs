// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLConsolSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXCacheName("GL Consolidation Setup")]
[Serializable]
public class GLConsolSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _SetupID;
  protected bool? _IsActive;
  protected int? _BranchID;
  protected int? _LedgerId;
  protected 
  #nullable disable
  string _SegmentValue;
  protected string _Description;
  protected string _Login;
  protected string _Password;
  protected string _Url;
  protected string _SourceLedgerCD;
  protected string _SourceBranchCD;
  protected bool? _PasteFlag;
  protected string _LastPostPeriod;
  protected string _StartPeriod;
  protected string _EndPeriod;
  protected DateTime? _LastConsDate;
  protected bool? _BypassAccountSubValidation;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBIdentity(IsKey = true)]
  public virtual int? SetupID
  {
    get => this._SetupID;
    set => this._SetupID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Search<Branch.ledgerID, Where<Branch.branchID, Equal<Current<GLConsolSetup.branchID>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search2<Ledger.ledgerID, LeftJoin<Branch, On<Branch.ledgerID, Equal<Ledger.ledgerID>>>, Where<Ledger.balanceType, NotEqual<BudgetLedger>, And<Where<Ledger.balanceType, NotEqual<LedgerBalanceType.actual>, Or<Branch.branchID, Equal<Current<GLConsolSetup.branchID>>>>>>>), SubstituteKey = typeof (Ledger.ledgerCD))]
  public virtual int? LedgerId
  {
    get => this._LedgerId;
    set => this._LedgerId = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.SegmentValue.value, Where<PX.Objects.CS.SegmentValue.dimensionID, Equal<SubAccountAttribute.dimensionName>, And<PX.Objects.CS.SegmentValue.segmentID, Equal<Current<GLSetup.consolSegmentId>>>>>))]
  [PXDefault]
  public virtual string SegmentValue
  {
    get => this._SegmentValue;
    set => this._SegmentValue = value;
  }

  [PXDefault]
  [PXDBString(30, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXDefault]
  public virtual string Login
  {
    get => this._Login;
    set => this._Login = value;
  }

  [PXRSACryptString(IsUnicode = true)]
  [PXUIField]
  [PXDefault]
  public virtual string Password
  {
    get => this._Password;
    set => this._Password = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXDefault]
  public virtual string Url
  {
    get => this._Url;
    set => this._Url = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Source Ledger")]
  [PXSelector(typeof (Search<GLConsolLedger.ledgerCD, Where<GLConsolLedger.setupID, Equal<Optional<GLConsolSetup.setupID>>>>))]
  public virtual string SourceLedgerCD
  {
    get => this._SourceLedgerCD;
    set => this._SourceLedgerCD = value;
  }

  [PXDBString(30, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Source Company/Branch")]
  [PXSelector(typeof (Search<GLConsolBranch.branchCD, Where<GLConsolBranch.setupID, Equal<Optional<GLConsolSetup.setupID>>>, OrderBy<Asc<GLConsolBranch.description>>>), new Type[] {typeof (GLConsolBranch.displayName), typeof (GLConsolBranch.description), typeof (GLConsolBranch.ledgerCD)})]
  public virtual string SourceBranchCD
  {
    get => this._SourceBranchCD;
    set => this._SourceBranchCD = value;
  }

  [PXDBBool]
  [PXUIField]
  public virtual bool? PasteFlag
  {
    get => this._PasteFlag;
    set => this._PasteFlag = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField]
  public virtual string LastPostPeriod
  {
    get => this._LastPostPeriod;
    set => this._LastPostPeriod = value;
  }

  [FinPeriodSelector]
  [PXUIField(DisplayName = "Start Period")]
  public virtual string StartPeriod
  {
    get => this._StartPeriod;
    set => this._StartPeriod = value;
  }

  [FinPeriodSelector]
  [PXUIField(DisplayName = "End Period")]
  public virtual string EndPeriod
  {
    get => this._EndPeriod;
    set => this._EndPeriod = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? LastConsDate
  {
    get => this._LastConsDate;
    set => this._LastConsDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Bypass Account/Sub Validation")]
  public virtual bool? BypassAccountSubValidation
  {
    get => this._BypassAccountSubValidation;
    set => this._BypassAccountSubValidation = value;
  }

  /// <summary>
  /// Timeout for Http request to get data from subsidiaries
  /// </summary>
  [PXDBInt(MinValue = 0)]
  [PXDefault(18000)]
  public virtual int? HttpClientTimeout { get; set; }

  /// <summary>
  /// The maximum time limit allowed for the consolidation process before it is forced to stop
  /// </summary>
  [PXDBInt]
  [PXDefault(300)]
  [PXUIField(DisplayName = "Process Time Limit")]
  [GLConsolSetup.processTimeLimit.List]
  public int? ProcessTimeLimit { get; set; }

  public class PK : PrimaryKeyOf<GLConsolSetup>.By<GLConsolSetup.setupID>
  {
    public static GLConsolSetup Find(PXGraph graph, int? setupID, PKFindOptions options = 0)
    {
      return (GLConsolSetup) PrimaryKeyOf<GLConsolSetup>.By<GLConsolSetup.setupID>.FindBy(graph, (object) setupID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLConsolSetup>.By<GLConsolSetup.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLConsolSetup>.By<GLConsolSetup.ledgerId>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLConsolSetup.selected>
  {
  }

  public abstract class setupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolSetup.setupID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLConsolSetup.isActive>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolSetup.branchID>
  {
  }

  public abstract class ledgerId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolSetup.ledgerId>
  {
  }

  public abstract class segmentValue : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolSetup.segmentValue>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolSetup.description>
  {
  }

  public abstract class login : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolSetup.login>
  {
  }

  public abstract class password : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolSetup.password>
  {
  }

  public abstract class url : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolSetup.url>
  {
  }

  public abstract class sourceLedgerCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLConsolSetup.sourceLedgerCD>
  {
  }

  public abstract class sourceBranchCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLConsolSetup.sourceBranchCD>
  {
  }

  public abstract class pasteFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLConsolSetup.pasteFlag>
  {
  }

  public abstract class lastPostPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLConsolSetup.lastPostPeriod>
  {
  }

  public abstract class startPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolSetup.startPeriod>
  {
  }

  public abstract class endPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolSetup.endPeriod>
  {
  }

  public abstract class lastConsDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLConsolSetup.lastConsDate>
  {
  }

  public abstract class bypassAccountSubValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLConsolSetup.bypassAccountSubValidation>
  {
  }

  public abstract class httpClientTimeout : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLConsolSetup.httpClientTimeout>
  {
  }

  public abstract class processTimeLimit : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLConsolSetup.processTimeLimit>
  {
    public const int Min15 = 15;
    public const int Min30 = 30;
    public const int Min45 = 45;
    public const int Min60 = 60;
    public const int Min90 = 90;
    public const int Min120 = 120;
    public const int Min180 = 180;
    public const int Min240 = 240 /*0xF0*/;
    public const int Min300 = 300;
    public const int Min360 = 360;
    public const int Min420 = 420;
    public const int Min480 = 480;
    public const int Min540 = 540;
    public const int Min600 = 600;
    public const int Min660 = 660;
    public const int Min720 = 720;
    public const int Min780 = 780;
    public const int Min840 = 840;
    public const int Min900 = 900;
    public const int Min960 = 960;
    public const int Min1020 = 1020;
    public const int Min1080 = 1080;
    public const int Min1140 = 1140;
    public const int Min1200 = 1200;
    public const int Min1260 = 1260;
    public const int Min1320 = 1320;
    public const int Min1380 = 1380;
    public const int Min1440 = 1440;

    public class ListAttribute : PXIntListAttribute
    {
      public ListAttribute()
        : base(new int[28]
        {
          15,
          30,
          45,
          60,
          90,
          120,
          180,
          240 /*0xF0*/,
          300,
          360,
          420,
          480,
          540,
          600,
          660,
          720,
          780,
          840,
          900,
          960,
          1020,
          1080,
          1140,
          1200,
          1260,
          1320,
          1380,
          1440
        }, new string[28]
        {
          "15 min",
          "30 min",
          "45 min",
          "1 h",
          "1 h 30 min",
          "2 h",
          "3 h",
          "4 h",
          "5 h",
          "6 h",
          "7 h",
          "8 h",
          "9 h",
          "10 h",
          "11 h",
          "12 h",
          "13 h",
          "14 h",
          "15 h",
          "16 h",
          "17 h",
          "18 h",
          "19 h",
          "20 h",
          "21 h",
          "22 h",
          "23 h",
          "24 h"
        })
      {
      }
    }

    public class min15 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min15>
    {
      public min15()
        : base(15)
      {
      }
    }

    public class min30 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min30>
    {
      public min30()
        : base(30)
      {
      }
    }

    public class min45 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min45>
    {
      public min45()
        : base(45)
      {
      }
    }

    public class min60 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min60>
    {
      public min60()
        : base(60)
      {
      }
    }

    public class min90 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min90>
    {
      public min90()
        : base(90)
      {
      }
    }

    public class min120 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min120>
    {
      public min120()
        : base(120)
      {
      }
    }

    public class min180 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min180>
    {
      public min180()
        : base(180)
      {
      }
    }

    public class min240 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min240>
    {
      public min240()
        : base(240 /*0xF0*/)
      {
      }
    }

    public class min300 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min300>
    {
      public min300()
        : base(300)
      {
      }
    }

    public class min360 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min360>
    {
      public min360()
        : base(360)
      {
      }
    }

    public class min420 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min420>
    {
      public min420()
        : base(420)
      {
      }
    }

    public class min480 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min480>
    {
      public min480()
        : base(480)
      {
      }
    }

    public class min540 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min540>
    {
      public min540()
        : base(540)
      {
      }
    }

    public class min600 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min600>
    {
      public min600()
        : base(600)
      {
      }
    }

    public class min660 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min660>
    {
      public min660()
        : base(660)
      {
      }
    }

    public class min720 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min720>
    {
      public min720()
        : base(720)
      {
      }
    }

    public class min780 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min780>
    {
      public min780()
        : base(780)
      {
      }
    }

    public class min840 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min840>
    {
      public min840()
        : base(840)
      {
      }
    }

    public class min900 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min900>
    {
      public min900()
        : base(900)
      {
      }
    }

    public class min960 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min960>
    {
      public min960()
        : base(960)
      {
      }
    }

    public class min1020 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min1020>
    {
      public min1020()
        : base(1020)
      {
      }
    }

    public class min1080 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min1080>
    {
      public min1080()
        : base(1080)
      {
      }
    }

    public class min1140 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min1140>
    {
      public min1140()
        : base(1140)
      {
      }
    }

    public class min1200 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min1200>
    {
      public min1200()
        : base(1200)
      {
      }
    }

    public class min1260 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min1260>
    {
      public min1260()
        : base(1260)
      {
      }
    }

    public class min1320 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min1320>
    {
      public min1320()
        : base(1320)
      {
      }
    }

    public class min1380 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min1380>
    {
      public min1380()
        : base(1380)
      {
      }
    }

    public class min1440 : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    GLConsolSetup.processTimeLimit.min1440>
    {
      public min1440()
        : base(1440)
      {
      }
    }
  }
}
