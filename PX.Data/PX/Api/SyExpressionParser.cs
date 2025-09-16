// Decompiled with JetBrains decompiler
// Type: PX.Api.SyExpressionParser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Parser;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

#nullable disable
namespace PX.Api;

internal class SyExpressionParser : ExpressionParser
{
  private SyExpressionParser(string text)
    : base(text)
  {
  }

  protected virtual ParserContext CreateContext()
  {
    return (ParserContext) new SyExpressionParser.SyExpressionContext();
  }

  protected virtual NameNode CreateNameNode(ExpressionNode node, string tokenString)
  {
    return (NameNode) new SyExpressionParser.SyNamedNode(node, tokenString, this.Context);
  }

  protected virtual void ValidateName(NameNode node, string tokenString)
  {
    throw new NotImplementedException();
  }

  protected virtual FunctionNode CreateFunctionNode(ExpressionNode node, string name)
  {
    return node is SyExpressionParser.SyNamedNode syNamedNode && ((IEnumerable<string>) syNamedNode.Names).First<string>() == "Provider" ? (FunctionNode) new SyExpressionParser.ProviderFunctionNode(name, this.Context) : (FunctionNode) new SyExpressionParser.ImportFunctionNode(node, name, this.Context);
  }

  protected virtual bool IsAggregate(string nodeName) => false;

  public static ExpressionNode Parse(string formula) => new SyExpressionParser(formula).Parse();

  public class SyNamedNode : NameNode
  {
    public readonly string[] Names;

    public SyNamedNode(ExpressionNode node, string name, ParserContext context)
      : base(node, name, context)
    {
      this.Names = this.Name.Split('.');
    }

    public virtual object Eval(object row) => ((SyFormulaFinalDelegate) row)(this.Names);
  }

  public class SyExpressionContext : ExpressionContext
  {
    /// <summary>
    /// Replaces original value to the corresponding substituted value from the list specified on the Substitution Lists (SM206026) form.
    /// Throws an exception if the specified original value is not found.
    /// </summary>
    public virtual object SubstituteAll(
      FunctionContext context,
      object value,
      object substitutionKey)
    {
      return this.Substitute(context, value, substitutionKey, false, nameof (SubstituteAll));
    }

    /// <summary>
    /// Replaces original value to the corresponding substituted value from the list specified on the Substitution Lists (SM206026) form.
    /// </summary>
    public virtual object SubstituteListed(
      FunctionContext context,
      object value,
      object substitutionKey)
    {
      return this.Substitute(context, value, substitutionKey, true, nameof (SubstituteListed));
    }

    /// <summary>
    /// Replaces original value for a multiselect with the corresponding substituted value from the list specified on the Substitution Lists (SM206026) form.
    /// Throws an exception if the specified original value is not found.
    /// </summary>
    public virtual object MultiselectSubstituteAll(
      FunctionContext context,
      object value,
      object substitutionKey,
      object externalDelimiter)
    {
      return this.MultiselectSubstitute(context, value, substitutionKey, externalDelimiter, false, nameof (MultiselectSubstituteAll));
    }

    /// <summary>
    /// Replaces original value for a multiselect with the corresponding substituted value from the list specified on the Substitution Lists (SM206026) form.
    /// </summary>
    public virtual object MultiselectSubstituteListed(
      FunctionContext context,
      object value,
      object substitutionKey,
      object externalDelimiter)
    {
      return this.MultiselectSubstitute(context, value, substitutionKey, externalDelimiter, true, nameof (MultiselectSubstituteListed));
    }

    private object MultiselectSubstitute(
      FunctionContext context,
      object value,
      object substitutionKey,
      object externalDelimiter,
      bool replacementOptional,
      string functionName)
    {
      switch (SyMappingUtils.ProcessMappingScope.OperationType)
      {
        case "I":
          return this.MultiselectSubstitute(context, value, substitutionKey, externalDelimiter, (object) ",", replacementOptional, functionName);
        case "E":
          return this.MultiselectSubstitute(context, value, substitutionKey, (object) ",", externalDelimiter, replacementOptional, functionName);
        default:
          throw new InvalidOperationException($"The operation type for function {functionName} could not be resolved.");
      }
    }

    private object MultiselectSubstitute(
      FunctionContext context,
      object value,
      object substitutionKey,
      object inputDelimiter,
      object outputDelimiter,
      bool replacementOptional,
      string functionName)
    {
      if (ParserTypeHelper.GetDataType(value) != 18)
        return this.Substitute(context, value, substitutionKey, replacementOptional, functionName);
      if (ParserTypeHelper.GetDataType(inputDelimiter) != 18 || ParserTypeHelper.GetDataType(outputDelimiter) != 18)
        throw ExpressionException.ArgumentTypeString(functionName, 3);
      string str1 = ((string) value).Trim();
      string str2 = ((string) inputDelimiter).Trim();
      string separator1 = ((string) outputDelimiter).Trim();
      List<string> values = new List<string>();
      string[] separator2 = new string[1]{ str2 };
      foreach (string str3 in str1.Split(separator2, StringSplitOptions.None))
        values.Add((string) this.Substitute(context, (object) str3, substitutionKey, replacementOptional, functionName));
      return (object) string.Join(separator1, (IEnumerable<string>) values);
    }

