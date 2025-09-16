// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSODetAlias
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXHidden]
public class FSSODetAlias : FSSODet
{
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    FSSODetAlias>.By<FSSODetAlias.srvOrdType, FSSODetAlias.refNbr, FSSODetAlias.lineNbr>
  {
    public static FSSODetAlias Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSSODetAlias) PrimaryKeyOf<FSSODetAlias>.By<FSSODetAlias.srvOrdType, FSSODetAlias.refNbr, FSSODetAlias.lineNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public new class UK : PrimaryKeyOf<FSSODetAlias>.By<FSSODetAlias.sODetID>
  {
    public static FSSODetAlias Find(PXGraph graph, int? sODetID, PKFindOptions options = 0)
    {
      return (FSSODetAlias) PrimaryKeyOf<FSSODetAlias>.By<FSSODetAlias.sODetID>.FindBy(graph, (object) sODetID, options);
    }
  }

  public new abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetAlias.srvOrdType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetAlias.refNbr>
  {
  }

  public new abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetAlias.sOID>
  {
  }

  public new abstract class sODetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetAlias.sODetID>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSODetAlias.lineNbr>
  {
  }

  public new abstract class lineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSODetAlias.lineRef>
  {
  }

  public new abstract class status : FSSODetAlias.ListField_Status_SODetAlias
  {
  }

  public abstract class ListField_Status_SODetAlias : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSODetAlias.status>
  {
    public class ListAtrribute : PXStringListAttribute
    {
      public ListAtrribute()
        : base(new Tuple<string, string>[4]
        {
          PXStringListAttribute.Pair("SN", "Requiring Scheduling"),
          PXStringListAttribute.Pair("SC", "Scheduled"),
          PXStringListAttribute.Pair("CC", "Canceled"),
          PXStringListAttribute.Pair("CP", "Completed")
        })
      {
      }
    }

    public class ScheduleNeeded : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSSODetAlias.ListField_Status_SODetAlias.ScheduleNeeded>
    {
      public ScheduleNeeded()
        : base("SN")
      {
      }
    }

    public class Scheduled : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSSODetAlias.ListField_Status_SODetAlias.Scheduled>
    {
      public Scheduled()
        : base("SC")
      {
      }
    }

    public class Completed : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSSODetAlias.ListField_Status_SODetAlias.Completed>
    {
      public Completed()
        : base("CP")
      {
      }
    }

    public class Canceled : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSSODetAlias.ListField_Status_SODetAlias.Canceled>
    {
      public Canceled()
        : base("CC")
      {
      }
    }
  }
}
