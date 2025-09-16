// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOBehavior
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.SO;

public class SOBehavior
{
  public const 
  #nullable disable
  string SO = "SO";
  public const string TR = "TR";
  public const string IN = "IN";
  public const string QT = "QT";
  public const string RM = "RM";
  public const string CM = "CM";
  public const string BL = "BL";
  public const string MO = "MO";

  public static bool? GetRequireShipmentValue(string behavior, bool? value)
  {
    if (EnumerableExtensions.IsIn<string>(behavior, "TR", "SO", "RM"))
      return new bool?(true);
    return EnumerableExtensions.IsIn<string>(behavior, "IN", "QT", "CM", "BL", "MO", Array.Empty<string>()) ? new bool?(false) : value;
  }

  public static bool IsPredefinedBehavior(string behavior)
  {
    return EnumerableExtensions.IsIn<string>(behavior, "IN", "QT", "CM", "SO", "TR", new string[3]
    {
      "RM",
      "BL",
      "MO"
    });
  }

  public static string DefaultOperation(string behavior, string ardoctype)
  {
    if (behavior != null && behavior.Length == 2)
    {
      switch (behavior[0])
      {
        case 'B':
          if (behavior == "BL")
            goto label_14;
          goto label_16;
        case 'C':
          if (behavior == "CM")
            goto label_15;
          goto label_16;
        case 'I':
          if (behavior == "IN")
            break;
          goto label_16;
        case 'M':
          if (behavior == "MO")
            goto label_14;
          goto label_16;
        case 'Q':
          if (behavior == "QT")
            goto label_14;
          goto label_16;
        case 'R':
          if (behavior == "RM")
            goto label_15;
          goto label_16;
        case 'S':
          if (behavior == "SO")
            break;
          goto label_16;
        case 'T':
          if (behavior == "TR")
            break;
          goto label_16;
        default:
          goto label_16;
      }
      switch (ardoctype)
      {
        case "INV":
        case "DRM":
        case "CSL":
        case "UND":
          return "I";
        case "CRM":
        case "RCS":
          return "R";
        default:
          return (string) null;
      }
label_14:
      return "I";
label_15:
      return "R";
    }
label_16:
    return (string) null;
  }

  public class sO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOBehavior.sO>
  {
    public sO()
      : base("SO")
    {
    }
  }

  public class tR : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOBehavior.tR>
  {
    public tR()
      : base("TR")
    {
    }
  }

  public class iN : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOBehavior.iN>
  {
    public iN()
      : base("IN")
    {
    }
  }

  public class qT : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOBehavior.qT>
  {
    public qT()
      : base("QT")
    {
    }
  }

  public class rM : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOBehavior.rM>
  {
    public rM()
      : base("RM")
    {
    }
  }

  public class cM : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOBehavior.cM>
  {
    public cM()
      : base("CM")
    {
    }
  }

  public class bL : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOBehavior.bL>
  {
    public bL()
      : base("BL")
    {
    }
  }

  public class mO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOBehavior.mO>
  {
    public mO()
      : base("MO")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[8]
      {
        PXStringListAttribute.Pair("SO", "Sales Order"),
        PXStringListAttribute.Pair("TR", "Transfer Order"),
        PXStringListAttribute.Pair("IN", "Invoice"),
        PXStringListAttribute.Pair("QT", "Quote"),
        PXStringListAttribute.Pair("CM", "Credit Memo"),
        PXStringListAttribute.Pair("RM", "RMA Order"),
        PXStringListAttribute.Pair("BL", "Blanket Order"),
        PXStringListAttribute.Pair("MO", "Mixed Order")
      })
    {
    }
  }

  public class RequireShipment<TSOBehavior, TSOTypeRequireShipment> : 
    IBqlUnary,
    IBqlCreator,
    IBqlVerifier
    where TSOBehavior : IBqlOperand
    where TSOTypeRequireShipment : IBqlOperand
  {
    private readonly IBqlCreator requireShipment = (IBqlCreator) new Where<TSOBehavior, Equal<SOBehavior.tR>, Or<TSOBehavior, Equal<SOBehavior.sO>, Or<TSOBehavior, Equal<SOBehavior.rM>, Or<TSOBehavior, NotEqual<SOBehavior.iN>, And<TSOBehavior, NotEqual<SOBehavior.qT>, And<TSOBehavior, NotEqual<SOBehavior.cM>, And<TSOBehavior, NotEqual<SOBehavior.bL>, And<TSOBehavior, NotEqual<SOBehavior.mO>, And<TSOTypeRequireShipment, Equal<True>>>>>>>>>>();

    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      return this.requireShipment.AppendExpression(ref exp, graph, info, selection);
    }

    public void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      ((IBqlVerifier) this.requireShipment).Verify(cache, item, pars, ref result, ref value);
    }
  }
}