    private object Substitute(
      FunctionContext context,
      object value,
      object substitutionKey,
      bool replacementOptional,
      string functionName)
    {
      DataType dataType1 = ParserTypeHelper.GetDataType(value);
      if (dataType1 == null || dataType1 == 2)
        return (object) null;
      if (dataType1 != 18)
        throw ExpressionException.ArgumentTypeString(functionName, 1);
      DataType dataType2 = ParserTypeHelper.GetDataType(substitutionKey);
      if (dataType2 == null || dataType2 == 2 || dataType2 != 18)
        throw ExpressionException.ArgumentTypeString(functionName, 2);
      string str1 = ((string) value).Trim();
      string subKeyStr = (string) substitutionKey;
      System.Type type = typeof (SYSubstitutionValues);
      string key = $"{type.Name}${subKeyStr}";
      PrefetchDelegate<Dictionary<string, string>> prefetchDelegate = (PrefetchDelegate<Dictionary<string, string>>) (() => PXDatabase.SelectRecords<SYSubstitutionValues>((PXDataField) new PXDataFieldValue<SYSubstitutionValues.substitutionID>((object) subKeyStr)).ToDictionary<SYSubstitutionValues, string, string>((Func<SYSubstitutionValues, string>) (val => val.OriginalValue.Trim()), (Func<SYSubstitutionValues, string>) (val => val.SubstitutedValue), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase));
      Dictionary<string, string> dictionary1 = PXContext.GetSlot<Dictionary<string, string>>(key);
      if (dictionary1 == null)
        dictionary1 = PXContext.SetSlot<Dictionary<string, string>>(key, PXDatabase.Provider.GetSlot<Dictionary<string, string>>(key, prefetchDelegate, type));
      Dictionary<string, string> dictionary2 = dictionary1;
      if (dictionary2.Count == 0)
      {
        string format = SyMappingUtils.ProcessMappingScope.OperationType == "I" ? "The {0} substitution list cannot be found. On the Import Scenarios (SM206025) form, select a substitution list in the Formula Editor dialog box, or add a new list by clicking Add Substitution." : "The {0} substitution list cannot be found. On the Export Scenarios (SM207025) form, select a substitution list in the Formula Editor dialog box, or add a new list on the Substitution Lists (SM206026) form.";
        throw new PXSubstitutionException(subKeyStr, str1, format, new object[1]
        {
          (object) subKeyStr
        });
      }
      string str2;
      if (dictionary2.TryGetValue(str1, out str2))
        return (object) str2;
      if (replacementOptional)
        return (object) str1;
      string format1 = SyMappingUtils.ProcessMappingScope.OperationType == "I" ? "The substitution value for {0} cannot be found. Add a new substitution value for {0} by clicking the Add Substitution button." : "The substitution value for {0} cannot be found. Add a new substitution value for {0} on the Substitution Lists (SM206026) form.";
      throw new PXSubstitutionException(subKeyStr, str1, format1, new object[1]
      {
        (object) str1
      });
    }
  }

  private class ImportFunctionNode(ExpressionNode node, string name, ParserContext context) : 
    FunctionNode(node, name, context)
  {
    protected virtual FunctionContext CreateFunctionContext()
    {
      return new FunctionContext((IFormatProvider) Thread.CurrentThread.CurrentCulture);
    }
  }

  private class ProviderFunctionNode : FunctionNode
  {
    private readonly string _name;

    public ProviderFunctionNode(string name, ParserContext context)
      : base((ExpressionNode) null, name, context)
    {
      this._name = name;
    }

    public virtual object Eval(object row)
    {
      object provider = SyProviderInstance.Provider;
      MethodInfo method = provider.GetType().GetMethod(this._name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
      if (method == (MethodInfo) null)
        throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The public instance method {0} is not found in {1}", (object) this._name, (object) provider.GetType().FullName));
      object[] parameters = (object[]) null;
      if (this.Arguments != null)
        parameters = this.Arguments.Select<ExpressionNode, object>((Func<ExpressionNode, object>) (_ => _.Eval(row))).ToArray<object>();
      try
      {
        return method.Invoke(provider, parameters);
      }
      catch (TargetInvocationException ex)
      {
        throw PXException.ExtractInner((Exception) ex);
      }
    }

    protected virtual FunctionContext CreateFunctionContext()
    {
      return new FunctionContext((IFormatProvider) Thread.CurrentThread.CurrentCulture);
    }
  }
}
