// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BatchModule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.FA;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PR.Standalone;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable enable
namespace PX.Objects.GL;

public static class BatchModule
{
  public const 
  #nullable disable
  string GL = "GL";
  public const string TA = "TA";
  public const string EA = "EA";
  public const string AP = "AP";
  public const string AR = "AR";
  public const string CA = "CA";
  public const string CM = "CM";
  public const string IN = "IN";
  public const string SO = "SO";
  public const string PO = "PO";
  public const string DR = "DR";
  public const string FA = "FA";
  public const string EP = "EP";
  public const string PM = "PM";
  public const string TX = "TX";
  public const string CR = "CR";
  public const string WZ = "WZ";
  public const string FS = "FS";
  public const string PR = "PR";
  public const string AM = "AM";

  /// <summary>
  /// Returns the localized display name of the specified module.
  /// The list of display names is taken from the <see cref="T:PX.Objects.GL.Messages" /> class.
  /// </summary>
  /// <example>
  /// <code>
  /// BatchModule.GetDisplayName("GL"); // returns "General Ledger"
  /// </code>
  /// </example>
  public static string GetDisplayName(string module)
  {
    if (string.IsNullOrWhiteSpace(module))
      throw new ArgumentException("Module cannot be null or whitespace", nameof (module));
    Type type = typeof (Messages);
    string name = "ModuleName" + module.ToUpperInvariant();
    FieldInfo field = type.GetField(name);
    if (field == (FieldInfo) null || !field.IsLiteral || field.FieldType != typeof (string))
      throw new NotSupportedException($"Module '{module}' doesn't have a corresponding display name.");
    return PXLocalizer.Localize((string) field.GetRawConstantValue(), type.FullName);
  }

  /// <summary>
  /// Returns the localized display name of the specified module.
  /// The list of display names is taken from the <see cref="T:PX.Objects.GL.Messages" /> class.
  /// </summary>
  /// <example>
  /// <code>
  /// BatchModule.GetDisplayName&lt;BatchModule.moduleGL&gt;(); // returns "General Ledger"
  /// </code>
  /// </example>
  public static string GetDisplayName<TModule>() where TModule : IConstant<string>, IBqlOperand, new()
  {
    return BatchModule.GetDisplayName(new TModule().Value);
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[10]
      {
        "GL",
        "AP",
        "AR",
        "CM",
        "CA",
        "IN",
        "DR",
        "FA",
        "PM",
        "PR"
      }, new string[10]
      {
        "GL",
        "AP",
        "AR",
        "CM",
        "CA",
        "IN",
        "DR",
        "FA",
        "PM",
        "PR"
      })
    {
    }
  }

  public class FullListAttribute : PXStringListAttribute
  {
    public FullListAttribute()
      : base(new string[14]
      {
        "GL",
        "AP",
        "AR",
        "CM",
        "CA",
        "IN",
        "DR",
        "FA",
        "PM",
        "TX",
        "SO",
        "PO",
        "EP",
        "PR"
      }, new string[14]
      {
        "GL",
        "AP",
        "AR",
        "CM",
        "CA",
        "IN",
        "DR",
        "FA",
        "PM",
        "TX",
        "SO",
        "PO",
        "EP",
        "PR"
      })
    {
    }
  }

  /// <summary>
  /// Specilaized for GL.BatchModule version of the <see cref="T:PX.Objects.CS.AutoNumberAttribute" /><br />
  /// It defines how the new numbers are generated for the GL Batch. <br />
  /// References Batch.module and Batch.dateEntered fields of the document,<br />
  /// and also define a link between  numbering ID's defined in GLSetup (namely GLSetup.batchNumberingID)<br />
  /// and CADeposit: <br />
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    private static string[] _Modules
    {
      get
      {
        return new string[10]
        {
          "GL",
          "AP",
          "AR",
          "CM",
          "CA",
          "IN",
          "DR",
          "FA",
          "PM",
          "PR"
        };
      }
    }

    private static Type[] _SetupFields
    {
      get
      {
        return new Type[10]
        {
          typeof (GLSetup.batchNumberingID),
          typeof (Search<APSetup.batchNumberingID>),
          typeof (Search<ARSetup.batchNumberingID>),
          typeof (Search<CMSetup.batchNumberingID>),
          typeof (Search<CASetup.batchNumberingID>),
          typeof (Search<INSetup.batchNumberingID>),
          typeof (GLSetup.batchNumberingID),
          typeof (Search<FASetup.batchNumberingID>),
          typeof (Search<PMSetup.batchNumberingID>),
          typeof (Search<PRSetup.batchNumberingID>)
        };
      }
    }

    public static Type GetNumberingIDField(string module)
    {
      foreach (Tuple<string, Type> tuple in EnumerableExtensions.Zip<string, Type>((IEnumerable<string>) BatchModule.NumberingAttribute._Modules, (IEnumerable<Type>) BatchModule.NumberingAttribute._SetupFields))
      {
        if (tuple.Item1 == module)
          return tuple.Item2;
      }
      return (Type) null;
    }

    public NumberingAttribute()
      : base(typeof (Batch.module), typeof (Batch.dateEntered), BatchModule.NumberingAttribute._Modules, BatchModule.NumberingAttribute._SetupFields)
    {
    }
  }

  public class CashManagerListAttribute : PXStringListAttribute
  {
    public CashManagerListAttribute()
      : base(new string[3]{ "AP", "AR", "CA" }, new string[3]
      {
        "AP",
        "AR",
        "CA"
      })
    {
    }
  }

  /// <summary>List of Modules supported in Project Management</summary>
  public class PMListAttribute : PXStringListAttribute
  {
    public PMListAttribute()
      : base(new string[8]
      {
        "GL",
        "AP",
        "AR",
        "IN",
        "PM",
        "CA",
        "DR",
        "PR"
      }, new string[8]
      {
        "GL",
        "AP",
        "AR",
        "IN",
        "PM",
        "CA",
        "DR",
        "PR"
      })
    {
    }
  }

  public class moduleGL : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleGL>
  {
    public moduleGL()
      : base("GL")
    {
    }
  }

  public class moduleAP : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleAP>
  {
    public moduleAP()
      : base("AP")
    {
    }
  }

  public class moduleTX : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleTX>
  {
    public moduleTX()
      : base("TX")
    {
    }
  }

  public class moduleAR : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleAR>
  {
    public moduleAR()
      : base("AR")
    {
    }
  }

  public class moduleCM : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleCM>
  {
    public moduleCM()
      : base("CM")
    {
    }
  }

  public class moduleCA : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleCA>
  {
    public moduleCA()
      : base("CA")
    {
    }
  }

  public class moduleIN : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleIN>
  {
    public moduleIN()
      : base("IN")
    {
    }
  }

  public class moduleSO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleSO>
  {
    public moduleSO()
      : base("SO")
    {
    }
  }

  public class modulePO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.modulePO>
  {
    public modulePO()
      : base("PO")
    {
    }
  }

  public class moduleDR : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleDR>
  {
    public moduleDR()
      : base("DR")
    {
    }
  }

  public class moduleFA : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleFA>
  {
    public moduleFA()
      : base("FA")
    {
    }
  }

  public class moduleEP : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleEP>
  {
    public moduleEP()
      : base("EP")
    {
    }
  }

  public class modulePM : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.modulePM>
  {
    public modulePM()
      : base("PM")
    {
    }
  }

  public class moduleWZ : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleWZ>
  {
    public moduleWZ()
      : base("WZ")
    {
    }
  }

  public class moduleFS : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleFS>
  {
    public moduleFS()
      : base("FS")
    {
    }
  }

  public class modulePR : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.modulePR>
  {
    public modulePR()
      : base("PR")
    {
    }
  }

  public sealed class moduleAM : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BatchModule.moduleAM>
  {
    public moduleAM()
      : base("AM")
    {
    }
  }
}
